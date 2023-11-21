namespace EasyPlc.Entry.ChrildrenForms.Adapter.PLC
{
    partial class MoveForm
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MoveForm));
            simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            SuspendLayout();
            // 
            // simpleButton1
            // 
            simpleButton1.ImageOptions.Image = (Image)resources.GetObject("simpleButton1.ImageOptions.Image");
            simpleButton1.Location = new Point(12, 45);
            simpleButton1.Name = "simpleButton1";
            simpleButton1.Size = new Size(75, 44);
            simpleButton1.TabIndex = 0;
            simpleButton1.Text = "上方";
            simpleButton1.Click += simpleButton1_Click;
            // 
            // simpleButton2
            // 
            simpleButton2.ImageOptions.Image = (Image)resources.GetObject("simpleButton2.ImageOptions.Image");
            simpleButton2.Location = new Point(93, 45);
            simpleButton2.Name = "simpleButton2";
            simpleButton2.Size = new Size(75, 44);
            simpleButton2.TabIndex = 1;
            simpleButton2.Text = "下方";
            simpleButton2.Click += simpleButton2_Click;
            // 
            // simpleButton3
            // 
            simpleButton3.ImageOptions.Image = (Image)resources.GetObject("simpleButton3.ImageOptions.Image");
            simpleButton3.Location = new Point(174, 45);
            simpleButton3.Name = "simpleButton3";
            simpleButton3.Size = new Size(75, 44);
            simpleButton3.TabIndex = 2;
            simpleButton3.Text = "对换";
            simpleButton3.Click += simpleButton3_Click;
            // 
            // simpleButton4
            // 
            simpleButton4.ImageOptions.Image = (Image)resources.GetObject("simpleButton4.ImageOptions.Image");
            simpleButton4.Location = new Point(289, 45);
            simpleButton4.Name = "simpleButton4";
            simpleButton4.Size = new Size(75, 44);
            simpleButton4.TabIndex = 3;
            simpleButton4.Text = "取消";
            simpleButton4.Click += simpleButton4_Click;
            // 
            // MoveForm
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(385, 139);
            Controls.Add(simpleButton4);
            Controls.Add(simpleButton3);
            Controls.Add(simpleButton2);
            Controls.Add(simpleButton1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MoveForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "MoveForm";
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
    }
}