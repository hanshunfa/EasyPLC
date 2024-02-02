using EasyPlc.Plugin.Plc.Global;
using Furion.FriendlyException;
using NetTaste;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace EasyPlc.Plugin.Plc;

/// <summary>
///SiemensPLC信息设定工具类
/// <summary>
public class SiemensPlcInfoUtil : IGenSiemensPlcInfoUtil
{
    private readonly IPlcResourceService _plcResourceService;
    private readonly IPlcConfigService _plcConfigService;
    private readonly IAddressService _addressService;

    public SiemensPlcInfoUtil(
        IPlcResourceService plcResourceService,
        IPlcConfigService plcConfigService,
        IAddressService addressService
        )
    {
        _plcResourceService = plcResourceService;
        _plcConfigService = plcConfigService;
        _addressService = addressService;
    }

    /// <summary>
    ///SiemensPLC获取信息
    /// <summary>
    public async Task<List<SiemensPlcInfo>> GenSiemensPlcInfoList()
    {
        //创建动态类型
        SiemensPlcGlobal.ListType = await GenSiemensPlcObjType2Global();
        SiemensPlcGlobal.ListSiemensPlcInfo.Clear();
        SiemensPlcGlobal.ListSiemensPlcInfo = await GenSiemensPlcInfoList2Global();
        return SiemensPlcGlobal.ListSiemensPlcInfo;
    }

