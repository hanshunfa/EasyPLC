namespace EasyPlc.Plugin.Gen;

/// <summary>
/// <inheritdoc cref="IGenbasicService"/>
/// </summary>
public class GenBasicService : IGenbasicService
{
    private readonly ILogger<GenBasicService> _logger;
    private readonly IViewEngine _viewEngine;
    private readonly IPlcResourceService _plcResourceService;
    private readonly IPlcConfigService _plcConfigService;
    private readonly IAddressService _addressService;

    public GenBasicService(
        ILogger<GenBasicService> logger,
        IViewEngine viewEngine,
        IPlcResourceService plcResourceService,
        IPlcConfigService plcConfigService,
        IAddressService addressService
        )
    {
        _logger = logger;
        _viewEngine = viewEngine;
        _plcResourceService = plcResourceService;
        _plcConfigService = plcConfigService;
        _addressService = addressService;
    }

    /// <inheritdoc/>
    public async Task ExecGenObjDefinedPro()
    {
        //01 获取当前工程目录
        var backendPath = Path.Combine(new DirectoryInfo(App.WebHostEnvironment.ContentRootPath).Parent.Parent.Parent.Parent.FullName);//获取主工程目录
        //02 配置文件夹保存路径
        var filePath = backendPath.CombinePath(new string[] { "EasyPlc.Plugin.Plc", "ObjDefined", "PlcObj.cs" });
        //03 动态生成文件内容
        var resourceList = await _plcResourceService.GetListAsync();
        List<string> codes = new List<string>();
        HashSet<long> structIds = new HashSet<long>();//结构对象Ids
        foreach (var resource in resourceList)
        {
            if(resource.Category == "STRUCTDATA")
            {
                if(!codes.Contains(resource.Code))
                {
                    codes.Add(resource.Code);
                    structIds.Add(resource.Id);
                }
            }
        }
        var codeContent = await CreateGenObjDefinedCode(structIds);
        //04 写入到指定路径中
        File.WriteAllText(filePath, codeContent);
    }

    public async Task ExecGenSiemensPlcInfoPro()
    {
        //01 获取当前工程目录
        var backendPath = Path.Combine(new DirectoryInfo(App.WebHostEnvironment.ContentRootPath).Parent.Parent.Parent.Parent.FullName);//获取主工程目录
        //02 配置文件夹保存路径
        var filePath = backendPath.CombinePath(new string[] { 
            "EasyPlc.Plugin.Plc",
            "Services",
            "GenSiemensPlc",
            "GenSiemensPlcInfoUtil.cs"
        });
        //03 动态生成文件内容
        var codeContent = await CreateGenSiemensPlcInfoCode();
        //04 写入到指定路径中
        File.WriteAllText(filePath, codeContent);
    }

