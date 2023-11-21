

namespace EasyPlc.Application;

public class ProProcessService : DbRepository<ProWorkingStep>, IProProcessService
{
    private readonly ILogger<ProProcessService> _logger;

    public ProProcessService(
        ILogger<ProProcessService> logger
        )
    {
        _logger = logger;
    }
    public async Task ReadyOrder(BaseIdInput input)
    {
        var proOrderService = App.GetService<IProOrderService>();
        var macFlowService = App.GetService<IMacFlowService>();
        var macModelParamService = App.GetService<IMacModelParamService>();
        var proLaserService = App.GetService<IProLaserService>();
        var proLabelService = App.GetService<IProLabelService>();
        try
        {
            //简单防呆
            var order = await proOrderService.GetProOrderById(input.Id);
            if (order == null) throw Oops.Bah("没有对应的工单");
            if (order.Status != OrderStatus.AWAIT) throw Oops.Bah("只有AWAIT状态工单才能进入READY状态");

            var flow = await macFlowService.GetMacFlowById(order.FlowId);
            if (flow == null) throw Oops.Bah($"工单{order.Sono}没有对应的流程");
            var modelParams = await macModelParamService.GetListByModelId(flow.ModelId);
            modelParams = modelParams.OrderBy(it => it.SortCode).ToList();//排序

            #region 镭射
            //镭射
            var path = modelParams.Where(it => it.Name == "激光机镭射-模板").First();//镭射模板
            var ps = modelParams.Where(it=>it.Name.Contains("激光机镭射-变量")).ToList();//变量列表
            //解析 
            var parseResult = ps.Select(it => it.ParamValue).ToList().ParseLaserValue(order.Batch, 1, DateTime.Now);
            if(!parseResult.IsSucceed) throw Oops.Bah($"解析异常:{parseResult.Msg}");
            //生成镭射数据记录
            var pl = new ProLaser()
            {
                OrderId = order.Id,
                Path = path.ParamValue
            };
            List< LaserParam > plParams = new List< LaserParam >();
            for(int i = 0; i < ps.Count; i++)
            {
                var lp = new LaserParam()
                {
                    Title = ps[i].Name,
                    Name = ps[i].Code,
                    Value = parseResult.ResultList[i]
                };
                plParams.Add( lp );
            }
            pl.ExtJson = plParams.ToJson();

            #endregion 镭射

            #region 标签

            var pathLabel = modelParams.Where(it => it.Name == "标签打印-模板").First();//标签模板
            var driveLabel = modelParams.Where(it => it.Name == "标签打印-驱动").First();//标签驱动
            var pList = modelParams.Where(it => it.Name.Contains("标签打印-变量")).ToList();//变量列表
            //解析 
            var parseResultLabel = pList.Select(it=>it.ParamValue).ToList().ParseLabelValue(order.Batch, 1, DateTime.Now);
            if (!parseResultLabel.IsSucceed) throw Oops.Bah($"标签解析异常:{parseResultLabel.Msg}");
            //生成镭射数据记录
            var pla = new ProLabel()
            {
                OrderId = order.Id,
                Path = pathLabel.ParamValue,
                DriveName = driveLabel.ParamValue,
            };
            List<LabelParam> plaParams = new List<LabelParam>();
            for (int i = 0; i < pList.Count; i++)
            {
                var lp = new LabelParam()
                {
                    Title = pList[i].Name,
                    Name = pList[i].Code,
                    Value = parseResultLabel.ResultList[i]
                };
                plaParams.Add(lp);
            }
            pla.ExtJson = plaParams.ToJson();

            #endregion 标签

            //事务
            var result = await itenant.UseTranAsync(async () => {
                var relationRep = ChangeRepository<DbRepository<ProLaser>>();//切换仓储
                await relationRep.InsertAsync(pl);
                var relationRep2 = ChangeRepository<DbRepository<ProLabel>>();//切换仓储
                await relationRep2.InsertAsync(pla);

                var relationRep3 = ChangeRepository<DbRepository<ProOrder>>();//切换仓储
                await relationRep3.UpdateAsync(it => new ProOrder { Status = "READY" }, it => it.Id == order.Id);
            });
            if (result.IsSuccess)
            {
                await proLaserService.RefreshCache();
                await proLabelService.RefreshCache();
                await proOrderService.RefreshCache();
            }
            else
            {
                //写日志
                _logger.LogError(result.ErrorMessage, result.ErrorException);
                throw Oops.Bah(result.ErrorMessage);
            }
            //生成镭射信息
        }
        catch (Exception ex)
        {
            throw Oops.Bah($"准备工单异常:{ex.Message}");
        }
    }

