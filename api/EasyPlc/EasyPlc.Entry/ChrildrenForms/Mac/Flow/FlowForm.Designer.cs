namespace EasyPlc.Entry.ChrildrenForms.Mac
{
    partial class FlowForm
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(FlowForm));
            ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem9 = new DevExpress.XtraBars.BarButtonItem();
            ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            treeListFlow = new DevExpress.XtraTreeList.TreeList();
            treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            treeListFlowRelation = new DevExpress.XtraTreeList.TreeList();
            barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((ISupportInitialize)ribbonControl1).BeginInit();
            ((ISupportInitialize)tablePanel1).BeginInit();
            tablePanel1.SuspendLayout();
            ((ISupportInitialize)treeListFlow).BeginInit();
            ((ISupportInitialize)treeListFlowRelation).BeginInit();
            SuspendLayout();
            // 
            // ribbonControl1
            // 
            ribbonControl1.ExpandCollapseItem.Id = 0;
            ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbonControl1.ExpandCollapseItem, ribbonControl1.SearchEditItem, barButtonItem1, barButtonItem2, barButtonItem3, barButtonItem4, barButtonItem6, barButtonItem8, barButtonItem9 });
            ribbonControl1.Location = new Point(0, 0);
            ribbonControl1.MaxItemId = 10;
            ribbonControl1.Name = "ribbonControl1";
            ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage1 });
            ribbonControl1.Size = new Size(878, 170);
            // 
            // barButtonItem1
            // 
            barButtonItem1.Caption = "流程新增";
            barButtonItem1.Id = 1;
            barButtonItem1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem1.ImageOptions.SvgImage");
            barButtonItem1.Name = "barButtonItem1";
            barButtonItem1.ItemClick += barButtonItem1_ItemClick;
            // 
            // barButtonItem2
            // 
            barButtonItem2.Caption = "流程编辑";
            barButtonItem2.Id = 2;
            barButtonItem2.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem2.ImageOptions.SvgImage");
            barButtonItem2.Name = "barButtonItem2";
            barButtonItem2.ItemClick += barButtonItem2_ItemClick;
            // 
            // barButtonItem3
            // 
            barButtonItem3.Caption = "流程删除";
            barButtonItem3.Id = 3;
            barButtonItem3.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem3.ImageOptions.SvgImage");
            barButtonItem3.Name = "barButtonItem3";
            barButtonItem3.ItemClick += barButtonItem3_ItemClick;
            // 
            // barButtonItem4
            // 
            barButtonItem4.Caption = "流程刷新";
            barButtonItem4.Id = 4;
            barButtonItem4.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem4.ImageOptions.SvgImage");
            barButtonItem4.Name = "barButtonItem4";
            barButtonItem4.ItemClick += barButtonItem4_ItemClick;
            // 
            // barButtonItem6
            // 
            barButtonItem6.Caption = "参数编辑";
            barButtonItem6.Id = 6;
            barButtonItem6.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem6.ImageOptions.SvgImage");
            barButtonItem6.Name = "barButtonItem6";
            barButtonItem6.ItemClick += barButtonItem6_ItemClick;
            // 
            // barButtonItem8
            // 
            barButtonItem8.Caption = "参数刷新";
            barButtonItem8.Id = 8;
            barButtonItem8.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem8.ImageOptions.SvgImage");
            barButtonItem8.Name = "barButtonItem8";
            barButtonItem8.ItemClick += barButtonItem8_ItemClick;
            // 
            // barButtonItem9
            // 
            barButtonItem9.Caption = "参数保存";
            barButtonItem9.Id = 9;
            barButtonItem9.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem9.ImageOptions.SvgImage");
            barButtonItem9.Name = "barButtonItem9";
            barButtonItem9.ItemClick += barButtonItem9_ItemClick;
            // 
            // ribbonPage1
            // 
            ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup1, ribbonPageGroup2 });
            ribbonPage1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("ribbonPage1.ImageOptions.SvgImage");
            ribbonPage1.Name = "ribbonPage1";
            ribbonPage1.Text = "流程管理编辑";
            // 
            // ribbonPageGroup1
            // 
            ribbonPageGroup1.ItemLinks.Add(barButtonItem1);
            ribbonPageGroup1.ItemLinks.Add(barButtonItem2);
            ribbonPageGroup1.ItemLinks.Add(barButtonItem3);
            ribbonPageGroup1.ItemLinks.Add(barButtonItem4);
            ribbonPageGroup1.Name = "ribbonPageGroup1";
            ribbonPageGroup1.Text = "型号操作项";
            // 
            // ribbonPageGroup2
            // 
            ribbonPageGroup2.ItemLinks.Add(barButtonItem6);
            ribbonPageGroup2.ItemLinks.Add(barButtonItem9);
            ribbonPageGroup2.ItemLinks.Add(barButtonItem8);
            ribbonPageGroup2.Name = "ribbonPageGroup2";
            ribbonPageGroup2.Text = "参数操作项";
            // 
            // tablePanel1
            // 
            tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5F) });
            tablePanel1.Controls.Add(treeListFlow);
            tablePanel1.Controls.Add(treeListFlowRelation);
            tablePanel1.Dock = DockStyle.Fill;
            tablePanel1.Location = new Point(0, 170);
            tablePanel1.Name = "tablePanel1";
            tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F) });
            tablePanel1.Size = new Size(878, 473);
            tablePanel1.TabIndex = 7;
            // 
            // treeListFlow
            // 
            tablePanel1.SetColumn(treeListFlow, 0);
            treeListFlow.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] { treeListColumn1, treeListColumn2, treeListColumn3, treeListColumn4 });
            treeListFlow.Dock = DockStyle.Fill;
            treeListFlow.Location = new Point(3, 3);
            treeListFlow.MenuManager = ribbonControl1;
            treeListFlow.Name = "treeListFlow";
            tablePanel1.SetRow(treeListFlow, 0);
            treeListFlow.Size = new Size(433, 467);
            treeListFlow.TabIndex = 2;
            treeListFlow.FocusedNodeChanged += treeListFlow_FocusedNodeChanged;
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
            // treeListFlowRelation
            // 
            tablePanel1.SetColumn(treeListFlowRelation, 1);
            treeListFlowRelation.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] { treeListColumn5 });
            treeListFlowRelation.Dock = DockStyle.Fill;
            treeListFlowRelation.KeyFieldName = "Id";
            treeListFlowRelation.Location = new Point(442, 3);
            treeListFlowRelation.MenuManager = ribbonControl1;
            treeListFlowRelation.Name = "treeListFlowRelation";
            treeListFlowRelation.ParentFieldName = "ParentId";
            tablePanel1.SetRow(treeListFlowRelation, 0);
            treeListFlowRelation.Size = new Size(433, 467);
            treeListFlowRelation.TabIndex = 1;
            treeListFlowRelation.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeView;
            treeListFlowRelation.BeforeCheckNode += treeListFlowParam_BeforeCheckNode;
            treeListFlowRelation.AfterCheckNode += treeListFlowParam_AfterCheckNode;
            // 
            // barButtonItem7
            // 
            barButtonItem7.Caption = "参数删除";
            barButtonItem7.Id = 7;
            barButtonItem7.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem7.ImageOptions.SvgImage");
            barButtonItem7.Name = "barButtonItem7";
            // 
            // barButtonItem5
            // 
            barButtonItem5.Caption = "参数新增";
            barButtonItem5.Id = 5;
            barButtonItem5.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem5.ImageOptions.SvgImage");
            barButtonItem5.Name = "barButtonItem5";
            // 
            // treeListColumn5
            // 
            treeListColumn5.Caption = "名称";
            treeListColumn5.FieldName = "Name";
            treeListColumn5.Name = "treeListColumn5";
            treeListColumn5.Visible = true;
            treeListColumn5.VisibleIndex = 0;
            // 
            // FlowForm
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(878, 643);
            Controls.Add(tablePanel1);
            Controls.Add(ribbonControl1);
            Name = "FlowForm";
            Text = "流程管理";
            Load += FlowForm_Load;
            ((ISupportInitialize)ribbonControl1).EndInit();
            ((ISupportInitialize)tablePanel1).EndInit();
            tablePanel1.ResumeLayout(false);
            ((ISupportInitialize)treeListFlow).EndInit();
            ((ISupportInitialize)treeListFlowRelation).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarButtonItem barButtonItem8;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem9;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraTreeList.TreeList treeListFlowRelation;
        private DevExpress.XtraTreeList.TreeList treeListFlow;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
    }
}