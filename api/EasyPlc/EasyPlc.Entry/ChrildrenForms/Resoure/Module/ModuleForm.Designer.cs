namespace EasyPlc.Entry.ChrildrenForms;

partial class ModuleForm
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
        ComponentResourceManager resources = new ComponentResourceManager(typeof(ModuleForm));
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
        tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
        paginationControl1 = new PaginationControl();
        gridControl1 = new DevExpress.XtraGrid.GridControl();
        gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
        gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
        gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
        gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
        repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
        ((ISupportInitialize)ribbonControl1).BeginInit();
        ((ISupportInitialize)tablePanel1).BeginInit();
        tablePanel1.SuspendLayout();
        ((ISupportInitialize)gridControl1).BeginInit();
        ((ISupportInitialize)gridView1).BeginInit();
        ((ISupportInitialize)repositoryItemImageComboBox1).BeginInit();
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
        ribbonControl1.Size = new Size(727, 170);
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
        // 
        // barButtonItem6
        // 
        barButtonItem6.Caption = "删除工单";
        barButtonItem6.Id = 6;
        barButtonItem6.ImageOptions.Image = (Image)resources.GetObject("barButtonItem6.ImageOptions.Image");
        barButtonItem6.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem6.ImageOptions.LargeImage");
        barButtonItem6.Name = "barButtonItem6";
        // 
        // barButtonItem7
        // 
        barButtonItem7.Caption = "开始";
        barButtonItem7.Id = 7;
        barButtonItem7.ImageOptions.Image = (Image)resources.GetObject("barButtonItem7.ImageOptions.Image");
        barButtonItem7.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem7.ImageOptions.LargeImage");
        barButtonItem7.Name = "barButtonItem7";
        // 
        // barButtonItem8
        // 
        barButtonItem8.Caption = "清料";
        barButtonItem8.Id = 8;
        barButtonItem8.ImageOptions.Image = (Image)resources.GetObject("barButtonItem8.ImageOptions.Image");
        barButtonItem8.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem8.ImageOptions.LargeImage");
        barButtonItem8.Name = "barButtonItem8";
        // 
        // barButtonItem9
        // 
        barButtonItem9.Caption = "人工确认清料完成";
        barButtonItem9.Id = 9;
        barButtonItem9.ImageOptions.Image = (Image)resources.GetObject("barButtonItem9.ImageOptions.Image");
        barButtonItem9.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem9.ImageOptions.LargeImage");
        barButtonItem9.Name = "barButtonItem9";
        // 
        // barButtonItem10
        // 
        barButtonItem10.Caption = "生成待产工单";
        barButtonItem10.Id = 10;
        barButtonItem10.ImageOptions.Image = (Image)resources.GetObject("barButtonItem10.ImageOptions.Image");
        barButtonItem10.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem10.ImageOptions.LargeImage");
        barButtonItem10.Name = "barButtonItem10";
        // 
        // ribbonPage1
        // 
        ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup1 });
        ribbonPage1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("ribbonPage1.ImageOptions.SvgImage");
        ribbonPage1.Name = "ribbonPage1";
        ribbonPage1.Text = "模块编辑";
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
        // tablePanel1
        // 
        tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5F) });
        tablePanel1.Controls.Add(paginationControl1);
        tablePanel1.Controls.Add(gridControl1);
        tablePanel1.Dock = DockStyle.Fill;
        tablePanel1.Location = new Point(0, 170);
        tablePanel1.Name = "tablePanel1";
        tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 26F) });
        tablePanel1.Size = new Size(727, 405);
        tablePanel1.TabIndex = 5;
        // 
        // paginationControl1
        // 
        tablePanel1.SetColumn(paginationControl1, 0);
        paginationControl1.Location = new Point(3, 376);
        paginationControl1.Name = "paginationControl1";
        tablePanel1.SetRow(paginationControl1, 1);
        paginationControl1.Size = new Size(721, 26);
        paginationControl1.TabIndex = 3;
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
        gridControl1.Size = new Size(721, 367);
        gridControl1.TabIndex = 2;
        gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
        // 
        // gridView1
        // 
        gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn1, gridColumn2, gridColumn4 });
        gridView1.GridControl = gridControl1;
        gridView1.Name = "gridView1";
        // 
        // gridColumn1
        // 
        gridColumn1.Caption = "Id";
        gridColumn1.FieldName = "Id";
        gridColumn1.Name = "gridColumn1";
        // 
        // gridColumn2
        // 
        gridColumn2.Caption = "显示名称";
        gridColumn2.FieldName = "Title";
        gridColumn2.Name = "gridColumn2";
        gridColumn2.Visible = true;
        gridColumn2.VisibleIndex = 0;
        // 
        // gridColumn4
        // 
        gridColumn4.Caption = "排序";
        gridColumn4.FieldName = "SortCode";
        gridColumn4.Name = "gridColumn4";
        gridColumn4.Visible = true;
        gridColumn4.VisibleIndex = 1;
        // 
        // repositoryItemImageComboBox1
        // 
        repositoryItemImageComboBox1.AutoHeight = false;
        repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
        repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] { new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "AWAIT", 0), new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "READY", 1), new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "RUN", 2), new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "STOP", 3), new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "CLEAR", 4), new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "FINISHED", 5) });
        repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
        // 
        // ModuleForm
        // 
        AutoScaleDimensions = new SizeF(7F, 14F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(727, 575);
        Controls.Add(tablePanel1);
        Controls.Add(ribbonControl1);
        Name = "ModuleForm";
        Text = "模块管理";
        Load += ModuleForm_Load;
        ((ISupportInitialize)ribbonControl1).EndInit();
        ((ISupportInitialize)tablePanel1).EndInit();
        tablePanel1.ResumeLayout(false);
        ((ISupportInitialize)gridControl1).EndInit();
        ((ISupportInitialize)gridView1).EndInit();
        ((ISupportInitialize)repositoryItemImageComboBox1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
    private DevExpress.XtraBars.BarButtonItem barButtonItem1;
    private DevExpress.XtraBars.BarButtonItem barButtonItem2;
    private DevExpress.XtraBars.BarButtonItem barButtonItem3;
    private DevExpress.XtraBars.BarButtonItem barButtonItem4;
    private DevExpress.XtraBars.BarButtonItem barButtonItem5;
    private DevExpress.XtraBars.BarButtonItem barButtonItem6;
    private DevExpress.XtraBars.BarButtonItem barButtonItem7;
    private DevExpress.XtraBars.BarButtonItem barButtonItem8;
    private DevExpress.XtraBars.BarButtonItem barButtonItem9;
    private DevExpress.XtraBars.BarButtonItem barButtonItem10;
    private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
    private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
    private DevExpress.Utils.Layout.TablePanel tablePanel1;
    private PaginationControl paginationControl1;
    private DevExpress.XtraGrid.GridControl gridControl1;
    private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
    private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
}