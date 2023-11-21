using DevExpress.XtraTreeList;

namespace EasyPlc.Entry.Utils;

public static class TreeListUtil
{
    public static void SetConfigTreeList(this TreeList treeList, bool isDrag = false, bool dragOutModes = false)
    {
        #region 设置列头、节点指示器面板、表格线样式

        treeList.OptionsView.ShowColumns = false;             //隐藏列标头
        treeList.OptionsView.ShowIndicator = false;           //隐藏节点指示器面板
        treeList.OptionsView.ShowHorzLines = false;           //隐藏水平表格线
        treeList.OptionsView.ShowVertLines = false;           //隐藏垂直表格线
        treeList.OptionsView.ShowIndentAsRowStyle = false;

        #endregion

        #region 初始禁用单元格选中，禁用整行选中

        treeList.OptionsView.FocusRectStyle = DrawFocusRectStyle.RowFocus;                               //设置显示焦点框
        treeList.OptionsSelection.EnableAppearanceFocusedCell = false;              //禁用单元格选中
        //treeList.OptionsSelection.EnableAppearanceFocusedRow = false;               //禁用正行选中

        #endregion

        #region 设置TreeList的展开折叠按钮样式和树线样式

        treeList.OptionsView.ShowButtons = true;                  //显示展开折叠按钮
        treeList.LookAndFeel.UseDefaultLookAndFeel = false;       //禁用默认外观与感觉
        treeList.LookAndFeel.UseWindowsXPTheme = true;            //使用WindowsXP主题
        treeList.TreeLineStyle = DevExpress.XtraTreeList.LineStyle.Percent50;     //设置树线的样式

        #endregion

        treeList.OptionsBehavior.Editable = false;
        if (isDrag)
        {
            treeList.OptionsDragAndDrop.DragNodesMode = DragNodesMode.Single;
            treeList.OptionsDragAndDrop.AcceptOuterNodes = dragOutModes;
            treeList.AllowDrop = true;
        }
        treeList.ExpandAll();//展开所有节点
        treeList.KeyFieldName = "Id";
        treeList.ParentFieldName = "ParentId";
    }
}
