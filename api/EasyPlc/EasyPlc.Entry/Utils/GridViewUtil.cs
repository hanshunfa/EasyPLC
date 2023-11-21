
using DevExpress.XtraGrid.Views.Grid;

namespace EasyPlc.Entry.Utils;

public static class GridViewUtil
{
    public static void SetConfigGridView(this GridView gridView)
    {
        //设置成一次选择一行，并且不能被编辑
        gridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
        gridView.OptionsBehavior.Editable = false;
        gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
        gridView.OptionsView.ShowGroupPanel = false;                                              //隐藏最上面的GroupPanel
        gridView.OptionsView.ShowIndicator = true;
        //默认选中第一行
        gridView.OptionsSelection.EnableAppearanceHideSelection = false;
        //显示行号
        gridView.IndicatorWidth = 42;

        gridView.OptionsView.EnableAppearanceEvenRow = true;//启用偶数行背景色

        gridView.CustomDrawRowIndicator += (obj, e) => {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        };
    }
}
