using DevExpress.XtraBars.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPlc.Entry.Utils
{
    public static class RibbonControlUtil
    {
        public static void SetControlStyle(this RibbonControl ribbon)
        {
            ribbon.CommandLayout = CommandLayout.Classic;//简化
            ribbon.RibbonStyle = RibbonControlStyle.OfficeUniversal;//去蓝条
            ribbon.ShowToolbarCustomizeItem = false;//隐藏工具栏
            ribbon.ShowQatLocationSelector = false;
            ribbon.ShowPageHeadersMode = ShowPageHeadersMode.Hide;//隐藏page头
        }
    }
}