    public async Task<List<SiemensPlcInfo>> GenSiemensPlcInfoList2Global()
    {
        var plcInofList = new List<SiemensPlcInfo>();
        //01 查询多少个PLC对象
        var opList = (await _plcConfigService.GetListBySortCodeAsync()).Where(it => it.Category == "PLC").ToList();
        var resourceList = await _plcResourceService.GetListAsync();
        foreach (var op in opList)
        {
            var plcExtJson = op.ExtJson.ToObject<PlcExtJson>();
            var plcInfo = new SiemensPlcInfo
            {
                Name = op.Name,
                IP = plcExtJson.Ip,
                Port = plcExtJson.Port,
                Rack = plcExtJson.Rack,
                Slot = plcExtJson.Slot,
                Version = plcExtJson.Version,
            };
            plcInofList.Add(plcInfo);
            //03 PLC各区
            var childs = await _plcConfigService.GetChildListById(op.Id, false);
            //04 公共区
            var pi = new PublicInfo();
            plcInfo.PI = pi;
            var ggqR = childs.Where(it => it.Category == "GGQ-R").FirstOrDefault();
            if (ggqR != null)//公共区读
            {
                var addrExtJson = ggqR.ExtJson.ToObject<AddrExtJson>();
                pi.ReadInfo.StartAddr = addrExtJson.StartAddr;
                var addrs = await _addressService.GetListByPlcId(ggqR.Id);
                var addr_resoure_ids = addrs.Select(it => it.ResourceId).ToList();
                if (addr_resoure_ids.Count != 1) throw Oops.Bah($"{op.Name}公共区（读）有且只有一个结构对象");
                var rObj = resourceList.Where(it => it.Id == addr_resoure_ids[0]).FirstOrDefault();
                pi.ReadInfo.ClassName = rObj.Code;
                pi.ReadInfo.ObjT = SiemensPlcGlobal.ListType.GetMyType(rObj.Code);
                //每个对象对应一颗树
                var tree = await _plcResourceService.Tree(null, new PlcResourceTreeInput { ParentId = rObj.Id }, false);
                pi.ReadInfo.ListPr = tree;
                var lr = await _plcResourceService.GetLenghAsync(rObj.Id);
                pi.ReadInfo.Lenght = (ushort)lr;
            }
            var ggqW = childs.Where(it => it.Category == "GGQ-W").FirstOrDefault();
            if (ggqW != null)//公共区读
            {
                var addrExtJson = ggqW.ExtJson.ToObject<AddrExtJson>();
                pi.WriterInfo.StartAddr = addrExtJson.StartAddr;
                var addrs = await _addressService.GetListByPlcId(ggqW.Id);
                var addr_resoure_ids = addrs.Select(it => it.ResourceId).ToList();
                if (addr_resoure_ids.Count != 1) throw Oops.Bah($"{op.Name}公共区（写）有且只有一个结构对象");
                var wObj = resourceList.FirstOrDefault(it => it.Id == addr_resoure_ids[0]);
                pi.WriterInfo.ClassName = wObj.Code;
                pi.WriterInfo.ObjT = SiemensPlcGlobal.ListType.GetMyType(wObj.Code);
                //每个对象对应一颗树
                var tree = await _plcResourceService.Tree(null, new PlcResourceTreeInput { ParentId = wObj.Id }, false);
                pi.WriterInfo.ListPr = tree;
                var lw = await _plcResourceService.GetLenghAsync(wObj.Id);
                pi.WriterInfo.Lenght = (ushort)lw;
                pi.WriterInfo.IsReset = true;
            }
            //事件区
            var eis = new List<EventInfo>();
            plcInfo.EIs = eis;
            var sjqR = childs.First(it => it.Category == "SJQ-R");
            var sjqW = childs.First(it => it.Category == "SJQ-W");
            if (sjqR == null || sjqW == null) throw Oops.Bah($"{op.Name}事件区存在未定义");
            var addrSjr = await _addressService.GetListByPlcId(sjqR.Id);
            var addrSjw = await _addressService.GetListByPlcId(sjqW.Id);
            if (addrSjr.Count != addrSjw.Count) throw Oops.Bah($"{op.Name}事件区读写数量不匹配，读{addrSjr.Count}条，写{addrSjw.Count}条");
            int lenR = 0, lenW = 0;
            //地址换算
            var ejR = sjqR.ExtJson.ToObject<AddrExtJson>();
            var addrSplitR = ejR.StartAddr.Split('.');
            lenR = addrSplitR[1].ToInt();
            var ejW = sjqW.ExtJson.ToObject<AddrExtJson>();
            var addrSplitW = ejW.StartAddr.Split('.');
            lenW = addrSplitW[1].ToInt();

            for (int i = 0; i < addrSjr.Count; i++)
            {
                var ei = new EventInfo();
                eis.Add(ei);

                var sjR = resourceList.First(it => it.Id == addrSjr[i].ResourceId);
                var sjW = resourceList.First(it => it.Id == addrSjw[i].ResourceId);
                ei.Idx = i;
                ei.ReadInfo.StartAddr = $"{addrSplitR[0]}.{lenR}";
                ei.ReadInfo.ClassName = sjR.Code;
                ei.ReadInfo.ObjT = SiemensPlcGlobal.ListType.GetMyType(sjR.Code);
                ei.WriteInfo.StartAddr = $"{addrSplitW[0]}.{lenW}";
                ei.WriteInfo.ClassName = sjW.Code;
                ei.WriteInfo.ObjT = SiemensPlcGlobal.ListType.GetMyType(sjW.Code);
                //对象tree
                var treeR = await _plcResourceService.Tree(null, new PlcResourceTreeInput { ParentId = sjR.Id }, false);
                ei.ReadInfo.ListPr = treeR;
                var treeW = await _plcResourceService.Tree(null, new PlcResourceTreeInput { ParentId = sjW.Id }, false);
                ei.WriteInfo.ListPr = treeW;

                //地址偏移计算

                var lr = await _plcResourceService.GetLenghAsync(sjR.Id);
                lenR += lr;
                var lw = await _plcResourceService.GetLenghAsync(sjW.Id);
                lenW += lw;

                ei.ReadInfo.Lenght = (ushort)lr;
                ei.WriteInfo.Lenght = (ushort)lw;
                ei.WriteInfo.IsReset = true;
            }
        }
        return plcInofList;
    }

