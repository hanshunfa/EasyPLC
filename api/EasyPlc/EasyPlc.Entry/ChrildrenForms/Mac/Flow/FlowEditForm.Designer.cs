namespace EasyPlc.Entry.ChrildrenForms;

partial class FlowEditForm
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
        ComponentResourceManager resources = new ComponentResourceManager(typeof(FlowEditForm));
        layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
        toggleSwitch1 = new DevExpress.XtraEditors.ToggleSwitch();
        treeList1 = new DevExpress.XtraTreeList.TreeList();
        treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
        treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
        treeListLookUpEdit1 = new DevExpress.XtraEditors.TreeListLookUpEdit();
        treeListLookUpEdit1TreeList = new DevExpress.XtraTreeList.TreeList();
        treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
        textEdit1 = new DevExpress.XtraEditors.TextEdit();
        comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
        spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
        simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
        simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
        comboBoxEdit2 = new DevExpress.XtraEditors.ComboBoxEdit();
        Root = new DevExpress.XtraLayout.LayoutControlGroup();
        layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
        layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
        layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
        emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
        layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
        layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
        layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
        layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
        layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
        layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
        splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
        layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
        layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
        emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
        ((ISupportInitialize)layoutControl1).BeginInit();
        layoutControl1.SuspendLayout();
        ((ISupportInitialize)toggleSwitch1.Properties).BeginInit();
        ((ISupportInitialize)treeList1).BeginInit();
        ((ISupportInitialize)treeListLookUpEdit1.Properties).BeginInit();
        ((ISupportInitialize)treeListLookUpEdit1TreeList).BeginInit();
        ((ISupportInitialize)textEdit1.Properties).BeginInit();
        ((ISupportInitialize)comboBoxEdit1.Properties).BeginInit();
        ((ISupportInitialize)spinEdit1.Properties).BeginInit();
        ((ISupportInitialize)comboBoxEdit2.Properties).BeginInit();
        ((ISupportInitialize)Root).BeginInit();
        ((ISupportInitialize)layoutControlGroup1).BeginInit();
        ((ISupportInitialize)layoutControlItem1).BeginInit();
        ((ISupportInitialize)layoutControlItem3).BeginInit();
        ((ISupportInitialize)emptySpaceItem1).BeginInit();
        ((ISupportInitialize)layoutControlItem4).BeginInit();
        ((ISupportInitialize)layoutControlItem5).BeginInit();
        ((ISupportInitialize)layoutControlItem8).BeginInit();
        ((ISupportInitialize)layoutControlItem9).BeginInit();
        ((ISupportInitialize)layoutControlGroup2).BeginInit();
        ((ISupportInitialize)layoutControlItem2).BeginInit();
        ((ISupportInitialize)splitterItem1).BeginInit();
        ((ISupportInitialize)layoutControlItem6).BeginInit();
        ((ISupportInitialize)layoutControlItem7).BeginInit();
        ((ISupportInitialize)emptySpaceItem2).BeginInit();
        SuspendLayout();
        // 
        // layoutControl1
        // 
        layoutControl1.Controls.Add(toggleSwitch1);
        layoutControl1.Controls.Add(treeList1);
        layoutControl1.Controls.Add(treeListLookUpEdit1);
        layoutControl1.Controls.Add(textEdit1);
        layoutControl1.Controls.Add(comboBoxEdit1);
        layoutControl1.Controls.Add(spinEdit1);
        layoutControl1.Controls.Add(simpleButton1);
        layoutControl1.Controls.Add(simpleButton2);
        layoutControl1.Controls.Add(comboBoxEdit2);
        layoutControl1.Dock = DockStyle.Fill;
        layoutControl1.Location = new Point(0, 0);
        layoutControl1.Name = "layoutControl1";
        layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(940, 220, 650, 400);
        layoutControl1.Root = Root;
        layoutControl1.Size = new Size(908, 619);
        layoutControl1.TabIndex = 0;
        layoutControl1.Text = "layoutControl1";
        // 
        // toggleSwitch1
        // 
        toggleSwitch1.Location = new Point(24, 165);
        toggleSwitch1.Name = "toggleSwitch1";
        toggleSwitch1.Properties.OffText = "不包含返修";
        toggleSwitch1.Properties.OnText = "包含返修";
        toggleSwitch1.Size = new Size(413, 19);
        toggleSwitch1.StyleController = layoutControl1;
        toggleSwitch1.TabIndex = 8;
        // 
        // treeList1
        // 
        treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] { treeListColumn1, treeListColumn2 });
        treeList1.Location = new Point(475, 45);
        treeList1.Name = "treeList1";
        treeList1.Size = new Size(409, 524);
        treeList1.TabIndex = 5;
        treeList1.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeView;
        treeList1.BeforeCheckNode += treeList1_BeforeCheckNode;
        treeList1.AfterCheckNode += treeList1_AfterCheckNode;
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
        treeListColumn2.Caption = "Name";
        treeListColumn2.FieldName = "Name";
        treeListColumn2.Name = "treeListColumn2";
        treeListColumn2.Visible = true;
        treeListColumn2.VisibleIndex = 0;
        // 
        // treeListLookUpEdit1
        // 
        treeListLookUpEdit1.EditValue = "Id";
        treeListLookUpEdit1.Location = new Point(91, 45);
        treeListLookUpEdit1.Name = "treeListLookUpEdit1";
        treeListLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
        treeListLookUpEdit1.Properties.DisplayMember = "Name";
        treeListLookUpEdit1.Properties.TreeList = treeListLookUpEdit1TreeList;
        treeListLookUpEdit1.Properties.ValueMember = "Id";
        treeListLookUpEdit1.Size = new Size(346, 20);
        treeListLookUpEdit1.StyleController = layoutControl1;
        treeListLookUpEdit1.TabIndex = 0;
        treeListLookUpEdit1.EditValueChanged += treeListLookUpEdit1_EditValueChanged;
        // 
        // treeListLookUpEdit1TreeList
        // 
        treeListLookUpEdit1TreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] { treeListColumn3 });
        treeListLookUpEdit1TreeList.KeyFieldName = "Id";
        treeListLookUpEdit1TreeList.Location = new Point(0, 0);
        treeListLookUpEdit1TreeList.Name = "treeListLookUpEdit1TreeList";
        treeListLookUpEdit1TreeList.OptionsView.ShowIndentAsRowStyle = true;
        treeListLookUpEdit1TreeList.ParentFieldName = "ParentId";
        treeListLookUpEdit1TreeList.Size = new Size(400, 200);
        treeListLookUpEdit1TreeList.TabIndex = 0;
        treeListLookUpEdit1TreeList.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeView;
        // 
        // treeListColumn3
        // 
        treeListColumn3.Caption = "名称";
        treeListColumn3.FieldName = "Name";
        treeListColumn3.Name = "treeListColumn3";
        treeListColumn3.Visible = true;
        treeListColumn3.VisibleIndex = 0;
        // 
        // textEdit1
        // 
        textEdit1.Location = new Point(91, 69);
        textEdit1.Name = "textEdit1";
        textEdit1.Size = new Size(346, 20);
        textEdit1.StyleController = layoutControl1;
        textEdit1.TabIndex = 2;
        // 
        // comboBoxEdit1
        // 
        comboBoxEdit1.Location = new Point(91, 93);
        comboBoxEdit1.Name = "comboBoxEdit1";
        comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
        comboBoxEdit1.Properties.Items.AddRange(new object[] { "正常流程", "返修流程" });
        comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
        comboBoxEdit1.Size = new Size(346, 20);
        comboBoxEdit1.StyleController = layoutControl1;
        comboBoxEdit1.TabIndex = 3;
        comboBoxEdit1.SelectedValueChanged += comboBoxEdit1_SelectedValueChanged;
        // 
        // spinEdit1
        // 
        spinEdit1.EditValue = new decimal(new int[] { 1, 0, 0, 0 });
        spinEdit1.Location = new Point(91, 141);
        spinEdit1.Name = "spinEdit1";
        spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
        spinEdit1.Properties.MaskSettings.Set("mask", "d");
        spinEdit1.Properties.MaxValue = new decimal(new int[] { 99, 0, 0, 0 });
        spinEdit1.Properties.MinValue = new decimal(new int[] { 1, 0, 0, 0 });
        spinEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
        spinEdit1.Size = new Size(346, 20);
        spinEdit1.StyleController = layoutControl1;
        spinEdit1.TabIndex = 4;
        // 
        // simpleButton1
        // 
        simpleButton1.ImageOptions.Image = (Image)resources.GetObject("simpleButton1.ImageOptions.Image");
        simpleButton1.Location = new Point(640, 585);
        simpleButton1.Name = "simpleButton1";
        simpleButton1.Size = new Size(125, 22);
        simpleButton1.StyleController = layoutControl1;
        simpleButton1.TabIndex = 6;
        simpleButton1.Text = "确定";
        simpleButton1.Click += simpleButton1_Click;
        // 
        // simpleButton2
        // 
        simpleButton2.ImageOptions.Image = (Image)resources.GetObject("simpleButton2.ImageOptions.Image");
        simpleButton2.Location = new Point(769, 585);
        simpleButton2.Name = "simpleButton2";
        simpleButton2.Size = new Size(127, 22);
        simpleButton2.StyleController = layoutControl1;
        simpleButton2.TabIndex = 7;
        simpleButton2.Text = "取消";
        simpleButton2.Click += simpleButton2_Click;
        // 
        // comboBoxEdit2
        // 
        comboBoxEdit2.Location = new Point(91, 117);
        comboBoxEdit2.Name = "comboBoxEdit2";
        comboBoxEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
        comboBoxEdit2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
        comboBoxEdit2.Size = new Size(346, 20);
        comboBoxEdit2.StyleController = layoutControl1;
        comboBoxEdit2.TabIndex = 9;
        comboBoxEdit2.SelectedValueChanged += comboBoxEdit2_SelectedValueChanged;
        // 
        // Root
        // 
        Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
        Root.GroupBordersVisible = false;
        Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup1, layoutControlGroup2, splitterItem1, layoutControlItem6, layoutControlItem7, emptySpaceItem2 });
        Root.Name = "Root";
        Root.Size = new Size(908, 619);
        Root.TextVisible = false;
        // 
        // layoutControlGroup1
        // 
        layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, layoutControlItem3, emptySpaceItem1, layoutControlItem4, layoutControlItem5, layoutControlItem8, layoutControlItem9 });
        layoutControlGroup1.Location = new Point(0, 0);
        layoutControlGroup1.Name = "layoutControlGroup1";
        layoutControlGroup1.Size = new Size(441, 573);
        layoutControlGroup1.Text = "基础配置";
        // 
        // layoutControlItem1
        // 
        layoutControlItem1.Control = treeListLookUpEdit1;
        layoutControlItem1.CustomizationFormText = "所属流程*";
        layoutControlItem1.Location = new Point(0, 0);
        layoutControlItem1.Name = "layoutControlItem1";
        layoutControlItem1.Size = new Size(417, 24);
        layoutControlItem1.Text = "所属流程*";
        layoutControlItem1.TextSize = new Size(55, 14);
        // 
        // layoutControlItem3
        // 
        layoutControlItem3.Control = textEdit1;
        layoutControlItem3.CustomizationFormText = "名称*";
        layoutControlItem3.Location = new Point(0, 24);
        layoutControlItem3.Name = "layoutControlItem3";
        layoutControlItem3.Size = new Size(417, 24);
        layoutControlItem3.Text = "流程名称";
        layoutControlItem3.TextSize = new Size(55, 14);
        // 
        // emptySpaceItem1
        // 
        emptySpaceItem1.AllowHotTrack = false;
        emptySpaceItem1.Location = new Point(0, 143);
        emptySpaceItem1.Name = "emptySpaceItem1";
        emptySpaceItem1.Size = new Size(417, 385);
        emptySpaceItem1.TextSize = new Size(0, 0);
        // 
        // layoutControlItem4
        // 
        layoutControlItem4.Control = comboBoxEdit1;
        layoutControlItem4.CustomizationFormText = "分类*";
        layoutControlItem4.Location = new Point(0, 48);
        layoutControlItem4.Name = "layoutControlItem4";
        layoutControlItem4.Size = new Size(417, 24);
        layoutControlItem4.Text = "分类";
        layoutControlItem4.TextSize = new Size(55, 14);
        // 
        // layoutControlItem5
        // 
        layoutControlItem5.Control = spinEdit1;
        layoutControlItem5.Location = new Point(0, 96);
        layoutControlItem5.Name = "layoutControlItem5";
        layoutControlItem5.Size = new Size(417, 24);
        layoutControlItem5.Text = "排序*";
        layoutControlItem5.TextSize = new Size(55, 14);
        // 
        // layoutControlItem8
        // 
        layoutControlItem8.Control = toggleSwitch1;
        layoutControlItem8.Location = new Point(0, 120);
        layoutControlItem8.Name = "layoutControlItem8";
        layoutControlItem8.Size = new Size(417, 23);
        layoutControlItem8.TextSize = new Size(0, 0);
        layoutControlItem8.TextVisible = false;
        // 
        // layoutControlItem9
        // 
        layoutControlItem9.Control = comboBoxEdit2;
        layoutControlItem9.Location = new Point(0, 72);
        layoutControlItem9.Name = "layoutControlItem9";
        layoutControlItem9.Size = new Size(417, 24);
        layoutControlItem9.Text = "所属型号*";
        layoutControlItem9.TextSize = new Size(55, 14);
        layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        // 
        // layoutControlGroup2
        // 
        layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2 });
        layoutControlGroup2.Location = new Point(451, 0);
        layoutControlGroup2.Name = "layoutControlGroup2";
        layoutControlGroup2.Size = new Size(437, 573);
        layoutControlGroup2.Text = "工位选择";
        // 
        // layoutControlItem2
        // 
        layoutControlItem2.Control = treeList1;
        layoutControlItem2.Location = new Point(0, 0);
        layoutControlItem2.Name = "layoutControlItem2";
        layoutControlItem2.Size = new Size(413, 528);
        layoutControlItem2.TextSize = new Size(0, 0);
        layoutControlItem2.TextVisible = false;
        // 
        // splitterItem1
        // 
        splitterItem1.AllowHotTrack = true;
        splitterItem1.Location = new Point(441, 0);
        splitterItem1.Name = "splitterItem1";
        splitterItem1.Size = new Size(10, 573);
        // 
        // layoutControlItem6
        // 
        layoutControlItem6.Control = simpleButton1;
        layoutControlItem6.Location = new Point(628, 573);
        layoutControlItem6.Name = "layoutControlItem6";
        layoutControlItem6.Size = new Size(129, 26);
        layoutControlItem6.TextSize = new Size(0, 0);
        layoutControlItem6.TextVisible = false;
        // 
        // layoutControlItem7
        // 
        layoutControlItem7.Control = simpleButton2;
        layoutControlItem7.Location = new Point(757, 573);
        layoutControlItem7.Name = "layoutControlItem7";
        layoutControlItem7.Size = new Size(131, 26);
        layoutControlItem7.TextSize = new Size(0, 0);
        layoutControlItem7.TextVisible = false;
        // 
        // emptySpaceItem2
        // 
        emptySpaceItem2.AllowHotTrack = false;
        emptySpaceItem2.Location = new Point(0, 573);
        emptySpaceItem2.Name = "emptySpaceItem2";
        emptySpaceItem2.Size = new Size(628, 26);
        emptySpaceItem2.TextSize = new Size(0, 0);
        // 
        // FlowEditForm
        // 
        AutoScaleDimensions = new SizeF(7F, 14F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(908, 619);
        Controls.Add(layoutControl1);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FlowEditForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "FlowEditForm";
        Load += FlowEditForm_Load;
        ((ISupportInitialize)layoutControl1).EndInit();
        layoutControl1.ResumeLayout(false);
        ((ISupportInitialize)toggleSwitch1.Properties).EndInit();
        ((ISupportInitialize)treeList1).EndInit();
        ((ISupportInitialize)treeListLookUpEdit1.Properties).EndInit();
        ((ISupportInitialize)treeListLookUpEdit1TreeList).EndInit();
        ((ISupportInitialize)textEdit1.Properties).EndInit();
        ((ISupportInitialize)comboBoxEdit1.Properties).EndInit();
        ((ISupportInitialize)spinEdit1.Properties).EndInit();
        ((ISupportInitialize)comboBoxEdit2.Properties).EndInit();
        ((ISupportInitialize)Root).EndInit();
        ((ISupportInitialize)layoutControlGroup1).EndInit();
        ((ISupportInitialize)layoutControlItem1).EndInit();
        ((ISupportInitialize)layoutControlItem3).EndInit();
        ((ISupportInitialize)emptySpaceItem1).EndInit();
        ((ISupportInitialize)layoutControlItem4).EndInit();
        ((ISupportInitialize)layoutControlItem5).EndInit();
        ((ISupportInitialize)layoutControlItem8).EndInit();
        ((ISupportInitialize)layoutControlItem9).EndInit();
        ((ISupportInitialize)layoutControlGroup2).EndInit();
        ((ISupportInitialize)layoutControlItem2).EndInit();
        ((ISupportInitialize)splitterItem1).EndInit();
        ((ISupportInitialize)layoutControlItem6).EndInit();
        ((ISupportInitialize)layoutControlItem7).EndInit();
        ((ISupportInitialize)emptySpaceItem2).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private DevExpress.XtraLayout.LayoutControl layoutControl1;
    private DevExpress.XtraLayout.LayoutControlGroup Root;
    private DevExpress.XtraEditors.TreeListLookUpEdit treeListLookUpEdit1;
    private DevExpress.XtraTreeList.TreeList treeListLookUpEdit1TreeList;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    private DevExpress.XtraTreeList.TreeList treeList1;
    private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    private DevExpress.XtraLayout.SplitterItem splitterItem1;
    private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
    private DevExpress.XtraEditors.TextEdit textEdit1;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
    private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    private DevExpress.XtraEditors.SpinEdit spinEdit1;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    private DevExpress.XtraEditors.SimpleButton simpleButton1;
    private DevExpress.XtraEditors.SimpleButton simpleButton2;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
    private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    private DevExpress.XtraEditors.ToggleSwitch toggleSwitch1;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
    private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit2;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
    private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
    private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
    private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
}