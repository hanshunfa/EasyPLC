namespace EasyPlc.Entry.ChrildrenForms;

partial class SygoleRFIDForm
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
        ComponentResourceManager resources = new ComponentResourceManager(typeof(SygoleRFIDForm));
        DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition3 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
        DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition4 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
        DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition6 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
        DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition7 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
        DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition8 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
        DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition9 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
        DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition10 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
        DevExpress.XtraEditors.TableLayout.TableSpan tableSpan4 = new DevExpress.XtraEditors.TableLayout.TableSpan();
        DevExpress.XtraEditors.TableLayout.TableSpan tableSpan5 = new DevExpress.XtraEditors.TableLayout.TableSpan();
        DevExpress.XtraEditors.TableLayout.TableSpan tableSpan6 = new DevExpress.XtraEditors.TableLayout.TableSpan();
        DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement7 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
        DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement8 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
        DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement9 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
        DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement10 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
        DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement11 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
        DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement12 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
        tileViewColumn1 = new DevExpress.XtraGrid.Columns.TileViewColumn();
        tileViewColumn2 = new DevExpress.XtraGrid.Columns.TileViewColumn();
        tileViewColumn3 = new DevExpress.XtraGrid.Columns.TileViewColumn();
        tileViewColumn5 = new DevExpress.XtraGrid.Columns.TileViewColumn();
        tileViewColumn6 = new DevExpress.XtraGrid.Columns.TileViewColumn();
        tileViewColumn4 = new DevExpress.XtraGrid.Columns.TileViewColumn();
        ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
        barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
        ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
        ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
        gridControl1 = new DevExpress.XtraGrid.GridControl();
        tileView1 = new DevExpress.XtraGrid.Views.Tile.TileView();
        popupMenu1 = new DevExpress.XtraBars.PopupMenu(components);
        tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
        memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
        imageCollection1 = new DevExpress.Utils.ImageCollection(components);
        timer1 = new Timer(components);
        ((ISupportInitialize)ribbonControl1).BeginInit();
        ((ISupportInitialize)gridControl1).BeginInit();
        ((ISupportInitialize)tileView1).BeginInit();
        ((ISupportInitialize)popupMenu1).BeginInit();
        ((ISupportInitialize)tablePanel1).BeginInit();
        tablePanel1.SuspendLayout();
        ((ISupportInitialize)memoEdit1.Properties).BeginInit();
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
        // tileViewColumn2
        // 
        tileViewColumn2.Caption = "IP";
        tileViewColumn2.FieldName = "Ip";
        tileViewColumn2.Name = "tileViewColumn2";
        tileViewColumn2.Visible = true;
        tileViewColumn2.VisibleIndex = 1;
        // 
        // tileViewColumn3
        // 
        tileViewColumn3.Caption = "Port";
        tileViewColumn3.FieldName = "Port";
        tileViewColumn3.Name = "tileViewColumn3";
        tileViewColumn3.Visible = true;
        tileViewColumn3.VisibleIndex = 2;
        // 
        // tileViewColumn5
        // 
        tileViewColumn5.Caption = "排序";
        tileViewColumn5.FieldName = "SortCode";
        tileViewColumn5.Name = "tileViewColumn5";
        tileViewColumn5.Visible = true;
        tileViewColumn5.VisibleIndex = 4;
        // 
        // tileViewColumn6
        // 
        tileViewColumn6.Caption = "连接状态";
        tileViewColumn6.FieldName = "IsConn";
        tileViewColumn6.Name = "tileViewColumn6";
        tileViewColumn6.Visible = true;
        tileViewColumn6.VisibleIndex = 5;
        // 
        // tileViewColumn4
        // 
        tileViewColumn4.Caption = "ReaderID";
        tileViewColumn4.FieldName = "ReaderId";
        tileViewColumn4.Name = "tileViewColumn4";
        tileViewColumn4.Visible = true;
        tileViewColumn4.VisibleIndex = 3;
        // 
        // ribbonControl1
        // 
        ribbonControl1.ExpandCollapseItem.Id = 0;
        ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbonControl1.ExpandCollapseItem, ribbonControl1.SearchEditItem, barButtonItem1, barButtonItem4, barButtonItem5, barButtonItem6, barButtonItem7, barButtonItem8, barButtonItem2 });
        ribbonControl1.Location = new Point(0, 0);
        ribbonControl1.MaxItemId = 10;
        ribbonControl1.Name = "ribbonControl1";
        ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage1 });
        ribbonControl1.Size = new Size(761, 170);
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
        barButtonItem5.ItemClick += barButtonItem5_ItemClick;
        // 
        // barButtonItem6
        // 
        barButtonItem6.Caption = "读取RFID";
        barButtonItem6.Id = 6;
        barButtonItem6.ImageOptions.Image = (Image)resources.GetObject("barButtonItem6.ImageOptions.Image");
        barButtonItem6.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem6.ImageOptions.LargeImage");
        barButtonItem6.Name = "barButtonItem6";
        barButtonItem6.ItemClick += barButtonItem6_ItemClick;
        // 
        // barButtonItem7
        // 
        barButtonItem7.Caption = "写入RFID";
        barButtonItem7.Id = 7;
        barButtonItem7.ImageOptions.Image = (Image)resources.GetObject("barButtonItem7.ImageOptions.Image");
        barButtonItem7.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem7.ImageOptions.LargeImage");
        barButtonItem7.Name = "barButtonItem7";
        barButtonItem7.ItemClick += barButtonItem7_ItemClick;
        // 
        // barButtonItem8
        // 
        barButtonItem8.Caption = "查看日志";
        barButtonItem8.Id = 8;
        barButtonItem8.ImageOptions.Image = (Image)resources.GetObject("barButtonItem8.ImageOptions.Image");
        barButtonItem8.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem8.ImageOptions.LargeImage");
        barButtonItem8.Name = "barButtonItem8";
        barButtonItem8.ItemClick += barButtonItem8_ItemClick;
        // 
        // barButtonItem2
        // 
        barButtonItem2.Caption = "删除RFID";
        barButtonItem2.Id = 9;
        barButtonItem2.ImageOptions.Image = (Image)resources.GetObject("barButtonItem2.ImageOptions.Image");
        barButtonItem2.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem2.ImageOptions.LargeImage");
        barButtonItem2.Name = "barButtonItem2";
        barButtonItem2.ItemClick += barButtonItem2_ItemClick;
        // 
        // ribbonPage1
        // 
        ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup1 });
        ribbonPage1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("ribbonPage1.ImageOptions.SvgImage");
        ribbonPage1.Name = "ribbonPage1";
        ribbonPage1.Text = "思谷RFID编辑";
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
        gridControl1.Size = new Size(275, 388);
        gridControl1.TabIndex = 2;
        gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { tileView1 });
        // 
        // tileView1
        // 
        tileView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { tileViewColumn1, tileViewColumn2, tileViewColumn3, tileViewColumn4, tileViewColumn5, tileViewColumn6 });
        tileView1.GridControl = gridControl1;
        tileView1.Name = "tileView1";
        tileView1.OptionsTiles.ItemSize = new Size(186, 148);
        tileView1.OptionsTiles.Orientation = Orientation.Vertical;
        tableColumnDefinition3.Length.Value = 158D;
        tableColumnDefinition4.Length.Value = 66D;
        tileView1.TileColumns.Add(tableColumnDefinition3);
        tileView1.TileColumns.Add(tableColumnDefinition4);
        tileView1.TileRows.Add(tableRowDefinition6);
        tileView1.TileRows.Add(tableRowDefinition7);
        tileView1.TileRows.Add(tableRowDefinition8);
        tileView1.TileRows.Add(tableRowDefinition9);
        tileView1.TileRows.Add(tableRowDefinition10);
        tableSpan4.RowIndex = 1;
        tableSpan4.RowSpan = 3;
        tableSpan5.ColumnSpan = 2;
        tableSpan6.ColumnSpan = 2;
        tableSpan6.RowIndex = 4;
        tileView1.TileSpans.Add(tableSpan4);
        tileView1.TileSpans.Add(tableSpan5);
        tileView1.TileSpans.Add(tableSpan6);
        tileViewItemElement7.Column = tileViewColumn1;
        tileViewItemElement7.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement7.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
        tileViewItemElement7.Text = "tileViewColumn1";
        tileViewItemElement7.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement8.Column = tileViewColumn2;
        tileViewItemElement8.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement8.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
        tileViewItemElement8.RowIndex = 4;
        tileViewItemElement8.Text = "tileViewColumn2";
        tileViewItemElement8.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement9.Column = tileViewColumn3;
        tileViewItemElement9.ColumnIndex = 1;
        tileViewItemElement9.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement9.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
        tileViewItemElement9.RowIndex = 2;
        tileViewItemElement9.Text = "tileViewColumn3";
        tileViewItemElement9.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement10.ImageOptions.Image = (Image)resources.GetObject("resource.Image");
        tileViewItemElement10.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement10.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
        tileViewItemElement10.RowIndex = 1;
        tileViewItemElement10.Text = "";
        tileViewItemElement10.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement11.Column = tileViewColumn5;
        tileViewItemElement11.ColumnIndex = 1;
        tileViewItemElement11.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement11.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
        tileViewItemElement11.RowIndex = 1;
        tileViewItemElement11.Text = "tileViewColumn5";
        tileViewItemElement11.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement12.Column = tileViewColumn6;
        tileViewItemElement12.ColumnIndex = 1;
        tileViewItemElement12.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement12.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
        tileViewItemElement12.RowIndex = 3;
        tileViewItemElement12.Text = "tileViewColumn6";
        tileViewItemElement12.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement12.TextVisible = false;
        tileView1.TileTemplate.Add(tileViewItemElement7);
        tileView1.TileTemplate.Add(tileViewItemElement8);
        tileView1.TileTemplate.Add(tileViewItemElement9);
        tileView1.TileTemplate.Add(tileViewItemElement10);
        tileView1.TileTemplate.Add(tileViewItemElement11);
        tileView1.TileTemplate.Add(tileViewItemElement12);
        tileView1.ItemCustomize += tileView1_ItemCustomize;
        tileView1.SelectionChanged += tileView1_SelectionChanged;
        tileView1.MouseUp += tileView1_MouseUp;
        // 
        // popupMenu1
        // 
        popupMenu1.ItemLinks.Add(barButtonItem5);
        popupMenu1.ItemLinks.Add(barButtonItem2);
        popupMenu1.ItemLinks.Add(barButtonItem6);
        popupMenu1.ItemLinks.Add(barButtonItem7);
        popupMenu1.ItemLinks.Add(barButtonItem8);
        popupMenu1.Name = "popupMenu1";
        popupMenu1.Ribbon = ribbonControl1;
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
        tablePanel1.Size = new Size(761, 394);
        tablePanel1.TabIndex = 4;
        // 
        // memoEdit1
        // 
        tablePanel1.SetColumn(memoEdit1, 1);
        memoEdit1.Dock = DockStyle.Fill;
        memoEdit1.Location = new Point(284, 3);
        memoEdit1.MenuManager = ribbonControl1;
        memoEdit1.Name = "memoEdit1";
        tablePanel1.SetRow(memoEdit1, 0);
        memoEdit1.Size = new Size(474, 388);
        memoEdit1.TabIndex = 3;
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
        // SygoleRFIDForm
        // 
        AutoScaleDimensions = new SizeF(7F, 14F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(761, 564);
        Controls.Add(tablePanel1);
        Controls.Add(ribbonControl1);
        Name = "SygoleRFIDForm";
        Text = "思谷RFID";
        Load += SygoleRFIDForm_Load;
        ((ISupportInitialize)ribbonControl1).EndInit();
        ((ISupportInitialize)gridControl1).EndInit();
        ((ISupportInitialize)tileView1).EndInit();
        ((ISupportInitialize)popupMenu1).EndInit();
        ((ISupportInitialize)tablePanel1).EndInit();
        tablePanel1.ResumeLayout(false);
        ((ISupportInitialize)memoEdit1.Properties).EndInit();
        ((ISupportInitialize)imageCollection1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
    private DevExpress.XtraBars.BarButtonItem barButtonItem1;
    private DevExpress.XtraBars.BarButtonItem barButtonItem4;
    private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
    private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
    private DevExpress.XtraGrid.GridControl gridControl1;
    private DevExpress.XtraGrid.Views.Tile.TileView tileView1;
    private DevExpress.XtraGrid.Columns.TileViewColumn tileViewColumn1;
    private DevExpress.XtraGrid.Columns.TileViewColumn tileViewColumn2;
    private DevExpress.XtraGrid.Columns.TileViewColumn tileViewColumn3;
    private DevExpress.XtraGrid.Columns.TileViewColumn tileViewColumn4;
    private DevExpress.XtraGrid.Columns.TileViewColumn tileViewColumn5;
    private DevExpress.XtraBars.BarButtonItem barButtonItem5;
    private DevExpress.XtraBars.PopupMenu popupMenu1;
    private DevExpress.XtraBars.BarButtonItem barButtonItem6;
    private DevExpress.XtraBars.BarButtonItem barButtonItem7;
    private DevExpress.XtraBars.BarButtonItem barButtonItem8;
    private DevExpress.XtraBars.BarButtonItem barButtonItem2;
    private DevExpress.Utils.Layout.TablePanel tablePanel1;
    private DevExpress.XtraEditors.MemoEdit memoEdit1;
    private DevExpress.XtraGrid.Columns.TileViewColumn tileViewColumn6;
    private DevExpress.Utils.ImageCollection imageCollection1;
    private Timer timer1;
}