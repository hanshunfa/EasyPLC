namespace EasyPlc.Entry.ChrildrenForms;

partial class FixedScanEditForm
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
        ComponentResourceManager resources = new ComponentResourceManager(typeof(FixedScanEditForm));
        layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
        simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
        simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
        textEdit1 = new DevExpress.XtraEditors.TextEdit();
        textEdit2 = new DevExpress.XtraEditors.TextEdit();
        spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
        spinEdit3 = new DevExpress.XtraEditors.SpinEdit();
        Root = new DevExpress.XtraLayout.LayoutControlGroup();
        layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
        emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
        emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
        layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
        layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
        layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
        layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
        layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
        ((ISupportInitialize)layoutControl1).BeginInit();
        layoutControl1.SuspendLayout();
        ((ISupportInitialize)textEdit1.Properties).BeginInit();
        ((ISupportInitialize)textEdit2.Properties).BeginInit();
        ((ISupportInitialize)spinEdit1.Properties).BeginInit();
        ((ISupportInitialize)spinEdit3.Properties).BeginInit();
        ((ISupportInitialize)Root).BeginInit();
        ((ISupportInitialize)layoutControlItem2).BeginInit();
        ((ISupportInitialize)emptySpaceItem3).BeginInit();
        ((ISupportInitialize)emptySpaceItem1).BeginInit();
        ((ISupportInitialize)layoutControlItem1).BeginInit();
        ((ISupportInitialize)layoutControlItem3).BeginInit();
        ((ISupportInitialize)layoutControlItem4).BeginInit();
        ((ISupportInitialize)layoutControlItem5).BeginInit();
        ((ISupportInitialize)layoutControlItem7).BeginInit();
        SuspendLayout();
        // 
        // layoutControl1
        // 
        layoutControl1.Controls.Add(simpleButton1);
        layoutControl1.Controls.Add(simpleButton2);
        layoutControl1.Controls.Add(textEdit1);
        layoutControl1.Controls.Add(textEdit2);
        layoutControl1.Controls.Add(spinEdit1);
        layoutControl1.Controls.Add(spinEdit3);
        layoutControl1.Dock = DockStyle.Fill;
        layoutControl1.Location = new Point(0, 0);
        layoutControl1.Name = "layoutControl1";
        layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(851, 171, 650, 400);
        layoutControl1.Root = Root;
        layoutControl1.Size = new Size(354, 254);
        layoutControl1.TabIndex = 0;
        layoutControl1.Text = "layoutControl1";
        // 
        // simpleButton1
        // 
        simpleButton1.ImageOptions.Image = (Image)resources.GetObject("simpleButton1.ImageOptions.Image");
        simpleButton1.Location = new Point(197, 220);
        simpleButton1.Name = "simpleButton1";
        simpleButton1.Size = new Size(69, 22);
        simpleButton1.StyleController = layoutControl1;
        simpleButton1.TabIndex = 2;
        simpleButton1.Text = "确定";
        simpleButton1.Click += simpleButton1_Click;
        // 
        // simpleButton2
        // 
        simpleButton2.ImageOptions.Image = (Image)resources.GetObject("simpleButton2.ImageOptions.Image");
        simpleButton2.Location = new Point(270, 220);
        simpleButton2.Name = "simpleButton2";
        simpleButton2.Size = new Size(72, 22);
        simpleButton2.StyleController = layoutControl1;
        simpleButton2.TabIndex = 3;
        simpleButton2.Text = "取消";
        simpleButton2.Click += simpleButton2_Click;
        // 
        // textEdit1
        // 
        textEdit1.Location = new Point(67, 12);
        textEdit1.Name = "textEdit1";
        textEdit1.Size = new Size(275, 20);
        textEdit1.StyleController = layoutControl1;
        textEdit1.TabIndex = 0;
        // 
        // textEdit2
        // 
        textEdit2.EditValue = "192.168.99.254";
        textEdit2.Location = new Point(67, 36);
        textEdit2.Name = "textEdit2";
        textEdit2.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.RegExpMaskManager));
        textEdit2.Properties.MaskSettings.Set("MaskManagerSignature", "isOptimistic=False");
        textEdit2.Properties.MaskSettings.Set("mask", "(([01]?[0-9]?[0-9])|(2[0-4][0-9])|(25[0-5]))\\.(([01]?[0-9]?[0-9])|(2[0-4][0-9])|(25[0-5]))\\.(([01]?[0-9]?[0-9])|(2[0-4][0-9])|(25[0-5]))\\.(([01]?[0-9]?[0-9])|(2[0-4][0-9])|(25[0-5]))");
        textEdit2.Size = new Size(275, 20);
        textEdit2.StyleController = layoutControl1;
        textEdit2.TabIndex = 4;
        // 
        // spinEdit1
        // 
        spinEdit1.EditValue = new decimal(new int[] { 12345, 0, 0, 0 });
        spinEdit1.Location = new Point(67, 60);
        spinEdit1.Name = "spinEdit1";
        spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
        spinEdit1.Properties.MaskSettings.Set("mask", "d");
        spinEdit1.Properties.MaxValue = new decimal(new int[] { 65535, 0, 0, 0 });
        spinEdit1.Properties.MinValue = new decimal(new int[] { 1000, 0, 0, 0 });
        spinEdit1.Size = new Size(275, 20);
        spinEdit1.StyleController = layoutControl1;
        spinEdit1.TabIndex = 5;
        // 
        // spinEdit3
        // 
        spinEdit3.EditValue = new decimal(new int[] { 99, 0, 0, 0 });
        spinEdit3.Location = new Point(67, 84);
        spinEdit3.Name = "spinEdit3";
        spinEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
        spinEdit3.Properties.MaskSettings.Set("mask", "d");
        spinEdit3.Properties.MaxValue = new decimal(new int[] { 99, 0, 0, 0 });
        spinEdit3.Properties.MinValue = new decimal(new int[] { 1, 0, 0, 0 });
        spinEdit3.Size = new Size(275, 20);
        spinEdit3.StyleController = layoutControl1;
        spinEdit3.TabIndex = 7;
        // 
        // Root
        // 
        Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
        Root.GroupBordersVisible = false;
        Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2, emptySpaceItem3, emptySpaceItem1, layoutControlItem1, layoutControlItem3, layoutControlItem4, layoutControlItem5, layoutControlItem7 });
        Root.Name = "Root";
        Root.Size = new Size(354, 254);
        Root.TextVisible = false;
        // 
        // layoutControlItem2
        // 
        layoutControlItem2.Control = simpleButton2;
        layoutControlItem2.Location = new Point(258, 208);
        layoutControlItem2.Name = "layoutControlItem2";
        layoutControlItem2.Size = new Size(76, 26);
        layoutControlItem2.TextSize = new Size(0, 0);
        layoutControlItem2.TextVisible = false;
        // 
        // emptySpaceItem3
        // 
        emptySpaceItem3.AllowHotTrack = false;
        emptySpaceItem3.Location = new Point(0, 208);
        emptySpaceItem3.Name = "emptySpaceItem3";
        emptySpaceItem3.Size = new Size(185, 26);
        emptySpaceItem3.TextSize = new Size(0, 0);
        // 
        // emptySpaceItem1
        // 
        emptySpaceItem1.AllowHotTrack = false;
        emptySpaceItem1.Location = new Point(0, 96);
        emptySpaceItem1.Name = "emptySpaceItem1";
        emptySpaceItem1.Size = new Size(334, 112);
        emptySpaceItem1.TextSize = new Size(0, 0);
        // 
        // layoutControlItem1
        // 
        layoutControlItem1.Control = simpleButton1;
        layoutControlItem1.Location = new Point(185, 208);
        layoutControlItem1.Name = "layoutControlItem1";
        layoutControlItem1.Size = new Size(73, 26);
        layoutControlItem1.TextSize = new Size(0, 0);
        layoutControlItem1.TextVisible = false;
        // 
        // layoutControlItem3
        // 
        layoutControlItem3.Control = textEdit1;
        layoutControlItem3.CustomizationFormText = "名称*";
        layoutControlItem3.Location = new Point(0, 0);
        layoutControlItem3.Name = "layoutControlItem3";
        layoutControlItem3.Size = new Size(334, 24);
        layoutControlItem3.Text = "名称*";
        layoutControlItem3.TextSize = new Size(43, 14);
        // 
        // layoutControlItem4
        // 
        layoutControlItem4.Control = textEdit2;
        layoutControlItem4.Location = new Point(0, 24);
        layoutControlItem4.Name = "layoutControlItem4";
        layoutControlItem4.Size = new Size(334, 24);
        layoutControlItem4.Text = "IP地址*";
        layoutControlItem4.TextSize = new Size(43, 14);
        // 
        // layoutControlItem5
        // 
        layoutControlItem5.Control = spinEdit1;
        layoutControlItem5.Location = new Point(0, 48);
        layoutControlItem5.Name = "layoutControlItem5";
        layoutControlItem5.Size = new Size(334, 24);
        layoutControlItem5.Text = "端口号*";
        layoutControlItem5.TextSize = new Size(43, 14);
        // 
        // layoutControlItem7
        // 
        layoutControlItem7.Control = spinEdit3;
        layoutControlItem7.Location = new Point(0, 72);
        layoutControlItem7.Name = "layoutControlItem7";
        layoutControlItem7.Size = new Size(334, 24);
        layoutControlItem7.Text = "排序";
        layoutControlItem7.TextSize = new Size(43, 14);
        // 
        // FixedScanEditForm
        // 
        AutoScaleDimensions = new SizeF(7F, 14F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(354, 254);
        Controls.Add(layoutControl1);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FixedScanEditForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "FixedScanEditForm";
        Load += FixedScanEditForm_Load;
        ((ISupportInitialize)layoutControl1).EndInit();
        layoutControl1.ResumeLayout(false);
        ((ISupportInitialize)textEdit1.Properties).EndInit();
        ((ISupportInitialize)textEdit2.Properties).EndInit();
        ((ISupportInitialize)spinEdit1.Properties).EndInit();
        ((ISupportInitialize)spinEdit3.Properties).EndInit();
        ((ISupportInitialize)Root).EndInit();
        ((ISupportInitialize)layoutControlItem2).EndInit();
        ((ISupportInitialize)emptySpaceItem3).EndInit();
        ((ISupportInitialize)emptySpaceItem1).EndInit();
        ((ISupportInitialize)layoutControlItem1).EndInit();
        ((ISupportInitialize)layoutControlItem3).EndInit();
        ((ISupportInitialize)layoutControlItem4).EndInit();
        ((ISupportInitialize)layoutControlItem5).EndInit();
        ((ISupportInitialize)layoutControlItem7).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private DevExpress.XtraLayout.LayoutControl layoutControl1;
    private DevExpress.XtraLayout.LayoutControlGroup Root;
    private DevExpress.XtraEditors.SimpleButton simpleButton1;
    private DevExpress.XtraEditors.SimpleButton simpleButton2;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
    private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    private DevExpress.XtraEditors.TextEdit textEdit1;
    private DevExpress.XtraEditors.TextEdit textEdit2;
    private DevExpress.XtraEditors.SpinEdit spinEdit1;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    private DevExpress.XtraEditors.SpinEdit spinEdit3;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
}