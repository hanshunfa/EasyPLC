namespace EasyPlc.Entry.ChrildrenForms
{
    partial class AddrConfigForm
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(AddrConfigForm));
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
            labelControl4 = new DevExpress.XtraEditors.LabelControl();
            labelControl3 = new DevExpress.XtraEditors.LabelControl();
            labelControl2 = new DevExpress.XtraEditors.LabelControl();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            treeList4 = new DevExpress.XtraTreeList.TreeList();
            treeList3 = new DevExpress.XtraTreeList.TreeList();
            treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            svgImageCollection1 = new DevExpress.Utils.SvgImageCollection(components);
            treeList2 = new DevExpress.XtraTreeList.TreeList();
            treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            treeListColumn6 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            treeListColumn7 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            treeList1 = new DevExpress.XtraTreeList.TreeList();
            treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((ISupportInitialize)ribbonControl1).BeginInit();
            ((ISupportInitialize)tablePanel1).BeginInit();
            tablePanel1.SuspendLayout();
            ((ISupportInitialize)treeList4).BeginInit();
            ((ISupportInitialize)treeList3).BeginInit();
            ((ISupportInitialize)svgImageCollection1).BeginInit();
            ((ISupportInitialize)treeList2).BeginInit();
            ((ISupportInitialize)treeList1).BeginInit();
            SuspendLayout();
            // 
            // ribbonControl1
            // 
            ribbonControl1.ExpandCollapseItem.Id = 0;
            ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbonControl1.ExpandCollapseItem, ribbonControl1.SearchEditItem, barButtonItem1, barButtonItem2, barButtonItem3, barButtonItem4, barButtonItem5 });
            ribbonControl1.Location = new Point(0, 0);
            ribbonControl1.MaxItemId = 6;
            ribbonControl1.Name = "ribbonControl1";
            ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage1 });
            ribbonControl1.Size = new Size(836, 170);
            // 
            // barButtonItem1
            // 
            barButtonItem1.Caption = "新增";
            barButtonItem1.Id = 1;
            barButtonItem1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem1.ImageOptions.SvgImage");
            barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            barButtonItem2.Caption = "编辑";
            barButtonItem2.Id = 2;
            barButtonItem2.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem2.ImageOptions.SvgImage");
            barButtonItem2.Name = "barButtonItem2";
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
            barButtonItem5.Caption = "生成对象定义代码";
            barButtonItem5.Id = 5;
            barButtonItem5.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem5.ImageOptions.SvgImage");
            barButtonItem5.Name = "barButtonItem5";
            barButtonItem5.ItemClick += barButtonItem5_ItemClick;
            // 
            // ribbonPage1
            // 
            ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup1, ribbonPageGroup2 });
            ribbonPage1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("ribbonPage1.ImageOptions.SvgImage");
            ribbonPage1.Name = "ribbonPage1";
            ribbonPage1.Text = "地址配置编辑";
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
            ribbonPageGroup2.Text = "代码生成";
            // 
            // tablePanel1
            // 
            tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F) });
            tablePanel1.Controls.Add(labelControl4);
            tablePanel1.Controls.Add(labelControl3);
            tablePanel1.Controls.Add(labelControl2);
            tablePanel1.Controls.Add(labelControl1);
            tablePanel1.Controls.Add(treeList4);
            tablePanel1.Controls.Add(treeList3);
            tablePanel1.Controls.Add(treeList2);
            tablePanel1.Controls.Add(treeList1);
            tablePanel1.Dock = DockStyle.Fill;
            tablePanel1.Location = new Point(0, 170);
            tablePanel1.Name = "tablePanel1";
            tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F) });
            tablePanel1.Size = new Size(836, 438);
            tablePanel1.TabIndex = 2;
            // 
            // labelControl4
            // 
            tablePanel1.SetColumn(labelControl4, 3);
            labelControl4.Dock = DockStyle.Fill;
            labelControl4.Location = new Point(630, 3);
            labelControl4.Name = "labelControl4";
            tablePanel1.SetRow(labelControl4, 0);
            labelControl4.Size = new Size(203, 20);
            labelControl4.TabIndex = 7;
            labelControl4.Text = "预览";
            // 
            // labelControl3
            // 
            tablePanel1.SetColumn(labelControl3, 2);
            labelControl3.Dock = DockStyle.Fill;
            labelControl3.Location = new Point(421, 3);
            labelControl3.Name = "labelControl3";
            tablePanel1.SetRow(labelControl3, 0);
            labelControl3.Size = new Size(203, 20);
            labelControl3.TabIndex = 6;
            labelControl3.Text = "变量树形结构";
            // 
            // labelControl2
            // 
            tablePanel1.SetColumn(labelControl2, 1);
            labelControl2.Dock = DockStyle.Fill;
            labelControl2.Location = new Point(212, 3);
            labelControl2.Name = "labelControl2";
            tablePanel1.SetRow(labelControl2, 0);
            labelControl2.Size = new Size(203, 20);
            labelControl2.TabIndex = 5;
            labelControl2.Text = "地址树形结构";
            // 
            // labelControl1
            // 
            tablePanel1.SetColumn(labelControl1, 0);
            labelControl1.Dock = DockStyle.Fill;
            labelControl1.Location = new Point(3, 3);
            labelControl1.Name = "labelControl1";
            tablePanel1.SetRow(labelControl1, 0);
            labelControl1.Size = new Size(203, 20);
            labelControl1.TabIndex = 4;
            labelControl1.Text = "PLC树形结构";
            // 
            // treeList4
            // 
            tablePanel1.SetColumn(treeList4, 3);
            treeList4.Dock = DockStyle.Fill;
            treeList4.Location = new Point(630, 29);
            treeList4.MenuManager = ribbonControl1;
            treeList4.Name = "treeList4";
            tablePanel1.SetRow(treeList4, 1);
            treeList4.Size = new Size(203, 406);
            treeList4.TabIndex = 3;
            treeList4.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeView;
            // 
            // treeList3
            // 
            tablePanel1.SetColumn(treeList3, 2);
            treeList3.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] { treeListColumn3, treeListColumn4 });
            treeList3.Dock = DockStyle.Fill;
            treeList3.Location = new Point(421, 29);
            treeList3.MenuManager = ribbonControl1;
            treeList3.Name = "treeList3";
            tablePanel1.SetRow(treeList3, 1);
            treeList3.SelectImageList = svgImageCollection1;
            treeList3.Size = new Size(203, 406);
            treeList3.TabIndex = 2;
            treeList3.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeView;
            treeList3.CustomDrawNodeImages += treeList3_CustomDrawNodeImages;
            treeList3.DragDrop += treeList3_DragDrop;
            treeList3.DragEnter += treeList3_DragEnter;
            treeList3.DragOver += treeList3_DragOver;
            // 
            // treeListColumn3
            // 
            treeListColumn3.Caption = "Id";
            treeListColumn3.FieldName = "Id";
            treeListColumn3.Name = "treeListColumn3";
            // 
            // treeListColumn4
            // 
            treeListColumn4.Caption = "Name";
            treeListColumn4.FieldName = "Name";
            treeListColumn4.Name = "treeListColumn4";
            treeListColumn4.Visible = true;
            treeListColumn4.VisibleIndex = 0;
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
            // treeList2
            // 
            tablePanel1.SetColumn(treeList2, 1);
            treeList2.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] { treeListColumn5, treeListColumn6, treeListColumn7 });
            treeList2.Dock = DockStyle.Fill;
            treeList2.Location = new Point(212, 29);
            treeList2.MenuManager = ribbonControl1;
            treeList2.Name = "treeList2";
            tablePanel1.SetRow(treeList2, 1);
            treeList2.SelectImageList = svgImageCollection1;
            treeList2.Size = new Size(203, 406);
            treeList2.TabIndex = 1;
            treeList2.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeView;
            treeList2.FocusedNodeChanged += treeList2_FocusedNodeChanged;
            treeList2.CustomDrawNodeImages += treeList2_CustomDrawNodeImages;
            treeList2.DragDrop += treeList2_DragDrop;
            treeList2.DragEnter += treeList2_DragEnter;
            treeList2.DragOver += treeList2_DragOver;
            // 
            // treeListColumn5
            // 
            treeListColumn5.Caption = "Id";
            treeListColumn5.FieldName = "Id";
            treeListColumn5.Name = "treeListColumn5";
            // 
            // treeListColumn6
            // 
            treeListColumn6.Caption = "名称";
            treeListColumn6.FieldName = "Name";
            treeListColumn6.Name = "treeListColumn6";
            treeListColumn6.Visible = true;
            treeListColumn6.VisibleIndex = 0;
            treeListColumn6.Width = 160;
            // 
            // treeListColumn7
            // 
            treeListColumn7.Caption = "地址";
            treeListColumn7.FieldName = "Addr";
            treeListColumn7.Name = "treeListColumn7";
            treeListColumn7.Visible = true;
            treeListColumn7.VisibleIndex = 1;
            treeListColumn7.Width = 41;
            // 
            // treeList1
            // 
            tablePanel1.SetColumn(treeList1, 0);
            treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] { treeListColumn1, treeListColumn2 });
            treeList1.Dock = DockStyle.Fill;
            treeList1.Location = new Point(3, 29);
            treeList1.MenuManager = ribbonControl1;
            treeList1.Name = "treeList1";
            tablePanel1.SetRow(treeList1, 1);
            treeList1.Size = new Size(203, 406);
            treeList1.TabIndex = 0;
            treeList1.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeView;
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
            // AddrConfigForm
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(836, 608);
            Controls.Add(tablePanel1);
            Controls.Add(ribbonControl1);
            Name = "AddrConfigForm";
            Text = "地址配置";
            Load += AddrConfigForm_Load;
            ((ISupportInitialize)ribbonControl1).EndInit();
            ((ISupportInitialize)tablePanel1).EndInit();
            tablePanel1.ResumeLayout(false);
            tablePanel1.PerformLayout();
            ((ISupportInitialize)treeList4).EndInit();
            ((ISupportInitialize)treeList3).EndInit();
            ((ISupportInitialize)svgImageCollection1).EndInit();
            ((ISupportInitialize)treeList2).EndInit();
            ((ISupportInitialize)treeList1).EndInit();
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
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraTreeList.TreeList treeList4;
        private DevExpress.XtraTreeList.TreeList treeList3;
        private DevExpress.XtraTreeList.TreeList treeList2;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.Utils.SvgImageCollection svgImageCollection1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn6;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
    }
}