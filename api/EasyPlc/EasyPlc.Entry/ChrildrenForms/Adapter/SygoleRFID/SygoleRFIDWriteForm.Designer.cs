namespace EasyPlc.Entry.ChrildrenForms;

partial class SygoleRFIDWriteForm
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
        ComponentResourceManager resources = new ComponentResourceManager(typeof(SygoleRFIDWriteForm));
        textEdit1 = new DevExpress.XtraEditors.TextEdit();
        labelControl1 = new DevExpress.XtraEditors.LabelControl();
        simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
        simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
        ((ISupportInitialize)textEdit1.Properties).BeginInit();
        SuspendLayout();
        // 
        // textEdit1
        // 
        textEdit1.Location = new Point(81, 27);
        textEdit1.Name = "textEdit1";
        textEdit1.Size = new Size(218, 20);
        textEdit1.TabIndex = 0;
        // 
        // labelControl1
        // 
        labelControl1.Location = new Point(27, 30);
        labelControl1.Name = "labelControl1";
        labelControl1.Size = new Size(48, 14);
        labelControl1.TabIndex = 1;
        labelControl1.Text = "载具编码";
        // 
        // simpleButton1
        // 
        simpleButton1.ImageOptions.Image = (Image)resources.GetObject("simpleButton1.ImageOptions.Image");
        simpleButton1.Location = new Point(145, 80);
        simpleButton1.Name = "simpleButton1";
        simpleButton1.Size = new Size(75, 23);
        simpleButton1.TabIndex = 2;
        simpleButton1.Text = "确定";
        simpleButton1.Click += simpleButton1_Click;
        // 
        // simpleButton2
        // 
        simpleButton2.ImageOptions.Image = (Image)resources.GetObject("simpleButton2.ImageOptions.Image");
        simpleButton2.Location = new Point(226, 80);
        simpleButton2.Name = "simpleButton2";
        simpleButton2.Size = new Size(75, 23);
        simpleButton2.TabIndex = 3;
        simpleButton2.Text = "取消";
        simpleButton2.Click += simpleButton2_Click;
        // 
        // SygoleRFIDWriteForm
        // 
        AcceptButton = simpleButton1;
        AutoScaleDimensions = new SizeF(7F, 14F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = simpleButton2;
        ClientSize = new Size(346, 131);
        Controls.Add(simpleButton2);
        Controls.Add(simpleButton1);
        Controls.Add(labelControl1);
        Controls.Add(textEdit1);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "SygoleRFIDWriteForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "思谷RFID写入内容";
        ((ISupportInitialize)textEdit1.Properties).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private DevExpress.XtraEditors.TextEdit textEdit1;
    private DevExpress.XtraEditors.LabelControl labelControl1;
    private DevExpress.XtraEditors.SimpleButton simpleButton1;
    private DevExpress.XtraEditors.SimpleButton simpleButton2;
}