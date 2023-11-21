namespace EasyPlc.Entry.ChrildrenForms
{
    partial class WaitButtonForm
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
            tableLayoutPanel1 = new TableLayoutPanel();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.BackColor = Color.Transparent;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(0, 15, 0, 15);
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(287, 101);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // labelControl1
            // 
            labelControl1.Location = new Point(241, 42);
            labelControl1.Margin = new Padding(4);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new Size(19, 14);
            labelControl1.TabIndex = 2;
            labelControl1.Text = "0秒";
            // 
            // WaitButtonForm
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(287, 71);
            Controls.Add(labelControl1);
            Margin = new Padding(5, 4, 5, 4);
            Name = "WaitButtonForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "WaitButtonForm";
            FormClosed += WaitButtonForm_FormClosed;
            Load += WaitButtonForm_Load;
            Controls.SetChildIndex(labelControl1, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