    public async Task<List<Type>> GenSiemensPlcObjType2Global()
    {
        //对象类型定义
        var classNames = new List<string>();
        var resourceList = await _plcResourceService.GetListAsync();
        var codes = new List<string>();
        var structIds = new HashSet<long>();//结构对象Ids
        foreach (var resource in from resource in resourceList 
                 where resource.Category == "STRUCTDATA" 
                 where !resource.Code.Contains('[') 
                 where !codes.Contains(resource.Code) 
                 select resource
                )
        {
            codes.Add(resource.Code);
            structIds.Add(resource.Id);
            classNames.Add(resource.Code);
        }
        return SyntaxTreeHelper.GetModelTypeByClass(await CreateGenClassCode(structIds), classNames);
    }
    /// <summary>
    /// 根据PLC结构定义创建类对象
    /// </summary>
    /// <param name="structIds"></param>
    /// <returns></returns>
    private async Task<string> CreateGenClassCode(HashSet<long> structIds)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("namespace RoslynCompileEasyPlcEntities");
        builder.AppendLine("{");
        builder.AppendLine();
        foreach (var structId in structIds)
        {
            var structResource = await _plcResourceService.GetResurceById(structId);
            int nIndentSpaces = 0;
            string strStartIs = "";
            nIndentSpaces += 4;
            strStartIs = GetIndentSpaces(nIndentSpaces);
            builder.AppendLine(strStartIs + "/// <summary>");
            builder.AppendLine(strStartIs + "///" + " " + structResource.Title);
            builder.AppendLine(strStartIs + "/// <summary>");
            builder.AppendLine(strStartIs + "public class " + structResource.Code);
            builder.AppendLine(strStartIs + "{");

            nIndentSpaces += 4;
            string strIs = "";
            strIs = GetIndentSpaces(nIndentSpaces);
            //类里面的
            var childResources = await _plcResourceService.GetChildListById(structId, false, false);
            foreach (var childResource in childResources)
            {
                if (childResource.Category == "BASEDATA")
                {
                    //基础类型
                    if (childResource.ValueType == "Bool")
                    {
                        //1bool->1字节
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine(strIs + "///" + " " + childResource.Title);
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine($"{strIs}public bool {childResource.Code} " + "{get; set; }");
                    }
                    if (childResource.ValueType == "Bool[]")
                    {
                        //1bool->1字节
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine(strIs + "///" + " " + childResource.Title);
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine($"{strIs}public bool[] {childResource.Code} " + "{get; set; }");
                    }
                    if (childResource.ValueType == "Int16")
                    {
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine(strIs + "///" + " " + childResource.Title);
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine($"{strIs}public short {childResource.Code} " + "{get; set; }");
                    }
                    if (childResource.ValueType == "Int16[]")
                    {
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine(strIs + "///" + " " + childResource.Title);
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine($"{strIs}public short[] {childResource.Code} " + "{get; set; }");
                    }
                    if (childResource.ValueType == "Int32")
                    {
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine(strIs + "///" + " " + childResource.Title);
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine($"{strIs}public int {childResource.Code} " + "{get; set; }");
                    }
                    if (childResource.ValueType == "Int32[]")
                    {
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine(strIs + "///" + " " + childResource.Title);
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine($"{strIs}public int[] {childResource.Code} " + "{get; set; }");
                    }
                    if (childResource.ValueType == "Float")
                    {
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine(strIs + "///" + " " + childResource.Title);
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine($"{strIs}public float {childResource.Code} " + "{get; set; }");
                    }
                    if (childResource.ValueType == "Float[]")
                    {
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine(strIs + "///" + " " + childResource.Title);
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine($"{strIs}public float[] {childResource.Code} " + "{get; set; }");
                    }
                    if (childResource.ValueType == "String")
                    {
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine(strIs + "///" + " " + childResource.Title);
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine($"{strIs}public string {childResource.Code} " + "{get; set; }");
                    }
                    if (childResource.ValueType == "WString")
                    {
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine(strIs + "///" + " " + childResource.Title);
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine($"{strIs}public string {childResource.Code} " + "{get; set; }");
                    }
                }
                if (childResource.Category == "ARRDATA")
                {
                    //查看数组里面的对象
                    var arrChilds = await _plcResourceService.GetChildListById(childResource.Id, false, false);
                    if (arrChilds.Count == 0) throw Oops.Bah($"数组里没有对象");
                    var ac = arrChilds[0];//当前只支持结构数据，故获取第一个则是结构信息

                    if (ac.Category == "STRUCTDATA")
                    {
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine(strIs + "///" + " " + childResource.Title);
                        builder.AppendLine(strIs + "/// <summary>");
                        //去掉[x]
                        var idxs = ac.Code.IndexOf('[');
                        var codeName = ac.Code.Substring(0, idxs);
                        builder.AppendLine($"{strIs}public {codeName}[] {childResource.Code}" + "{get; set; }");
                    }
                }
                if (childResource.Category == "STRUCTDATA")
                {
                    builder.AppendLine(strIs + "/// <summary>");
                    builder.AppendLine(strIs + "///" + " " + childResource.Title);
                    builder.AppendLine(strIs + "/// <summary>");
                    builder.AppendLine($"{strIs}public {childResource.Code} {childResource.Code}" + "{get; set; }");
                }
            }
            builder.AppendLine(strStartIs + "}");
        }
        builder.AppendLine("}");
        return builder.ToString();
    }
    
    private async Task<string> CreateGenSiemensPlcInfoCode()
    {
        var resourceList = await _plcResourceService.GetListAsync();

        StringBuilder builder = new StringBuilder();
        int nIndentSpaces = 0;
        builder.AppendLine("namespace EasyPlc.Plugin.Plc;");
        builder.AppendLine();
        builder.AppendLine("/// <summary>");
        builder.AppendLine("///" + " " + "SiemensPLC信息设定工具类");
        builder.AppendLine("/// <summary>");
        builder.AppendLine("public class SiemensPLCInfoUtil : IGenSiemensPlcInfoUtil");
        builder.AppendLine("{");
        nIndentSpaces += 4;
        var strIs_1 = GetIndentSpaces(nIndentSpaces);

        //添加静态方法
        builder.AppendLine(strIs_1 + "/// <summary>");
        builder.AppendLine(strIs_1 + "///" + " " + "SiemensPLC获取信息");
        builder.AppendLine(strIs_1 + "/// <summary>");
        builder.AppendLine(strIs_1 + "public PlcInfo[] GetSiemensPLCInfo()");
        builder.AppendLine(strIs_1 + "{");
        nIndentSpaces += 4;
        var strIs_2 = GetIndentSpaces(nIndentSpaces);
        //new PlcInfo[]
        builder.AppendLine(strIs_2 + "return new PlcInfo[]");
        builder.AppendLine(strIs_2 + "{");
        nIndentSpaces += 4;
        var strIs_3 = GetIndentSpaces(nIndentSpaces);
        //01 查询多少个PLC对象
        var plcList = await _plcConfigService.GetListBySortCodeAsync();
        var opList = plcList.Where(it=>it.Category == "PLC").ToList();
        
        foreach (var op in opList)
        {
            var nIS = nIndentSpaces;
            //new PlcInfo()
            builder.AppendLine(strIs_3 + "new PlcInfo()");
            builder.AppendLine(strIs_3 + "{");
            nIS += 4;
            var strIs_4 = GetIndentSpaces(nIS);
            //02 PLC对象进行赋值
            builder.AppendLine(strIs_4 + $"OP = \"{op.Name}\",");
            var plcExtJson = op.ExtJson.ToObject<PlcExtJson>();
            builder.AppendLine(strIs_4 + $"IP = \"{plcExtJson.Ip}\",");
            builder.AppendLine(strIs_4 + $"Port = {plcExtJson.Port},");
            builder.AppendLine(strIs_4 + $"Rack = {plcExtJson.Rack},");
            builder.AppendLine(strIs_4 + $"Slot = {plcExtJson.Slot},");
            builder.AppendLine(strIs_4 + $"Version = \"{plcExtJson.Version}\",");
            //03 PLC各区
            var childs = await _plcConfigService.GetChildListById(op.Id, false);
            //04 公共区
            var nIS_1 = nIS;
            builder.AppendLine(strIs_4 + $"PI = new PublicInfo()");
            builder.AppendLine(strIs_4 + "{");
            nIS_1 += 4;
            var strIs_4_1 = GetIndentSpaces(nIS_1);
            var ggqR = childs.Where(it => it.Category == "GGQ-R").FirstOrDefault();
            if(ggqR != null)//公共区读
            {
                var addrExtJson = ggqR.ExtJson.ToObject<AddrExtJson>();
                builder.AppendLine(strIs_4_1 + $"ReadAddr = \"{addrExtJson.StartAddr}\",");

                var addrs = await _addressService.GetListByPlcId(ggqR.Id);
                var addr_resoure_ids = addrs.Select(it => it.ResourceId).ToList();
                if(addr_resoure_ids.Count != 1) throw Oops.Bah($"{op.Name}公共区（读）有且只有一个结构对象");
                var rObj = resourceList.Where(it => it.Id == addr_resoure_ids[0]).FirstOrDefault();
                builder.AppendLine(strIs_4_1 + $"ReadClassName = \"{rObj.Code}\",");
            }
            var ggqW = childs.Where(it => it.Category == "GGQ-W").FirstOrDefault();
            if (ggqW != null)//公共区读
            {
                var addrExtJson = ggqW.ExtJson.ToObject<AddrExtJson>();
                builder.AppendLine(strIs_4_1 + $"WriteAddr = \"{addrExtJson.StartAddr}\",");

                var addrs = await _addressService.GetListByPlcId(ggqW.Id);
                var addr_resoure_ids = addrs.Select(it => it.ResourceId).ToList();
                if (addr_resoure_ids.Count != 1) throw Oops.Bah($"{op.Name}公共区（写）有且只有一个结构对象");
                var rObj = resourceList.Where(it => it.Id == addr_resoure_ids[0]).FirstOrDefault();
                builder.AppendLine(strIs_4_1 + $"WriteClassName = \"{rObj.Code}\",");
            }
            builder.AppendLine(strIs_4 + "},");//公共区
            //05 事件区
            var nIS_2 = nIS;
            builder.AppendLine(strIs_4 + $" EIs = new List<EventInfo>");
            builder.AppendLine(strIs_4 + "{");
            nIS_2 += 4;
            var strIs_4_2 = GetIndentSpaces(nIS_2);
            var sjqR = childs.Where(it => it.Category == "SJQ-R").FirstOrDefault();
            var sjqW = childs.Where(it => it.Category == "SJQ-W").FirstOrDefault();
            if (sjqR != null && sjqW != null)//事件区读
            {
                var addrsSJR = await _addressService.GetListByPlcId(sjqR.Id);
                var addrsSJW = await _addressService.GetListByPlcId(sjqW.Id);
                if(addrsSJR.Count != addrsSJW.Count) throw Oops.Bah($"{op.Name}事件区读写数量不匹配，读{addrsSJR.Count}条，写{addrsSJW.Count}条");
                
                int lenR = 0, lenW = 0;
                //地址换算
                var ejR = sjqR.ExtJson.ToObject<AddrExtJson>();
                var addrSplitR = ejR.StartAddr.Split('.');
                lenR = addrSplitR[1].ToInt();
                var ejW = sjqW.ExtJson.ToObject<AddrExtJson>();
                var addrSplitW = ejW.StartAddr.Split('.');
                lenW = addrSplitW[1].ToInt();

                for (int i = 0; i < addrsSJR.Count; i++)
                {
                    var sjR = resourceList.FirstOrDefault(it => it.Id == addrsSJR[i].ResourceId);
                    var sjW = resourceList.FirstOrDefault(it => it.Id == addrsSJW[i].ResourceId);
                   
                    //new EventInfo()
                    var nIS_3 = nIS_2;
                    var strIs_4_3 = GetIndentSpaces(nIS_3);
                    builder.AppendLine(strIs_4_3 + "new EventInfo()");
                    builder.AppendLine(strIs_4_3 + "{");
                    nIS_3 += 4;
                    var strIs_4_4 = GetIndentSpaces(nIS_3);
                    builder.AppendLine(strIs_4_4 + $"Idx = {i},");//事件索引
                    //builder.AppendLine(strIs_4_4 + $"Name = \"{sjR.Title}\",");//事件名称
                    //读
                    builder.AppendLine(strIs_4_4 + $"ReadAddr = \"{addrSplitR[0]}.{lenR}\",");//事件开始地址
                    builder.AppendLine(strIs_4_4 + $"ReadClassName = \"{sjR.Code}\",");//事件名称
                    //写
                    builder.AppendLine(strIs_4_4 + $"WriteAddr = \"{addrSplitW[0]}.{lenW}\",");//事件开始地址
                    builder.AppendLine(strIs_4_4 + $"WriteClassName = \"{sjW.Code}\",");//事件名称

                    lenR += await _plcResourceService.GetLenghAsync(sjR.Id);
                    lenW += await _plcResourceService.GetLenghAsync(sjW.Id);

                    builder.AppendLine(strIs_4_3 + "},");
                }
            }

            builder.AppendLine(strIs_4 + "}");
            builder.AppendLine(strIs_3 + "},");
        }
        builder.AppendLine(strIs_2 + "};");
        builder.AppendLine(strIs_1 + "}");
        builder.AppendLine("}");
        return builder.ToString();
    }

    private string GetIndentSpaces(int num)
    {
        if (num <= 0)
            return "";
        else
        {
            string str = "";
            for (int i = 0; i < num; i++)
            {
                str += " ";
            }
            return str;
        }
    }
    
    
}