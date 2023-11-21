namespace EasyPlc.Entry.ChrildrenForms;

partial class PreviewLaserForm
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
        ComponentResourceManager resources = new ComponentResourceManager(typeof(PreviewLaserForm));
        layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
        comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
        memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
        simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
        Root = new DevExpress.XtraLayout.LayoutControlGroup();
        emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
        layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
        layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
        layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
        layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
        simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
        layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
        ((ISupportInitialize)layoutControl1).BeginInit();
        layoutControl1.SuspendLayout();
        ((ISupportInitialize)comboBoxEdit1.Properties).BeginInit();
        ((ISupportInitialize)memoEdit1.Properties).BeginInit();
        ((ISupportInitialize)Root).BeginInit();
        ((ISupportInitialize)emptySpaceItem1).BeginInit();
        ((ISupportInitialize)layoutControlItem1).BeginInit();
        ((ISupportInitialize)layoutControlGroup1).BeginInit();
        ((ISupportInitialize)layoutControlItem2).BeginInit();
        ((ISupportInitialize)layoutControlItem3).BeginInit();
        ((ISupportInitialize)layoutControlItem4).BeginInit();
        SuspendLayout();
        // 
        // layoutControl1
        // 
        layoutControl1.Controls.Add(simpleButton2);
        layoutControl1.Controls.Add(comboBoxEdit1);
        layoutControl1.Controls.Add(memoEdit1);
        layoutControl1.Controls.Add(simpleButton1);
        layoutControl1.Dock = DockStyle.Fill;
        layoutControl1.Location = new Point(0, 0);
        layoutControl1.Name = "layoutControl1";
        layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(529, 462, 650, 400);
        layoutControl1.Root = Root;
        layoutControl1.Size = new Size(848, 610);
        layoutControl1.TabIndex = 0;
        layoutControl1.Text = "layoutControl1";
        // 
        // comboBoxEdit1
        // 
        comboBoxEdit1.Location = new Point(132, 12);
        comboBoxEdit1.Name = "comboBoxEdit1";
        comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
        comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
        comboBoxEdit1.Size = new Size(174, 20);
        comboBoxEdit1.StyleController = layoutControl1;
        comboBoxEdit1.TabIndex = 0;
        comboBoxEdit1.SelectedIndexChanged += comboBoxEdit1_SelectedIndexChanged;
        // 
        // memoEdit1
        // 
        memoEdit1.Location = new Point(24, 71);
        memoEdit1.Name = "memoEdit1";
        memoEdit1.Properties.ReadOnly = true;
        memoEdit1.Size = new Size(396, 489);
        memoEdit1.StyleController = layoutControl1;
        memoEdit1.TabIndex = 4;
        // 
        // simpleButton1
        // 
        simpleButton1.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
        simpleButton1.Appearance.ForeColor = Color.Green;
        simpleButton1.Appearance.Options.UseFont = true;
        simpleButton1.Appearance.Options.UseForeColor = true;
        simpleButton1.ImageOptions.Image = (Image)resources.GetObject("simpleButton1.ImageOptions.Image");
        simpleButton1.Location = new Point(24, 564);
        simpleButton1.Name = "simpleButton1";
        simpleButton1.Size = new Size(396, 22);
        simpleButton1.StyleController = layoutControl1;
        simpleButton1.TabIndex = 5;
        simpleButton1.Text = "发送当前预览到镭射机，镭射预览";
        simpleButton1.Click += simpleButton1_Click;
        // 
        // Root
        // 
        Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
        Root.GroupBordersVisible = false;
        Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { emptySpaceItem1, layoutControlItem1, layoutControlGroup1, layoutControlItem4 });
        Root.Name = "Root";
        Root.Size = new Size(848, 610);
        Root.TextVisible = false;
        // 
        // emptySpaceItem1
        // 
        emptySpaceItem1.AllowHotTrack = false;
        emptySpaceItem1.Location = new Point(424, 0);
        emptySpaceItem1.Name = "emptySpaceItem1";
        emptySpaceItem1.Size = new Size(404, 590);
        emptySpaceItem1.TextSize = new Size(0, 0);
        // 
        // layoutControlItem1
        // 
        layoutControlItem1.Control = comboBoxEdit1;
        layoutControlItem1.CustomizationFormText = "工单";
        layoutControlItem1.Location = new Point(0, 0);
        layoutControlItem1.Name = "layoutControlItem1";
        layoutControlItem1.Size = new Size(298, 26);
        layoutControlItem1.Text = "选择需要预览的工单";
        layoutControlItem1.TextSize = new Size(108, 14);
        // 
        // layoutControlGroup1
        // 
        layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2, layoutControlItem3 });
        layoutControlGroup1.Location = new Point(0, 26);
        layoutControlGroup1.Name = "layoutControlGroup1";
        layoutControlGroup1.Size = new Size(424, 564);
        layoutControlGroup1.Text = "镭射详情预览";
        // 
        // layoutControlItem2
        // 
        layoutControlItem2.Control = memoEdit1;
        layoutControlItem2.Location = new Point(0, 0);
        layoutControlItem2.Name = "layoutControlItem2";
        layoutControlItem2.Size = new Size(400, 493);
        layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
        layoutControlItem2.TextSize = new Size(0, 0);
        layoutControlItem2.TextVisible = false;
        // 
        // layoutControlItem3
        // 
        layoutControlItem3.Control = simpleButton1;
        layoutControlItem3.Location = new Point(0, 493);
        layoutControlItem3.Name = "layoutControlItem3";
        layoutControlItem3.Size = new Size(400, 26);
        layoutControlItem3.TextSize = new Size(0, 0);
        layoutControlItem3.TextVisible = false;
        // 
        // simpleButton2
        // 
        simpleButton2.ImageOptions.Image = (Image)resources.GetObject("simpleButton2.ImageOptions.Image");
        simpleButton2.Location = new Point(310, 12);
        simpleButton2.Name = "simpleButton2";
        simpleButton2.Size = new Size(122, 22);
        simpleButton2.StyleController = layoutControl1;
        simpleButton2.TabIndex = 3;
        simpleButton2.Text = "当前正在生产工单";
        simpleButton2.Click += simpleButton2_Click;
        // 
        // layoutControlItem4
        // 
        layoutControlItem4.Control = simpleButton2;
        layoutControlItem4.Location = new Point(298, 0);
        layoutControlItem4.Name = "layoutControlItem4";
        layoutControlItem4.Size = new Size(126, 26);
        layoutControlItem4.TextSize = new Size(0, 0);
        layoutControlItem4.TextVisible = false;
        // 
        // PreviewLaserForm
        // 
        AutoScaleDimensions = new SizeF(7F, 14F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(848, 610);
        Controls.Add(layoutControl1);
        Name = "PreviewLaserForm";
        Text = "镭射预览";
        Load += PreviewLaserForm_Load;
        ((ISupportInitialize)layoutControl1).EndInit();
        layoutControl1.ResumeLayout(false);
        ((ISupportInitialize)comboBoxEdit1.Properties).EndInit();
        ((ISupportInitialize)memoEdit1.Properties).EndInit();
        ((ISupportInitialize)Root).EndInit();
        ((ISupportInitialize)emptySpaceItem1).EndInit();
        ((ISupportInitialize)layoutControlItem1).EndInit();
        ((ISupportInitialize)layoutControlGroup1).EndInit();
        ((ISupportInitialize)layoutControlItem2).EndInit();
        ((ISupportInitialize)layoutControlItem3).EndInit();
        ((ISupportInitialize)layoutControlItem4).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private DevExpress.XtraLayout.LayoutControl layoutControl1;
    private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
    private DevExpress.XtraLayout.LayoutControlGroup Root;
    private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    private DevExpress.XtraEditors.MemoEdit memoEdit1;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    private DevExpress.XtraEditors.SimpleButton simpleButton1;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    private DevExpress.XtraEditors.SimpleButton simpleButton2;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
}