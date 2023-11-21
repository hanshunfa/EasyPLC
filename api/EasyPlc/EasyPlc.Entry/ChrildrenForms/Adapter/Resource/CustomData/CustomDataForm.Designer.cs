namespace EasyPlc.Entry.ChrildrenForms
{
    partial class CustomDataForm
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(CustomDataForm));
            ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            tablePanel2 = new DevExpress.Utils.Layout.TablePanel();
            paginationControl1 = new PaginationControl();
            gridControl1 = new DevExpress.XtraGrid.GridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemToggleSwitch1 = new DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch();
            gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            treeList1 = new DevExpress.XtraTreeList.TreeList();
            treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            svgImageCollection1 = new DevExpress.Utils.SvgImageCollection(components);
            ((ISupportInitialize)ribbonControl1).BeginInit();
            ((ISupportInitialize)tablePanel1).BeginInit();
            tablePanel1.SuspendLayout();
            ((ISupportInitialize)tablePanel2).BeginInit();
            tablePanel2.SuspendLayout();
            ((ISupportInitialize)gridControl1).BeginInit();
            ((ISupportInitialize)gridView1).BeginInit();
            ((ISupportInitialize)repositoryItemToggleSwitch1).BeginInit();
            ((ISupportInitialize)treeList1).BeginInit();
            ((ISupportInitialize)svgImageCollection1).BeginInit();
            SuspendLayout();
            // 
            // ribbonControl1
            // 
            ribbonControl1.ExpandCollapseItem.Id = 0;
            ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbonControl1.ExpandCollapseItem, ribbonControl1.SearchEditItem, barButtonItem1, barButtonItem2, barButtonItem3, barButtonItem4, barButtonItem5 });
            ribbonControl1.Location = new Point(0, 0);
            ribbonControl1.MaxItemId = 8;
            ribbonControl1.Name = "ribbonControl1";
            ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage1 });
            ribbonControl1.Size = new Size(955, 170);
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
            barButtonItem5.Caption = "数据复制";
            barButtonItem5.Id = 5;
            barButtonItem5.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem5.ImageOptions.SvgImage");
            barButtonItem5.Name = "barButtonItem5";
            // 
            // ribbonPage1
            // 
            ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup1, ribbonPageGroup2 });
            ribbonPage1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("ribbonPage1.ImageOptions.SvgImage");
            ribbonPage1.Name = "ribbonPage1";
            ribbonPage1.Text = "PLC自定义数据编辑";
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
            // ribbonPageGroup2
            // 
            ribbonPageGroup2.ItemLinks.Add(barButtonItem5);
            ribbonPageGroup2.Name = "ribbonPageGroup2";
            ribbonPageGroup2.Text = "高级操作";
            // 
            // tablePanel1
            // 
            tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 680F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 55F) });
            tablePanel1.Controls.Add(tablePanel2);
            tablePanel1.Controls.Add(treeList1);
            tablePanel1.Dock = DockStyle.Fill;
            tablePanel1.Location = new Point(0, 170);
            tablePanel1.Name = "tablePanel1";
            tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F) });
            tablePanel1.Size = new Size(955, 423);
            tablePanel1.TabIndex = 3;
            // 
            // tablePanel2
            // 
            tablePanel1.SetColumn(tablePanel2, 1);
            tablePanel2.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5F) });
            tablePanel2.Controls.Add(paginationControl1);
            tablePanel2.Controls.Add(gridControl1);
            tablePanel2.Dock = DockStyle.Fill;
            tablePanel2.Location = new Point(683, 3);
            tablePanel2.Name = "tablePanel2";
            tablePanel1.SetRow(tablePanel2, 0);
            tablePanel2.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 26F) });
            tablePanel2.Size = new Size(269, 417);
            tablePanel2.TabIndex = 2;
            // 
            // paginationControl1
            // 
            tablePanel2.SetColumn(paginationControl1, 0);
            paginationControl1.Location = new Point(3, 388);
            paginationControl1.Name = "paginationControl1";
            tablePanel2.SetRow(paginationControl1, 1);
            paginationControl1.Size = new Size(263, 26);
            paginationControl1.TabIndex = 2;
            // 
            // gridControl1
            // 
            tablePanel2.SetColumn(gridControl1, 0);
            gridControl1.Dock = DockStyle.Fill;
            gridControl1.Location = new Point(3, 3);
            gridControl1.MainView = gridView1;
            gridControl1.MenuManager = ribbonControl1;
            gridControl1.Name = "gridControl1";
            gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemToggleSwitch1 });
            tablePanel2.SetRow(gridControl1, 0);
            gridControl1.Size = new Size(263, 379);
            gridControl1.TabIndex = 1;
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn1, gridColumn2, gridColumn7, gridColumn3, gridColumn4, gridColumn5, gridColumn6 });
            gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridView1.GridControl = gridControl1;
            gridView1.Name = "gridView1";
            gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            gridView1.CustomDrawRowIndicator += gridView1_CustomDrawRowIndicator;
            gridView1.SelectionChanged += gridView1_SelectionChanged;
            // 
            // gridColumn1
            // 
            gridColumn1.Caption = "Id";
            gridColumn1.ColumnEdit = repositoryItemToggleSwitch1;
            gridColumn1.FieldName = "Id";
            gridColumn1.Name = "gridColumn1";
            // 
            // repositoryItemToggleSwitch1
            // 
            repositoryItemToggleSwitch1.AutoHeight = false;
            repositoryItemToggleSwitch1.Name = "repositoryItemToggleSwitch1";
            repositoryItemToggleSwitch1.OffText = "Off";
            repositoryItemToggleSwitch1.OnText = "On";
            repositoryItemToggleSwitch1.ValueOff = "禁用";
            repositoryItemToggleSwitch1.ValueOn = "启用";
            // 
            // gridColumn2
            // 
            gridColumn2.Caption = "名称";
            gridColumn2.FieldName = "Title";
            gridColumn2.Name = "gridColumn2";
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn7
            // 
            gridColumn7.Caption = "编码";
            gridColumn7.FieldName = "Code";
            gridColumn7.Name = "gridColumn7";
            gridColumn7.Visible = true;
            gridColumn7.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            gridColumn3.Caption = "类型";
            gridColumn3.FieldName = "ValueType";
            gridColumn3.Name = "gridColumn3";
            gridColumn3.Visible = true;
            gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            gridColumn4.Caption = "长度";
            gridColumn4.FieldName = "ValueLength";
            gridColumn4.Name = "gridColumn4";
            gridColumn4.Visible = true;
            gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            gridColumn5.Caption = "分类";
            gridColumn5.FieldName = "Category";
            gridColumn5.Name = "gridColumn5";
            gridColumn5.Visible = true;
            gridColumn5.VisibleIndex = 4;
            // 
            // gridColumn6
            // 
            gridColumn6.Caption = "排序";
            gridColumn6.FieldName = "SortCode";
            gridColumn6.Name = "gridColumn6";
            gridColumn6.Visible = true;
            gridColumn6.VisibleIndex = 5;
            // 
            // treeList1
            // 
            tablePanel1.SetColumn(treeList1, 0);
            treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] { treeListColumn1, treeListColumn2 });
            treeList1.Dock = DockStyle.Fill;
            treeList1.Location = new Point(3, 3);
            treeList1.MenuManager = ribbonControl1;
            treeList1.Name = "treeList1";
            tablePanel1.SetRow(treeList1, 0);
            treeList1.SelectImageList = svgImageCollection1;
            treeList1.Size = new Size(674, 417);
            treeList1.TabIndex = 0;
            treeList1.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeView;
            treeList1.FocusedNodeChanged += treeList1_FocusedNodeChanged;
            treeList1.CustomDrawNodeImages += treeList1_CustomDrawNodeImages;
            treeList1.DragDrop += treeList1_DragDrop;
            treeList1.DragEnter += treeList1_DragEnter;
            treeList1.DragOver += treeList1_DragOver;
            // 
            // treeListColumn1
            // 
            treeListColumn1.Caption = "Id";
            treeListColumn1.FieldName = "Id";
            treeListColumn1.Name = "treeListColumn1";
            // 
            // treeListColumn2
            // 
            treeListColumn2.Caption = "数据结构";
            treeListColumn2.FieldName = "Name";
            treeListColumn2.Name = "treeListColumn2";
            treeListColumn2.Visible = true;
            treeListColumn2.VisibleIndex = 0;
            // 
            // svgImageCollection1
            // 
            svgImageCollection1.Add("showexportwarnings", "image://svgimages/reports/showexportwarnings.svg");
            svgImageCollection1.Add("addquery", "image://svgimages/dashboards/addquery.svg");
            svgImageCollection1.Add("listnumbers", "image://svgimages/outlook inspired/listnumbers.svg");
            svgImageCollection1.Add("bool", "image://svgimages/snap/bool.svg");
            svgImageCollection1.Add("calcinteger", "image://svgimages/snap/calcinteger.svg");
            svgImageCollection1.Add("integer", "image://svgimages/snap/integer.svg");
            svgImageCollection1.Add("float", "image://svgimages/snap/float.svg");
            svgImageCollection1.Add("string", "image://svgimages/snap/string.svg");
            svgImageCollection1.Add("calcstring", "image://svgimages/snap/calcstring.svg");
            // 
            // CustomDataForm
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(955, 593);
            Controls.Add(tablePanel1);
            Controls.Add(ribbonControl1);
            Name = "CustomDataForm";
            Text = "PLC数据结构定义";
            Load += CustomDataForm_Load;
            ((ISupportInitialize)ribbonControl1).EndInit();
            ((ISupportInitialize)tablePanel1).EndInit();
            tablePanel1.ResumeLayout(false);
            ((ISupportInitialize)tablePanel2).EndInit();
            tablePanel2.ResumeLayout(false);
            ((ISupportInitialize)gridControl1).EndInit();
            ((ISupportInitialize)gridView1).EndInit();
            ((ISupportInitialize)repositoryItemToggleSwitch1).EndInit();
            ((ISupportInitialize)treeList1).EndInit();
            ((ISupportInitialize)svgImageCollection1).EndInit();
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
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.Utils.Layout.TablePanel tablePanel2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch repositoryItemToggleSwitch1;
        private PaginationControl paginationControl1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.Utils.SvgImageCollection svgImageCollection1;
    }
}