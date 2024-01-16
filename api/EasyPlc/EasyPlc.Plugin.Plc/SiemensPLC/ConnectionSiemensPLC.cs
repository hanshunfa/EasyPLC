using HslCommunication.Profinet.Siemens;
using HslCommunication;
using System.Collections.Concurrent;
using EasyPlc.Plugin.Plc.Utils;

namespace EasyPlc.Plugin.Plc;

public class ConnectionSiemensPLC : IConnectionSiemensPLC
{
    private SiemensS7Net _plc;
    private Thread _workThread;
    private ManualResetEvent _mre;
    public SiemensPlcInfo PlcInfo { get; set; } = null;

    //定义回调事件
    public delegate Task Err(string errMsg);
    public event Err OnErr;

    public delegate Task Info(string Info);
    public event Info OnInfo;

    public delegate Task PublicCallback(SiemensPlcInfo plc);
    public event PublicCallback OnPublicCallback;

    public delegate Task EventCallback(SiemensPlcInfo plc, int eventIdx);
    public event EventCallback OnEventCallback;

    private PublicInfo _writePi = null;
    private ConcurrentQueue<EventInfo> _queueHandleEventCompleted;

    public ConnectionSiemensPLC()
    {
        _mre = new ManualResetEvent(false);
        _queueHandleEventCompleted = new ConcurrentQueue<EventInfo>();
    }
    public bool SetWritePi(PublicInfo publicInfo)
    {
        if (PlcInfo.IsConn)
        {
            //使用值转递
            _writePi = publicInfo.DeepClone();
            return true;
        }
        return false;
    }
    /// <summary>
    /// 设置PLCInfo
    /// </summary>
    /// <param name="plcInfo"></param>
    public void SetPlcInfo(SiemensPlcInfo plcInfo)
    {
        PlcInfo = plcInfo;
    }
    /// <summary>
    /// 事件写
    /// </summary>
    /// <param name="ei"></param>
    public bool SetEventInfo(EventInfo ei)
    {
        if (PlcInfo.IsConn)
        {
            _queueHandleEventCompleted.Enqueue(ei);//丢入写队列中
            return true;
        }
        return false;
    }
    /// <summary>
    /// 启动plc
    /// </summary>
    /// <returns></returns>
    public string StartWork()
    {
        if (PlcInfo == null)
            return "请实例化PlcInfo对象";

        //配置防呆检查  EventTrigger  SequenceID


        //等会再写

        if (_plc == null)
        {
            Authorization.SetAuthorizationCode("4672fd9a-4743-4a08-ad2f-5cd3374e496d");//授权 最好加密下

            _plc = new SiemensS7Net((SiemensPLCS)Enum.Parse(typeof(SiemensPLCS), PlcInfo.Version));
            _plc.IpAddress = PlcInfo.IP;
            _plc.Port = PlcInfo.Port;
            _plc.Rack = PlcInfo.Rack;
            _plc.Slot = PlcInfo.Slot;
            _plc.SetPersistentConnection();
        }
        if (!PlcInfo.IsConn)
        {
            if (!_plc.ConnectServer().IsSuccess)
            {
                PlcInfo.IsConn = false;
                return "Fail";
            }
            //开始线程
            _workThread = new Thread(new ThreadStart(WorkThread)) { IsBackground = true };
            _workThread.Start();
            PlcInfo.IsConn = true;
            return "OK";
        }
        else
        {
            return "已连接";
        }
    }

    public string StopWork()
    {
        string ret = "OK";
        if (PlcInfo != null && PlcInfo.IsConn)
        {
            _mre.Set();
            //等待plc关闭
            while (_plc != null) { Thread.Sleep(100); }
        }

        return ret;

    }

