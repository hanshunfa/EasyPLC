﻿namespace EasyPlc.Entry.ChrildrenForms.Mac.Carrier
{
    partial class PointEditForm
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(PointEditForm));
            tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            tablePanel2 = new DevExpress.Utils.Layout.TablePanel();
            textEdit2 = new DevExpress.XtraEditors.TextEdit();
            labelControl5 = new DevExpress.XtraEditors.LabelControl();
            comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            textEdit1 = new DevExpress.XtraEditors.TextEdit();
            treeListLookUpEdit1 = new DevExpress.XtraEditors.TreeListLookUpEdit();
            treeListLookUpEdit1TreeList = new DevExpress.XtraTreeList.TreeList();
            treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            labelControl3 = new DevExpress.XtraEditors.LabelControl();
            labelControl2 = new DevExpress.XtraEditors.LabelControl();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            stackPanel1 = new DevExpress.Utils.Layout.StackPanel();
            btnCancel = new DevExpress.XtraEditors.SimpleButton();
            btnOk = new DevExpress.XtraEditors.SimpleButton();
            ((ISupportInitialize)tablePanel1).BeginInit();
            tablePanel1.SuspendLayout();
            ((ISupportInitialize)tablePanel2).BeginInit();
            tablePanel2.SuspendLayout();
            ((ISupportInitialize)textEdit2.Properties).BeginInit();
            ((ISupportInitialize)comboBoxEdit1.Properties).BeginInit();
            ((ISupportInitialize)textEdit1.Properties).BeginInit();
            ((ISupportInitialize)treeListLookUpEdit1.Properties).BeginInit();
            ((ISupportInitialize)treeListLookUpEdit1TreeList).BeginInit();
            ((ISupportInitialize)stackPanel1).BeginInit();
            stackPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tablePanel1
            // 
            tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 55F) });
            tablePanel1.Controls.Add(tablePanel2);
            tablePanel1.Controls.Add(stackPanel1);
            tablePanel1.Dock = DockStyle.Fill;
            tablePanel1.Location = new Point(0, 0);
            tablePanel1.Name = "tablePanel1";
            tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 40F) });
            tablePanel1.Size = new Size(620, 515);
            tablePanel1.TabIndex = 4;
            // 
            // tablePanel2
            // 
            tablePanel1.SetColumn(tablePanel2, 0);
            tablePanel2.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 120F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 55F) });
            tablePanel2.Controls.Add(textEdit2);
            tablePanel2.Controls.Add(labelControl5);
            tablePanel2.Controls.Add(comboBoxEdit1);
            tablePanel2.Controls.Add(textEdit1);
            tablePanel2.Controls.Add(treeListLookUpEdit1);
            tablePanel2.Controls.Add(labelControl3);
            tablePanel2.Controls.Add(labelControl2);
            tablePanel2.Controls.Add(labelControl1);
            tablePanel2.Dock = DockStyle.Fill;
            tablePanel2.Location = new Point(3, 3);
            tablePanel2.Name = "tablePanel2";
            tablePanel1.SetRow(tablePanel2, 0);
            tablePanel2.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 29F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 29F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 29F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 29F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 29F) });
            tablePanel2.Size = new Size(614, 469);
            tablePanel2.TabIndex = 1;
            // 
            // textEdit2
            // 
            tablePanel2.SetColumn(textEdit2, 1);
            textEdit2.Location = new Point(123, 62);
            textEdit2.Name = "textEdit2";
            tablePanel2.SetRow(textEdit2, 2);
            textEdit2.Size = new Size(488, 20);
            textEdit2.TabIndex = 9;
            // 
            // labelControl5
            // 
            tablePanel2.SetColumn(labelControl5, 0);
            labelControl5.Location = new Point(3, 65);
            labelControl5.Name = "labelControl5";
            tablePanel2.SetRow(labelControl5, 2);
            labelControl5.Size = new Size(72, 14);
            labelControl5.TabIndex = 8;
            labelControl5.Text = "绑定对象编码";
            // 
            // comboBoxEdit1
            // 
            tablePanel2.SetColumn(comboBoxEdit1, 1);
            comboBoxEdit1.Location = new Point(123, 91);
            comboBoxEdit1.Name = "comboBoxEdit1";
            comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            comboBoxEdit1.Properties.Items.AddRange(new object[] { "启用", "禁用" });
            comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            tablePanel2.SetRow(comboBoxEdit1, 3);
            comboBoxEdit1.Size = new Size(488, 20);
            comboBoxEdit1.TabIndex = 6;
            // 
            // textEdit1
            // 
            tablePanel2.SetColumn(textEdit1, 1);
            textEdit1.Location = new Point(123, 33);
            textEdit1.Name = "textEdit1";
            tablePanel2.SetRow(textEdit1, 1);
            textEdit1.Size = new Size(488, 20);
            textEdit1.TabIndex = 5;
            // 
            // treeListLookUpEdit1
            // 
            tablePanel2.SetColumn(treeListLookUpEdit1, 1);
            treeListLookUpEdit1.EditValue = "";
            treeListLookUpEdit1.Location = new Point(123, 4);
            treeListLookUpEdit1.Name = "treeListLookUpEdit1";
            treeListLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            treeListLookUpEdit1.Properties.DisplayMember = "Name";
            treeListLookUpEdit1.Properties.TreeList = treeListLookUpEdit1TreeList;
            treeListLookUpEdit1.Properties.ValueMember = "Id";
            tablePanel2.SetRow(treeListLookUpEdit1, 0);
            treeListLookUpEdit1.Size = new Size(488, 20);
            treeListLookUpEdit1.TabIndex = 4;
            // 
            // treeListLookUpEdit1TreeList
            // 
            treeListLookUpEdit1TreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] { treeListColumn1 });
            treeListLookUpEdit1TreeList.KeyFieldName = "Id";
            treeListLookUpEdit1TreeList.Location = new Point(118, 142);
            treeListLookUpEdit1TreeList.Name = "treeListLookUpEdit1TreeList";
            treeListLookUpEdit1TreeList.OptionsView.ShowIndentAsRowStyle = true;
            treeListLookUpEdit1TreeList.ParentFieldName = "ParentId";
            treeListLookUpEdit1TreeList.Size = new Size(300, 200);
            treeListLookUpEdit1TreeList.TabIndex = 0;
            treeListLookUpEdit1TreeList.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeView;
            // 
            // treeListColumn1
            // 
            treeListColumn1.Caption = "组织";
            treeListColumn1.FieldName = "Name";
            treeListColumn1.Name = "treeListColumn1";
            treeListColumn1.Visible = true;
            treeListColumn1.VisibleIndex = 0;
            // 
            // labelControl3
            // 
            tablePanel2.SetColumn(labelControl3, 0);
            labelControl3.Location = new Point(3, 94);
            labelControl3.Name = "labelControl3";
            tablePanel2.SetRow(labelControl3, 3);
            labelControl3.Size = new Size(55, 14);
            labelControl3.TabIndex = 2;
            labelControl3.Text = "位置状态*";
            // 
            // labelControl2
            // 
            tablePanel2.SetColumn(labelControl2, 0);
            labelControl2.Location = new Point(3, 36);
            labelControl2.Name = "labelControl2";
            tablePanel2.SetRow(labelControl2, 1);
            labelControl2.Size = new Size(55, 14);
            labelControl2.TabIndex = 1;
            labelControl2.Text = "位置名称*";
            // 
            // labelControl1
            // 
            tablePanel2.SetColumn(labelControl1, 0);
            labelControl1.Location = new Point(3, 7);
            labelControl1.Name = "labelControl1";
            tablePanel2.SetRow(labelControl1, 0);
            labelControl1.Size = new Size(55, 14);
            labelControl1.TabIndex = 0;
            labelControl1.Text = "所属载具*";
            // 
            // stackPanel1
            // 
            tablePanel1.SetColumn(stackPanel1, 0);
            stackPanel1.Controls.Add(btnCancel);
            stackPanel1.Controls.Add(btnOk);
            stackPanel1.Dock = DockStyle.Fill;
            stackPanel1.LayoutDirection = DevExpress.Utils.Layout.StackPanelLayoutDirection.RightToLeft;
            stackPanel1.Location = new Point(3, 478);
            stackPanel1.Name = "stackPanel1";
            tablePanel1.SetRow(stackPanel1, 1);
            stackPanel1.Size = new Size(614, 34);
            stackPanel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            btnCancel.ImageOptions.Image = (Image)resources.GetObject("btnCancel.ImageOptions.Image");
            btnCancel.Location = new Point(536, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "取消";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnOk
            // 
            btnOk.ImageOptions.Image = (Image)resources.GetObject("btnOk.ImageOptions.Image");
            btnOk.Location = new Point(455, 5);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 23);
            btnOk.TabIndex = 0;
            btnOk.Text = "确定";
            btnOk.Click += btnOk_Click;
            // 
            // PointEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(620, 515);
            Controls.Add(tablePanel1);
            Name = "PointEditForm";
            Text = "位置编辑";
            ((ISupportInitialize)tablePanel1).EndInit();
            tablePanel1.ResumeLayout(false);
            ((ISupportInitialize)tablePanel2).EndInit();
            tablePanel2.ResumeLayout(false);
            tablePanel2.PerformLayout();
            ((ISupportInitialize)textEdit2.Properties).EndInit();
            ((ISupportInitialize)comboBoxEdit1.Properties).EndInit();
            ((ISupportInitialize)textEdit1.Properties).EndInit();
            ((ISupportInitialize)treeListLookUpEdit1.Properties).EndInit();
            ((ISupportInitialize)treeListLookUpEdit1TreeList).EndInit();
            ((ISupportInitialize)stackPanel1).EndInit();
            stackPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.Utils.Layout.TablePanel tablePanel2;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.TreeListLookUpEdit treeListLookUpEdit1;
        private DevExpress.XtraTreeList.TreeList treeListLookUpEdit1TreeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.Utils.Layout.StackPanel stackPanel1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOk;
    }
}