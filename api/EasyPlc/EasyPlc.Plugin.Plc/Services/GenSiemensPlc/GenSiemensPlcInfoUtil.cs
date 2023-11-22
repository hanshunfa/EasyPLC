using EasyPlc.Plugin.Plc.Utils;
using Furion.ClayObject;
using Furion.FriendlyException;

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
            var ggqR = childs.Where(it => it.Category == "GGQ-R").FirstOrDefault();
            if (ggqR != null)//公共区读
            {
                var pi = new PublicInfo();
                plcInfo.PI = pi;

                var addrExtJson = ggqR.ExtJson.ToObject<AddrExtJson>();
                pi.ReadAddr = addrExtJson.StartAddr;
                var addrs = await _addressService.GetListByPlcId(ggqR.Id);
                var addr_resoure_ids = addrs.Select(it => it.ResourceId).ToList();
                if (addr_resoure_ids.Count != 1) throw Oops.Bah($"{op.Name}公共区（读）有且只有一个结构对象");
                var rObj = resourceList.Where(it => it.Id == addr_resoure_ids[0]).FirstOrDefault();
                pi.ReadClassName = rObj.Name;
                //每个对象对应一颗树
               var tree = await _plcResourceService.Tree(null, new PlcResourceTreeInput { ParentId = rObj.Id });
            }
        }
        return plcInofList;
    }
}
