namespace EasyPlc.Entry.ChrildrenForms;

partial class OrderForm
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
        ComponentResourceManager resources = new ComponentResourceManager(typeof(OrderForm));
        ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
        barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem9 = new DevExpress.XtraBars.BarButtonItem();
        barButtonItem10 = new DevExpress.XtraBars.BarButtonItem();
        ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
        ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
        gridControl1 = new DevExpress.XtraGrid.GridControl();
        gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
        gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
        gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
        gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
        gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
        gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
        gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
        gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
        gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
        gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
        gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
        repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
        imageCollection1 = new DevExpress.Utils.ImageCollection(components);
        gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
        gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
        gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
        tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
        paginationControl1 = new PaginationControl();
        popupMenu1 = new DevExpress.XtraBars.PopupMenu(components);
        ((ISupportInitialize)ribbonControl1).BeginInit();
        ((ISupportInitialize)gridControl1).BeginInit();
        ((ISupportInitialize)gridView1).BeginInit();
        ((ISupportInitialize)repositoryItemImageComboBox1).BeginInit();
        ((ISupportInitialize)imageCollection1).BeginInit();
        ((ISupportInitialize)tablePanel1).BeginInit();
        tablePanel1.SuspendLayout();
        ((ISupportInitialize)popupMenu1).BeginInit();
        SuspendLayout();
        // 
        // ribbonControl1
        // 
        ribbonControl1.ExpandCollapseItem.Id = 0;
        ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbonControl1.ExpandCollapseItem, ribbonControl1.SearchEditItem, barButtonItem1, barButtonItem2, barButtonItem3, barButtonItem4, barButtonItem5, barButtonItem6, barButtonItem7, barButtonItem8, barButtonItem9, barButtonItem10 });
        ribbonControl1.Location = new Point(0, 0);
        ribbonControl1.MaxItemId = 11;
        ribbonControl1.Name = "ribbonControl1";
        ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage1 });
        ribbonControl1.Size = new Size(772, 170);
        // 
        // barButtonItem1
        // 
        barButtonItem1.Caption = "新增";
        barButtonItem1.Id = 1;
        barButtonItem1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem1.ImageOptions.SvgImage");
        barButtonItem1.Name = "barButtonItem1";
        barButtonItem1.ItemClick += barButtonItem1_ItemClick;
        // 
        // barButtonItem2
        // 
        barButtonItem2.Caption = "编辑";
        barButtonItem2.Id = 2;
        barButtonItem2.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem2.ImageOptions.SvgImage");
        barButtonItem2.Name = "barButtonItem2";
        barButtonItem2.ItemClick += barButtonItem2_ItemClick;
        // 
        // barButtonItem3
        // 
        barButtonItem3.Caption = "删除";
        barButtonItem3.Id = 3;
        barButtonItem3.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem3.ImageOptions.SvgImage");
        barButtonItem3.Name = "barButtonItem3";
        barButtonItem3.ItemClick += barButtonItem3_ItemClick;
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
        barButtonItem5.Caption = "编辑工单";
        barButtonItem5.Id = 5;
        barButtonItem5.ImageOptions.Image = (Image)resources.GetObject("barButtonItem5.ImageOptions.Image");
        barButtonItem5.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem5.ImageOptions.LargeImage");
        barButtonItem5.Name = "barButtonItem5";
        barButtonItem5.ItemClick += barButtonItem5_ItemClick;
        // 
        // barButtonItem6
        // 
        barButtonItem6.Caption = "删除工单";
        barButtonItem6.Id = 6;
        barButtonItem6.ImageOptions.Image = (Image)resources.GetObject("barButtonItem6.ImageOptions.Image");
        barButtonItem6.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem6.ImageOptions.LargeImage");
        barButtonItem6.Name = "barButtonItem6";
        barButtonItem6.ItemClick += barButtonItem6_ItemClick;
        // 
        // barButtonItem7
        // 
        barButtonItem7.Caption = "开始";
        barButtonItem7.Id = 7;
        barButtonItem7.ImageOptions.Image = (Image)resources.GetObject("barButtonItem7.ImageOptions.Image");
        barButtonItem7.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem7.ImageOptions.LargeImage");
        barButtonItem7.Name = "barButtonItem7";
        barButtonItem7.ItemClick += barButtonItem7_ItemClick;
        // 
        // barButtonItem8
        // 
        barButtonItem8.Caption = "清料";
        barButtonItem8.Id = 8;
        barButtonItem8.ImageOptions.Image = (Image)resources.GetObject("barButtonItem8.ImageOptions.Image");
        barButtonItem8.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem8.ImageOptions.LargeImage");
        barButtonItem8.Name = "barButtonItem8";
        barButtonItem8.ItemClick += barButtonItem8_ItemClick;
        // 
        // barButtonItem9
        // 
        barButtonItem9.Caption = "人工确认清料完成";
        barButtonItem9.Id = 9;
        barButtonItem9.ImageOptions.Image = (Image)resources.GetObject("barButtonItem9.ImageOptions.Image");
        barButtonItem9.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem9.ImageOptions.LargeImage");
        barButtonItem9.Name = "barButtonItem9";
        barButtonItem9.ItemClick += barButtonItem9_ItemClick;
        // 
        // barButtonItem10
        // 
        barButtonItem10.Caption = "生成待产工单";
        barButtonItem10.Id = 10;
        barButtonItem10.ImageOptions.Image = (Image)resources.GetObject("barButtonItem10.ImageOptions.Image");
        barButtonItem10.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem10.ImageOptions.LargeImage");
        barButtonItem10.Name = "barButtonItem10";
        barButtonItem10.ItemClick += barButtonItem10_ItemClick;
        // 
        // ribbonPage1
        // 
        ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup1 });
        ribbonPage1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("ribbonPage1.ImageOptions.SvgImage");
        ribbonPage1.Name = "ribbonPage1";
        ribbonPage1.Text = "工单编辑";
        // 
        // ribbonPageGroup1
        // 
        ribbonPageGroup1.ItemLinks.Add(barButtonItem1);
        ribbonPageGroup1.ItemLinks.Add(barButtonItem2);
        ribbonPageGroup1.ItemLinks.Add(barButtonItem3);
        ribbonPageGroup1.ItemLinks.Add(barButtonItem4);
        ribbonPageGroup1.Name = "ribbonPageGroup1";
        ribbonPageGroup1.Text = "操作项";
        // 
        // gridControl1
        // 
        tablePanel1.SetColumn(gridControl1, 0);
        gridControl1.Dock = DockStyle.Fill;
        gridControl1.Location = new Point(3, 3);
        gridControl1.MainView = gridView1;
        gridControl1.MenuManager = ribbonControl1;
        gridControl1.Name = "gridControl1";
        gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemImageComboBox1 });
        tablePanel1.SetRow(gridControl1, 0);
        gridControl1.Size = new Size(766, 390);
        gridControl1.TabIndex = 2;
        gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
        // 
        // gridView1
        // 
        gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn10, gridColumn1, gridColumn2, gridColumn4, gridColumn3, gridColumn5, gridColumn6, gridColumn7, gridColumn8, gridColumn9, gridColumn11, gridColumn12, gridColumn13 });
        gridView1.GridControl = gridControl1;
        gridView1.Name = "gridView1";
        gridView1.RowCellStyle += gridView1_RowCellStyle;
        gridView1.RowStyle += gridView1_RowStyle;
        gridView1.SelectionChanged += gridView1_SelectionChanged;
        gridView1.MouseUp += gridView1_MouseUp;
        // 
        // gridColumn10
        // 
        gridColumn10.Caption = "Id";
        gridColumn10.FieldName = "Id";
        gridColumn10.Name = "gridColumn10";
        // 
        // gridColumn1
        // 
        gridColumn1.Caption = "车间订单号";
        gridColumn1.FieldName = "Sono";
        gridColumn1.Name = "gridColumn1";
        gridColumn1.Visible = true;
        gridColumn1.VisibleIndex = 1;
        // 
        // gridColumn2
        // 
        gridColumn2.Caption = "批次";
        gridColumn2.FieldName = "Batch";
        gridColumn2.Name = "gridColumn2";
        gridColumn2.Visible = true;
        gridColumn2.VisibleIndex = 4;
        // 
        // gridColumn4
        // 
        gridColumn4.Caption = "计划数量";
        gridColumn4.FieldName = "PlanQty";
        gridColumn4.Name = "gridColumn4";
        gridColumn4.Visible = true;
        gridColumn4.VisibleIndex = 5;
        // 
        // gridColumn3
        // 
        gridColumn3.Caption = "工艺路线";
        gridColumn3.FieldName = "FlowName";
        gridColumn3.Name = "gridColumn3";
        gridColumn3.Visible = true;
        gridColumn3.VisibleIndex = 3;
        // 
        // gridColumn5
        // 
        gridColumn5.Caption = "在线加工数量";
        gridColumn5.FieldName = "OnlineQty";
        gridColumn5.Name = "gridColumn5";
        gridColumn5.Visible = true;
        gridColumn5.VisibleIndex = 7;
        // 
        // gridColumn6
        // 
        gridColumn6.Caption = "合格数量";
        gridColumn6.FieldName = "OkQty";
        gridColumn6.Name = "gridColumn6";
        gridColumn6.Visible = true;
        gridColumn6.VisibleIndex = 8;
        // 
        // gridColumn7
        // 
        gridColumn7.Caption = "待返修数量";
        gridColumn7.FieldName = "RepairQty";
        gridColumn7.Name = "gridColumn7";
        gridColumn7.Visible = true;
        gridColumn7.VisibleIndex = 9;
        // 
        // gridColumn8
        // 
        gridColumn8.Caption = "报废数量";
        gridColumn8.FieldName = "ScrapQty";
        gridColumn8.Name = "gridColumn8";
        gridColumn8.Visible = true;
        gridColumn8.VisibleIndex = 10;
        // 
        // gridColumn9
        // 
        gridColumn9.Caption = "状态";
        gridColumn9.ColumnEdit = repositoryItemImageComboBox1;
        gridColumn9.FieldName = "Status";
        gridColumn9.Name = "gridColumn9";
        gridColumn9.Visible = true;
        gridColumn9.VisibleIndex = 2;
        // 
        // repositoryItemImageComboBox1
        // 
        repositoryItemImageComboBox1.AutoHeight = false;
        repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
        repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] { new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "AWAIT", 0), new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "READY", 1), new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "RUN", 2), new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "STOP", 3), new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "CLEAR", 4), new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "FINISHED", 5) });
        repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
        repositoryItemImageComboBox1.SmallImages = imageCollection1;
        // 
        // imageCollection1
        // 
        imageCollection1.ImageStream = (DevExpress.Utils.ImageCollectionStreamer)resources.GetObject("imageCollection1.ImageStream");
        imageCollection1.Images.SetKeyName(0, "iconsetquarters5_16x16.png");
        imageCollection1.Images.SetKeyName(1, "finishmerge_16x16.png");
        imageCollection1.Images.SetKeyName(2, "iconsetsigns3_16x16.png");
        imageCollection1.Images.SetKeyName(3, "remove_16x16.png");
        imageCollection1.Images.SetKeyName(4, "sortasc_16x16.png");
        imageCollection1.Images.SetKeyName(5, "iconsetsymbols3_16x16.png");
        // 
        // gridColumn11
        // 
        gridColumn11.Caption = "创建时间";
        gridColumn11.DisplayFormat.FormatString = "G";
        gridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
        gridColumn11.FieldName = "CreateTime";
        gridColumn11.Name = "gridColumn11";
        gridColumn11.Visible = true;
        gridColumn11.VisibleIndex = 11;
        // 
        // gridColumn12
        // 
        gridColumn12.Caption = "投产数量";
        gridColumn12.FieldName = "PutQty";
        gridColumn12.Name = "gridColumn12";
        gridColumn12.Visible = true;
        gridColumn12.VisibleIndex = 6;
        // 
        // gridColumn13
        // 
        gridColumn13.Caption = "工单类型";
        gridColumn13.FieldName = "OrderType";
        gridColumn13.Name = "gridColumn13";
        gridColumn13.Visible = true;
        gridColumn13.VisibleIndex = 0;
        // 
        // tablePanel1
        // 
        tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5F) });
        tablePanel1.Controls.Add(paginationControl1);
        tablePanel1.Controls.Add(gridControl1);
        tablePanel1.Dock = DockStyle.Fill;
        tablePanel1.Location = new Point(0, 170);
        tablePanel1.Name = "tablePanel1";
        tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 26F) });
        tablePanel1.Size = new Size(772, 428);
        tablePanel1.TabIndex = 4;
        // 
        // paginationControl1
        // 
        tablePanel1.SetColumn(paginationControl1, 0);
        paginationControl1.Location = new Point(3, 399);
        paginationControl1.Name = "paginationControl1";
        tablePanel1.SetRow(paginationControl1, 1);
        paginationControl1.Size = new Size(766, 26);
        paginationControl1.TabIndex = 3;
        // 
        // popupMenu1
        // 
        popupMenu1.ItemLinks.Add(barButtonItem5);
        popupMenu1.ItemLinks.Add(barButtonItem6);
        popupMenu1.ItemLinks.Add(barButtonItem10);
        popupMenu1.ItemLinks.Add(barButtonItem7);
        popupMenu1.ItemLinks.Add(barButtonItem8);
        popupMenu1.ItemLinks.Add(barButtonItem9);
        popupMenu1.Name = "popupMenu1";
        popupMenu1.Ribbon = ribbonControl1;
        // 
        // OrderForm
        // 
        AutoScaleDimensions = new SizeF(7F, 14F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(772, 598);
        Controls.Add(tablePanel1);
        Controls.Add(ribbonControl1);
        Name = "OrderForm";
        Text = "工单管理";
        Load += OrderForm_Load;
        ((ISupportInitialize)ribbonControl1).EndInit();
        ((ISupportInitialize)gridControl1).EndInit();
        ((ISupportInitialize)gridView1).EndInit();
        ((ISupportInitialize)repositoryItemImageComboBox1).EndInit();
        ((ISupportInitialize)imageCollection1).EndInit();
        ((ISupportInitialize)tablePanel1).EndInit();
        tablePanel1.ResumeLayout(false);
        ((ISupportInitialize)popupMenu1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
    private DevExpress.XtraBars.BarButtonItem barButtonItem1;
    private DevExpress.XtraBars.BarButtonItem barButtonItem2;
    private DevExpress.XtraBars.BarButtonItem barButtonItem3;
    private DevExpress.XtraBars.BarButtonItem barButtonItem4;
    private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
    private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
    private DevExpress.XtraGrid.GridControl gridControl1;
    private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
    private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
    private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
    private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
    private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
    private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
    private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
    private DevExpress.Utils.Layout.TablePanel tablePanel1;
    private PaginationControl paginationControl1;
    private DevExpress.XtraBars.BarButtonItem barButtonItem5;
    private DevExpress.XtraBars.BarButtonItem barButtonItem6;
    private DevExpress.XtraBars.PopupMenu popupMenu1;
    private DevExpress.XtraBars.BarButtonItem barButtonItem7;
    private DevExpress.XtraBars.BarButtonItem barButtonItem8;
    private DevExpress.XtraBars.BarButtonItem barButtonItem9;
    private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
    private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
    private DevExpress.Utils.ImageCollection imageCollection1;
    private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
    private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    private DevExpress.XtraBars.BarButtonItem barButtonItem10;
    private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
    private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
}