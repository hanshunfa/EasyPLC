namespace EasyPlc.Entry.ChrildrenForms
{
    partial class WelcomeForm
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(WelcomeForm));
            SuspendLayout();
            // 
            // WelcomeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayoutStore = ImageLayout.Zoom;
            BackgroundImageStore = (Image)resources.GetObject("$this.BackgroundImageStore");
            ClientSize = new Size(789, 480);
            DoubleBuffered = true;
            Name = "WelcomeForm";
            Text = "欢迎界面";
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
        }

        #endregion
    }
}