    private async Task<string> CreateGenObjDefinedCode(HashSet<long> structIds)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("namespace EasyPlc.Plugin.Plc;");
        builder.AppendLine();
        foreach (var structId in structIds)
        {
            var structResource = await _plcResourceService.GetResurceById(structId);
            int nIndentSpaces = 0;
            string strIs = "";
            builder.AppendLine("/// <summary>");
            builder.AppendLine("///" + structResource.Title);
            builder.AppendLine("/// <summary>");
            builder.AppendLine("public class " + structResource.Code);
            builder.AppendLine("{");
            nIndentSpaces += 4;
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
                        builder.AppendLine($"{strIs}[KstopaStructProperty(1)]");//特性
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine(strIs + "///" + childResource.Title);
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine($"{strIs}public bool {childResource.Code} " + "{get; set; }");
                    }
                    if (childResource.ValueType == "Int16")
                    {
                        builder.AppendLine($"{strIs}[KstopaStructProperty(2)]");//特性
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine(strIs + "///" + childResource.Title);
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine($"{strIs}public short {childResource.Code} " + "{get; set; }");
                    }
                    if (childResource.ValueType == "Int32")
                    {
                        builder.AppendLine($"{strIs}[KstopaStructProperty(4)]");//特性
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine(strIs + "///" + childResource.Title);
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine($"{strIs}public int {childResource.Code} " + "{get; set; }");
                    }
                    if (childResource.ValueType == "Float")
                    {
                        builder.AppendLine($"{strIs}[KstopaStructProperty(4)]");//特性
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine(strIs + "///" + childResource.Title);
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine($"{strIs}public float {childResource.Code} " + "{get; set; }");
                    }
                    if (childResource.ValueType == "String")
                    {
                        builder.AppendLine($"{strIs}[KstopaStructProperty({childResource.ValueLength + 2})]");//特性
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine(strIs + "///" + childResource.Title);
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine($"{strIs}public string {childResource.Code} " + "{get; set; }");
                    }
                    if (childResource.ValueType == "WString")
                    {
                        builder.AppendLine($"{strIs}[KstopaStructProperty({childResource.ValueLength * 2 + 4}, \"UNICODE\")]");//特性
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine(strIs + "///" + childResource.Title);
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine($"{strIs}public string {childResource.Code} " + "{get; set; }");
                    }
                }
                if (childResource.Category == "ARRDATA")
                {
                    //查看数组里面的对象
                    var arrChilds = await _plcResourceService.GetChildListById(childResource.Id, false, false);
                    if (arrChilds.Count != 1) throw Oops.Bah($"数组里面有且只有一个对象");
                    var ac = arrChilds[0];

                    //基础类型
                    if (ac.Category == "BASEDATA")
                    {
                        if (ac.ValueType == "Bool")
                        {
                            int nL = childResource.ValueLength / 8 + (childResource.ValueLength % 8 > 0 ? 1 : 0);
                            builder.AppendLine($"{strIs}[KstopaStructProperty({nL})]");//特性
                            builder.AppendLine(strIs + "/// <summary>");
                            builder.AppendLine(strIs + "///" + childResource.Title);
                            builder.AppendLine(strIs + "/// <summary>");
                            builder.AppendLine($"{strIs}public bool[] {childResource.Code} " + "{get; set; }");
                        }
                        if (ac.ValueType == "Int16")
                        {
                            int nL = childResource.ValueLength * 2;
                            builder.AppendLine($"{strIs}[KstopaStructProperty({nL})]");//特性
                            builder.AppendLine(strIs + "/// <summary>");
                            builder.AppendLine(strIs + "///" + childResource.Title);
                            builder.AppendLine(strIs + "/// <summary>");
                            builder.AppendLine($"{strIs}public short[] {childResource.Code} " + "{get; set; }");
                        }
                        if (ac.ValueType == "Int32")
                        {
                            int nL = childResource.ValueLength * 4;
                            builder.AppendLine($"{strIs}[KstopaStructProperty({nL})]");//特性
                            builder.AppendLine(strIs + "/// <summary>");
                            builder.AppendLine(strIs + "///" + childResource.Title);
                            builder.AppendLine(strIs + "/// <summary>");
                            builder.AppendLine($"{strIs}public int[] {childResource.Code} " + "{get; set; }");
                        }
                        if (ac.ValueType == "Float")
                        {

                            int nL = childResource.ValueLength * 4;
                            builder.AppendLine($"{strIs}[KstopaStructProperty({nL})]");//特性
                            builder.AppendLine(strIs + "/// <summary>");
                            builder.AppendLine(strIs + "///" + childResource.Title);
                            builder.AppendLine(strIs + "/// <summary>");
                            builder.AppendLine($"{strIs}public float[] {childResource.Code} " + "{get; set; }");
                        }
                    }
                    if (ac.Category == "STRUCTDATA")
                    {
                        int nL = childResource.ValueLength;
                        builder.AppendLine($"{strIs}[KstopaStruct({nL})]");//特性
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine(strIs + "///" + childResource.Title);
                        builder.AppendLine(strIs + "/// <summary>");
                        builder.AppendLine($"{strIs}public {ac.Code}[] {childResource.Code}" + "{get; set; }");
                    }
                }
                if (childResource.Category == "STRUCTDATA")
                {
                    int nL = 1;
                    builder.AppendLine($"{strIs}[KstopaStruct({nL})]");//特性
                    builder.AppendLine(strIs + "/// <summary>");
                    builder.AppendLine(strIs + "///" + childResource.Title);
                    builder.AppendLine(strIs + "/// <summary>");
                    builder.AppendLine($"{strIs}public {childResource.Code} {childResource.Code}" + "{get; set; }");
                }
            }
            builder.AppendLine("}");
        }
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

    public Task ExecGenPlcInfoPro()
    {
        throw new NotImplementedException();
    }

    private async Task<string> CreateGenSiemensPlcInfoCode()
    {
        var resourceList = await _plcResourceService.GetListAsync();

        StringBuilder builder = new StringBuilder();
        int nIndentSpaces = 0;
        builder.AppendLine("namespace EasyPlc.Plugin.Plc;");
        builder.AppendLine();
        builder.AppendLine("/// <summary>");
        builder.AppendLine("///" + "SiemensPLC信息设定工具类");
        builder.AppendLine("/// <summary>");
        builder.AppendLine("public class SiemensPLCInfoUtil : IGenSiemensPlcInfoUtil");
        builder.AppendLine("{");
        nIndentSpaces += 4;
        var strIs_1 = GetIndentSpaces(nIndentSpaces);

        //添加静态方法
        builder.AppendLine(strIs_1 + "/// <summary>");
        builder.AppendLine(strIs_1 + "///" + "SiemensPLC获取信息");
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
                    var sjR = resourceList.Where(it => it.Id == addrsSJR[i].ResourceId).FirstOrDefault();
                    var sjW = resourceList.Where(it => it.Id == addrsSJW[i].ResourceId).FirstOrDefault();
                   
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
}