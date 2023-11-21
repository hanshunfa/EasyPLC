namespace EasyPlc.Entry.ChrildrenForms.Role
{
    partial class RoleEditForm
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(RoleEditForm));
            tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            tablePanel2 = new DevExpress.Utils.Layout.TablePanel();
            treeListLookUpEdit1 = new DevExpress.XtraEditors.TreeListLookUpEdit();
            treeListLookUpEdit1TreeList = new DevExpress.XtraTreeList.TreeList();
            treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            labelControl5 = new DevExpress.XtraEditors.LabelControl();
            textEdit1 = new DevExpress.XtraEditors.TextEdit();
            radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            trackBarControl1 = new DevExpress.XtraEditors.TrackBarControl();
            comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            labelControl4 = new DevExpress.XtraEditors.LabelControl();
            labelControl3 = new DevExpress.XtraEditors.LabelControl();
            labelControl2 = new DevExpress.XtraEditors.LabelControl();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            stackPanel1 = new DevExpress.Utils.Layout.StackPanel();
            btnCancel = new DevExpress.XtraEditors.SimpleButton();
            btnOk = new DevExpress.XtraEditors.SimpleButton();
            treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((ISupportInitialize)tablePanel1).BeginInit();
            tablePanel1.SuspendLayout();
            ((ISupportInitialize)tablePanel2).BeginInit();
            tablePanel2.SuspendLayout();
            ((ISupportInitialize)treeListLookUpEdit1.Properties).BeginInit();
            ((ISupportInitialize)treeListLookUpEdit1TreeList).BeginInit();
            ((ISupportInitialize)textEdit1.Properties).BeginInit();
            ((ISupportInitialize)radioGroup1.Properties).BeginInit();
            ((ISupportInitialize)trackBarControl1).BeginInit();
            ((ISupportInitialize)trackBarControl1.Properties).BeginInit();
            ((ISupportInitialize)comboBoxEdit1.Properties).BeginInit();
            ((ISupportInitialize)stackPanel1).BeginInit();
            stackPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tablePanel1
            // 
            tablePanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 55F) });
            tablePanel1.Controls.Add(tablePanel2);
            tablePanel1.Controls.Add(stackPanel1);
            tablePanel1.Location = new Point(12, 12);
            tablePanel1.Name = "tablePanel1";
            tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 40F) });
            tablePanel1.Size = new Size(548, 330);
            tablePanel1.TabIndex = 1;
            // 
            // tablePanel2
            // 
            tablePanel1.SetColumn(tablePanel2, 0);
            tablePanel2.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 120F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 55F) });
            tablePanel2.Controls.Add(treeListLookUpEdit1);
            tablePanel2.Controls.Add(labelControl5);
            tablePanel2.Controls.Add(textEdit1);
            tablePanel2.Controls.Add(radioGroup1);
            tablePanel2.Controls.Add(trackBarControl1);
            tablePanel2.Controls.Add(comboBoxEdit1);
            tablePanel2.Controls.Add(labelControl4);
            tablePanel2.Controls.Add(labelControl3);
            tablePanel2.Controls.Add(labelControl2);
            tablePanel2.Controls.Add(labelControl1);
            tablePanel2.Dock = DockStyle.Fill;
            tablePanel2.Location = new Point(3, 3);
            tablePanel2.Name = "tablePanel2";
            tablePanel1.SetRow(tablePanel2, 0);
            tablePanel2.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 29F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 29F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 29F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 29F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 29F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 29F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 29F) });
            tablePanel2.Size = new Size(542, 284);
            tablePanel2.TabIndex = 1;
            // 
            // treeListLookUpEdit1
            // 
            tablePanel2.SetColumn(treeListLookUpEdit1, 1);
            treeListLookUpEdit1.EditValue = "";
            treeListLookUpEdit1.Location = new Point(123, 62);
            treeListLookUpEdit1.Name = "treeListLookUpEdit1";
            treeListLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            treeListLookUpEdit1.Properties.DisplayMember = "Name";
            treeListLookUpEdit1.Properties.TreeList = treeListLookUpEdit1TreeList;
            treeListLookUpEdit1.Properties.ValueMember = "Id";
            tablePanel2.SetRow(treeListLookUpEdit1, 2);
            treeListLookUpEdit1.Size = new Size(416, 20);
            treeListLookUpEdit1.TabIndex = 11;
            treeListLookUpEdit1.Visible = false;
            // 
            // treeListLookUpEdit1TreeList
            // 
            treeListLookUpEdit1TreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] { treeListColumn2 });
            treeListLookUpEdit1TreeList.KeyFieldName = "Id";
            treeListLookUpEdit1TreeList.Location = new Point(62, -57);
            treeListLookUpEdit1TreeList.Name = "treeListLookUpEdit1TreeList";
            treeListLookUpEdit1TreeList.OptionsView.ShowIndentAsRowStyle = true;
            treeListLookUpEdit1TreeList.ParentFieldName = "ParentId";
            treeListLookUpEdit1TreeList.Size = new Size(485, 644);
            treeListLookUpEdit1TreeList.TabIndex = 0;
            treeListLookUpEdit1TreeList.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeView;
            // 
            // treeListColumn2
            // 
            treeListColumn2.Caption = "组织";
            treeListColumn2.FieldName = "Name";
            treeListColumn2.Name = "treeListColumn2";
            treeListColumn2.Visible = true;
            treeListColumn2.VisibleIndex = 0;
            // 
            // labelControl5
            // 
            tablePanel2.SetColumn(labelControl5, 0);
            labelControl5.Location = new Point(3, 65);
            labelControl5.Name = "labelControl5";
            tablePanel2.SetRow(labelControl5, 2);
            labelControl5.Size = new Size(55, 14);
            labelControl5.TabIndex = 10;
            labelControl5.Text = "所属机构*";
            labelControl5.Visible = false;
            // 
            // textEdit1
            // 
            tablePanel2.SetColumn(textEdit1, 1);
            textEdit1.Location = new Point(123, 4);
            textEdit1.Name = "textEdit1";
            tablePanel2.SetRow(textEdit1, 0);
            textEdit1.Size = new Size(416, 20);
            textEdit1.TabIndex = 9;
            // 
            // radioGroup1
            // 
            tablePanel2.SetColumn(radioGroup1, 1);
            radioGroup1.EditValue = "全部";
            radioGroup1.Location = new Point(123, 90);
            radioGroup1.Name = "radioGroup1";
            radioGroup1.Properties.ItemHorzAlignment = DevExpress.XtraEditors.RadioItemHorzAlignment.Near;
            radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] { new DevExpress.XtraEditors.Controls.RadioGroupItem("全部", "全部"), new DevExpress.XtraEditors.Controls.RadioGroupItem("仅自己", "仅自己"), new DevExpress.XtraEditors.Controls.RadioGroupItem("所属组织", "所属组织"), new DevExpress.XtraEditors.Controls.RadioGroupItem("所属组织及以下", "所属组织及以下") });
            tablePanel2.SetRow(radioGroup1, 3);
            radioGroup1.Size = new Size(416, 23);
            radioGroup1.TabIndex = 8;
            // 
            // trackBarControl1
            // 
            tablePanel2.SetColumn(trackBarControl1, 1);
            trackBarControl1.EditValue = 1;
            trackBarControl1.Location = new Point(123, 119);
            trackBarControl1.Name = "trackBarControl1";
            trackBarControl1.Properties.LabelAppearance.Options.UseTextOptions = true;
            trackBarControl1.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            trackBarControl1.Properties.Maximum = 99;
            trackBarControl1.Properties.Minimum = 1;
            trackBarControl1.Properties.ShowLabels = true;
            trackBarControl1.Properties.ShowValueToolTip = true;
            trackBarControl1.Properties.TickStyle = TickStyle.None;
            tablePanel2.SetRow(trackBarControl1, 4);
            trackBarControl1.Size = new Size(416, 23);
            trackBarControl1.TabIndex = 7;
            trackBarControl1.Value = 1;
            // 
            // comboBoxEdit1
            // 
            tablePanel2.SetColumn(comboBoxEdit1, 1);
            comboBoxEdit1.Location = new Point(123, 33);
            comboBoxEdit1.Name = "comboBoxEdit1";
            comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            comboBoxEdit1.Properties.Items.AddRange(new object[] { "全局", "机构" });
            comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            tablePanel2.SetRow(comboBoxEdit1, 1);
            comboBoxEdit1.Size = new Size(416, 20);
            comboBoxEdit1.TabIndex = 6;
            comboBoxEdit1.SelectedIndexChanged += comboBoxEdit1_SelectedIndexChanged;
            // 
            // labelControl4
            // 
            tablePanel2.SetColumn(labelControl4, 0);
            labelControl4.Location = new Point(3, 123);
            labelControl4.Name = "labelControl4";
            tablePanel2.SetRow(labelControl4, 4);
            labelControl4.Size = new Size(31, 14);
            labelControl4.TabIndex = 3;
            labelControl4.Text = "排序*";
            // 
            // labelControl3
            // 
            tablePanel2.SetColumn(labelControl3, 0);
            labelControl3.Location = new Point(3, 94);
            labelControl3.Name = "labelControl3";
            tablePanel2.SetRow(labelControl3, 3);
            labelControl3.Size = new Size(79, 14);
            labelControl3.TabIndex = 2;
            labelControl3.Text = "默认数据范围*";
            // 
            // labelControl2
            // 
            tablePanel2.SetColumn(labelControl2, 0);
            labelControl2.Location = new Point(3, 36);
            labelControl2.Name = "labelControl2";
            tablePanel2.SetRow(labelControl2, 1);
            labelControl2.Size = new Size(55, 14);
            labelControl2.TabIndex = 1;
            labelControl2.Text = "角色分类*";
            // 
            // labelControl1
            // 
            tablePanel2.SetColumn(labelControl1, 0);
            labelControl1.Location = new Point(3, 7);
            labelControl1.Name = "labelControl1";
            tablePanel2.SetRow(labelControl1, 0);
            labelControl1.Size = new Size(55, 14);
            labelControl1.TabIndex = 0;
            labelControl1.Text = "角色名称*";
            // 
            // stackPanel1
            // 
            tablePanel1.SetColumn(stackPanel1, 0);
            stackPanel1.Controls.Add(btnCancel);
            stackPanel1.Controls.Add(btnOk);
            stackPanel1.Dock = DockStyle.Fill;
            stackPanel1.LayoutDirection = DevExpress.Utils.Layout.StackPanelLayoutDirection.RightToLeft;
            stackPanel1.Location = new Point(3, 293);
            stackPanel1.Name = "stackPanel1";
            tablePanel1.SetRow(stackPanel1, 1);
            stackPanel1.Size = new Size(542, 34);
            stackPanel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            btnCancel.ImageOptions.Image = (Image)resources.GetObject("btnCancel.ImageOptions.Image");
            btnCancel.Location = new Point(464, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "取消";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnOk
            // 
            btnOk.ImageOptions.Image = (Image)resources.GetObject("btnOk.ImageOptions.Image");
            btnOk.Location = new Point(383, 5);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 23);
            btnOk.TabIndex = 0;
            btnOk.Text = "确定";
            btnOk.Click += btnOk_Click;
            // 
            // treeListColumn1
            // 
            treeListColumn1.Caption = "组织";
            treeListColumn1.FieldName = "Name";
            treeListColumn1.Name = "treeListColumn1";
            treeListColumn1.Visible = true;
            treeListColumn1.VisibleIndex = 0;
            // 
            // RoleEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(572, 354);
            Controls.Add(tablePanel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RoleEditForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "RoleEditForm";
            ((ISupportInitialize)tablePanel1).EndInit();
            tablePanel1.ResumeLayout(false);
            ((ISupportInitialize)tablePanel2).EndInit();
            tablePanel2.ResumeLayout(false);
            tablePanel2.PerformLayout();
            ((ISupportInitialize)treeListLookUpEdit1.Properties).EndInit();
            ((ISupportInitialize)treeListLookUpEdit1TreeList).EndInit();
            ((ISupportInitialize)textEdit1.Properties).EndInit();
            ((ISupportInitialize)radioGroup1.Properties).EndInit();
            ((ISupportInitialize)trackBarControl1.Properties).EndInit();
            ((ISupportInitialize)trackBarControl1).EndInit();
            ((ISupportInitialize)comboBoxEdit1.Properties).EndInit();
            ((ISupportInitialize)stackPanel1).EndInit();
            stackPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.Utils.Layout.TablePanel tablePanel2;
        private DevExpress.XtraEditors.TrackBarControl trackBarControl1;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.Utils.Layout.StackPanel stackPanel1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TreeListLookUpEdit treeListLookUpEdit1;
        private DevExpress.XtraTreeList.TreeList treeListLookUpEdit1TreeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
    }
}