namespace EasyPlc.Entry.ChrildrenForms
{
    partial class SplashScreenForm
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(SplashScreenForm));
            progressBarControl = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            labelCopyright = new DevExpress.XtraEditors.LabelControl();
            labelStatus = new DevExpress.XtraEditors.LabelControl();
            peImage = new DevExpress.XtraEditors.PictureEdit();
            peLogo = new DevExpress.XtraEditors.PictureEdit();
            ((ISupportInitialize)progressBarControl.Properties).BeginInit();
            ((ISupportInitialize)peImage.Properties).BeginInit();
            ((ISupportInitialize)peLogo.Properties).BeginInit();
            SuspendLayout();
            // 
            // progressBarControl
            // 
            progressBarControl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBarControl.EditValue = 0;
            progressBarControl.Location = new Point(28, 250);
            progressBarControl.Margin = new Padding(4, 3, 4, 3);
            progressBarControl.Name = "progressBarControl";
            progressBarControl.Size = new Size(469, 13);
            progressBarControl.TabIndex = 5;
            // 
            // labelCopyright
            // 
            labelCopyright.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            labelCopyright.Location = new Point(28, 309);
            labelCopyright.Margin = new Padding(4, 3, 4, 3);
            labelCopyright.Name = "labelCopyright";
            labelCopyright.Size = new Size(52, 14);
            labelCopyright.TabIndex = 6;
            labelCopyright.Text = "Copyright";
            // 
            // labelStatus
            // 
            labelStatus.Location = new Point(28, 232);
            labelStatus.Margin = new Padding(4, 3, 4, 1);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(36, 14);
            labelStatus.TabIndex = 7;
            labelStatus.Text = "加载...";
            // 
            // peImage
            // 
            peImage.Dock = DockStyle.Top;
            peImage.EditValue = resources.GetObject("peImage.EditValue");
            peImage.Location = new Point(1, 1);
            peImage.Margin = new Padding(4, 3, 4, 3);
            peImage.Name = "peImage";
            peImage.Properties.AllowFocused = false;
            peImage.Properties.Appearance.BackColor = Color.Transparent;
            peImage.Properties.Appearance.Options.UseBackColor = true;
            peImage.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            peImage.Properties.ShowMenu = false;
            peImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            peImage.Properties.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None;
            peImage.Size = new Size(523, 215);
            peImage.TabIndex = 9;
            // 
            // peLogo
            // 
            peLogo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            peLogo.EditValue = resources.GetObject("peLogo.EditValue");
            peLogo.Location = new Point(326, 288);
            peLogo.Margin = new Padding(4, 3, 4, 3);
            peLogo.Name = "peLogo";
            peLogo.Properties.AllowFocused = false;
            peLogo.Properties.Appearance.BackColor = Color.Transparent;
            peLogo.Properties.Appearance.Options.UseBackColor = true;
            peLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            peLogo.Properties.ShowMenu = false;
            peLogo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            peLogo.Size = new Size(184, 45);
            peLogo.TabIndex = 8;
            // 
            // SplashScreenForm
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(525, 345);
            Controls.Add(peImage);
            Controls.Add(peLogo);
            Controls.Add(labelStatus);
            Controls.Add(labelCopyright);
            Controls.Add(progressBarControl);
            Margin = new Padding(4, 3, 4, 3);
            Name = "SplashScreenForm";
            Padding = new Padding(1);
            Text = "SplashScreenForm";
            ((ISupportInitialize)progressBarControl.Properties).EndInit();
            ((ISupportInitialize)peImage.Properties).EndInit();
            ((ISupportInitialize)peLogo.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.MarqueeProgressBarControl progressBarControl;
        private DevExpress.XtraEditors.LabelControl labelCopyright;
        private DevExpress.XtraEditors.LabelControl labelStatus;
        private DevExpress.XtraEditors.PictureEdit peLogo;
        private DevExpress.XtraEditors.PictureEdit peImage;
    }
}
