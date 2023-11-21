namespace EasyPlc.Entry.ChrildrenForms
{
    partial class LoginForm
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(LoginForm));
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            labelControl2 = new DevExpress.XtraEditors.LabelControl();
            txtUser = new DevExpress.XtraEditors.TextEdit();
            txtPwd = new DevExpress.XtraEditors.TextEdit();
            tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            lbMsg = new DevExpress.XtraEditors.LabelControl();
            labelControl3 = new DevExpress.XtraEditors.LabelControl();
            tablePanel2 = new DevExpress.Utils.Layout.TablePanel();
            cancelBtn = new DevExpress.XtraEditors.SimpleButton();
            okBtn = new DevExpress.XtraEditors.SimpleButton();
            ((ISupportInitialize)txtUser.Properties).BeginInit();
            ((ISupportInitialize)txtPwd.Properties).BeginInit();
            ((ISupportInitialize)tablePanel1).BeginInit();
            tablePanel1.SuspendLayout();
            ((ISupportInitialize)tablePanel2).BeginInit();
            tablePanel2.SuspendLayout();
            SuspendLayout();
            // 
            // labelControl1
            // 
            tablePanel1.SetColumn(labelControl1, 0);
            labelControl1.Location = new Point(3, 71);
            labelControl1.Name = "labelControl1";
            tablePanel1.SetRow(labelControl1, 1);
            labelControl1.Size = new Size(24, 14);
            labelControl1.TabIndex = 0;
            labelControl1.Text = "用户";
            // 
            // labelControl2
            // 
            tablePanel1.SetColumn(labelControl2, 0);
            labelControl2.Location = new Point(3, 98);
            labelControl2.Name = "labelControl2";
            tablePanel1.SetRow(labelControl2, 2);
            labelControl2.Size = new Size(24, 14);
            labelControl2.TabIndex = 1;
            labelControl2.Text = "密码";
            // 
            // txtUser
            // 
            tablePanel1.SetColumn(txtUser, 1);
            txtUser.Location = new Point(63, 68);
            txtUser.Name = "txtUser";
            tablePanel1.SetRow(txtUser, 1);
            txtUser.Size = new Size(222, 20);
            txtUser.TabIndex = 2;
            // 
            // txtPwd
            // 
            tablePanel1.SetColumn(txtPwd, 1);
            txtPwd.Location = new Point(63, 95);
            txtPwd.Name = "txtPwd";
            tablePanel1.SetRow(txtPwd, 2);
            txtPwd.Size = new Size(222, 20);
            txtPwd.TabIndex = 3;
            txtPwd.KeyDown += txtPwd_KeyDown;
            // 
            // tablePanel1
            // 
            tablePanel1.Appearance.BackColor = Color.Transparent;
            tablePanel1.Appearance.Options.UseBackColor = true;
            tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10.16F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 38.26F) });
            tablePanel1.Controls.Add(lbMsg);
            tablePanel1.Controls.Add(labelControl3);
            tablePanel1.Controls.Add(tablePanel2);
            tablePanel1.Controls.Add(txtPwd);
            tablePanel1.Controls.Add(txtUser);
            tablePanel1.Controls.Add(labelControl2);
            tablePanel1.Controls.Add(labelControl1);
            tablePanel1.Location = new Point(106, 12);
            tablePanel1.Name = "tablePanel1";
            tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 64F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 28F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 27F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 38F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F) });
            tablePanel1.Size = new Size(288, 235);
            tablePanel1.TabIndex = 0;
            // 
            // lbMsg
            // 
            lbMsg.Appearance.ForeColor = Color.Red;
            lbMsg.Appearance.Options.UseForeColor = true;
            tablePanel1.SetColumn(lbMsg, 1);
            lbMsg.Dock = DockStyle.Fill;
            lbMsg.Location = new Point(63, 186);
            lbMsg.Name = "lbMsg";
            tablePanel1.SetRow(lbMsg, 5);
            lbMsg.Size = new Size(222, 46);
            lbMsg.TabIndex = 6;
            // 
            // labelControl3
            // 
            labelControl3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            labelControl3.Appearance.Font = new Font("Tahoma", 17F, FontStyle.Regular, GraphicsUnit.Point);
            labelControl3.Appearance.Options.UseFont = true;
            tablePanel1.SetColumn(labelControl3, 0);
            tablePanel1.SetColumnSpan(labelControl3, 2);
            labelControl3.Location = new Point(3, 18);
            labelControl3.Name = "labelControl3";
            tablePanel1.SetRow(labelControl3, 0);
            labelControl3.Size = new Size(230, 28);
            labelControl3.TabIndex = 5;
            labelControl3.Text = "欢迎登入设备管理系统";
            // 
            // tablePanel2
            // 
            tablePanel1.SetColumn(tablePanel2, 1);
            tablePanel2.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 6F) });
            tablePanel2.Controls.Add(cancelBtn);
            tablePanel2.Controls.Add(okBtn);
            tablePanel2.Location = new Point(63, 148);
            tablePanel2.Name = "tablePanel2";
            tablePanel1.SetRow(tablePanel2, 4);
            tablePanel2.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F) });
            tablePanel2.Size = new Size(222, 32);
            tablePanel2.TabIndex = 4;
            // 
            // cancelBtn
            // 
            tablePanel2.SetColumn(cancelBtn, 1);
            cancelBtn.ImageOptions.Image = (Image)resources.GetObject("cancelBtn.ImageOptions.Image");
            cancelBtn.Location = new Point(88, 3);
            cancelBtn.Name = "cancelBtn";
            tablePanel2.SetRow(cancelBtn, 0);
            cancelBtn.Size = new Size(79, 26);
            cancelBtn.TabIndex = 1;
            cancelBtn.Text = "取消";
            cancelBtn.Click += cancelBtn_Click;
            // 
            // okBtn
            // 
            tablePanel2.SetColumn(okBtn, 0);
            okBtn.ImageOptions.Image = (Image)resources.GetObject("okBtn.ImageOptions.Image");
            okBtn.Location = new Point(3, 3);
            okBtn.Name = "okBtn";
            tablePanel2.SetRow(okBtn, 0);
            okBtn.Size = new Size(79, 26);
            okBtn.TabIndex = 0;
            okBtn.Text = "确定";
            okBtn.Click += okBtn_Click;
            // 
            // LoginForm
            // 
            AcceptButton = okBtn;
            Appearance.BackColor = Color.Transparent;
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayoutStore = ImageLayout.Tile;
            BackgroundImageStore = (Image)resources.GetObject("$this.BackgroundImageStore");
            CancelButton = cancelBtn;
            ClientSize = new Size(502, 259);
            Controls.Add(tablePanel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "登入";
            Load += LoginForm_Load;
            ((ISupportInitialize)txtUser.Properties).EndInit();
            ((ISupportInitialize)txtPwd.Properties).EndInit();
            ((ISupportInitialize)tablePanel1).EndInit();
            tablePanel1.ResumeLayout(false);
            tablePanel1.PerformLayout();
            ((ISupportInitialize)tablePanel2).EndInit();
            tablePanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.TextEdit txtPwd;
        private DevExpress.XtraEditors.TextEdit txtUser;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.Utils.Layout.TablePanel tablePanel2;
        private DevExpress.XtraEditors.SimpleButton okBtn;
        private DevExpress.XtraEditors.SimpleButton cancelBtn;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl lbMsg;
    }
}