namespace EasyPlc.Entry.ChrildrenForms;

partial class RoleGrantUserForm
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
        ComponentResourceManager resources = new ComponentResourceManager(typeof(RoleGrantUserForm));
        tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
        tablePanel3 = new DevExpress.Utils.Layout.TablePanel();
        gridControl2 = new DevExpress.XtraGrid.GridControl();
        gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
        gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
        repositoryItemToggleSwitch2 = new DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch();
        paginationControl2 = new PaginationControl();
        flowLayoutPanel1 = new FlowLayoutPanel();
        simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
        simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
        tablePanel2 = new DevExpress.Utils.Layout.TablePanel();
        gridControl1 = new DevExpress.XtraGrid.GridControl();
        gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
        gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
        gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
        repositoryItemToggleSwitch1 = new DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch();
        paginationControl1 = new PaginationControl();
        treeList1 = new DevExpress.XtraTreeList.TreeList();
        treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
        treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
        ((ISupportInitialize)tablePanel1).BeginInit();
        tablePanel1.SuspendLayout();
        ((ISupportInitialize)tablePanel3).BeginInit();
        tablePanel3.SuspendLayout();
        ((ISupportInitialize)gridControl2).BeginInit();
        ((ISupportInitialize)gridView2).BeginInit();
        ((ISupportInitialize)repositoryItemToggleSwitch2).BeginInit();
        flowLayoutPanel1.SuspendLayout();
        ((ISupportInitialize)tablePanel2).BeginInit();
        tablePanel2.SuspendLayout();
        ((ISupportInitialize)gridControl1).BeginInit();
        ((ISupportInitialize)gridView1).BeginInit();
        ((ISupportInitialize)repositoryItemToggleSwitch1).BeginInit();
        ((ISupportInitialize)treeList1).BeginInit();
        SuspendLayout();
        // 
        // tablePanel1
        // 
        tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 260F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 60F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 40F) });
        tablePanel1.Controls.Add(tablePanel3);
        tablePanel1.Controls.Add(flowLayoutPanel1);
        tablePanel1.Controls.Add(tablePanel2);
        tablePanel1.Controls.Add(treeList1);
        tablePanel1.Dock = DockStyle.Fill;
        tablePanel1.Location = new Point(0, 0);
        tablePanel1.Name = "tablePanel1";
        tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 26F) });
        tablePanel1.Size = new Size(1077, 607);
        tablePanel1.TabIndex = 3;
        // 
        // tablePanel3
        // 
        tablePanel1.SetColumn(tablePanel3, 2);
        tablePanel3.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5F) });
        tablePanel3.Controls.Add(gridControl2);
        tablePanel3.Controls.Add(paginationControl2);
        tablePanel3.Dock = DockStyle.Fill;
        tablePanel3.Location = new Point(753, 3);
        tablePanel3.Name = "tablePanel3";
        tablePanel1.SetRow(tablePanel3, 0);
        tablePanel3.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 26F) });
        tablePanel3.Size = new Size(321, 566);
        tablePanel3.TabIndex = 4;
        // 
        // gridControl2
        // 
        tablePanel3.SetColumn(gridControl2, 0);
        gridControl2.Dock = DockStyle.Fill;
        gridControl2.Location = new Point(3, 3);
        gridControl2.MainView = gridView2;
        gridControl2.Name = "gridControl2";
        gridControl2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemToggleSwitch2 });
        tablePanel3.SetRow(gridControl2, 0);
        gridControl2.Size = new Size(315, 528);
        gridControl2.TabIndex = 1;
        gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView2 });
        // 
        // gridView2
        // 
        gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn1 });
        gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
        gridView2.GridControl = gridControl2;
        gridView2.Name = "gridView2";
        gridView2.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
        // 
        // gridColumn1
        // 
        gridColumn1.Caption = "姓名";
        gridColumn1.FieldName = "Name";
        gridColumn1.Name = "gridColumn1";
        gridColumn1.Visible = true;
        gridColumn1.VisibleIndex = 0;
        // 
        // repositoryItemToggleSwitch2
        // 
        repositoryItemToggleSwitch2.AutoHeight = false;
        repositoryItemToggleSwitch2.Name = "repositoryItemToggleSwitch2";
        repositoryItemToggleSwitch2.OffText = "Off";
        repositoryItemToggleSwitch2.OnText = "On";
        repositoryItemToggleSwitch2.ValueOff = "禁用";
        repositoryItemToggleSwitch2.ValueOn = "启用";
        // 
        // paginationControl2
        // 
        tablePanel3.SetColumn(paginationControl2, 0);
        paginationControl2.Location = new Point(3, 537);
        paginationControl2.Name = "paginationControl2";
        tablePanel3.SetRow(paginationControl2, 1);
        paginationControl2.Size = new Size(315, 26);
        paginationControl2.TabIndex = 0;
        // 
        // flowLayoutPanel1
        // 
        tablePanel1.SetColumn(flowLayoutPanel1, 0);
        tablePanel1.SetColumnSpan(flowLayoutPanel1, 3);
        flowLayoutPanel1.Controls.Add(simpleButton2);
        flowLayoutPanel1.Controls.Add(simpleButton1);
        flowLayoutPanel1.Dock = DockStyle.Fill;
        flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
        flowLayoutPanel1.Location = new Point(3, 575);
        flowLayoutPanel1.Name = "flowLayoutPanel1";
        tablePanel1.SetRow(flowLayoutPanel1, 1);
        flowLayoutPanel1.Size = new Size(1071, 29);
        flowLayoutPanel1.TabIndex = 3;
        // 
        // simpleButton2
        // 
        simpleButton2.ImageOptions.Image = (Image)resources.GetObject("simpleButton2.ImageOptions.Image");
        simpleButton2.Location = new Point(983, 3);
        simpleButton2.Name = "simpleButton2";
        simpleButton2.Size = new Size(85, 23);
        simpleButton2.TabIndex = 1;
        simpleButton2.Text = "取消";
        // 
        // simpleButton1
        // 
        simpleButton1.ImageOptions.Image = (Image)resources.GetObject("simpleButton1.ImageOptions.Image");
        simpleButton1.Location = new Point(892, 3);
        simpleButton1.Name = "simpleButton1";
        simpleButton1.Size = new Size(85, 23);
        simpleButton1.TabIndex = 0;
        simpleButton1.Text = "确定";
        // 
        // tablePanel2
        // 
        tablePanel1.SetColumn(tablePanel2, 1);
        tablePanel2.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5F) });
        tablePanel2.Controls.Add(gridControl1);
        tablePanel2.Controls.Add(paginationControl1);
        tablePanel2.Dock = DockStyle.Fill;
        tablePanel2.Location = new Point(263, 3);
        tablePanel2.Name = "tablePanel2";
        tablePanel1.SetRow(tablePanel2, 0);
        tablePanel2.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 26F) });
        tablePanel2.Size = new Size(484, 566);
        tablePanel2.TabIndex = 2;
        // 
        // gridControl1
        // 
        tablePanel2.SetColumn(gridControl1, 0);
        gridControl1.Dock = DockStyle.Fill;
        gridControl1.Location = new Point(3, 3);
        gridControl1.MainView = gridView1;
        gridControl1.Name = "gridControl1";
        gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemToggleSwitch1 });
        tablePanel2.SetRow(gridControl1, 0);
        gridControl1.Size = new Size(478, 528);
        gridControl1.TabIndex = 1;
        gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
        // 
        // gridView1
        // 
        gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn3, gridColumn2 });
        gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
        gridView1.GridControl = gridControl1;
        gridView1.Name = "gridView1";
        gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
        // 
        // gridColumn3
        // 
        gridColumn3.Caption = "姓名";
        gridColumn3.FieldName = "Name";
        gridColumn3.Name = "gridColumn3";
        gridColumn3.Visible = true;
        gridColumn3.VisibleIndex = 1;
        // 
        // gridColumn2
        // 
        gridColumn2.Caption = "账号";
        gridColumn2.FieldName = "Account";
        gridColumn2.Name = "gridColumn2";
        gridColumn2.Visible = true;
        gridColumn2.VisibleIndex = 0;
        // 
        // repositoryItemToggleSwitch1
        // 
        repositoryItemToggleSwitch1.AutoHeight = false;
        repositoryItemToggleSwitch1.Name = "repositoryItemToggleSwitch1";
        repositoryItemToggleSwitch1.OffText = "Off";
        repositoryItemToggleSwitch1.OnText = "On";
        repositoryItemToggleSwitch1.ValueOff = "禁用";
        repositoryItemToggleSwitch1.ValueOn = "启用";
        // 
        // paginationControl1
        // 
        tablePanel2.SetColumn(paginationControl1, 0);
        paginationControl1.Location = new Point(3, 537);
        paginationControl1.Name = "paginationControl1";
        tablePanel2.SetRow(paginationControl1, 1);
        paginationControl1.Size = new Size(478, 26);
        paginationControl1.TabIndex = 0;
        // 
        // treeList1
        // 
        tablePanel1.SetColumn(treeList1, 0);
        treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] { treeListColumn1, treeListColumn2 });
        treeList1.Dock = DockStyle.Fill;
        treeList1.Location = new Point(3, 3);
        treeList1.Name = "treeList1";
        tablePanel1.SetRow(treeList1, 0);
        treeList1.Size = new Size(254, 566);
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
        treeListColumn2.Caption = "组织";
        treeListColumn2.FieldName = "Name";
        treeListColumn2.Name = "treeListColumn2";
        treeListColumn2.Visible = true;
        treeListColumn2.VisibleIndex = 0;
        // 
        // RoleGrantUserForm
        // 
        AcceptButton = simpleButton1;
        AutoScaleDimensions = new SizeF(7F, 14F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = simpleButton2;
        ClientSize = new Size(1077, 607);
        Controls.Add(tablePanel1);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "RoleGrantUserForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "角色授权用户";
        Load += RoleGrantUserForm_Load;
        ((ISupportInitialize)tablePanel1).EndInit();
        tablePanel1.ResumeLayout(false);
        ((ISupportInitialize)tablePanel3).EndInit();
        tablePanel3.ResumeLayout(false);
        ((ISupportInitialize)gridControl2).EndInit();
        ((ISupportInitialize)gridView2).EndInit();
        ((ISupportInitialize)repositoryItemToggleSwitch2).EndInit();
        flowLayoutPanel1.ResumeLayout(false);
        ((ISupportInitialize)tablePanel2).EndInit();
        tablePanel2.ResumeLayout(false);
        ((ISupportInitialize)gridControl1).EndInit();
        ((ISupportInitialize)gridView1).EndInit();
        ((ISupportInitialize)repositoryItemToggleSwitch1).EndInit();
        ((ISupportInitialize)treeList1).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private DevExpress.Utils.Layout.TablePanel tablePanel1;
    private DevExpress.XtraEditors.SimpleButton simpleButton1;
    private DevExpress.Utils.Layout.TablePanel tablePanel2;
    private DevExpress.XtraGrid.GridControl gridControl1;
    private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
    private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    private DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch repositoryItemToggleSwitch1;
    private PaginationControl paginationControl1;
    private DevExpress.XtraTreeList.TreeList treeList1;
    private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
    private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
    private FlowLayoutPanel flowLayoutPanel1;
    private DevExpress.XtraEditors.SimpleButton simpleButton2;
    private DevExpress.Utils.Layout.TablePanel tablePanel3;
    private DevExpress.XtraGrid.GridControl gridControl2;
    private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
    private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    private DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch repositoryItemToggleSwitch2;
    private PaginationControl paginationControl2;
}