    public async Task GetWorkingOrderInfo()
    {
        var orderSvr = App.GetService<IProOrderService>();
        var workingOrder = await orderSvr.GetWorkingOrder();
        if (workingOrder == null)
        {
            return;
        }
        OrderInfo.OrderSono = workingOrder.Sono;
        //查找工单对应的工艺路线
        var flowSvr = App.GetService<IMacFlowService>();
        var flow = await flowSvr.GetMacFlowById(workingOrder.FlowId);
        if (flow == null)
        {
            return;
        }
        //获取型号配置参数
        var modelParamSvr = App.GetService<IMacModelParamService>();
        var listModelParam = await modelParamSvr.GetListByModelId(flow.ModelId);
        var command = listModelParam.Where(it=>it.Code == "Command01").FirstOrDefault();
        if(command == null)
        {
            return;
        }
        OrderInfo.Model = Convert.ToInt16(command.ParamValue);
    }

    public async Task<CableOutput> SetCableSN(CableInput input)
    {
        var cableOutput = new CableOutput() { IsSucceed = true, Code = 2, Msg = "成功"};
        if (string.IsNullOrEmpty(input.CableSN))
        {
            cableOutput.IsSucceed = false;
            cableOutput.Msg = "线束SN不能为空";
            return cableOutput;
        }
        //SN检测 是否是当前工单型号匹配

        //没有载具进站

        //if (!CableGlabel.IsWorking)
        //{
        //    cableOutput.IsSucceed = false;
        //    cableOutput.Code = 3;
        //    cableOutput.Msg = "没有正在加工的载具";
        //    return cableOutput;
        //}

        //查看当前是否有可生产工单
        var orderSvr = App.GetService<IProOrderService>();
        var workingOrder = await orderSvr.GetWorkingOrder();
        if (workingOrder == null || workingOrder.OrderType == "Repair")
        {
            cableOutput.IsSucceed = false;
            cableOutput.Code = 4;
            cableOutput.Msg = "没有正在加工的订单";
            return cableOutput;
        }
        //查找工单对应的工艺路线
        var flowSvr = App.GetService<IMacFlowService>();
        var flow = await flowSvr.GetMacFlowById(workingOrder.FlowId);
        if (flow == null)
        {
            cableOutput.IsSucceed = false;
            cableOutput.Code = 4;
            cableOutput.Msg = $"工单{workingOrder.Sono}工艺流程为空";
            return cableOutput;
        }
        //获取型号配置参数
        var modelParamSvr = App.GetService<IMacModelParamService>();
        var listModelParam = await modelParamSvr.GetListByModelId(flow.ModelId);
        //通过载具编码查找产品码
        var carrierSvr = App.GetService<IMacCarrierService>();
        //查看载具编码是否在列表中
        var carrierList = await carrierSvr.GetListByModelId(flow.ModelId);
        var carrier = carrierList.Where(it => it.Code == CableGlabel.CarrierSN).FirstOrDefault();
        if (carrier == null)
        {
            cableOutput.IsSucceed = false;
            cableOutput.Code = 5;
            cableOutput.Msg = $"载具[{CableGlabel.CarrierSN}]不在载具列表中";
            return cableOutput;
        }
        if (carrier.CarrierStatus == "DISABLED")//不能使用的载具
        {
            cableOutput.IsSucceed = false;
            cableOutput.Code = 5;
            cableOutput.Msg = $"载具{CableGlabel.CarrierSN}是弃用的";
            return cableOutput;
        }

        //获取当前工艺流程对应的工位列表
        var equipmentSvr = App.GetService<IMacEquipmentService>();
        var listEquipment = await equipmentSvr.GetEquipmentListByFlowId(flow.Id);//工位列表
        var currentEquipemt = listEquipment.Where(it => it.Code == "OP1001").FirstOrDefault();//当前工位对应的设备工位信息
        if (currentEquipemt == null)
        {
            cableOutput.IsSucceed = false;
            cableOutput.Code = 6;
            cableOutput.Msg = $"当前工位[OP1001]不在工艺{flow.Name}的工位列表中";
            return cableOutput;
        }
        //防止重复出站
        var currrentEquipmentIdx = listEquipment.IndexOf(currentEquipemt);//当前工位在流程中的索引位置
                                                                          //根据载具编码获取产品编码
        var carrierPointSvr = App.GetService<IMacPointService>();
        var carrierPoint = await carrierPointSvr.GetMacPointByCarrierIdAndPoint(carrier.Id);//当前载具穴位信息
        if (carrierPoint == null)
        {
            cableOutput.IsSucceed = false;
            cableOutput.Code = 7;
            cableOutput.Msg = $"载具{carrier.Code}穴位1信息不存在";
            return cableOutput;
        }
        //产品SN 虚拟
        long productId = 0;
        if (carrierPoint.BindStatus == "BIND") productId = carrierPoint.BindCode;
        //通过产品SN查找对应过程质量数据
        var dataTmpSvr = App.GetService<IProDataTmpService>();
        var dataTmp = await dataTmpSvr.GetDataTmpById(productId);//过程质量数据
        //产品SN对应的过着记录
        var workingStepSvr = App.GetService<IProWorkingStepService>();
        var workingStepIdForDateTmp = dataTmp == null ? 0 : dataTmp.WorkingStepId;
        var workingStep = await workingStepSvr.GetWorkingStepById(workingStepIdForDateTmp);
        if (productId == 0 && dataTmp == null && workingStep == null)//产品SN不存在
        {
            cableOutput.IsSucceed = false;
            cableOutput.Code = 8;
            cableOutput.Msg = $"载具上没有产品";
            return cableOutput;
        }

        //若已绑定。。。。。。


        //设置产品线束编码
        dataTmp.CableSN = input.CableSN;
        await dataTmpSvr.Edit(dataTmp.Adapt<ProDataTmpEditInput>());

        return cableOutput;
    }
    /// <summary>
    /// 根据WorkingStepID获取其兄弟返修流程
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<RepairGetOutput> GetRepairStep(RepairGetInput input)
    {
        //查看当前是否有可生产工单
        var orderSvr = App.GetService<IProOrderService>();
        var workingOrder = await orderSvr.GetWorkingOrder();
        if (workingOrder == null || workingOrder.OrderType == "Normal")
        {
            return new RepairGetOutput
            {
                IsSucceed = false,
                Code = 2,
                Msg = $"没有正在返修的工单"
            };
        }
        //通过WorkingStepId查找返修过程
        var workingStepSvr = App.GetService<IProWorkingStepService>();
        var wokingStep = await workingStepSvr.GetWorkingStepById(input.WorkingStepId);
        if (wokingStep == null)
        {
            return new RepairGetOutput {
                IsSucceed = false,
                Code = 2,
                Msg = $"{input.WorkingStepId}没有对应的返修记录"
            };
        }
        var flowSvr = App.GetService<IMacFlowService>();
        var repairFlow = await flowSvr.GetMacFlowById(wokingStep.RepairFlowId);
        if(repairFlow == null)
        {
            return new RepairGetOutput
            {
                IsSucceed = false,
                Code = 2,
                Msg = $"返修流程ID[{wokingStep.RepairFlowId}]没有找到对应返修流程"
            };
        }
        var childrenFlowList = await flowSvr.GetChildListById(repairFlow.ParentId, false);
        List<MyFlow> flows = new List<MyFlow>();
        childrenFlowList.ForEach(flow =>
        {
            bool isChecked = repairFlow.Id == flow.Id;
            flows.Add(new MyFlow { Id = flow.Id, Name = flow.Name ,IsChecked = isChecked});
        });
        return new RepairGetOutput
        {
            IsSucceed = true,
            Code = 1,
            Msg = $"成功",
            Flows = flows
        };
    }

