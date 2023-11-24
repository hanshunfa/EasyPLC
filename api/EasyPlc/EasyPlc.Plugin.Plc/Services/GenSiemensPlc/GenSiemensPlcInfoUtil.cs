using Furion.FriendlyException;
using NetTaste;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace EasyPlc.Plugin.Plc;

/// <summary>
///SiemensPLC信息设定工具类
/// <summary>
public class SiemensPLCInfoUtil : IGenSiemensPlcInfoUtil
{
    private readonly IPlcResourceService _plcResourceService;
    private readonly IPlcConfigService _plcConfigService;
    private readonly IAddressService _addressService;
    public SiemensPLCInfoUtil(
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
    public async Task<List<SiemensPlcInfo>> GenSiemensPLCInfoList()
    {
        var plcInofList = new List<SiemensPlcInfo>();
        //01 查询多少个PLC对象
        var opList = (await _plcConfigService.GetListBySortCodeAsync()).Where(it => it.Category == "PLC").ToList();
        var resourceList = await _plcResourceService.GetListAsync();
        foreach (var op in opList)
        {
            var plcExtJson = op.ExtJson.ToObject<PlcExtJson>();
            var plcInfo = new SiemensPlcInfo()
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
                pi.ReadAddr = addrExtJson.StartAddr;
                var addrs = await _addressService.GetListByPlcId(ggqR.Id);
                var addr_resoure_ids = addrs.Select(it => it.ResourceId).ToList();
                if (addr_resoure_ids.Count != 1) throw Oops.Bah($"{op.Name}公共区（读）有且只有一个结构对象");
                var rObj = resourceList.Where(it => it.Id == addr_resoure_ids[0]).FirstOrDefault();
                pi.ReadClassName = rObj.Name;
                //每个对象对应一颗树
                var tree = await _plcResourceService.Tree(null, new PlcResourceTreeInput { ParentId = rObj.Id }, true);
                pi.ObjR = tree;
                var lr = await _plcResourceService.GetLenghAsync(ggqR.Id);
                pi.ReadLen = (ushort)lr;
            }
            var ggqW = childs.Where(it => it.Category == "GGQ-W").FirstOrDefault();
            if (ggqW != null)//公共区读
            {
                var addrExtJson = ggqW.ExtJson.ToObject<AddrExtJson>();
                pi.WriteAddr = addrExtJson.StartAddr;
                var addrs = await _addressService.GetListByPlcId(ggqW.Id);
                var addr_resoure_ids = addrs.Select(it => it.ResourceId).ToList();
                if (addr_resoure_ids.Count != 1) throw Oops.Bah($"{op.Name}公共区（写）有且只有一个结构对象");
                var wObj = resourceList.Where(it => it.Id == addr_resoure_ids[0]).FirstOrDefault();
                pi.WriteClassName = wObj.Name;
                //每个对象对应一颗树
                var tree = await _plcResourceService.Tree(null, new PlcResourceTreeInput { ParentId = wObj.Id }, true);
                pi.ObjW = tree;
                var lw = await _plcResourceService.GetLenghAsync(ggqW.Id);
                pi.WriteLen = (ushort)lw;
                pi.IsReset = true;
            }
            //事件区
            var eis = new List<EventInfo>();
            plcInfo.EIs = eis;
            var sjqR = childs.Where(it => it.Category == "SJQ-R").FirstOrDefault();
            var sjqW = childs.Where(it => it.Category == "SJQ-W").FirstOrDefault();
            if (sjqR == null || sjqW == null) throw Oops.Bah($"{op.Name}事件区存在未定义");
            var addrsSJR = await _addressService.GetListByPlcId(sjqR.Id);
            var addrsSJW = await _addressService.GetListByPlcId(sjqW.Id);
            if (addrsSJR.Count != addrsSJW.Count) throw Oops.Bah($"{op.Name}事件区读写数量不匹配，读{addrsSJR.Count}条，写{addrsSJW.Count}条");
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
                var ei = new EventInfo();
                eis.Add(ei);

                var sjR = resourceList.Where(it => it.Id == addrsSJR[i].ResourceId).FirstOrDefault();
                var sjW = resourceList.Where(it => it.Id == addrsSJW[i].ResourceId).FirstOrDefault();
                ei.Idx = i;
                ei.ReadAddr = $"{addrSplitR[0]}.{lenR}";
                ei.ReadClassName = sjR.Code;
                ei.WriteAddr = $"{addrSplitW[0]}.{lenW}";
                ei.WriteClassName = sjW.Code;
                //对象tree
                var treeR = await _plcResourceService.Tree(null, new PlcResourceTreeInput { ParentId = sjR.Id });
                ei.ObjR = treeR;
                var treeW = await _plcResourceService.Tree(null, new PlcResourceTreeInput { ParentId = sjW.Id });
                ei.ObjW = treeW;

                //地址偏移计算
                
                var lr = await _plcResourceService.GetLenghAsync(sjR.Id);
                lenR += lr;
                var lw = await _plcResourceService.GetLenghAsync(sjW.Id);
                lenW += lw;

                ei.ReadLen = (ushort)lr;
                ei.WriteLen = (ushort)lw;
                ei.IsReset = true;
            }
        }
        return plcInofList;
    }
}
