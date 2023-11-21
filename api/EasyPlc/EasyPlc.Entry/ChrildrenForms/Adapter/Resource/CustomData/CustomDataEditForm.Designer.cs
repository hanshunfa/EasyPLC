namespace EasyPlc.Entry.ChrildrenForms
{
    partial class CustomDataEditForm
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(CustomDataEditForm));
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            treeListLookUpEdit1 = new DevExpress.XtraEditors.TreeListLookUpEdit();
            treeListLookUpEdit1TreeList = new DevExpress.XtraTreeList.TreeList();
            textEdit1 = new DevExpress.XtraEditors.TextEdit();
            simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            spinEdit2 = new DevExpress.XtraEditors.SpinEdit();
            comboBoxEdit2 = new DevExpress.XtraEditors.ComboBoxEdit();
            textEdit2 = new DevExpress.XtraEditors.TextEdit();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((ISupportInitialize)treeListLookUpEdit1.Properties).BeginInit();
            ((ISupportInitialize)treeListLookUpEdit1TreeList).BeginInit();
            ((ISupportInitialize)textEdit1.Properties).BeginInit();
            ((ISupportInitialize)spinEdit1.Properties).BeginInit();
            ((ISupportInitialize)comboBoxEdit1.Properties).BeginInit();
            ((ISupportInitialize)spinEdit2.Properties).BeginInit();
            ((ISupportInitialize)comboBoxEdit2.Properties).BeginInit();
            ((ISupportInitialize)textEdit2.Properties).BeginInit();
            ((ISupportInitialize)Root).BeginInit();
            ((ISupportInitialize)layoutControlItem3).BeginInit();
            ((ISupportInitialize)layoutControlItem4).BeginInit();
            ((ISupportInitialize)emptySpaceItem2).BeginInit();
            ((ISupportInitialize)layoutControlGroup2).BeginInit();
            ((ISupportInitialize)layoutControlItem1).BeginInit();
            ((ISupportInitialize)layoutControlItem2).BeginInit();
            ((ISupportInitialize)layoutControlItem7).BeginInit();
            ((ISupportInitialize)layoutControlItem5).BeginInit();
            ((ISupportInitialize)layoutControlItem8).BeginInit();
            ((ISupportInitialize)layoutControlItem6).BeginInit();
            ((ISupportInitialize)layoutControlItem9).BeginInit();
            ((ISupportInitialize)emptySpaceItem1).BeginInit();
            ((ISupportInitialize)layoutControlGroup5).BeginInit();
            ((ISupportInitialize)emptySpaceItem4).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(treeListLookUpEdit1);
            layoutControl1.Controls.Add(textEdit1);
            layoutControl1.Controls.Add(simpleButton1);
            layoutControl1.Controls.Add(simpleButton2);
            layoutControl1.Controls.Add(spinEdit1);
            layoutControl1.Controls.Add(comboBoxEdit1);
            layoutControl1.Controls.Add(spinEdit2);
            layoutControl1.Controls.Add(comboBoxEdit2);
            layoutControl1.Controls.Add(textEdit2);
            layoutControl1.Dock = DockStyle.Fill;
            layoutControl1.Location = new Point(0, 0);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(809, 173, 650, 426);
            layoutControl1.Root = Root;
            layoutControl1.Size = new Size(584, 511);
            layoutControl1.TabIndex = 0;
            layoutControl1.Text = "layoutControl1";
            // 
            // treeListLookUpEdit1
            // 
            treeListLookUpEdit1.EditValue = "全部结构";
            treeListLookUpEdit1.Location = new Point(91, 45);
            treeListLookUpEdit1.Name = "treeListLookUpEdit1";
            treeListLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            treeListLookUpEdit1.Properties.DisplayMember = "Name";
            treeListLookUpEdit1.Properties.TreeList = treeListLookUpEdit1TreeList;
            treeListLookUpEdit1.Properties.ValueMember = "Id";
            treeListLookUpEdit1.Size = new Size(469, 20);
            treeListLookUpEdit1.StyleController = layoutControl1;
            treeListLookUpEdit1.TabIndex = 16;
            // 
            // treeListLookUpEdit1TreeList
            // 
            treeListLookUpEdit1TreeList.KeyFieldName = "Id";
            treeListLookUpEdit1TreeList.Location = new Point(0, 0);
            treeListLookUpEdit1TreeList.Name = "treeListLookUpEdit1TreeList";
            treeListLookUpEdit1TreeList.OptionsView.ShowIndentAsRowStyle = true;
            treeListLookUpEdit1TreeList.ParentFieldName = "ParentId";
            treeListLookUpEdit1TreeList.RootValue = (short)0;
            treeListLookUpEdit1TreeList.Size = new Size(400, 200);
            treeListLookUpEdit1TreeList.TabIndex = 0;
            treeListLookUpEdit1TreeList.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeView;
            // 
            // textEdit1
            // 
            textEdit1.Location = new Point(91, 93);
            textEdit1.Name = "textEdit1";
            textEdit1.Size = new Size(469, 20);
            textEdit1.StyleController = layoutControl1;
            textEdit1.TabIndex = 0;
            // 
            // simpleButton1
            // 
            simpleButton1.ImageOptions.Image = (Image)resources.GetObject("simpleButton1.ImageOptions.Image");
            simpleButton1.Location = new Point(416, 477);
            simpleButton1.Name = "simpleButton1";
            simpleButton1.Size = new Size(76, 22);
            simpleButton1.StyleController = layoutControl1;
            simpleButton1.TabIndex = 11;
            simpleButton1.Text = "确定";
            simpleButton1.Click += simpleButton1_Click;
            // 
            // simpleButton2
            // 
            simpleButton2.ImageOptions.Image = (Image)resources.GetObject("simpleButton2.ImageOptions.Image");
            simpleButton2.Location = new Point(496, 477);
            simpleButton2.Name = "simpleButton2";
            simpleButton2.Size = new Size(76, 22);
            simpleButton2.StyleController = layoutControl1;
            simpleButton2.TabIndex = 12;
            simpleButton2.Text = "取消";
            simpleButton2.Click += simpleButton2_Click;
            // 
            // spinEdit1
            // 
            spinEdit1.EditValue = new decimal(new int[] { 1, 0, 0, 0 });
            spinEdit1.Location = new Point(91, 189);
            spinEdit1.Name = "spinEdit1";
            spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            spinEdit1.Properties.MaskSettings.Set("mask", "d");
            spinEdit1.Properties.MaxValue = new decimal(new int[] { 9999, 0, 0, 0 });
            spinEdit1.Properties.MinValue = new decimal(new int[] { 1, 0, 0, 0 });
            spinEdit1.Size = new Size(469, 20);
            spinEdit1.StyleController = layoutControl1;
            spinEdit1.TabIndex = 2;
            // 
            // comboBoxEdit1
            // 
            comboBoxEdit1.Location = new Point(91, 141);
            comboBoxEdit1.Name = "comboBoxEdit1";
            comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            comboBoxEdit1.Properties.Items.AddRange(new object[] { "Bool", "Int16", "Int32", "Float", "String", "WString" });
            comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            comboBoxEdit1.Size = new Size(469, 20);
            comboBoxEdit1.StyleController = layoutControl1;
            comboBoxEdit1.TabIndex = 13;
            comboBoxEdit1.SelectedIndexChanged += comboBoxEdit1_SelectedIndexChanged;
            // 
            // spinEdit2
            // 
            spinEdit2.EditValue = new decimal(new int[] { 1, 0, 0, 0 });
            spinEdit2.Location = new Point(91, 165);
            spinEdit2.Name = "spinEdit2";
            spinEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            spinEdit2.Properties.MaskSettings.Set("mask", "d");
            spinEdit2.Properties.MaxValue = new decimal(new int[] { 1024, 0, 0, 0 });
            spinEdit2.Properties.MinValue = new decimal(new int[] { 1, 0, 0, 0 });
            spinEdit2.Size = new Size(469, 20);
            spinEdit2.StyleController = layoutControl1;
            spinEdit2.TabIndex = 14;
            // 
            // comboBoxEdit2
            // 
            comboBoxEdit2.Location = new Point(91, 69);
            comboBoxEdit2.Name = "comboBoxEdit2";
            comboBoxEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            comboBoxEdit2.Properties.Items.AddRange(new object[] { "基本类型", "结构类型", "数组类型" });
            comboBoxEdit2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            comboBoxEdit2.Size = new Size(469, 20);
            comboBoxEdit2.StyleController = layoutControl1;
            comboBoxEdit2.TabIndex = 17;
            comboBoxEdit2.SelectedIndexChanged += comboBoxEdit2_SelectedIndexChanged;
            // 
            // textEdit2
            // 
            textEdit2.Location = new Point(91, 117);
            textEdit2.Name = "textEdit2";
            textEdit2.Size = new Size(469, 20);
            textEdit2.StyleController = layoutControl1;
            textEdit2.TabIndex = 18;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem3, layoutControlItem4, emptySpaceItem2, layoutControlGroup2, emptySpaceItem1 });
            Root.Name = "Root";
            Root.Size = new Size(584, 511);
            Root.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = simpleButton1;
            layoutControlItem3.Location = new Point(404, 465);
            layoutControlItem3.MaxSize = new Size(80, 26);
            layoutControlItem3.MinSize = new Size(80, 26);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new Size(80, 26);
            layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem3.TextSize = new Size(0, 0);
            layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = simpleButton2;
            layoutControlItem4.Location = new Point(484, 465);
            layoutControlItem4.MaxSize = new Size(80, 26);
            layoutControlItem4.MinSize = new Size(80, 26);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new Size(80, 26);
            layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem4.TextSize = new Size(0, 0);
            layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            emptySpaceItem2.AllowHotTrack = false;
            emptySpaceItem2.Location = new Point(0, 465);
            emptySpaceItem2.Name = "emptySpaceItem2";
            emptySpaceItem2.Size = new Size(404, 26);
            emptySpaceItem2.TextSize = new Size(0, 0);
            // 
            // layoutControlGroup2
            // 
            layoutControlGroup2.CustomizationFormText = "数据结构信息";
            layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, layoutControlItem2, layoutControlItem7, layoutControlItem5, layoutControlItem8, layoutControlItem6, layoutControlItem9 });
            layoutControlGroup2.Location = new Point(0, 0);
            layoutControlGroup2.Name = "layoutControlGroup2";
            layoutControlGroup2.Size = new Size(564, 213);
            layoutControlGroup2.Text = "数据结构信息";
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = textEdit1;
            layoutControlItem1.CustomizationFormText = "名称";
            layoutControlItem1.Location = new Point(0, 48);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new Size(540, 24);
            layoutControlItem1.Text = "名称";
            layoutControlItem1.TextSize = new Size(55, 14);
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = comboBoxEdit1;
            layoutControlItem2.CustomizationFormText = "数据类型*";
            layoutControlItem2.Location = new Point(0, 96);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new Size(540, 24);
            layoutControlItem2.Text = "数据类型*";
            layoutControlItem2.TextSize = new Size(55, 14);
            // 
            // layoutControlItem7
            // 
            layoutControlItem7.Control = spinEdit1;
            layoutControlItem7.Location = new Point(0, 144);
            layoutControlItem7.Name = "layoutControlItem7";
            layoutControlItem7.Size = new Size(540, 24);
            layoutControlItem7.Text = "排序";
            layoutControlItem7.TextSize = new Size(55, 14);
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.Control = spinEdit2;
            layoutControlItem5.CustomizationFormText = "长度*";
            layoutControlItem5.Location = new Point(0, 120);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new Size(540, 24);
            layoutControlItem5.Text = "长度*";
            layoutControlItem5.TextSize = new Size(55, 14);
            // 
            // layoutControlItem8
            // 
            layoutControlItem8.Control = treeListLookUpEdit1;
            layoutControlItem8.CustomizationFormText = "所属结构*";
            layoutControlItem8.Location = new Point(0, 0);
            layoutControlItem8.Name = "layoutControlItem8";
            layoutControlItem8.Size = new Size(540, 24);
            layoutControlItem8.Text = "所属结构*";
            layoutControlItem8.TextSize = new Size(55, 14);
            // 
            // layoutControlItem6
            // 
            layoutControlItem6.Control = comboBoxEdit2;
            layoutControlItem6.CustomizationFormText = "分类*";
            layoutControlItem6.Location = new Point(0, 24);
            layoutControlItem6.Name = "layoutControlItem6";
            layoutControlItem6.Size = new Size(540, 24);
            layoutControlItem6.Text = "分类*";
            layoutControlItem6.TextSize = new Size(55, 14);
            // 
            // layoutControlItem9
            // 
            layoutControlItem9.Control = textEdit2;
            layoutControlItem9.CustomizationFormText = "编码*";
            layoutControlItem9.Location = new Point(0, 72);
            layoutControlItem9.Name = "layoutControlItem9";
            layoutControlItem9.Size = new Size(540, 24);
            layoutControlItem9.Text = "编码*";
            layoutControlItem9.TextSize = new Size(55, 14);
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.AllowHotTrack = false;
            emptySpaceItem1.Location = new Point(0, 213);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new Size(564, 252);
            emptySpaceItem1.TextSize = new Size(0, 0);
            // 
            // layoutControlGroup5
            // 
            layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { emptySpaceItem4 });
            layoutControlGroup5.Location = new Point(0, 0);
            layoutControlGroup5.Name = "layoutControlGroup4";
            layoutControlGroup5.Size = new Size(410, 237);
            layoutControlGroup5.Text = "PLC";
            // 
            // emptySpaceItem4
            // 
            emptySpaceItem4.AllowHotTrack = false;
            emptySpaceItem4.Location = new Point(0, 0);
            emptySpaceItem4.Name = "emptySpaceItem3";
            emptySpaceItem4.Size = new Size(402, 211);
            emptySpaceItem4.TextSize = new Size(0, 0);
            // 
            // CustomDataEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 511);
            Controls.Add(layoutControl1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CustomDataEditForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "CustomDataEditForm";
            ((ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((ISupportInitialize)treeListLookUpEdit1.Properties).EndInit();
            ((ISupportInitialize)treeListLookUpEdit1TreeList).EndInit();
            ((ISupportInitialize)textEdit1.Properties).EndInit();
            ((ISupportInitialize)spinEdit1.Properties).EndInit();
            ((ISupportInitialize)comboBoxEdit1.Properties).EndInit();
            ((ISupportInitialize)spinEdit2.Properties).EndInit();
            ((ISupportInitialize)comboBoxEdit2.Properties).EndInit();
            ((ISupportInitialize)textEdit2.Properties).EndInit();
            ((ISupportInitialize)Root).EndInit();
            ((ISupportInitialize)layoutControlItem3).EndInit();
            ((ISupportInitialize)layoutControlItem4).EndInit();
            ((ISupportInitialize)emptySpaceItem2).EndInit();
            ((ISupportInitialize)layoutControlGroup2).EndInit();
            ((ISupportInitialize)layoutControlItem1).EndInit();
            ((ISupportInitialize)layoutControlItem2).EndInit();
            ((ISupportInitialize)layoutControlItem7).EndInit();
            ((ISupportInitialize)layoutControlItem5).EndInit();
            ((ISupportInitialize)layoutControlItem8).EndInit();
            ((ISupportInitialize)layoutControlItem6).EndInit();
            ((ISupportInitialize)layoutControlItem9).EndInit();
            ((ISupportInitialize)emptySpaceItem1).EndInit();
            ((ISupportInitialize)layoutControlGroup5).EndInit();
            ((ISupportInitialize)emptySpaceItem4).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraEditors.SpinEdit spinEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SpinEdit spinEdit2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.TreeListLookUpEdit treeListLookUpEdit1;
        private DevExpress.XtraTreeList.TreeList treeListLookUpEdit1TreeList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
    }
}