    /// <summary>
    /// Eap告诉系统返修品作业员确定的返修流程
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<RepairSetOutput> SetRepairStep(RepairSetInput input)
    {
        //查看当前是否有可生产工单
        var orderSvr = App.GetService<IProOrderService>();
        var workingOrder = await orderSvr.GetWorkingOrder();
        if (workingOrder == null || workingOrder.OrderType == "Normal")
        {
            return new RepairSetOutput
            {
                IsSucceed = false,
                Code = 2,
                Msg = $"没有正在返修的工单"
            };
        }
        //通过WorkingStepId查找返修过程
        var workingStepSvr = App.GetService<IProWorkingStepService>();
        var wokingStep = await workingStepSvr.GetWorkingStepById(input.WorkingStepId);
        if (wokingStep == null)
        {
            return new RepairSetOutput
            {
                IsSucceed = false,
                Code = 2,
                Msg = $"{input.WorkingStepId}没有对应的返修记录"
            };
        }
        //修改当前加工记录的返修流程
        wokingStep.RepairFlowId = input.FlowId;
        wokingStep.CurrentStep = "OP1001";
        wokingStep.NextStep = "OP1001";
        wokingStep.ProductStatus = "ok";
        
        //设置当前产品与加工记录绑定
        var dataTmpId = CableGlabel.GetDataTmpId();
        if(dataTmpId == 0)
        {
            return new RepairSetOutput
            {
                IsSucceed = false,
                Code = 3,
                Msg = $"{input.WorkingStepId}没有找到DataTmpId"
            };
        }
        var dataTmpSvr = App.GetService<IProDataTmpService>();
        var dataTmp = await dataTmpSvr.GetDataTmpById(dataTmpId);
        if (dataTmp == null)
        {
            return new RepairSetOutput
            {
                IsSucceed = false,
                Code = 3,
                Msg = $"临时记录ID[{dataTmpId}]没有找到对象"
            };
        }
        dataTmp.WorkingStepId = input.WorkingStepId;

        await dataTmpSvr.Edit(dataTmp.Adapt<ProDataTmpEditInput>());
        await workingStepSvr.Edit(workingOrder.Adapt<ProWorkingStepEditInput>());

        return new RepairSetOutput
        {
            IsSucceed = true,
            Code = 1,
            Msg = $"成功"
        };
    }
}
