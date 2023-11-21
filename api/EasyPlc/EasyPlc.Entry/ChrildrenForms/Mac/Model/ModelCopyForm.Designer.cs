namespace EasyPlc.Entry.ChrildrenForms;

partial class ModelCopyForm
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
        ComponentResourceManager resources = new ComponentResourceManager(typeof(ModelCopyForm));
        layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
        treeListLookUpEdit1 = new DevExpress.XtraEditors.TreeListLookUpEdit();
        treeListLookUpEdit1TreeList = new DevExpress.XtraTreeList.TreeList();
        simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
        simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
        Root = new DevExpress.XtraLayout.LayoutControlGroup();
        layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
        layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
        emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
        emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
        layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
        treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
        ((ISupportInitialize)layoutControl1).BeginInit();
        layoutControl1.SuspendLayout();
        ((ISupportInitialize)treeListLookUpEdit1.Properties).BeginInit();
        ((ISupportInitialize)treeListLookUpEdit1TreeList).BeginInit();
        ((ISupportInitialize)Root).BeginInit();
        ((ISupportInitialize)layoutControlItem1).BeginInit();
        ((ISupportInitialize)layoutControlItem2).BeginInit();
        ((ISupportInitialize)emptySpaceItem1).BeginInit();
        ((ISupportInitialize)emptySpaceItem2).BeginInit();
        ((ISupportInitialize)layoutControlItem3).BeginInit();
        SuspendLayout();
        // 
        // layoutControl1
        // 
        layoutControl1.Controls.Add(treeListLookUpEdit1);
        layoutControl1.Controls.Add(simpleButton1);
        layoutControl1.Controls.Add(simpleButton2);
        layoutControl1.Dock = DockStyle.Fill;
        layoutControl1.Location = new Point(0, 0);
        layoutControl1.Name = "layoutControl1";
        layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(779, 81, 650, 400);
        layoutControl1.Root = Root;
        layoutControl1.Size = new Size(461, 339);
        layoutControl1.TabIndex = 0;
        layoutControl1.Text = "layoutControl1";
        // 
        // treeListLookUpEdit1
        // 
        treeListLookUpEdit1.EditValue = "";
        treeListLookUpEdit1.Location = new Point(96, 12);
        treeListLookUpEdit1.Name = "treeListLookUpEdit1";
        treeListLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
        treeListLookUpEdit1.Properties.DisplayMember = "Name";
        treeListLookUpEdit1.Properties.TreeList = treeListLookUpEdit1TreeList;
        treeListLookUpEdit1.Properties.ValueMember = "Id";
        treeListLookUpEdit1.Size = new Size(353, 20);
        treeListLookUpEdit1.StyleController = layoutControl1;
        treeListLookUpEdit1.TabIndex = 4;
        // 
        // treeListLookUpEdit1TreeList
        // 
        treeListLookUpEdit1TreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] { treeListColumn1 });
        treeListLookUpEdit1TreeList.KeyFieldName = "Id";
        treeListLookUpEdit1TreeList.Location = new Point(0, 0);
        treeListLookUpEdit1TreeList.Name = "treeListLookUpEdit1TreeList";
        treeListLookUpEdit1TreeList.OptionsView.ShowIndentAsRowStyle = true;
        treeListLookUpEdit1TreeList.ParentFieldName = "ParentId";
        treeListLookUpEdit1TreeList.Size = new Size(400, 200);
        treeListLookUpEdit1TreeList.TabIndex = 0;
        treeListLookUpEdit1TreeList.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeView;
        // 
        // simpleButton1
        // 
        simpleButton1.ImageOptions.Image = (Image)resources.GetObject("simpleButton1.ImageOptions.Image");
        simpleButton1.Location = new Point(254, 305);
        simpleButton1.Name = "simpleButton1";
        simpleButton1.Size = new Size(92, 22);
        simpleButton1.StyleController = layoutControl1;
        simpleButton1.TabIndex = 0;
        simpleButton1.Text = "确定";
        simpleButton1.Click += simpleButton1_Click;
        // 
        // simpleButton2
        // 
        simpleButton2.ImageOptions.Image = (Image)resources.GetObject("simpleButton2.ImageOptions.Image");
        simpleButton2.Location = new Point(350, 305);
        simpleButton2.Name = "simpleButton2";
        simpleButton2.Size = new Size(99, 22);
        simpleButton2.StyleController = layoutControl1;
        simpleButton2.TabIndex = 2;
        simpleButton2.Text = "取消";
        simpleButton2.Click += simpleButton2_Click;
        // 
        // Root
        // 
        Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
        Root.GroupBordersVisible = false;
        Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, layoutControlItem2, emptySpaceItem1, emptySpaceItem2, layoutControlItem3 });
        Root.Name = "Root";
        Root.Size = new Size(461, 339);
        Root.TextVisible = false;
        // 
        // layoutControlItem1
        // 
        layoutControlItem1.Control = simpleButton1;
        layoutControlItem1.Location = new Point(242, 293);
        layoutControlItem1.Name = "layoutControlItem1";
        layoutControlItem1.Size = new Size(96, 26);
        layoutControlItem1.TextSize = new Size(0, 0);
        layoutControlItem1.TextVisible = false;
        // 
        // layoutControlItem2
        // 
        layoutControlItem2.Control = simpleButton2;
        layoutControlItem2.Location = new Point(338, 293);
        layoutControlItem2.Name = "layoutControlItem2";
        layoutControlItem2.Size = new Size(103, 26);
        layoutControlItem2.TextSize = new Size(0, 0);
        layoutControlItem2.TextVisible = false;
        // 
        // emptySpaceItem1
        // 
        emptySpaceItem1.AllowHotTrack = false;
        emptySpaceItem1.Location = new Point(0, 24);
        emptySpaceItem1.Name = "emptySpaceItem1";
        emptySpaceItem1.Size = new Size(441, 269);
        emptySpaceItem1.TextSize = new Size(0, 0);
        // 
        // emptySpaceItem2
        // 
        emptySpaceItem2.AllowHotTrack = false;
        emptySpaceItem2.Location = new Point(0, 293);
        emptySpaceItem2.Name = "emptySpaceItem2";
        emptySpaceItem2.Size = new Size(242, 26);
        emptySpaceItem2.TextSize = new Size(0, 0);
        // 
        // layoutControlItem3
        // 
        layoutControlItem3.Control = treeListLookUpEdit1;
        layoutControlItem3.Location = new Point(0, 0);
        layoutControlItem3.Name = "layoutControlItem3";
        layoutControlItem3.Size = new Size(441, 24);
        layoutControlItem3.Text = "复制目标对象";
        layoutControlItem3.TextSize = new Size(72, 14);
        // 
        // treeListColumn1
        // 
        treeListColumn1.Caption = "名称";
        treeListColumn1.FieldName = "Name";
        treeListColumn1.Name = "treeListColumn1";
        treeListColumn1.Visible = true;
        treeListColumn1.VisibleIndex = 0;
        // 
        // ModelCopyForm
        // 
        AutoScaleDimensions = new SizeF(7F, 14F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(461, 339);
        Controls.Add(layoutControl1);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "ModelCopyForm";
        Text = "型号产生复制窗体";
        Load += ModelCopyForm_Load;
        ((ISupportInitialize)layoutControl1).EndInit();
        layoutControl1.ResumeLayout(false);
        ((ISupportInitialize)treeListLookUpEdit1.Properties).EndInit();
        ((ISupportInitialize)treeListLookUpEdit1TreeList).EndInit();
        ((ISupportInitialize)Root).EndInit();
        ((ISupportInitialize)layoutControlItem1).EndInit();
        ((ISupportInitialize)layoutControlItem2).EndInit();
        ((ISupportInitialize)emptySpaceItem1).EndInit();
        ((ISupportInitialize)emptySpaceItem2).EndInit();
        ((ISupportInitialize)layoutControlItem3).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private DevExpress.XtraLayout.LayoutControl layoutControl1;
    private DevExpress.XtraEditors.TreeListLookUpEdit treeListLookUpEdit1;
    private DevExpress.XtraTreeList.TreeList treeListLookUpEdit1TreeList;
    private DevExpress.XtraEditors.SimpleButton simpleButton1;
    private DevExpress.XtraEditors.SimpleButton simpleButton2;
    private DevExpress.XtraLayout.LayoutControlGroup Root;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
}