    private async void WorkThread()
    {
        _mre.Reset();
        while (!_mre.WaitOne(10))
        {
            /*
             * 事件优先处理
             */
            while (!_queueHandleEventCompleted.IsEmpty)
            {
                EventInfo ets = null;
                if (_queueHandleEventCompleted.TryDequeue(out ets))
                {
                    WirteToPLC(ets);
                }
            }
            //读取公共区
            OperateResult<byte[]> operateResult01 = _plc.Read(PlcInfo.PI.ReadAddr, PlcInfo.PI.ReadLen);
            if (!operateResult01.IsSuccess)
            {
                await OnErr?.Invoke($"{PlcInfo.Name}PLC公共区读取失败：{operateResult01.Message}");
                continue;
            }
            //判断长度是否与定义的一致
            if (operateResult01.Content.Length != PlcInfo.PI.ReadLen)
            {
                //在读一次
                await OnErr?.Invoke($"{PlcInfo.Name}PLC公共区读取长度[{operateResult01.Content.Length}]与定义[{PlcInfo.PI.ReadLen}]的不一致");
                continue;
            }
            //解析byte[]到指定特性对象种
            PlcInfo.PI.ReadTime = DateTime.Now;//读到时候时间
            PlcInfo.PI.ObjR.ReadTree(operateResult01.Content);
            //读取PC公共区 只需要读取一次
            if (PlcInfo.PI.IsReset)
            {
                PlcInfo.PI.IsReset = false;

                OperateResult<byte[]> operateResult02 = _plc.Read(PlcInfo.PI.WriteAddr, PlcInfo.PI.WriteLen);
                if (!operateResult02.IsSuccess)
                {
                    await OnErr?.Invoke($"{PlcInfo.Name}PLC公共区读取失败：{operateResult02.Message}");
                    continue;
                }
                //判断长度是否与定义的一致
                if (operateResult02.Content.Length != PlcInfo.PI.WriteLen)
                {
                    //在读一次
                    await OnErr?.Invoke($"{PlcInfo.Name}PLC公共区读取长度[{operateResult02.Content.Length}]与定义[{PlcInfo.PI.WriteLen}]的不一致");
                    continue;
                }
                //解析byte[]到指定特性对象种
                PlcInfo.PI.ObjW.ReadTree(operateResult02.Content);
            }
            //公共区回调
            await OnPublicCallback?.Invoke(PlcInfo);
            
            //公共区写
            if(_writePi != null)
            {
                //转换
                var buffs = _writePi.ObjW.WriteBuffer();
                //写PLC
                var result = _plc.Write(_writePi.WriteAddr, buffs);
                if (!result.IsSuccess)
                {
                    await OnInfo?.Invoke($"{PlcInfo.Name}公共区写入PLC失败");
                }
            }

            //-----------------------------------------------------------------------
            //事件处理...........
            //获取触发变量 EventTrigger
            var eventTrigger = PlcInfo.PI.ObjR.GetResourceWithEventTrigger();
            if(eventTrigger == null)
            {
                OnErr?.Invoke($"{PlcInfo.Name}公共区配置中缺少【EventTrigger】");
                continue;
            }
            var bitTarger = eventTrigger.Value;//配置时候保证一定是bool[]类型
            for (int i = 0; i < bitTarger.Length; i++)
            {
                bool bit = bitTarger[i];
                if (bit)
                {
                    if (i >= PlcInfo.EIs.Count)
                    {
                        //调用错误回调事件
                        OnErr?.Invoke($"PLC触发事件索引超出定义范围，触发位{i}，定义个数{PlcInfo.EIs.Count}");
                        break;
                    }
                    if (!PlcInfo.EIs[i].TriggerCompleted)
                    {
                        if (PlcInfo.EIs[i].IsReset)
                        {
                            PlcInfo.EIs[i].IsReset = false;
                            var opW = _plc.Read(PlcInfo.EIs[i].WriteAddr, PlcInfo.EIs[i].WriteLen);
                            if (!opW.IsSuccess)
                            {
                                await OnErr?.Invoke($"{PlcInfo.EIs[i].Idx}事件读取PLC->PC失败：{opW.Message}");
                                continue;
                            }
                            PlcInfo.EIs[i].ObjW.ReadTree(opW.Content);
                            var sequenceIdw = PlcInfo.EIs[i].ObjW.GetResourceWithSequenceID();
                            if (sequenceIdw == null)
                            {
                                await OnErr.Invoke($"事件{PlcInfo.EIs[i].WriteClassName}配置中缺少【SequenceID】");
                                continue;
                            }
                            PlcInfo.EIs[i].SequenceIDW = sequenceIdw.Value;
                        }


                        var opR = _plc.Read(PlcInfo.EIs[i].ReadAddr, PlcInfo.EIs[i].ReadLen);
                        if (!opR.IsSuccess)
                        {
                            OnErr?.Invoke($"{PlcInfo.EIs[i].Idx}事件读取PLC->PC失败：{opR.Message}");
                            continue;
                        }

                        //判断长度是否与定义的一致
                        if (opR.Content.Length != PlcInfo.EIs[i].ReadLen)
                        {
                            await OnErr?.Invoke($"{PlcInfo.Name}PLC事件读取区读取长度[{opR.Content.Length}]与定义[{PlcInfo.EIs[i].ReadLen}]的不一致");
                            continue;
                        }

                        //更新事件实例内容
                        PlcInfo.EIs[i].ObjR.ReadTree(opR.Content);
                        var sequenceIdr = PlcInfo.EIs[i].ObjW.GetResourceWithSequenceID();
                        if (sequenceIdr == null)
                        {
                            await OnErr?.Invoke($"事件{PlcInfo.EIs[i].ReadClassName}配置中缺少【SequenceID】");
                            continue;
                        }
                        PlcInfo.EIs[i].SequenceIDR = sequenceIdr.Value;
                        //查看 SequenceID
                        if (PlcInfo.EIs[i].SequenceIDW != PlcInfo.EIs[i].SequenceIDR)
                        {
                            PlcInfo.EIs[i].TriggerCompleted = true;
                            //回调事件处理
                            await OnEventCallback?.Invoke(PlcInfo, i);
                        }
                        else
                        {
                            await OnInfo?.Invoke($"{PlcInfo.EIs[i].Idx}事件SequnenceID相等：SequnenceID = {PlcInfo.EIs[i].SequenceIDR}");

                            //该情况可以考虑再次事件写回PLC
                            WirteToPLC2(PlcInfo.EIs[i]);
                        }
                    }
                    else
                    {
                        //已经有线程处理当前事件 当前事件正在处理中
                        await OnInfo?.Invoke($"{PlcInfo.EIs[i].Idx}当前事件正在处理中");
                    }
                }
            }
        }
        //断开PLC
        _plc.ConnectClose();
        _plc.Dispose();
        _plc = null;
        PlcInfo.IsConn = false;
    }
    private void WirteToPLC(EventInfo ei)
    {
        //转换
        var buffs = ei.ObjW.WriteBuffer();
        //写PLC
        var result = _plc.Write(ei.WriteAddr, buffs);
        if (!result.IsSuccess)
        {
            OnErr?.Invoke($"{PlcInfo.Name}事件区写入PLC失败");
        }
        else
        {
            ei.TriggerCompleted = false;
        }
    }
    private void WirteToPLC2(EventInfo ei)
    {
        //转换
        var buffs = ei.ObjW.WriteBuffer();
        //写PLC
        var result = _plc.Write(ei.WriteAddr, buffs);
        if (!result.IsSuccess)
        {
            OnInfo?.Invoke($"{PlcInfo.Name}事件区写入PLC失败");
        }
        else
        {
            ei.TriggerCompleted = false;
        }
    }
}
