namespace EasyPlc.Entry.ChrildrenForms;

partial class ScrewGunForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new Container();
        ComponentResourceManager resources = new ComponentResourceManager(typeof(ScrewGunForm));
        DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition1 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
        DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition2 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
        DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition1 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
        DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition2 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
        DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition3 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
        DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition4 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
        DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition5 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
        DevExpress.XtraEditors.TableLayout.TableSpan tableSpan1 = new DevExpress.XtraEditors.TableLayout.TableSpan();
        DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement1 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
        DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement2 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
        DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement3 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
        DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement4 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
        DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement5 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
        tileViewColumn1 = new DevExpress.XtraGrid.Columns.TileViewColumn();
        tileViewColumn3 = new DevExpress.XtraGrid.Columns.TileViewColumn();
        tileViewColumn2 = new DevExpress.XtraGrid.Columns.TileViewColumn();
        tileViewColumn4 = new DevExpress.XtraGrid.Columns.TileViewColumn();
        ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
        barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem9 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem10 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem11 = new DevExpress.XtraBars.BarButtonItem();
        ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
        ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
        gridControl1 = new DevExpress.XtraGrid.GridControl();
        tileView1 = new DevExpress.XtraGrid.Views.Tile.TileView();
        tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
        memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
        popupMenu1 = new DevExpress.XtraBars.PopupMenu(components);
        imageCollection1 = new DevExpress.Utils.ImageCollection(components);
        timer1 = new Timer(components);
        ((ISupportInitialize)ribbonControl1).BeginInit();
        ((ISupportInitialize)gridControl1).BeginInit();
        ((ISupportInitialize)tileView1).BeginInit();
        ((ISupportInitialize)tablePanel1).BeginInit();
        tablePanel1.SuspendLayout();
        ((ISupportInitialize)memoEdit1.Properties).BeginInit();
        ((ISupportInitialize)popupMenu1).BeginInit();
        ((ISupportInitialize)imageCollection1).BeginInit();
        SuspendLayout();
        // 
        // tileViewColumn1
        // 
        tileViewColumn1.Caption = "名称";
        tileViewColumn1.FieldName = "Name";
        tileViewColumn1.Name = "tileViewColumn1";
        tileViewColumn1.Visible = true;
        tileViewColumn1.VisibleIndex = 0;
        // 
        // tileViewColumn3
        // 
        tileViewColumn3.Caption = "排序";
        tileViewColumn3.FieldName = "SortCode";
        tileViewColumn3.Name = "tileViewColumn3";
        tileViewColumn3.Visible = true;
        tileViewColumn3.VisibleIndex = 2;
        // 
        // tileViewColumn2
        // 
        tileViewColumn2.Caption = "IP";
        tileViewColumn2.FieldName = "Ip";
        tileViewColumn2.Name = "tileViewColumn2";
        tileViewColumn2.Visible = true;
        tileViewColumn2.VisibleIndex = 1;
        // 
        // tileViewColumn4
        // 
        tileViewColumn4.Caption = "连接状态";
        tileViewColumn4.FieldName = "IsConn";
        tileViewColumn4.Name = "tileViewColumn4";
        tileViewColumn4.Visible = true;
        tileViewColumn4.VisibleIndex = 3;
        // 
        // ribbonControl1
        // 
        ribbonControl1.ExpandCollapseItem.Id = 0;
        ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbonControl1.ExpandCollapseItem, ribbonControl1.SearchEditItem, barButtonItem1, barButtonItem4, barButtonItem5, barButtonItem6, barButtonItem7, barButtonItem8, barButtonItem2, barButtonItem3, barButtonItem9, barButtonItem10, barButtonItem11 });
        ribbonControl1.Location = new Point(0, 0);
        ribbonControl1.MaxItemId = 14;
        ribbonControl1.Name = "ribbonControl1";
        ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage1 });
        ribbonControl1.Size = new Size(893, 170);
        // 
        // barButtonItem1
        // 
        barButtonItem1.Caption = "新增";
        barButtonItem1.Id = 1;
        barButtonItem1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem1.ImageOptions.SvgImage");
        barButtonItem1.Name = "barButtonItem1";
        barButtonItem1.ItemClick += barButtonItem1_ItemClick;
        // 
        // barButtonItem4
        // 
        barButtonItem4.Caption = "刷新";
        barButtonItem4.Id = 4;
        barButtonItem4.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem4.ImageOptions.SvgImage");
        barButtonItem4.Name = "barButtonItem4";
        barButtonItem4.ItemClick += barButtonItem4_ItemClick;
        // 
        // barButtonItem5
        // 
        barButtonItem5.Caption = "编辑RFID";
        barButtonItem5.Id = 5;
        barButtonItem5.ImageOptions.Image = (Image)resources.GetObject("barButtonItem5.ImageOptions.Image");
        barButtonItem5.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem5.ImageOptions.LargeImage");
        barButtonItem5.Name = "barButtonItem5";
        // 
        // barButtonItem6
        // 
        barButtonItem6.Caption = "读取RFID";
        barButtonItem6.Id = 6;
        barButtonItem6.ImageOptions.Image = (Image)resources.GetObject("barButtonItem6.ImageOptions.Image");
        barButtonItem6.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem6.ImageOptions.LargeImage");
        barButtonItem6.Name = "barButtonItem6";
        // 
        // barButtonItem7
        // 
        barButtonItem7.Caption = "写入RFID";
        barButtonItem7.Id = 7;
        barButtonItem7.ImageOptions.Image = (Image)resources.GetObject("barButtonItem7.ImageOptions.Image");
        barButtonItem7.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem7.ImageOptions.LargeImage");
        barButtonItem7.Name = "barButtonItem7";
        // 
        // barButtonItem8
        // 
        barButtonItem8.Caption = "查看日志";
        barButtonItem8.Id = 8;
        barButtonItem8.ImageOptions.Image = (Image)resources.GetObject("barButtonItem8.ImageOptions.Image");
        barButtonItem8.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem8.ImageOptions.LargeImage");
        barButtonItem8.Name = "barButtonItem8";
        // 
        // barButtonItem2
        // 
        barButtonItem2.Caption = "删除RFID";
        barButtonItem2.Id = 9;
        barButtonItem2.ImageOptions.Image = (Image)resources.GetObject("barButtonItem2.ImageOptions.Image");
        barButtonItem2.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem2.ImageOptions.LargeImage");
        barButtonItem2.Name = "barButtonItem2";
        // 
        // barButtonItem3
        // 
        barButtonItem3.Caption = "编辑螺丝枪";
        barButtonItem3.Id = 10;
        barButtonItem3.ImageOptions.Image = (Image)resources.GetObject("barButtonItem3.ImageOptions.Image");
        barButtonItem3.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem3.ImageOptions.LargeImage");
        barButtonItem3.Name = "barButtonItem3";
        barButtonItem3.ItemClick += barButtonItem3_ItemClick;
        // 
        // barButtonItem9
        // 
        barButtonItem9.Caption = "删除螺丝枪";
        barButtonItem9.Id = 11;
        barButtonItem9.ImageOptions.Image = (Image)resources.GetObject("barButtonItem9.ImageOptions.Image");
        barButtonItem9.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem9.ImageOptions.LargeImage");
        barButtonItem9.Name = "barButtonItem9";
        barButtonItem9.ItemClick += barButtonItem9_ItemClick;
        // 
        // barButtonItem10
        // 
        barButtonItem10.Caption = "切换程序号";
        barButtonItem10.Id = 12;
        barButtonItem10.ImageOptions.Image = (Image)resources.GetObject("barButtonItem10.ImageOptions.Image");
        barButtonItem10.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem10.ImageOptions.LargeImage");
        barButtonItem10.Name = "barButtonItem10";
        barButtonItem10.ItemClick += barButtonItem10_ItemClick;
        // 
        // barButtonItem11
        // 
        barButtonItem11.Caption = "查看日志";
        barButtonItem11.Id = 13;
        barButtonItem11.ImageOptions.Image = (Image)resources.GetObject("barButtonItem11.ImageOptions.Image");
        barButtonItem11.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem11.ImageOptions.LargeImage");
        barButtonItem11.Name = "barButtonItem11";
        barButtonItem11.ItemClick += barButtonItem11_ItemClick;
        // 
        // ribbonPage1
        // 
        ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup1 });
        ribbonPage1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("ribbonPage1.ImageOptions.SvgImage");
        ribbonPage1.Name = "ribbonPage1";
        ribbonPage1.Text = "螺丝枪编辑";
        // 
        // ribbonPageGroup1
        // 
        ribbonPageGroup1.ItemLinks.Add(barButtonItem1);
        ribbonPageGroup1.ItemLinks.Add(barButtonItem4);
        ribbonPageGroup1.Name = "ribbonPageGroup1";
        ribbonPageGroup1.Text = "操作项";
        // 
        // gridControl1
        // 
        tablePanel1.SetColumn(gridControl1, 0);
        gridControl1.Dock = DockStyle.Fill;
        gridControl1.Location = new Point(3, 3);
        gridControl1.MainView = tileView1;
        gridControl1.MenuManager = ribbonControl1;
        gridControl1.Name = "gridControl1";
        tablePanel1.SetRow(gridControl1, 0);
        gridControl1.Size = new Size(407, 480);
        gridControl1.TabIndex = 2;
        gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { tileView1 });
        // 
        // tileView1
        // 
        tileView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { tileViewColumn1, tileViewColumn2, tileViewColumn3, tileViewColumn4 });
        tileView1.GridControl = gridControl1;
        tileView1.Name = "tileView1";
        tileView1.OptionsTiles.ItemSize = new Size(198, 164);
        tileView1.OptionsTiles.Orientation = Orientation.Vertical;
        tileView1.OptionsTiles.RowCount = 0;
        tableColumnDefinition1.Length.Value = 120D;
        tableColumnDefinition2.Length.Value = 54D;
        tileView1.TileColumns.Add(tableColumnDefinition1);
        tileView1.TileColumns.Add(tableColumnDefinition2);
        tileView1.TileRows.Add(tableRowDefinition1);
        tileView1.TileRows.Add(tableRowDefinition2);
        tileView1.TileRows.Add(tableRowDefinition3);
        tileView1.TileRows.Add(tableRowDefinition4);
        tileView1.TileRows.Add(tableRowDefinition5);
        tableSpan1.ColumnSpan = 2;
        tableSpan1.RowIndex = 1;
        tableSpan1.RowSpan = 3;
        tileView1.TileSpans.Add(tableSpan1);
        tileViewItemElement1.Column = tileViewColumn1;
        tileViewItemElement1.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement1.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
        tileViewItemElement1.Text = "tileViewColumn1";
        tileViewItemElement1.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement2.Column = tileViewColumn3;
        tileViewItemElement2.ColumnIndex = 1;
        tileViewItemElement2.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement2.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
        tileViewItemElement2.Text = "tileViewColumn3";
        tileViewItemElement2.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement3.Column = tileViewColumn2;
        tileViewItemElement3.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement3.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
        tileViewItemElement3.RowIndex = 4;
        tileViewItemElement3.Text = "tileViewColumn2";
        tileViewItemElement3.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement4.ImageOptions.Image = (Image)resources.GetObject("resource.Image");
        tileViewItemElement4.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement4.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
        tileViewItemElement4.RowIndex = 1;
        tileViewItemElement4.Text = "";
        tileViewItemElement4.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement5.Column = tileViewColumn4;
        tileViewItemElement5.ColumnIndex = 1;
        tileViewItemElement5.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement5.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
        tileViewItemElement5.RowIndex = 4;
        tileViewItemElement5.Text = "tileViewColumn4";
        tileViewItemElement5.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
        tileViewItemElement5.TextVisible = false;
        tileView1.TileTemplate.Add(tileViewItemElement1);
        tileView1.TileTemplate.Add(tileViewItemElement2);
        tileView1.TileTemplate.Add(tileViewItemElement3);
        tileView1.TileTemplate.Add(tileViewItemElement4);
        tileView1.TileTemplate.Add(tileViewItemElement5);
        tileView1.ItemCustomize += tileView1_ItemCustomize;
        tileView1.SelectionChanged += tileView1_SelectionChanged;
        tileView1.MouseUp += tileView1_MouseUp;
        // 
        // tablePanel1
        // 
        tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 480F) });
        tablePanel1.Controls.Add(memoEdit1);
        tablePanel1.Controls.Add(gridControl1);
        tablePanel1.Dock = DockStyle.Fill;
        tablePanel1.Location = new Point(0, 170);
        tablePanel1.Name = "tablePanel1";
        tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F) });
        tablePanel1.Size = new Size(893, 486);
        tablePanel1.TabIndex = 3;
        // 
        // memoEdit1
        // 
        tablePanel1.SetColumn(memoEdit1, 1);
        memoEdit1.Dock = DockStyle.Fill;
        memoEdit1.Location = new Point(416, 3);
        memoEdit1.MenuManager = ribbonControl1;
        memoEdit1.Name = "memoEdit1";
        tablePanel1.SetRow(memoEdit1, 0);
        memoEdit1.Size = new Size(474, 480);
        memoEdit1.TabIndex = 3;
        // 
        // popupMenu1
        // 
        popupMenu1.ItemLinks.Add(barButtonItem3);
        popupMenu1.ItemLinks.Add(barButtonItem9);
        popupMenu1.ItemLinks.Add(barButtonItem10);
        popupMenu1.ItemLinks.Add(barButtonItem11);
        popupMenu1.Name = "popupMenu1";
        popupMenu1.Ribbon = ribbonControl1;
        // 
        // imageCollection1
        // 
        imageCollection1.ImageStream = (DevExpress.Utils.ImageCollectionStreamer)resources.GetObject("imageCollection1.ImageStream");
        imageCollection1.Images.SetKeyName(0, "iconsetsigns3_16x16.png");
        imageCollection1.Images.SetKeyName(1, "iconsetredtoblack4_16x16.png");
        // 
        // timer1
        // 
        timer1.Interval = 500;
        timer1.Tick += timer1_Tick;
        // 
        // ScrewGunForm
        // 
        AutoScaleDimensions = new SizeF(7F, 14F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(893, 656);
        Controls.Add(tablePanel1);
        Controls.Add(ribbonControl1);
        Name = "ScrewGunForm";
        Text = "康沃螺丝枪";
        Load += ScrewGunForm_Load;
        ((ISupportInitialize)ribbonControl1).EndInit();
        ((ISupportInitialize)gridControl1).EndInit();
        ((ISupportInitialize)tileView1).EndInit();
        ((ISupportInitialize)tablePanel1).EndInit();
        tablePanel1.ResumeLayout(false);
        ((ISupportInitialize)memoEdit1.Properties).EndInit();
        ((ISupportInitialize)popupMenu1).EndInit();
        ((ISupportInitialize)imageCollection1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
    private DevExpress.XtraBars.BarButtonItem barButtonItem1;
    private DevExpress.XtraBars.BarButtonItem barButtonItem4;
    private DevExpress.XtraBars.BarButtonItem barButtonItem5;
    private DevExpress.XtraBars.BarButtonItem barButtonItem6;
    private DevExpress.XtraBars.BarButtonItem barButtonItem7;
    private DevExpress.XtraBars.BarButtonItem barButtonItem8;
    private DevExpress.XtraBars.BarButtonItem barButtonItem2;
    private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
    private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
    private DevExpress.XtraGrid.GridControl gridControl1;
    private DevExpress.XtraGrid.Views.Tile.TileView tileView1;
    private DevExpress.Utils.Layout.TablePanel tablePanel1;
    private DevExpress.XtraGrid.Columns.TileViewColumn tileViewColumn1;
    private DevExpress.XtraGrid.Columns.TileViewColumn tileViewColumn2;
    private DevExpress.XtraGrid.Columns.TileViewColumn tileViewColumn3;
    private DevExpress.XtraBars.BarButtonItem barButtonItem3;
    private DevExpress.XtraBars.BarButtonItem barButtonItem9;
    private DevExpress.XtraBars.BarButtonItem barButtonItem10;
    private DevExpress.XtraBars.BarButtonItem barButtonItem11;
    private DevExpress.XtraBars.PopupMenu popupMenu1;
    private DevExpress.XtraEditors.MemoEdit memoEdit1;
    private DevExpress.XtraGrid.Columns.TileViewColumn tileViewColumn4;
    private DevExpress.Utils.ImageCollection imageCollection1;
    private Timer timer1;
}