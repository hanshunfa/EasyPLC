namespace EasyPlc.Entry.ChrildrenForms.Org
{
    partial class OrgForm
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(OrgForm));
            ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            treeList1 = new DevExpress.XtraTreeList.TreeList();
            treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((ISupportInitialize)ribbonControl1).BeginInit();
            ((ISupportInitialize)treeList1).BeginInit();
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
            ribbonControl1.Size = new Size(839, 170);
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
            ribbonPage1.Text = "组织管理编辑";
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
            treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] { treeListColumn4, treeListColumn1, treeListColumn2, treeListColumn3 });
            treeList1.Dock = DockStyle.Fill;
            treeList1.Location = new Point(0, 170);
            treeList1.MenuManager = ribbonControl1;
            treeList1.Name = "treeList1";
            treeList1.Size = new Size(839, 415);
            treeList1.TabIndex = 3;
            treeList1.BeforeCheckNode += treeList1_BeforeCheckNode;
            treeList1.AfterCheckNode += treeList1_AfterCheckNode;
            treeList1.FocusedNodeChanged += treeList1_FocusedNodeChanged;
            // 
            // treeListColumn4
            // 
            treeListColumn4.Caption = "Id";
            treeListColumn4.FieldName = "Id";
            treeListColumn4.Name = "treeListColumn4";
            treeListColumn4.Width = 20;
            // 
            // treeListColumn1
            // 
            treeListColumn1.Caption = "名称";
            treeListColumn1.FieldName = "Name";
            treeListColumn1.Name = "treeListColumn1";
            treeListColumn1.Visible = true;
            treeListColumn1.VisibleIndex = 0;
            treeListColumn1.Width = 266;
            // 
            // treeListColumn2
            // 
            treeListColumn2.Caption = "分类";
            treeListColumn2.FieldName = "Category";
            treeListColumn2.Name = "treeListColumn2";
            treeListColumn2.Visible = true;
            treeListColumn2.VisibleIndex = 1;
            treeListColumn2.Width = 264;
            // 
            // treeListColumn3
            // 
            treeListColumn3.Caption = "排序码";
            treeListColumn3.FieldName = "SortCode";
            treeListColumn3.Name = "treeListColumn3";
            treeListColumn3.Visible = true;
            treeListColumn3.VisibleIndex = 2;
            treeListColumn3.Width = 264;
            // 
            // OrgForm
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(839, 585);
            Controls.Add(treeList1);
            Controls.Add(ribbonControl1);
            Name = "OrgForm";
            Text = "组织管理";
            Load += OrgForm_Load;
            ((ISupportInitialize)ribbonControl1).EndInit();
            ((ISupportInitialize)treeList1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
    }
}