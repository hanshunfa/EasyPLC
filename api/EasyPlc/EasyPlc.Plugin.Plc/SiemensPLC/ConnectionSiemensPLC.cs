using HslCommunication.Profinet.Siemens;
using HslCommunication;
using System.Collections.Concurrent;

namespace EasyPlc.Plugin.Plc;

public class ConnectionSiemensPLC : IConnectionSiemensPLC
{
    private SiemensS7Net _plc;
    private Thread _workThread;
    private ManualResetEvent _mre;
    public SiemensPlcInfo PlcInfo { get; set; } = new SiemensPlcInfo();

    //定义回调事件
    public delegate void Err(string errMsg);
    public event Err OnErr;

    public delegate void Info(string Info);
    public event Info OnInfo;

    public delegate void PublicCallback(string op, PublicInfo publicInfo);
    public event PublicCallback OnPublicCallback;

    public delegate Task EventCallback(string op, EventInfo ei);
    public event EventCallback OnEventCallback;

    private ConcurrentQueue<EventInfo> _queueHandleEventCompleted;

    public ConnectionSiemensPLC()
    {
        _mre = new ManualResetEvent(false);
        _queueHandleEventCompleted = new ConcurrentQueue<EventInfo>();
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
    /// 启动plc
    /// </summary>
    /// <returns></returns>
    public string StartWork()
    {
        if (PlcInfo == null)
            return "请实例化PlcInfo对象";
        if (_plc == null)
        {
            Authorization.SetAuthorizationCode("4672fd9a-4743-4a08-ad2f-5cd3374e496d");

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
            
        }

        return ret;

    }

    private void WorkThread()
    {
        _mre.Reset();
        while (!_mre.WaitOne(10))
        {
            /*
             * 先处理事件回写
             */
            if (!_queueHandleEventCompleted.IsEmpty)
            {
                EventInfo ets = null;
                if (_queueHandleEventCompleted.TryDequeue(out ets))
                {
                    WirteToPLC(ets);
                }
            }
            //读取公共区
            if (PlcInfo.PI.ReadLen == 0) PlcInfo.PI.ResetReadClassLenth();

            OperateResult<byte[]> operateResult01 = _plc.Read(PlcInfo.PI.ReadAddr, PlcInfo.PI.ReadLen);
            if (!operateResult01.IsSuccess)
            {
                OnErr?.Invoke($"{PlcInfo.Name}PLC公共区读取失败：{operateResult01.Message}");
                continue;
            }
            //判断长度是否与定义的一致
            if (operateResult01.Content.Length != PlcInfo.PI.ReadLen)
            {
                //在读一次
                OnErr?.Invoke($"{PlcInfo.Name}PLC公共区读取长度[{operateResult01.Content.Length}]与定义[{PlcInfo.PI.ReadLen}]的不一致");
                continue;
            }
            //解析byte[]到指定特性对象种
            PlcInfo.PI.UpdateReadContent(operateResult01.Content);
            //读取PC公共区 只需要读取一次
            if (PlcInfo.PI.WriteLen == 0)
            {
                PlcInfo.PI.ResetWriteClassLenth();
                OperateResult<byte[]> operateResult02 = _plc.Read(PlcInfo.PI.WriteAddr, PlcInfo.PI.WriteLen);
                if (!operateResult02.IsSuccess)
                {
                    OnErr?.Invoke($"{PlcInfo.Name}PLC公共区读取失败：{operateResult02.Message}");
                    continue;
                }
                //判断长度是否与定义的一致
                if (operateResult02.Content.Length != PlcInfo.PI.WriteLen)
                {
                    //在读一次
                    OnErr?.Invoke($"{PlcInfo.Name}PLC公共区读取长度[{operateResult02.Content.Length}]与定义[{PlcInfo.PI.WriteLen}]的不一致");
                    continue;
                }
                PlcInfo.PI.WriteBuffer = operateResult02.Content;
                //解析byte[]到指定特性对象种
                PlcInfo.PI.UpdateWriteContent(operateResult02.Content);
            }
            if (OnPublicCallback != null)
            {
                OnPublicCallback(PlcInfo.Name, PlcInfo.PI);
            }

            if (PlcInfo.PI.WriteLen != 0)
            {
                SiemensRefelectionUtil.Object2Buffer(PlcInfo.PI.ObjW, PlcInfo.PI.WriteBuffer);
                _plc.Write(PlcInfo.PI.WriteAddr, PlcInfo.PI.WriteBuffer);
            }

            var bitTarger = (PlcInfo.PI.ObjR).EventTriggerBit;
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
                        if (PlcInfo.EIs[i].ReadLen == 0)
                        {
                            PlcInfo.EIs[i].ResetReadClassLenth();
                        }

                        if (PlcInfo.EIs[i].WriteLen == 0)
                        {
                            PlcInfo.EIs[i].ResetWriteClassLenth();
                            var opW = _plc.Read(PlcInfo.EIs[i].WriteAddr, PlcInfo.EIs[i].WriteLen);
                            if (!opW.IsSuccess)
                            {
                                OnErr?.Invoke($"{PlcInfo.EIs[i].Idx}事件读取PLC->PC失败：{opW.Message}");
                                continue;
                            }
                            PlcInfo.EIs[i].UpdateWriteContent(opW.Content);
                            PlcInfo.EIs[i].SequenceIDW = (PlcInfo.EIs[i].ObjW).SequenceID;
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
                            //在读一次
                            OnErr?.Invoke($"{PlcInfo.Name}PLC事件读取区读取长度[{opR.Content.Length}]与定义[{PlcInfo.EIs[i].ReadLen}]的不一致");
                            continue;
                        }

                        //更新事件实例内容
                        PlcInfo.EIs[i].UpdateReadContent(opR.Content);

                        PlcInfo.EIs[i].SequenceIDR = (PlcInfo.EIs[i].ObjR).SequenceID;
                        //查看 SequenceID
                        if (PlcInfo.EIs[i].SequenceIDW != PlcInfo.EIs[i].SequenceIDR)
                        {
                            PlcInfo.EIs[i].TriggerCompleted = true;
                            //回调事件处理
                            TaskEventCallback(PlcInfo.EIs[i]);
                        }
                        else
                        {
                            OnInfo?.Invoke($"{PlcInfo.EIs[i].Idx}事件SequnenceID相等：SequnenceID = {PlcInfo.EIs[i].SequenceIDR}");

                            //该情况可以考虑再次事件写回PLC
                            WirteToPLC2(PlcInfo.EIs[i]);
                        }
                    }
                    else
                    {
                        //已经有线程处理当前事件 当前事件正在处理中
                        OnInfo?.Invoke($"{PlcInfo.EIs[i].Idx}当前事件正在处理中");
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
    private void TaskEventCallback(EventInfo ei)
    {
        Task.Run(async () =>
        {
            if (OnEventCallback != null)
            {
                await OnEventCallback.Invoke(PlcInfo.Name, ei);
                _queueHandleEventCompleted.Enqueue(ei);
            }
        });
    }
    private void WirteToPLC(EventInfo ei)
    {
        //SequenceID自动相等
        ei.SequenceIDW = ei.ObjW.SequenceID = ei.ObjR.SequenceID;
        SiemensRefelectionUtil.Object2Buffer(ei.ObjW, ei.WriteBuffer);

        var result = _plc.Write(ei.WriteAddr, ei.WriteBuffer);
        if (!result.IsSuccess)
        {
            OnInfo?.Invoke($"{PlcInfo.Name}第{ei.Idx}个事件{ei.WriteClassName}写入PLC失败");
        }
        else
        {
            ei.TriggerCompleted = false;
        }
    }
    private void WirteToPLC2(EventInfo ei)
    {
        SiemensRefelectionUtil.Object2Buffer(ei.ObjW, ei.WriteBuffer);
        var result = _plc.Write(ei.WriteAddr, ei.WriteBuffer);
        if (!result.IsSuccess)
        {
            OnInfo?.Invoke($"{PlcInfo.Name}第{ei.Idx}个事件{ei.WriteClassName}非第一次写入PLC失败");
        }
        else
        {
            ei.TriggerCompleted = false;
        }
    }
}
