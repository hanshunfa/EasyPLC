using Mapster;
using EasyPlc.Plugin.Scan;
using EasyPlc.Plugin.ScrewGun;
using EasyPlc.Plugin.SygoleRFID;

namespace EasyPlc.Web.Core;

/// <summary>
/// 生产加工
/// </summary>
public class WorkingProcess
{
    private ILogger _Logger;
    public void Init()
    {
        var siemensPlcFactoryService = App.GetService<ISiemensPlcFactoryService>();
        _Logger = App.GetService<ILogger<WorkingProcess>>();

        var connList = siemensPlcFactoryService.GetConnectionSiemensPLCList();
        foreach (var conn in connList)
        {
            conn.OnPublicCallback += OnPublicCallback;
            conn.OnEventCallback += OnEventCallback;
            conn.OnInfo += OnInfo;
            conn.OnErr += OnErr;
        }
    }
    /// <summary>
    /// 公共区回调
    /// </summary>
    /// <param name="op"></param>
    /// <param name="publicInfo"></param>
    private void OnPublicCallback(string op, PublicInfo publicInfo)
    {
        //var r = publicInfo.ObjR as General_PI_PlcToEap;
        //var w = publicInfo.ObjW as General_PI_EapToPlc;

        //if (r.HeartBeat == 0) w.HearBeat = 1; else w.HearBeat = 0;

        ////使能
        //w.Command = 1;
        ////型号
        //w.Model1 = OrderInfo.Model;
    }
    /// <summary>
    /// 事件区回调
    /// </summary>
    /// <param name="op"></param>
    /// <param name="ei"></param>
    private async Task OnEventCallback(string op, EventInfo ei)
    {
        //查看当前是否有可生产工单
        var orderSvr = App.GetService<IProOrderService>();
        var workingOrder = await orderSvr.GetWorkingOrder();
        if (workingOrder == null)
        {
            ei.ObjW.ResultCode = 2;
            ei.ObjW.ACK = 10;
            ei.ObjW.Msg = "没有可加工的工单";
            return;
        }
        //查找工单对应的工艺路线
        var flowSvr = App.GetService<IMacFlowService>();
        var flow = await flowSvr.GetMacFlowById(workingOrder.FlowId);
        if (flow == null)
        {
            ei.ObjW.ResultCode = 3;
            ei.ObjW.ACK = 12;
            ei.ObjW.Msg = $"工单{workingOrder.Sono}工艺流程为空";
            return;
        }
        //获取型号配置参数
        var modelParamSvr = App.GetService<IMacModelParamService>();
        var listModelParam = await modelParamSvr.GetListByModelId(flow.ModelId);


        //进站
        if (ei.ReadClassName.Contains("PlcToEap_IN"))
        {
            string s = "";
            if (ei.Idx % 4 == 0 || ei.Idx % 4 == 1) s = "01"; else s = "02";
            var st = $"OP{((ei.Idx /4) + 1) * 10}{s}";

            //载具编码读取
            var readCarrierText = ReadScan(st);
            if (readCarrierText == "NoRead")
            {
                ei.ObjW.ResultCode = 3;
                ei.ObjW.ACK = 11;
                ei.ObjW.Msg = "载具编码读取失败";
                return;
            }

            //通过载具编码查找产品码
            var carrierSvr = App.GetService<IMacCarrierService>();
            //查看载具编码是否在列表中
            var carrierList = await carrierSvr.GetListByModelId(flow.ModelId);
            var carrier = carrierList.Where(it => it.Code == readCarrierText).FirstOrDefault();
            if (carrier == null)
            {
                ei.ObjW.ResultCode = 3;
                ei.ObjW.ACK = 13;
                ei.ObjW.Msg = $"载具{readCarrierText}不在载具列表中";
                return;
            }
            if (carrier.CarrierStatus == "DISABLED")//不能使用的载具
            {
                ei.ObjW.ResultCode = 3;
                ei.ObjW.ACK = 14;
                ei.ObjW.Msg = $"载具{readCarrierText}是弃用的";
                return;
            }
            ei.ObjW.CarrierSN = carrier.Code;//载具编码赋值给PLC


            if (workingOrder.OrderType == "Normal")//正常生成工单
            {
                //获取当前工艺流程对应的工位列表
                var equipmentSvr = App.GetService<IMacEquipmentService>();
                var listEquipment = await equipmentSvr.GetEquipmentListByFlowId(flow.Id);//工位列表
                var currentEquipemt = listEquipment.Where(it => it.Code == st).FirstOrDefault();//当前工位对应的设备工位信息
                if (currentEquipemt == null)
                {
                    ei.ObjW.ResultCode = 2;
                    ei.ObjW.ACK = 16;
                    ei.ObjW.Msg = $"当前工位{st}不在工艺{flow.Name}的工位列表中";
                    return;
                }
                var currrentEquipmentIdx = listEquipment.IndexOf(currentEquipemt);//当前工位在流程中的索引位置
                                                                                  //根据载具编码获取产品编码
                var carrierPointSvr = App.GetService<IMacPointService>();
                var carrierPoint = await carrierPointSvr.GetMacPointByCarrierIdAndPoint(carrier.Id);//当前载具穴位信息
                if (carrierPoint == null)
                {
                    ei.ObjW.ResultCode = 3;
                    ei.ObjW.ACK = 15;
                    ei.ObjW.Msg = $"载具{carrier.Code}穴位1信息不存在";
                    return;
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
                if (productId == 0 && dataTmp == null && workingStep == null)
                {
                    //进站逻辑中，只有首站允许没有加工记录的产品进站
                    if (currrentEquipmentIdx != 0) //不是首站
                    {
                        ei.ObjW.ResultCode = 2;
                        ei.ObjW.ACK = 17;
                        ei.ObjW.Msg = $"载具上没有产品";
                        return;
                    }
                    else//空载具进入首站
                    {
                        //是否清料模式
                        if (workingOrder.Status == "CLEAR")
                        {
                            ei.ObjW.ResultCode = 2;
                            ei.ObjW.ACK = 18;
                            ei.ObjW.Msg = $"清料状态下不允许加工";
                            return;
                        }
                        //新增过站记录
                        var workingStepId = await workingStepSvr.AddReturnId(new ProWorkingStepAddInput
                        {
                            OrderId = workingOrder.Id,
                            ProductStatus = "ok",
                            CurrentStep = st,
                            NextStep = st
                        });

                        //新增产品质量记录

                        var dataTmpId = await dataTmpSvr.AddReturnId(new ProDataTmpAddInput()
                        {
                            OrderId = workingOrder.Id,
                            WorkingStepId = workingStepId,
                            ProductStatus = "ok",
                        });
                        //产品绑定载具
                        carrierPoint.BindCode = dataTmpId;
                        carrierPoint.BindStatus = "BIND";
                        await carrierPointSvr.Edit(carrierPoint.Adapt<PointEditInput>());

                        //工单已投产数量+1
                        await orderSvr.EditPutQty(new BaseIdInput { Id = workingOrder.Id }, workingOrder.PutQty + 1, workingOrder.OnlineQty + 1);
                        //通过比对工单计划数量与已投产数量
                        if (workingOrder.PutQty >= workingOrder.PlanQty)
                        {
                            await orderSvr.SetStatus(new ProOrderStatusInput { Id = workingOrder.Id, Status = "CLEAR" });
                        }

                        //更新全局-线束
                        CableGlabel.StartWorking(carrier.Code);


                        ei.ObjW.ResultCode = 1;
                        ei.ObjW.ACK = 1;
                        ei.ObjW.Msg = $"空载具进入首站";
                        return;
                    }
                }
                //载具上有产品进站，只需要根据产品过站记录进行业务逻辑处理
                //进站产品是根据NextStep进行判断
                var nextStepEquipemt = listEquipment.Where(it => it.Code == workingStep.NextStep).FirstOrDefault();//加工记录中下一工位
                if (nextStepEquipemt == null)
                {
                    ei.ObjW.ResultCode = 3;
                    ei.ObjW.ACK = 19;
                    ei.ObjW.Msg = $"加工记录中下一个加工工位{workingStep.NextStep}，对应工艺流程不存在";
                    return;
                }
                var nextStepEquipmentIdx = listEquipment.IndexOf(nextStepEquipemt);//加工记录中下一工位在流程中的索引位置
                if (currrentEquipmentIdx < nextStepEquipmentIdx)//当前加工工位在理论上加工工位的前面，可直接放行
                {
                    ei.ObjW.ResultCode = 2;
                    ei.ObjW.ACK = 20;
                    ei.ObjW.Msg = $"当前加工工位{st}在理论上加工工位{workingStep.NextStep}的前面";
                    return;
                }
                if (currrentEquipmentIdx > nextStepEquipmentIdx)//当前加工工位在理论上加工工位的后面
                {
                    ei.ObjW.ResultCode = 3;
                    ei.ObjW.ACK = 21;
                    ei.ObjW.Msg = $"当前加工工位{st}在理论上加工工位{workingStep.NextStep}的后面";
                    return;
                }
                //可以加工 currrentEquipmentIdx == nextStepEquipmentIdx
                //修改当前加工记录状态
                workingStep.CurrentStep = st;//跟新当前工位
                await workingStepSvr.Edit(workingStep.Adapt<ProWorkingStepEditInput>());

                //尾站打标需要告知PLC产品状态 ACK = 101 ok  = 102 ng
                if (listEquipment.Count == (currrentEquipmentIdx + 1))
                {
                    if (workingStep.ProductStatus == "ok")
                    {
                        ei.ObjW.ACK = 101;
                    }
                    else if (workingStep.ProductStatus == "ng" || workingStep.ProductStatus == "scrap")
                    {
                        ei.ObjW.ACK = 102;
                    }
                    else
                    {
                        ei.ObjW.ACK = 1;
                    }
                }
                else
                {
                    //非尾站
                    ei.ObjW.ACK = 1;
                }
                ei.ObjW.ResultCode = 1;
                ei.ObjW.Msg = $"当前载具{carrier.Code}允许加工";
            }
            else//返修
            {
                //清空全局变量
                CableGlabel.SetDataTmpId(0);

                //根据载具编码获取产品编码
                var carrierPointSvr = App.GetService<IMacPointService>();
                var carrierPoint = await carrierPointSvr.GetMacPointByCarrierIdAndPoint(carrier.Id);//当前载具穴位信息
                if (carrierPoint == null)
                {
                    ei.ObjW.ResultCode = 3;
                    ei.ObjW.ACK = 15;
                    ei.ObjW.Msg = $"载具{carrier.Code}穴位1信息不存在";
                    return;
                }
                //获取当前工艺流程对应的工位列表
                var equipmentSvr = App.GetService<IMacEquipmentService>();
                var listEquipment = await equipmentSvr.GetEquipmentListByFlowId(flow.Id);//工位列表
                var currentEquipemt = listEquipment.Where(it => it.Code == st).FirstOrDefault();//当前工位对应的设备工位信息
                if (currentEquipemt == null)
                {
                    ei.ObjW.ResultCode = 2;
                    ei.ObjW.ACK = 16;
                    ei.ObjW.Msg = $"当前工位{st}不在工艺{flow.Name}的工位列表中";
                    return;
                }
                var currrentEquipmentIdx = listEquipment.IndexOf(currentEquipemt);//当前工位在流程中的索引位置

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

                if (productId == 0 && dataTmp == null)
                {
                    //进站逻辑中，只有首站允许没有加工记录的产品进站
                    if (currrentEquipmentIdx != 0) //不是首站
                    {
                        ei.ObjW.ResultCode = 2;
                        ei.ObjW.ACK = 17;
                        ei.ObjW.Msg = $"载具上没有产品";
                        return;
                    }
                    else//空载具进入首站
                    {
                        //是否清料模式
                        if (workingOrder.Status == "CLEAR")
                        {
                            ei.ObjW.ResultCode = 2;
                            ei.ObjW.ACK = 18;
                            ei.ObjW.Msg = $"清料状态下不允许加工";
                            return;
                        }
                        //新增产品质量记录
                        var dataTmpId = await dataTmpSvr.AddReturnId(new ProDataTmpAddInput()
                        {
                            OrderId = workingOrder.Id,
                            WorkingStepId = 0,
                            ProductStatus = "ok",
                        });
                        //产品绑定载具
                        carrierPoint.BindCode = dataTmpId;
                        carrierPoint.BindStatus = "BIND";
                        await carrierPointSvr.Edit(carrierPoint.Adapt<PointEditInput>());

                        //工单已投产数量+1
                        await orderSvr.EditPutQty(new BaseIdInput { Id = workingOrder.Id }, workingOrder.PutQty + 1, workingOrder.OnlineQty + 1);

                        //更新全局-临时数据ID
                        CableGlabel.SetDataTmpId(dataTmpId);


                        ei.ObjW.ResultCode = 1;
                        ei.ObjW.ACK = 1;
                        ei.ObjW.Msg = $"空载具进入首站";
                        return;
                    }
                }
                else
                {
                    if (workingStep == null)//表示返修载具已进站，但是没有扫返修编码
                    {
                        if (currrentEquipmentIdx != 0) //不是首站
                        {
                            ei.ObjW.ResultCode = 3;
                            ei.ObjW.ACK = 17;
                            ei.ObjW.Msg = $"载具[{carrier.Code}]，没有对应的WorkingStep";
                            return;
                        }
                        ei.ObjW.ResultCode = 1;
                        ei.ObjW.ACK = 1;
                        ei.ObjW.Msg = $"载具[{carrier.Code}]，已进站但未扫返修编码，允许加工";
                        return;
                    }
                    else//已有WorkingStep，需要根据其，切换工艺流程
                    {
                        flow = await flowSvr.GetMacFlowById(workingStep.RepairFlowId);
                        if (flow == null)
                        {
                            ei.ObjW.ResultCode = 3;
                            ei.ObjW.ACK = 12;
                            ei.ObjW.Msg = $"工单{workingOrder.Sono}工艺流程为空";
                            return;
                        }
                        //获取型号配置参数
                        modelParamSvr = App.GetService<IMacModelParamService>();
                        listModelParam = await modelParamSvr.GetListByModelId(flow.ModelId);

                        //获取当前工艺流程对应的工位列表
                        equipmentSvr = App.GetService<IMacEquipmentService>();
                        listEquipment = await equipmentSvr.GetEquipmentListByFlowId(flow.Id);//工位列表
                        currentEquipemt = listEquipment.Where(it => it.Code == st).FirstOrDefault();//当前工位对应的设备工位信息
                        if (currentEquipemt == null)
                        {
                            ei.ObjW.ResultCode = 2;
                            ei.ObjW.ACK = 16;
                            ei.ObjW.Msg = $"当前工位{st}不在工艺{flow.Name}的工位列表中";
                            return;
                        }
                        currrentEquipmentIdx = listEquipment.IndexOf(currentEquipemt);//当前工位在流程中的索引位置
                    }
                }

                //载具上有产品进站，只需要根据产品过站记录进行业务逻辑处理
                //进站产品是根据NextStep进行判断
                var nextStepEquipemt = listEquipment.Where(it => it.Code == workingStep.NextStep).FirstOrDefault();//加工记录中下一工位
                if (nextStepEquipemt == null)
                {
                    ei.ObjW.ResultCode = 3;
                    ei.ObjW.ACK = 19;
                    ei.ObjW.Msg = $"加工记录中下一个加工工位{workingStep.NextStep}，对应工艺流程不存在";
                    return;
                }
                var nextStepEquipmentIdx = listEquipment.IndexOf(nextStepEquipemt);//加工记录中下一工位在流程中的索引位置
                if (currrentEquipmentIdx < nextStepEquipmentIdx)//当前加工工位在理论上加工工位的前面，可直接放行
                {
                    ei.ObjW.ResultCode = 2;
                    ei.ObjW.ACK = 20;
                    ei.ObjW.Msg = $"当前加工工位{st}在理论上加工工位{workingStep.NextStep}的前面";
                    return;
                }
                if (currrentEquipmentIdx > nextStepEquipmentIdx)//当前加工工位在理论上加工工位的后面
                {
                    ei.ObjW.ResultCode = 3;
                    ei.ObjW.ACK = 21;
                    ei.ObjW.Msg = $"当前加工工位{st}在理论上加工工位{workingStep.NextStep}的后面";
                    return;
                }
                //可以加工 currrentEquipmentIdx == nextStepEquipmentIdx
                //修改当前加工记录状态
                workingStep.CurrentStep = st;//跟新当前工位
                await workingStepSvr.Edit(workingStep.Adapt<ProWorkingStepEditInput>());

                //尾站打标需要告知PLC产品状态 ACK = 101 ok  = 102 ng
                if (listEquipment.Count == (currrentEquipmentIdx + 1))
                {
                    if (workingStep.ProductStatus == "ok")
                    {
                        ei.ObjW.ACK = 101;
                    }
                    else if (workingStep.ProductStatus == "ng" || workingStep.ProductStatus == "scrap")
                    {
                        ei.ObjW.ACK = 102;
                    }
                    else
                    {
                        ei.ObjW.ACK = 1;
                    }
                }
                else
                {
                    //非尾站
                    ei.ObjW.ACK = 1;
                }
                ei.ObjW.ResultCode = 1;
                ei.ObjW.Msg = $"当前载具{carrier.Code}允许加工";
            }
        }

        //出站
        if (ei.ReadClassName.Contains("PlcToEap_OUT"))
        {
            //因为这个项目不需要返修，所以PLC反馈结果2/3都当作报废处理
            if (ei.ObjR.ResultCode == 2 || ei.ObjR.ResultCode == 3) ei.ObjR.ResultCode = 3;

            string s = "";
            if (ei.Idx % 4 == 0 || ei.Idx % 4 == 1) s = "01"; else s = "02";
            var st = $"OP{((ei.Idx / 4) + 1) * 10}{s}";

            //载具编码读取
            var readCarrierText = ReadScan(st);
            if (readCarrierText == "NoRead")
            {
                ei.ObjW.ResultCode = 3;
                ei.ObjW.ACK = 11;
                ei.ObjW.Msg = "载具编码读取失败";
                return;
            }

            //通过载具编码查找产品码
            var carrierSvr = App.GetService<IMacCarrierService>();
            //查看载具编码是否在列表中
            var carrierList = await carrierSvr.GetListByModelId(flow.ModelId);
            var carrier = carrierList.Where(it => it.Code == readCarrierText).FirstOrDefault();
            if (carrier == null)
            {
                ei.ObjW.ResultCode = 3;
                ei.ObjW.ACK = 13;
                ei.ObjW.Msg = $"载具{readCarrierText}不在载具列表中";
                return;
            }
            if (carrier.CarrierStatus == "DISABLED")//不能使用的载具
            {
                ei.ObjW.ResultCode = 3;
                ei.ObjW.ACK = 14;
                ei.ObjW.Msg = $"载具{readCarrierText}是弃用的";
                return;
            }
            ei.ObjW.CarrierSN = carrier.Code;//载具编码赋值给PLC

            if (workingOrder.OrderType == "Normal")
            {
                //获取当前工艺流程对应的工位列表
                var equipmentSvr = App.GetService<IMacEquipmentService>();
                var listEquipment = await equipmentSvr.GetEquipmentListByFlowId(flow.Id);//工位列表
                var currentEquipemt = listEquipment.Where(it => it.Code == st).FirstOrDefault();//当前工位对应的设备工位信息
                if (currentEquipemt == null)
                {
                    ei.ObjW.ResultCode = 2;
                    ei.ObjW.ACK = 16;
                    ei.ObjW.Msg = $"当前工位{st}不在工艺{flow.Name}的工位列表中";
                    return;
                }
                //防止重复出站
                var currrentEquipmentIdx = listEquipment.IndexOf(currentEquipemt);//当前工位在流程中的索引位置
                                                                                  //根据载具编码获取产品编码
                var carrierPointSvr = App.GetService<IMacPointService>();
                var carrierPoint = await carrierPointSvr.GetMacPointByCarrierIdAndPoint(carrier.Id);//当前载具穴位信息
                if (carrierPoint == null)
                {
                    ei.ObjW.ResultCode = 3;
                    ei.ObjW.ACK = 15;
                    ei.ObjW.Msg = $"载具{carrier.Code}穴位1信息不存在";
                    return;
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

                var workingStepOut = await workingStepSvr.GetWorkingStepById(workingStepIdForDateTmp);

                if (productId == 0 && dataTmp == null && workingStepOut == null)//产品SN不存在
                {
                    ei.ObjW.ResultCode = 2;
                    ei.ObjW.ACK = 17;
                    ei.ObjW.Msg = $"载具上没有产品";
                    return;
                }
                //进站产品是根据CurrentStep进行判断
                var currentStepEquipemt = listEquipment.Where(it => it.Code == workingStepOut.CurrentStep).FirstOrDefault();//加工记录中当前工位
                if (currentStepEquipemt == null)
                {
                    ei.ObjW.ResultCode = 3;
                    ei.ObjW.ACK = 19;
                    ei.ObjW.Msg = $"加工记录中当前加工工位{workingStepOut.CurrentStep}，对应工艺流程不存在";
                    return;
                }
                var currentStepEquipmentIdx = listEquipment.IndexOf(currentStepEquipemt);//加工记录中下一工位在流程中的索引位置
                if (currrentEquipmentIdx < currentStepEquipmentIdx)
                {
                    ei.ObjW.ResultCode = 2;
                    ei.ObjW.ACK = 20;
                    ei.ObjW.Msg = $"当前加工工位{st}在理论上加工工位{workingStepOut.CurrentStep}的前面";
                    return;
                }
                if (currrentEquipmentIdx > currentStepEquipmentIdx)//当前加工工位在理论上加工工位的后面
                {
                    ei.ObjW.ResultCode = 3;
                    ei.ObjW.ACK = 21;
                    ei.ObjW.Msg = $"当前加工工位{st}在理论上加工工位{workingStepOut.CurrentStep}的后面";
                    return;
                }
                // currrentEquipmentIdx == currentStepEquipmentIdx
                //防止重复出站
                if (workingStepOut.CurrentStep != workingStepOut.NextStep)//已经报工出站过了
                {
                    ei.ObjW.ResultCode = 2;
                    ei.ObjW.ACK = 21;
                    ei.ObjW.Msg = $"当前加工工位{st}已出过站，不能重复出站";
                    return;
                }
                //第一站出站前需要检查有没有扫线束码
                //需要扫描线束编码

                //if (string.IsNullOrEmpty(dataTmp.CableSN))//若有产品没有线束编码可通过型号参数设定
                //{
                //  //  if(listModelParam.Where(it=>it))
                //    ei.ObjW.ResultCode = 3;
                //    ei.ObjW.ACK = 23;
                //    ei.ObjW.Msg = $"没有扫描线束编码，请扫码后再试";
                //    return;
                //}

                //修改载具+过程数据+加工流程 的状态
                //加工流程
                //查找当前流程的下一个流程 以及最后一个流程
                MacEquipment nextEquipment = null;
                MacEquipment finishedEquipenmt = null;
                if (listEquipment.Count > (currrentEquipmentIdx + 1))//当前工位后面还有需要加工的工位
                {
                    nextEquipment = listEquipment[currrentEquipmentIdx + 1];
                    finishedEquipenmt = listEquipment.LastOrDefault();
                    //NG产品下一工位设定到最后一个工位
                    if (ei.ObjR.ResultCode == 1)//OK品
                    {

                    }
                    else if (ei.ObjR.ResultCode == 2)//ng品，可返修
                    {
                        nextEquipment = finishedEquipenmt;
                        workingStepOut.NgStep = st;//ng工位
                                                   //设置返修工艺路线
                        var flowChildrenList = await flowSvr.GetChildListById(flow.Id, false);
                        var ngFlow = flowChildrenList.Where(it => it.EquipmentId == currentEquipemt.Id).FirstOrDefault();
                        if (ngFlow == null)//没有找到对应的返修工艺，可能生产过程中有人改变了工艺导致
                        {
                            ei.ObjW.ResultCode = 3;
                            ei.ObjW.ACK = 22;
                            ei.ObjW.Msg = $"工位{st}没有对应的返修工艺路线";
                            return;
                        }
                        workingStepOut.RepairFlowId = ngFlow.Id;//返修工艺路线ID
                        workingStepOut.RepairCount += 1;//返修次数+1
                        workingStepOut.ProductStatus = "ng";//ng 返修扫码后才会改成返修状态
                    }
                    else if (ei.ObjR.ResultCode == 3)//报废品，不可返修
                    {
                        nextEquipment = finishedEquipenmt;
                        workingStepOut.NgStep = st;//ng工位
                        workingStepOut.ProductStatus = "scrap";
                    }
                    else
                    {
                        ei.ObjW.ResultCode = 3;
                        ei.ObjW.ACK = 23;
                        ei.ObjW.Msg = $"工位{st}，resultCode:[{ei.ObjR.ResultCode}]不在设定范围内";
                        return;
                    }
                    workingStepOut.NextStep = nextEquipment.Code;//赋值下一个工位
                                                                 //设置缓存数据 
                    SetDateTmp(dataTmp, st, ei.ObjR);

                    //更新 WorkingStep 
                    await workingStepSvr.Edit(workingStepOut.Adapt<ProWorkingStepEditInput>());
                }
                else if (listEquipment.Count == (currrentEquipmentIdx + 1))//当前工位就是最后一个工位
                {
                    workingStepOut.NextStep = "end";//表示生产结束
                                                    //根据产品情况修改产品数据+过站记录+工单数据
                    if (workingStepOut.ProductStatus == "ok")//ok品
                    {
                        //跟新工单
                        workingOrder.OkQty += 1;
                        workingOrder.OnlineQty -= 1;
                        //保存数据 封装到ProDataTmpService中



                        //转移数据
                        var dataSvr = App.GetService<IProDataService>();
                        await dataSvr.Add(dataTmp.Adapt<ProDataAddInput>());
                        await dataTmpSvr.Delete(new List<BaseIdInput> { new BaseIdInput { Id = dataTmp.Id } });
                        //删除过站记录
                        await workingStepSvr.Delete(new List<BaseIdInput> { new BaseIdInput { Id = workingStepOut.Id } });
                        //删除extWrokingStep 里面主要记录打标内容
                        var extWorkingStepSvr = App.GetService<IExtWorkingStepService>();
                        await extWorkingStepSvr.DeleteByWorkingStepIdAsync(workingStepOut.Id);
                        //解绑载具
                        carrierPoint.BindStatus = "UNBIND";//解绑

                        await carrierPointSvr.Edit(carrierPoint.Adapt<PointEditInput>());
                    }
                    else if (workingStepOut.ProductStatus == "repair" || workingStepOut.ProductStatus == "ng")//返修品or ng品
                    {
                        //跟新工单
                        workingOrder.RepairQty += 1;//返修数量加1
                        workingOrder.OnlineQty -= 1;
                        //更新过站记录
                        workingStepOut.RepairCount += 1;//返修次数+1
                        //保存数据 封装到ProDataTmpService中
                        // do ................

                        //转移数据
                        var dataSvr = App.GetService<IProDataService>();
                        await dataSvr.Add(dataTmp.Adapt<ProDataAddInput>());
                        await dataTmpSvr.Delete(new List<BaseIdInput> { new BaseIdInput { Id = dataTmp.Id } });//每次返修产生一条新的临时数据，返修扫码后新增
                        //修改过站记录
                        await workingStepSvr.Edit(workingStepOut.Adapt<ProWorkingStepEditInput>());
                        //解绑载具
                        carrierPoint.BindStatus = "UNBIND";//解绑
                        await carrierPointSvr.Edit(carrierPoint.Adapt<PointEditInput>());
                    }
                    else//报废品
                    {
                        //跟新工单
                        workingOrder.ScrapQty += 1;//报废数量加1
                        workingOrder.OnlineQty -= 1;

                        //保存数据 封装到ProDataTmpService中

                        //转移数据
                        IProDataService dataSvr = App.GetService<IProDataService>();
                        await dataSvr.Add(dataTmp.Adapt<ProDataAddInput>());
                        await dataTmpSvr.Delete(new List<BaseIdInput> { new BaseIdInput { Id = dataTmp.Id } });
                        //删除过站记录
                        await workingStepSvr.Delete(new List<BaseIdInput> { new BaseIdInput { Id = workingStepOut.Id } });
                        //删除extWrokingStep 里面主要记录打标内容
                        var extWorkingStepSvr = App.GetService<IExtWorkingStepService>();
                        await extWorkingStepSvr.DeleteByWorkingStepIdAsync(workingStepOut.Id);
                        //解绑载具
                        carrierPoint.BindStatus = "UNBIND";//解绑
                        await carrierPointSvr.Edit(carrierPoint.Adapt<PointEditInput>());
                    }
                    //跟新工单数据 存在bug，修改成只跟新指定字段
                    await orderSvr.EditQty(new BaseIdInput { Id = workingOrder.Id }, workingOrder.OkQty, workingOrder.OnlineQty, workingOrder.RepairQty, workingOrder.ScrapQty);
                }
                else
                {
                    //不存在
                    ei.ObjW.ResultCode = 3;
                    ei.ObjW.ACK = 100;
                    ei.ObjW.Msg = $"不存在的流程";
                    return;
                }


                //数据保存

                await dataTmpSvr.Edit(dataTmp.Adapt<ProDataTmpEditInput>());
                //跟新 线束全局变量
                CableGlabel.EndWorking();
                ei.ObjW.ResultCode = 1;
                ei.ObjW.Msg = $"载具{carrier.Code}出站成功";
                return;
            }
            else//返修
            {
                //获取当前工艺流程对应的工位列表
                var equipmentSvr = App.GetService<IMacEquipmentService>();
                var listEquipment = await equipmentSvr.GetEquipmentListByFlowId(flow.Id);//工位列表
                var currentEquipemt = listEquipment.Where(it => it.Code == st).FirstOrDefault();//当前工位对应的设备工位信息
                if (currentEquipemt == null)
                {
                    ei.ObjW.ResultCode = 2;
                    ei.ObjW.ACK = 16;
                    ei.ObjW.Msg = $"当前工位{st}不在工艺{flow.Name}的工位列表中";
                    return;
                }
                //防止重复出站
                var currrentEquipmentIdx = listEquipment.IndexOf(currentEquipemt);//当前工位在流程中的索引位置
                                                                                  //根据载具编码获取产品编码
                var carrierPointSvr = App.GetService<IMacPointService>();
                var carrierPoint = await carrierPointSvr.GetMacPointByCarrierIdAndPoint(carrier.Id);//当前载具穴位信息
                if (carrierPoint == null)
                {
                    ei.ObjW.ResultCode = 3;
                    ei.ObjW.ACK = 15;
                    ei.ObjW.Msg = $"载具{carrier.Code}穴位1信息不存在";
                    return;
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

                var workingStepOut = await workingStepSvr.GetWorkingStepById(workingStepIdForDateTmp);

                if (productId == 0 && dataTmp == null)//产品SN不存在
                {
                    ei.ObjW.ResultCode = 2;
                    ei.ObjW.ACK = 17;
                    ei.ObjW.Msg = $"载具上没有产品";
                    return;
                }
                else
                {
                    if (workingStepOut == null)//表示返修载具已进站，但是没有扫返修编码
                    {
                        ei.ObjW.ResultCode = 3;
                        ei.ObjW.ACK = 17;
                        ei.ObjW.Msg = $"载具[{carrier.Code}]，没有对应的WorkingStep";
                        return;
                    }
                    else//已有WorkingStep，需要根据其，切换工艺流程
                    {
                        flow = await flowSvr.GetMacFlowById(workingStepOut.RepairFlowId);
                        if (flow == null)
                        {
                            ei.ObjW.ResultCode = 3;
                            ei.ObjW.ACK = 12;
                            ei.ObjW.Msg = $"工单{workingOrder.Sono}工艺流程为空";
                            return;
                        }
                        //获取型号配置参数
                        modelParamSvr = App.GetService<IMacModelParamService>();
                        listModelParam = await modelParamSvr.GetListByModelId(flow.ModelId);

                        //获取当前工艺流程对应的工位列表
                        equipmentSvr = App.GetService<IMacEquipmentService>();
                        listEquipment = await equipmentSvr.GetEquipmentListByFlowId(flow.Id);//工位列表
                        currentEquipemt = listEquipment.Where(it => it.Code == st).FirstOrDefault();//当前工位对应的设备工位信息
                        if (currentEquipemt == null)
                        {
                            ei.ObjW.ResultCode = 2;
                            ei.ObjW.ACK = 16;
                            ei.ObjW.Msg = $"当前工位{st}不在工艺{flow.Name}的工位列表中";
                            return;
                        }
                        currrentEquipmentIdx = listEquipment.IndexOf(currentEquipemt);//当前工位在流程中的索引位置
                    }
                }
                //进站产品是根据CurrentStep进行判断
                var currentStepEquipemt = listEquipment.Where(it => it.Code == workingStepOut.CurrentStep).FirstOrDefault();//加工记录中当前工位
                if (currentStepEquipemt == null)
                {
                    ei.ObjW.ResultCode = 3;
                    ei.ObjW.ACK = 19;
                    ei.ObjW.Msg = $"加工记录中当前加工工位{workingStepOut.CurrentStep}，对应工艺流程不存在";
                    return;
                }
                var currentStepEquipmentIdx = listEquipment.IndexOf(currentStepEquipemt);//加工记录中下一工位在流程中的索引位置
                if (currrentEquipmentIdx < currentStepEquipmentIdx)
                {
                    ei.ObjW.ResultCode = 2;
                    ei.ObjW.ACK = 20;
                    ei.ObjW.Msg = $"当前加工工位{st}在理论上加工工位{workingStepOut.CurrentStep}的前面";
                    return;
                }
                if (currrentEquipmentIdx > currentStepEquipmentIdx)//当前加工工位在理论上加工工位的后面
                {
                    ei.ObjW.ResultCode = 3;
                    ei.ObjW.ACK = 21;
                    ei.ObjW.Msg = $"当前加工工位{st}在理论上加工工位{workingStepOut.CurrentStep}的后面";
                    return;
                }
                // currrentEquipmentIdx == currentStepEquipmentIdx
                //防止重复出站
                if (workingStepOut.CurrentStep != workingStepOut.NextStep)//已经报工出站过了
                {
                    ei.ObjW.ResultCode = 2;
                    ei.ObjW.ACK = 21;
                    ei.ObjW.Msg = $"当前加工工位{st}已出过站，不能重复出站";
                    return;
                }
                //第一站出站前需要检查有没有扫线束码
                //需要扫描线束编码

                //if (string.IsNullOrEmpty(dataTmp.CableSN))//若有产品没有线束编码可通过型号参数设定
                //{
                //  //  if(listModelParam.Where(it=>it))
                //    ei.ObjW.ResultCode = 3;
                //    ei.ObjW.ACK = 23;
                //    ei.ObjW.Msg = $"没有扫描线束编码，请扫码后再试";
                //    return;
                //}
                //修改载具+过程数据+加工流程 的状态
                //加工流程
                //查找当前流程的下一个流程 以及最后一个流程
                MacEquipment nextEquipment = null;
                MacEquipment finishedEquipenmt = null;
                if (listEquipment.Count > (currrentEquipmentIdx + 1))//当前工位后面还有需要加工的工位
                {
                    nextEquipment = listEquipment[currrentEquipmentIdx + 1];
                    finishedEquipenmt = listEquipment.LastOrDefault();
                    //NG产品下一工位设定到最后一个工位
                    if (ei.ObjR.ResultCode == 1)//OK品
                    {

                    }
                    else if (ei.ObjR.ResultCode == 2)//ng品，可返修
                    {
                        nextEquipment = finishedEquipenmt;
                        workingStepOut.NgStep = st;//ng工位
                                                   //设置返修工艺路线
                        var flowChildrenList = await flowSvr.GetChildListById(flow.Id, false);
                        var ngFlow = flowChildrenList.Where(it => it.EquipmentId == currentEquipemt.Id).FirstOrDefault();
                        if (ngFlow == null)//没有找到对应的返修工艺，可能生产过程中有人改变了工艺导致
                        {
                            ei.ObjW.ResultCode = 3;
                            ei.ObjW.ACK = 22;
                            ei.ObjW.Msg = $"工位{st}没有对应的返修工艺路线";
                            return;
                        }
                        workingStepOut.RepairFlowId = ngFlow.Id;//返修工艺路线ID
                        workingStepOut.RepairCount += 1;//返修次数+1
                        workingStepOut.ProductStatus = "ng";//ng 返修扫码后才会改成返修状态
                    }
                    else if (ei.ObjR.ResultCode == 3)//报废品，不可返修
                    {
                        nextEquipment = finishedEquipenmt;
                        workingStepOut.NgStep = st;//ng工位
                        workingStepOut.ProductStatus = "scrap";
                    }
                    else
                    {
                        ei.ObjW.ResultCode = 3;
                        ei.ObjW.ACK = 23;
                        ei.ObjW.Msg = $"工位{st}，resultCode:[{ei.ObjR.ResultCode}]不在设定范围内";
                        return;
                    }
                    workingStepOut.NextStep = nextEquipment.Code;//赋值下一个工位
                                                                 //设置缓存数据 
                    SetDateTmp(dataTmp, st, ei.ObjR);

                    //更新 WorkingStep 
                    await workingStepSvr.Edit(workingStepOut.Adapt<ProWorkingStepEditInput>());
                }
                else if (listEquipment.Count == (currrentEquipmentIdx + 1))//当前工位就是最后一个工位
                {
                    workingStepOut.NextStep = "end";//表示生产结束
                                                    //根据产品情况修改产品数据+过站记录+工单数据
                    if (workingStepOut.ProductStatus == "ok")//ok品
                    {
                        //跟新工单
                        workingOrder.OkQty += 1;
                        workingOrder.OnlineQty -= 1;
                        //保存数据 封装到ProDataTmpService中



                        //转移数据
                        var dataSvr = App.GetService<IProDataService>();
                        await dataSvr.Add(dataTmp.Adapt<ProDataAddInput>());
                        await dataTmpSvr.Delete(new List<BaseIdInput> { new BaseIdInput { Id = dataTmp.Id } });
                        //删除过站记录
                        await workingStepSvr.Delete(new List<BaseIdInput> { new BaseIdInput { Id = workingStepOut.Id } });
                        //删除extWrokingStep 里面主要记录打标内容
                        var extWorkingStepSvr = App.GetService<IExtWorkingStepService>();
                        await extWorkingStepSvr.DeleteByWorkingStepIdAsync(workingStepOut.Id);
                        //解绑载具
                        carrierPoint.BindStatus = "UNBIND";//解绑

                        await carrierPointSvr.Edit(carrierPoint.Adapt<PointEditInput>());
                    }
                    else if (workingStepOut.ProductStatus == "repair")//返修品
                    {
                        //跟新工单
                        workingOrder.RepairQty += 1;//返修数量加1
                        workingOrder.OnlineQty -= 1;
                        //更新过站记录
                        workingStepOut.RepairCount += 1;//返修次数+1
                                                        //保存数据 封装到ProDataTmpService中


                        //转移数据
                        var dataSvr = App.GetService<IProDataService>();
                        await dataSvr.Add(dataTmp.Adapt<ProDataAddInput>());
                        await dataTmpSvr.Delete(new List<BaseIdInput> { new BaseIdInput { Id = dataTmp.Id } });//每次返修产生一条新的临时数据，返修扫码后新增
                                                                                                               //修改过站记录
                        await workingStepSvr.Edit(workingStepOut.Adapt<ProWorkingStepEditInput>());
                        //解绑载具
                        carrierPoint.BindStatus = "UNBIND";//解绑
                        await carrierPointSvr.Edit(carrierPoint.Adapt<PointEditInput>());
                    }
                    else//报废品
                    {
                        //跟新工单
                        workingOrder.ScrapQty += 1;//报废数量加1
                        workingOrder.OnlineQty -= 1;

                        //保存数据 封装到ProDataTmpService中

                        //转移数据
                        IProDataService dataSvr = App.GetService<IProDataService>();
                        await dataSvr.Add(dataTmp.Adapt<ProDataAddInput>());
                        await dataTmpSvr.Delete(new List<BaseIdInput> { new BaseIdInput { Id = dataTmp.Id } });
                        //删除过站记录
                        await workingStepSvr.Delete(new List<BaseIdInput> { new BaseIdInput { Id = workingStepOut.Id } });
                        //删除extWrokingStep 里面主要记录打标内容
                        var extWorkingStepSvr = App.GetService<IExtWorkingStepService>();
                        await extWorkingStepSvr.DeleteByWorkingStepIdAsync(workingStepOut.Id);
                        //解绑载具
                        carrierPoint.BindStatus = "UNBIND";//解绑
                        await carrierPointSvr.Edit(carrierPoint.Adapt<PointEditInput>());
                    }
                    //跟新工单数据 存在bug，修改成只跟新指定字段
                    await orderSvr.EditQty(new BaseIdInput { Id = workingOrder.Id }, workingOrder.OkQty, workingOrder.OnlineQty, workingOrder.RepairQty, workingOrder.ScrapQty);
                }
                else
                {
                    //不存在
                    ei.ObjW.ResultCode = 3;
                    ei.ObjW.ACK = 100;
                    ei.ObjW.Msg = $"不存在的流程";
                    return;
                }


                //数据保存

                await dataTmpSvr.Edit(dataTmp.Adapt<ProDataTmpEditInput>());

                //更新全局-临时数据ID
                CableGlabel.SetDataTmpId(0);

                ei.ObjW.ResultCode = 1;

                ei.ObjW.Msg = $"载具{carrier.Code}出站成功";
                return;
            }
        }

        //读取螺丝枪数据
        if (ei.ReadClassName.Contains("Base_PlcToEap_ScrewGun"))
        {
            var screwGunName = ei.ReadClassName.Split("_")[0];
            var r = ei.ObjR as Base_PlcToEap_ScrewGun;
            var w = ei.ObjW as Base_EapToPlc_ScrewGun;

            //获取螺丝枪服务中新采集到的数据
            var kwScrewSvr = App.GetService<IKwScrewGunFactoryService>();
            var recvInfo = kwScrewSvr.GetNewRecvInfoAndUsed(screwGunName);
            if (recvInfo == null)
            {
                ei.ObjW.ResultCode = 3;
                ei.ObjW.ACK = 12;
                ei.ObjW.Msg = $"螺丝枪[{screwGunName}没有新的数据]";
                return;
            }
            //螺丝数据发送给PLC
            w.ScrewGun_Send.Result = recvInfo.Result ? (short)1 : (short)2;//1 ok 2 ng
            w.ScrewGun_Send.TorqueSet = recvInfo.SetTorque;
            w.ScrewGun_Send.Torque = recvInfo.Torque;//扭力
            w.ScrewGun_Send.AngleSet = recvInfo.SetAngle;
            w.ScrewGun_Send.Angle = recvInfo.Angle;//角度
            w.ScrewGun_Send.RunTime = recvInfo.RunTimeS;
            w.ScrewGun_Send.ProNm = (short)recvInfo.ProNum;//角度

            ei.ObjW.ResultCode = 1;
            ei.ObjW.ACK = 1;
            ei.ObjW.Msg = $"螺丝枪[{screwGunName}数据成功";

        }
       
    }
    /// <summary>
    /// 信息提示 不影响通讯
    /// </summary>
    /// <param name="infoMsg"></param>
    private void OnInfo(string infoMsg)
    {
        //XtraMessageBox.Show($"{errMsg}", "提示", MessageBoxButtons.OK, MessageBoxIcon.inf);
        _Logger.LogInformation(infoMsg);
    }
    /// <summary>
    /// 错误回调 致命错误
    /// </summary>
    /// <param name="errMsg"></param>
    private void OnErr(string errMsg)
    {
        //XtraMessageBox.Show($"{errMsg}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        _Logger.LogError(errMsg);
    }

    #region 读取RFID
    /// <summary>
    /// 通过ST读取RFID内容，失败返回NoRead
    /// </summary>
    /// <param name="st"></param>
    /// <returns></returns>
    public string ReadRFID(string st)
    {
        var sygoleFac = App.GetService<ISygoleFactoryService>();
        var listConn = sygoleFac.GetConnections();
        string resStr = string.Empty;
        if (sygoleFac.ReadRFID(st, 6, ref resStr))
        {
            if (string.IsNullOrEmpty(resStr))
                return "NoRead";
            else
                return resStr;
        }
        else
        {
            return "NoRead";
        }
    }

    public string ReadScan(string st)
    {
        var scanFac = App.GetService<IFixedScanFactoryService>();
        string resStr = string.Empty;
        
        if (scanFac.ReadScan(st, ref resStr))
        {
            return resStr;
        }
        else
        {
            if (scanFac.ReadScan(st, ref resStr))
            {
                return resStr;
            }
            else
            {
                return "NoRead";
            }
        }
    }
    #endregion

    public class LabelRecive
    {
        public bool Result { get; set; }
        public string ResultText { get; set; }
    }

    #region 更新数据方法

    private void SetDateTmp(ProDataTmp dataTmp, string st, dynamic objR)
    {

    }

    #endregion
}
