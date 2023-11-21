namespace EasyPlc.Entry.ChrildrenForms;

partial class OrderEditForm
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
        ComponentResourceManager resources = new ComponentResourceManager(typeof(OrderEditForm));
        layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
        simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
        simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
        textEdit1 = new DevExpress.XtraEditors.TextEdit();
        simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
        comboBoxEdit2 = new DevExpress.XtraEditors.ComboBoxEdit();
        spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
        textEdit2 = new DevExpress.XtraEditors.TextEdit();
        comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
        Root = new DevExpress.XtraLayout.LayoutControlGroup();
        layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
        layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
        emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
        emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
        layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
        layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
        layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
        layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
        layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
        layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
        ((ISupportInitialize)layoutControl1).BeginInit();
        layoutControl1.SuspendLayout();
        ((ISupportInitialize)textEdit1.Properties).BeginInit();
        ((ISupportInitialize)comboBoxEdit2.Properties).BeginInit();
        ((ISupportInitialize)spinEdit1.Properties).BeginInit();
        ((ISupportInitialize)textEdit2.Properties).BeginInit();
        ((ISupportInitialize)comboBoxEdit1.Properties).BeginInit();
        ((ISupportInitialize)Root).BeginInit();
        ((ISupportInitialize)layoutControlItem2).BeginInit();
        ((ISupportInitialize)layoutControlItem1).BeginInit();
        ((ISupportInitialize)emptySpaceItem2).BeginInit();
        ((ISupportInitialize)emptySpaceItem1).BeginInit();
        ((ISupportInitialize)layoutControlItem3).BeginInit();
        ((ISupportInitialize)layoutControlItem4).BeginInit();
        ((ISupportInitialize)layoutControlItem6).BeginInit();
        ((ISupportInitialize)layoutControlItem7).BeginInit();
        ((ISupportInitialize)layoutControlItem5).BeginInit();
        ((ISupportInitialize)layoutControlItem8).BeginInit();
        SuspendLayout();
        // 
        // layoutControl1
        // 
        layoutControl1.Controls.Add(simpleButton1);
        layoutControl1.Controls.Add(simpleButton2);
        layoutControl1.Controls.Add(textEdit1);
        layoutControl1.Controls.Add(simpleButton3);
        layoutControl1.Controls.Add(comboBoxEdit2);
        layoutControl1.Controls.Add(spinEdit1);
        layoutControl1.Controls.Add(textEdit2);
        layoutControl1.Controls.Add(comboBoxEdit1);
        layoutControl1.Dock = DockStyle.Fill;
        layoutControl1.Location = new Point(0, 0);
        layoutControl1.Name = "layoutControl1";
        layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(737, 147, 650, 400);
        layoutControl1.Root = Root;
        layoutControl1.Size = new Size(419, 405);
        layoutControl1.TabIndex = 0;
        layoutControl1.Text = "layoutControl1";
        // 
        // simpleButton1
        // 
        simpleButton1.ImageOptions.Image = (Image)resources.GetObject("simpleButton1.ImageOptions.Image");
        simpleButton1.Location = new Point(285, 371);
        simpleButton1.Name = "simpleButton1";
        simpleButton1.Size = new Size(57, 22);
        simpleButton1.StyleController = layoutControl1;
        simpleButton1.TabIndex = 7;
        simpleButton1.Text = "确定";
        simpleButton1.Click += simpleButton1_Click;
        // 
        // simpleButton2
        // 
        simpleButton2.ImageOptions.Image = (Image)resources.GetObject("simpleButton2.ImageOptions.Image");
        simpleButton2.Location = new Point(346, 371);
        simpleButton2.Name = "simpleButton2";
        simpleButton2.Size = new Size(61, 22);
        simpleButton2.StyleController = layoutControl1;
        simpleButton2.TabIndex = 8;
        simpleButton2.Text = "取消";
        simpleButton2.Click += simpleButton2_Click;
        // 
        // textEdit1
        // 
        textEdit1.Location = new Point(91, 36);
        textEdit1.Name = "textEdit1";
        textEdit1.Size = new Size(316, 20);
        textEdit1.StyleController = layoutControl1;
        textEdit1.TabIndex = 2;
        // 
        // simpleButton3
        // 
        simpleButton3.Appearance.ForeColor = Color.Green;
        simpleButton3.Appearance.Options.UseForeColor = true;
        simpleButton3.Location = new Point(12, 60);
        simpleButton3.Name = "simpleButton3";
        simpleButton3.Size = new Size(395, 22);
        simpleButton3.StyleController = layoutControl1;
        simpleButton3.TabIndex = 3;
        simpleButton3.Text = "依据车间订单号获取MES信息";
        simpleButton3.Click += simpleButton3_Click;
        // 
        // comboBoxEdit2
        // 
        comboBoxEdit2.Location = new Point(91, 86);
        comboBoxEdit2.Name = "comboBoxEdit2";
        comboBoxEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
        comboBoxEdit2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
        comboBoxEdit2.Size = new Size(316, 20);
        comboBoxEdit2.StyleController = layoutControl1;
        comboBoxEdit2.TabIndex = 4;
        // 
        // spinEdit1
        // 
        spinEdit1.EditValue = new decimal(new int[] { 1, 0, 0, 0 });
        spinEdit1.Location = new Point(91, 134);
        spinEdit1.Name = "spinEdit1";
        spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
        spinEdit1.Properties.MaskSettings.Set("mask", "d");
        spinEdit1.Properties.MaxValue = new decimal(new int[] { 100000, 0, 0, 0 });
        spinEdit1.Properties.MinValue = new decimal(new int[] { 1, 0, 0, 0 });
        spinEdit1.Size = new Size(316, 20);
        spinEdit1.StyleController = layoutControl1;
        spinEdit1.TabIndex = 6;
        // 
        // textEdit2
        // 
        textEdit2.Location = new Point(91, 110);
        textEdit2.Name = "textEdit2";
        textEdit2.Size = new Size(316, 20);
        textEdit2.StyleController = layoutControl1;
        textEdit2.TabIndex = 5;
        // 
        // comboBoxEdit1
        // 
        comboBoxEdit1.Location = new Point(91, 12);
        comboBoxEdit1.Name = "comboBoxEdit1";
        comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
        comboBoxEdit1.Properties.Items.AddRange(new object[] { "正常工单" });
        comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
        comboBoxEdit1.Size = new Size(316, 20);
        comboBoxEdit1.StyleController = layoutControl1;
        comboBoxEdit1.TabIndex = 0;
        comboBoxEdit1.SelectedIndexChanged += comboBoxEdit1_SelectedIndexChanged;
        // 
        // Root
        // 
        Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
        Root.GroupBordersVisible = false;
        Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2, layoutControlItem1, emptySpaceItem2, emptySpaceItem1, layoutControlItem3, layoutControlItem4, layoutControlItem6, layoutControlItem7, layoutControlItem5, layoutControlItem8 });
        Root.Name = "Root";
        Root.Size = new Size(419, 405);
        Root.TextVisible = false;
        // 
        // layoutControlItem2
        // 
        layoutControlItem2.Control = simpleButton2;
        layoutControlItem2.Location = new Point(334, 359);
        layoutControlItem2.Name = "layoutControlItem2";
        layoutControlItem2.Size = new Size(65, 26);
        layoutControlItem2.TextSize = new Size(0, 0);
        layoutControlItem2.TextVisible = false;
        // 
        // layoutControlItem1
        // 
        layoutControlItem1.Control = simpleButton1;
        layoutControlItem1.Location = new Point(273, 359);
        layoutControlItem1.Name = "layoutControlItem1";
        layoutControlItem1.Size = new Size(61, 26);
        layoutControlItem1.TextSize = new Size(0, 0);
        layoutControlItem1.TextVisible = false;
        // 
        // emptySpaceItem2
        // 
        emptySpaceItem2.AllowHotTrack = false;
        emptySpaceItem2.Location = new Point(0, 146);
        emptySpaceItem2.Name = "emptySpaceItem2";
        emptySpaceItem2.Size = new Size(399, 213);
        emptySpaceItem2.TextSize = new Size(0, 0);
        // 
        // emptySpaceItem1
        // 
        emptySpaceItem1.AllowHotTrack = false;
        emptySpaceItem1.Location = new Point(0, 359);
        emptySpaceItem1.Name = "emptySpaceItem1";
        emptySpaceItem1.Size = new Size(273, 26);
        emptySpaceItem1.TextSize = new Size(0, 0);
        // 
        // layoutControlItem3
        // 
        layoutControlItem3.Control = textEdit1;
        layoutControlItem3.Location = new Point(0, 24);
        layoutControlItem3.Name = "layoutControlItem3";
        layoutControlItem3.Size = new Size(399, 24);
        layoutControlItem3.Text = "车间订单号*";
        layoutControlItem3.TextSize = new Size(67, 14);
        // 
        // layoutControlItem4
        // 
        layoutControlItem4.Control = simpleButton3;
        layoutControlItem4.Location = new Point(0, 48);
        layoutControlItem4.Name = "layoutControlItem4";
        layoutControlItem4.Size = new Size(399, 26);
        layoutControlItem4.TextSize = new Size(0, 0);
        layoutControlItem4.TextVisible = false;
        // 
        // layoutControlItem6
        // 
        layoutControlItem6.Control = comboBoxEdit2;
        layoutControlItem6.Location = new Point(0, 74);
        layoutControlItem6.Name = "layoutControlItem6";
        layoutControlItem6.Size = new Size(399, 24);
        layoutControlItem6.Text = "工艺路线*";
        layoutControlItem6.TextSize = new Size(67, 14);
        // 
        // layoutControlItem7
        // 
        layoutControlItem7.Control = spinEdit1;
        layoutControlItem7.Location = new Point(0, 122);
        layoutControlItem7.Name = "layoutControlItem7";
        layoutControlItem7.Size = new Size(399, 24);
        layoutControlItem7.Text = "计划数量*";
        layoutControlItem7.TextSize = new Size(67, 14);
        // 
        // layoutControlItem5
        // 
        layoutControlItem5.Control = textEdit2;
        layoutControlItem5.Location = new Point(0, 98);
        layoutControlItem5.Name = "layoutControlItem5";
        layoutControlItem5.Size = new Size(399, 24);
        layoutControlItem5.Text = "批次号*";
        layoutControlItem5.TextSize = new Size(67, 14);
        // 
        // layoutControlItem8
        // 
        layoutControlItem8.Control = comboBoxEdit1;
        layoutControlItem8.Location = new Point(0, 0);
        layoutControlItem8.Name = "layoutControlItem8";
        layoutControlItem8.Size = new Size(399, 24);
        layoutControlItem8.Text = "工单模式*";
        layoutControlItem8.TextSize = new Size(67, 14);
        // 
        // OrderEditForm
        // 
        AutoScaleDimensions = new SizeF(7F, 14F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(419, 405);
        Controls.Add(layoutControl1);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "OrderEditForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "OrderEditForm";
        Load += OrderEditForm_Load;
        ((ISupportInitialize)layoutControl1).EndInit();
        layoutControl1.ResumeLayout(false);
        ((ISupportInitialize)textEdit1.Properties).EndInit();
        ((ISupportInitialize)comboBoxEdit2.Properties).EndInit();
        ((ISupportInitialize)spinEdit1.Properties).EndInit();
        ((ISupportInitialize)textEdit2.Properties).EndInit();
        ((ISupportInitialize)comboBoxEdit1.Properties).EndInit();
        ((ISupportInitialize)Root).EndInit();
        ((ISupportInitialize)layoutControlItem2).EndInit();
        ((ISupportInitialize)layoutControlItem1).EndInit();
        ((ISupportInitialize)emptySpaceItem2).EndInit();
        ((ISupportInitialize)emptySpaceItem1).EndInit();
        ((ISupportInitialize)layoutControlItem3).EndInit();
        ((ISupportInitialize)layoutControlItem4).EndInit();
        ((ISupportInitialize)layoutControlItem6).EndInit();
        ((ISupportInitialize)layoutControlItem7).EndInit();
        ((ISupportInitialize)layoutControlItem5).EndInit();
        ((ISupportInitialize)layoutControlItem8).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private DevExpress.XtraLayout.LayoutControl layoutControl1;
    private DevExpress.XtraEditors.SimpleButton simpleButton1;
    private DevExpress.XtraEditors.SimpleButton simpleButton2;
    private DevExpress.XtraLayout.LayoutControlGroup Root;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    private DevExpress.XtraEditors.TextEdit textEdit1;
    private DevExpress.XtraEditors.SimpleButton simpleButton3;
    private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit2;
    private DevExpress.XtraEditors.SpinEdit spinEdit1;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
    private DevExpress.XtraEditors.TextEdit textEdit2;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
}