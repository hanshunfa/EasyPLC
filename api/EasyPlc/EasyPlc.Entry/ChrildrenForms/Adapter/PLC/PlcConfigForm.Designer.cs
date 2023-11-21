namespace EasyPlc.Entry.ChrildrenForms
{
    partial class PlcConfigForm
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(PlcConfigForm));
            ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            treeList1 = new DevExpress.XtraTreeList.TreeList();
            treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            ((ISupportInitialize)ribbonControl1).BeginInit();
            ((ISupportInitialize)treeList1).BeginInit();
            ((ISupportInitialize)tablePanel1).BeginInit();
            tablePanel1.SuspendLayout();
            ((ISupportInitialize)memoEdit1.Properties).BeginInit();
            SuspendLayout();
            // 
            // ribbonControl1
            // 
            ribbonControl1.ExpandCollapseItem.Id = 0;
            ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbonControl1.ExpandCollapseItem, ribbonControl1.SearchEditItem, barButtonItem1, barButtonItem2, barButtonItem3, barButtonItem4 });
            ribbonControl1.Location = new Point(0, 0);
            ribbonControl1.MaxItemId = 5;
            ribbonControl1.Name = "ribbonControl1";
            ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage1 });
            ribbonControl1.Size = new Size(873, 170);
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
            // ribbonPage1
            // 
            ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup1 });
            ribbonPage1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("ribbonPage1.ImageOptions.SvgImage");
            ribbonPage1.Name = "ribbonPage1";
            ribbonPage1.Text = "PLC配置编辑";
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
            // treeList1
            // 
            tablePanel1.SetColumn(treeList1, 0);
            treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] { treeListColumn1, treeListColumn2, treeListColumn3, treeListColumn4 });
            treeList1.Dock = DockStyle.Fill;
            treeList1.Location = new Point(3, 3);
            treeList1.MenuManager = ribbonControl1;
            treeList1.Name = "treeList1";
            tablePanel1.SetRow(treeList1, 0);
            treeList1.Size = new Size(507, 520);
            treeList1.TabIndex = 2;
            treeList1.FocusedNodeChanged += treeList1_FocusedNodeChanged;
            // 
            // treeListColumn1
            // 
            treeListColumn1.Caption = "Id";
            treeListColumn1.FieldName = "Id";
            treeListColumn1.Name = "treeListColumn1";
            // 
            // treeListColumn2
            // 
            treeListColumn2.Caption = "名称";
            treeListColumn2.FieldName = "Name";
            treeListColumn2.Name = "treeListColumn2";
            treeListColumn2.Visible = true;
            treeListColumn2.VisibleIndex = 0;
            // 
            // treeListColumn3
            // 
            treeListColumn3.Caption = "分类";
            treeListColumn3.FieldName = "Category";
            treeListColumn3.Name = "treeListColumn3";
            treeListColumn3.Visible = true;
            treeListColumn3.VisibleIndex = 1;
            // 
            // treeListColumn4
            // 
            treeListColumn4.Caption = "排序";
            treeListColumn4.FieldName = "SortCode";
            treeListColumn4.Name = "treeListColumn4";
            treeListColumn4.Visible = true;
            treeListColumn4.VisibleIndex = 2;
            // 
            // tablePanel1
            // 
            tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 360F) });
            tablePanel1.Controls.Add(memoEdit1);
            tablePanel1.Controls.Add(treeList1);
            tablePanel1.Dock = DockStyle.Fill;
            tablePanel1.Location = new Point(0, 170);
            tablePanel1.Name = "tablePanel1";
            tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F) });
            tablePanel1.Size = new Size(873, 526);
            tablePanel1.TabIndex = 4;
            // 
            // memoEdit1
            // 
            tablePanel1.SetColumn(memoEdit1, 1);
            memoEdit1.Dock = DockStyle.Fill;
            memoEdit1.Location = new Point(516, 3);
            memoEdit1.MenuManager = ribbonControl1;
            memoEdit1.Name = "memoEdit1";
            memoEdit1.Properties.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            memoEdit1.Properties.Appearance.ForeColor = Color.Green;
            memoEdit1.Properties.Appearance.Options.UseFont = true;
            memoEdit1.Properties.Appearance.Options.UseForeColor = true;
            memoEdit1.Properties.ReadOnly = true;
            tablePanel1.SetRow(memoEdit1, 0);
            memoEdit1.Size = new Size(354, 520);
            memoEdit1.TabIndex = 3;
            // 
            // PlcConfigForm
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(873, 696);
            Controls.Add(tablePanel1);
            Controls.Add(ribbonControl1);
            Name = "PlcConfigForm";
            Text = "PLC配置";
            Load += PlcConfigForm_Load;
            ((ISupportInitialize)ribbonControl1).EndInit();
            ((ISupportInitialize)treeList1).EndInit();
            ((ISupportInitialize)tablePanel1).EndInit();
            tablePanel1.ResumeLayout(false);
            ((ISupportInitialize)memoEdit1.Properties).EndInit();
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
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
    }
}