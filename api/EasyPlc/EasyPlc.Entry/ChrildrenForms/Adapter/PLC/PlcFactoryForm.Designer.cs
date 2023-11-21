namespace EasyPlc.Entry.ChrildrenForms;

partial class PlcFactoryForm
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
        ComponentResourceManager resources = new ComponentResourceManager(typeof(PlcFactoryForm));
        DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition1 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
        DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition2 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
        DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition3 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
        DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition1 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
        DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition2 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
        DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition3 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
        DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition4 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
        DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition5 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
        DevExpress.XtraEditors.TableLayout.TableSpan tableSpan1 = new DevExpress.XtraEditors.TableLayout.TableSpan();
        DevExpress.XtraEditors.TableLayout.TableSpan tableSpan2 = new DevExpress.XtraEditors.TableLayout.TableSpan();
        DevExpress.XtraEditors.TableLayout.TableSpan tableSpan3 = new DevExpress.XtraEditors.TableLayout.TableSpan();
        DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement1 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
        DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement2 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
        DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement3 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
        DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement4 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
        DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement5 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
        tileViewColumn1 = new DevExpress.XtraGrid.Columns.TileViewColumn();
        tileViewColumn2 = new DevExpress.XtraGrid.Columns.TileViewColumn();
        tileViewColumn4 = new DevExpress.XtraGrid.Columns.TileViewColumn();
        tileViewColumn3 = new DevExpress.XtraGrid.Columns.TileViewColumn();
        repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
        imageCollection1 = new DevExpress.Utils.ImageCollection(components);
        ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
        barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
        ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
        ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
        gridControl1 = new DevExpress.XtraGrid.GridControl();
        tileView1 = new DevExpress.XtraGrid.Views.Tile.TileView();
        popupMenu1 = new DevExpress.XtraBars.PopupMenu(components);
        tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
        memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
        timer1 = new Timer(components);
        ((ISupportInitialize)repositoryItemButtonEdit1).BeginInit();
        ((ISupportInitialize)imageCollection1).BeginInit();
        ((ISupportInitialize)ribbonControl1).BeginInit();
        ((ISupportInitialize)gridControl1).BeginInit();
        ((ISupportInitialize)tileView1).BeginInit();
        ((ISupportInitialize)popupMenu1).BeginInit();
        ((ISupportInitialize)tablePanel1).BeginInit();
        tablePanel1.SuspendLayout();
        ((ISupportInitialize)memoEdit1.Properties).BeginInit();
        SuspendLayout();
        // 
        // tileViewColumn1
        // 
        tileViewColumn1.Caption = "PLC名称";
        tileViewColumn1.FieldName = "OP";
        tileViewColumn1.Name = "tileViewColumn1";
        tileViewColumn1.Visible = true;
        tileViewColumn1.VisibleIndex = 0;
        // 
        // tileViewColumn2
        // 
        tileViewColumn2.Caption = "IP地址";
        tileViewColumn2.FieldName = "IP";
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
        // tileViewColumn3
        // 
        tileViewColumn3.Caption = "CPU型号";
        tileViewColumn3.FieldName = "Version";
        tileViewColumn3.Name = "tileViewColumn3";
        tileViewColumn3.Visible = true;
        tileViewColumn3.VisibleIndex = 2;
        // 
        // repositoryItemButtonEdit1
        // 
        repositoryItemButtonEdit1.AutoHeight = false;
        repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton() });
        repositoryItemButtonEdit1.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
        repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
        // 
        // imageCollection1
        // 
        imageCollection1.ImageStream = (DevExpress.Utils.ImageCollectionStreamer)resources.GetObject("imageCollection1.ImageStream");
        imageCollection1.Images.SetKeyName(0, "iconsetsigns3_16x16.png");
        imageCollection1.Images.SetKeyName(1, "iconsetredtoblack4_16x16.png");
        // 
        // ribbonControl1
        // 
        ribbonControl1.ExpandCollapseItem.Id = 0;
        ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbonControl1.ExpandCollapseItem, ribbonControl1.SearchEditItem, barButtonItem1, barButtonItem2, barButtonItem3, barButtonItem5 });
        ribbonControl1.Location = new Point(0, 0);
        ribbonControl1.MaxItemId = 9;
        ribbonControl1.Name = "ribbonControl1";
        ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage1 });
        ribbonControl1.Size = new Size(835, 170);
        // 
        // barButtonItem1
        // 
        barButtonItem1.Caption = "连接";
        barButtonItem1.Id = 5;
        barButtonItem1.ImageOptions.Image = (Image)resources.GetObject("barButtonItem1.ImageOptions.Image");
        barButtonItem1.Name = "barButtonItem1";
        barButtonItem1.ItemClick += barButtonItem1_ItemClick;
        // 
        // barButtonItem2
        // 
        barButtonItem2.Caption = "断开";
        barButtonItem2.Id = 6;
        barButtonItem2.ImageOptions.Image = (Image)resources.GetObject("barButtonItem2.ImageOptions.Image");
        barButtonItem2.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem2.ImageOptions.LargeImage");
        barButtonItem2.Name = "barButtonItem2";
        barButtonItem2.ItemClick += barButtonItem2_ItemClick;
        // 
        // barButtonItem3
        // 
        barButtonItem3.Caption = "监控";
        barButtonItem3.Id = 7;
        barButtonItem3.ImageOptions.Image = (Image)resources.GetObject("barButtonItem3.ImageOptions.Image");
        barButtonItem3.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem3.ImageOptions.LargeImage");
        barButtonItem3.Name = "barButtonItem3";
        barButtonItem3.ItemClick += barButtonItem3_ItemClick;
        // 
        // barButtonItem5
        // 
        barButtonItem5.Caption = "全部连接";
        barButtonItem5.Id = 8;
        barButtonItem5.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem5.ImageOptions.SvgImage");
        barButtonItem5.Name = "barButtonItem5";
        barButtonItem5.ItemClick += barButtonItem5_ItemClick;
        // 
        // ribbonPage1
        // 
        ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup1 });
        ribbonPage1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("ribbonPage1.ImageOptions.SvgImage");
        ribbonPage1.Name = "ribbonPage1";
        ribbonPage1.Text = "PLC操作";
        // 
        // ribbonPageGroup1
        // 
        ribbonPageGroup1.ItemLinks.Add(barButtonItem5);
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
        gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemButtonEdit1 });
        tablePanel1.SetRow(gridControl1, 0);
        gridControl1.Size = new Size(412, 416);
        gridControl1.TabIndex = 3;
        gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { tileView1 });
        // 
        // tileView1
        // 
        tileView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { tileViewColumn1, tileViewColumn2, tileViewColumn3, tileViewColumn4 });
        tileView1.GridControl = gridControl1;
        tileView1.Name = "tileView1";
        tileView1.OptionsTiles.ItemSize = new Size(248, 146);
        tileView1.OptionsTiles.Orientation = Orientation.Vertical;
        tileView1.OptionsTiles.RowCount = 0;
        tileView1.TileColumns.Add(tableColumnDefinition1);
        tileView1.TileColumns.Add(tableColumnDefinition2);
        tileView1.TileColumns.Add(tableColumnDefinition3);
        tileView1.TileRows.Add(tableRowDefinition1);
        tileView1.TileRows.Add(tableRowDefinition2);
        tileView1.TileRows.Add(tableRowDefinition3);
        tileView1.TileRows.Add(tableRowDefinition4);
        tileView1.TileRows.Add(tableRowDefinition5);
        tableSpan1.ColumnSpan = 3;
        tableSpan2.ColumnSpan = 2;
        tableSpan2.RowIndex = 4;
        tableSpan3.ColumnSpan = 2;
        tableSpan3.RowIndex = 1;
        tableSpan3.RowSpan = 3;
        tileView1.TileSpans.Add(tableSpan1);
        tileView1.TileSpans.Add(tableSpan2);
        tileView1.TileSpans.Add(tableSpan3);
        tileViewItemElement1.Column = tileViewColumn1;
        tileViewItemElement1.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement1.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
        tileViewItemElement1.Text = "tileViewColumn1";
        tileViewItemElement1.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement2.Column = tileViewColumn2;
        tileViewItemElement2.ColumnIndex = 1;
        tileViewItemElement2.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement2.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
        tileViewItemElement2.RowIndex = 4;
        tileViewItemElement2.Text = "tileViewColumn2";
        tileViewItemElement2.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement3.Column = tileViewColumn4;
        tileViewItemElement3.ColumnIndex = 2;
        tileViewItemElement3.ImageOptions.Image = (Image)resources.GetObject("resource.Image");
        tileViewItemElement3.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement3.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
        tileViewItemElement3.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Right;
        tileViewItemElement3.RowIndex = 4;
        tileViewItemElement3.Text = "tileViewColumn4";
        tileViewItemElement3.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement4.Column = tileViewColumn3;
        tileViewItemElement4.ColumnIndex = 2;
        tileViewItemElement4.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement4.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
        tileViewItemElement4.RowIndex = 1;
        tileViewItemElement4.Text = "tileViewColumn3";
        tileViewItemElement4.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement5.ColumnIndex = 1;
        tileViewItemElement5.ImageOptions.Image = (Image)resources.GetObject("resource.Image1");
        tileViewItemElement5.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileViewItemElement5.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
        tileViewItemElement5.RowIndex = 1;
        tileViewItemElement5.Text = "";
        tileViewItemElement5.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
        tileView1.TileTemplate.Add(tileViewItemElement1);
        tileView1.TileTemplate.Add(tileViewItemElement2);
        tileView1.TileTemplate.Add(tileViewItemElement3);
        tileView1.TileTemplate.Add(tileViewItemElement4);
        tileView1.TileTemplate.Add(tileViewItemElement5);
        tileView1.ItemCustomize += tileView1_ItemCustomize;
        tileView1.SelectionChanged += tileView1_SelectionChanged;
        tileView1.MouseUp += tileView1_MouseUp;
        // 
        // popupMenu1
        // 
        popupMenu1.ItemLinks.Add(barButtonItem1);
        popupMenu1.ItemLinks.Add(barButtonItem2);
        popupMenu1.ItemLinks.Add(barButtonItem3);
        popupMenu1.Name = "popupMenu1";
        popupMenu1.Ribbon = ribbonControl1;
        // 
        // tablePanel1
        // 
        tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F) });
        tablePanel1.Controls.Add(memoEdit1);
        tablePanel1.Controls.Add(gridControl1);
        tablePanel1.Dock = DockStyle.Fill;
        tablePanel1.Location = new Point(0, 170);
        tablePanel1.Name = "tablePanel1";
        tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F) });
        tablePanel1.Size = new Size(835, 422);
        tablePanel1.TabIndex = 5;
        // 
        // memoEdit1
        // 
        tablePanel1.SetColumn(memoEdit1, 1);
        memoEdit1.Dock = DockStyle.Fill;
        memoEdit1.EditValue = "";
        memoEdit1.Location = new Point(421, 3);
        memoEdit1.MenuManager = ribbonControl1;
        memoEdit1.Name = "memoEdit1";
        tablePanel1.SetRow(memoEdit1, 0);
        memoEdit1.Size = new Size(412, 416);
        memoEdit1.TabIndex = 4;
        // 
        // timer1
        // 
        timer1.Interval = 1000;
        timer1.Tick += timer1_Tick;
        // 
        // PlcFactoryForm
        // 
        AutoScaleDimensions = new SizeF(7F, 14F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(835, 592);
        Controls.Add(tablePanel1);
        Controls.Add(ribbonControl1);
        Name = "PlcFactoryForm";
        Text = "PLC工厂";
        Load += PlcFactoryForm_Load;
        ((ISupportInitialize)repositoryItemButtonEdit1).EndInit();
        ((ISupportInitialize)imageCollection1).EndInit();
        ((ISupportInitialize)ribbonControl1).EndInit();
        ((ISupportInitialize)gridControl1).EndInit();
        ((ISupportInitialize)tileView1).EndInit();
        ((ISupportInitialize)popupMenu1).EndInit();
        ((ISupportInitialize)tablePanel1).EndInit();
        tablePanel1.ResumeLayout(false);
        ((ISupportInitialize)memoEdit1.Properties).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
    private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
    private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
    private DevExpress.XtraGrid.GridControl gridControl1;
    private DevExpress.Utils.ImageCollection imageCollection1;
    private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
    private DevExpress.XtraGrid.Views.Tile.TileView tileView1;
    private DevExpress.XtraGrid.Columns.TileViewColumn tileViewColumn1;
    private DevExpress.XtraGrid.Columns.TileViewColumn tileViewColumn2;
    private DevExpress.XtraGrid.Columns.TileViewColumn tileViewColumn3;
    private DevExpress.XtraGrid.Columns.TileViewColumn tileViewColumn4;
    private DevExpress.XtraBars.PopupMenu popupMenu1;
    private DevExpress.XtraBars.BarButtonItem barButtonItem1;
    private DevExpress.XtraBars.BarButtonItem barButtonItem2;
    private DevExpress.XtraBars.BarButtonItem barButtonItem3;
    private DevExpress.Utils.Layout.TablePanel tablePanel1;
    private DevExpress.XtraEditors.MemoEdit memoEdit1;
    private Timer timer1;
    private DevExpress.XtraBars.BarButtonItem barButtonItem5;
}