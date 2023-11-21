namespace EasyPlc.Entry.ChrildrenForms
{
    partial class PaginationControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(PaginationControl));
            barManager1 = new DevExpress.XtraBars.BarManager(components);
            bar3 = new DevExpress.XtraBars.Bar();
            btnFirst = new DevExpress.XtraBars.BarButtonItem();
            btnPrevious = new DevExpress.XtraBars.BarButtonItem();
            txtPage = new DevExpress.XtraBars.BarEditItem();
            repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            btnNext = new DevExpress.XtraBars.BarButtonItem();
            btnLast = new DevExpress.XtraBars.BarButtonItem();
            cmbPageSize = new DevExpress.XtraBars.BarEditItem();
            repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            lblSummary = new DevExpress.XtraBars.BarStaticItem();
            barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((ISupportInitialize)barManager1).BeginInit();
            ((ISupportInitialize)repositoryItemTextEdit1).BeginInit();
            ((ISupportInitialize)repositoryItemComboBox1).BeginInit();
            SuspendLayout();
            // 
            // barManager1
            // 
            barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] { bar3 });
            barManager1.DockControls.Add(barDockControlTop);
            barManager1.DockControls.Add(barDockControlBottom);
            barManager1.DockControls.Add(barDockControlLeft);
            barManager1.DockControls.Add(barDockControlRight);
            barManager1.Form = this;
            barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { btnFirst, btnPrevious, txtPage, btnNext, btnLast, cmbPageSize, lblSummary });
            barManager1.MaxItemId = 7;
            barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemTextEdit1, repositoryItemComboBox1 });
            barManager1.StatusBar = bar3;
            // 
            // bar3
            // 
            bar3.BarName = "Status bar";
            bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            bar3.DockCol = 0;
            bar3.DockRow = 0;
            bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] { new DevExpress.XtraBars.LinkPersistInfo(btnFirst), new DevExpress.XtraBars.LinkPersistInfo(btnPrevious), new DevExpress.XtraBars.LinkPersistInfo(txtPage), new DevExpress.XtraBars.LinkPersistInfo(btnNext), new DevExpress.XtraBars.LinkPersistInfo(btnLast), new DevExpress.XtraBars.LinkPersistInfo(cmbPageSize), new DevExpress.XtraBars.LinkPersistInfo(lblSummary) });
            bar3.OptionsBar.AllowQuickCustomization = false;
            bar3.OptionsBar.DrawDragBorder = false;
            bar3.OptionsBar.UseWholeRow = true;
            bar3.Text = "Status bar";
            // 
            // btnFirst
            // 
            btnFirst.Caption = "第一页";
            btnFirst.Enabled = false;
            btnFirst.Id = 0;
            btnFirst.ImageOptions.Image = (Image)resources.GetObject("btnFirst.ImageOptions.Image");
            btnFirst.ImageOptions.LargeImage = (Image)resources.GetObject("btnFirst.ImageOptions.LargeImage");
            btnFirst.Name = "btnFirst";
            btnFirst.ItemClick += btnFirst_ItemClick;
            // 
            // btnPrevious
            // 
            btnPrevious.Caption = "上一页";
            btnPrevious.Enabled = false;
            btnPrevious.Id = 1;
            btnPrevious.ImageOptions.Image = (Image)resources.GetObject("btnPrevious.ImageOptions.Image");
            btnPrevious.ImageOptions.LargeImage = (Image)resources.GetObject("btnPrevious.ImageOptions.LargeImage");
            btnPrevious.Name = "btnPrevious";
            btnPrevious.ItemClick += btnPrevious_ItemClick;
            // 
            // txtPage
            // 
            txtPage.Caption = "输入页码并回车";
            txtPage.Edit = repositoryItemTextEdit1;
            txtPage.EditValue = "1";
            txtPage.Enabled = false;
            txtPage.Id = 2;
            txtPage.Name = "txtPage";
            txtPage.EditValueChanged += txtPage_EditValueChanged;
            // 
            // repositoryItemTextEdit1
            // 
            repositoryItemTextEdit1.AutoHeight = false;
            repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // btnNext
            // 
            btnNext.Caption = "下一页";
            btnNext.Id = 3;
            btnNext.ImageOptions.Image = (Image)resources.GetObject("btnNext.ImageOptions.Image");
            btnNext.ImageOptions.LargeImage = (Image)resources.GetObject("btnNext.ImageOptions.LargeImage");
            btnNext.Name = "btnNext";
            btnNext.ItemClick += btnNext_ItemClick;
            // 
            // btnLast
            // 
            btnLast.Caption = "最后一页";
            btnLast.Id = 4;
            btnLast.ImageOptions.Image = (Image)resources.GetObject("btnLast.ImageOptions.Image");
            btnLast.ImageOptions.LargeImage = (Image)resources.GetObject("btnLast.ImageOptions.LargeImage");
            btnLast.Name = "btnLast";
            btnLast.ItemClick += btnLast_ItemClick;
            // 
            // cmbPageSize
            // 
            cmbPageSize.Caption = "每页记录数";
            cmbPageSize.Edit = repositoryItemComboBox1;
            cmbPageSize.EditValue = "50";
            cmbPageSize.Id = 5;
            cmbPageSize.Name = "cmbPageSize";
            cmbPageSize.EditValueChanged += cmbPageSize_EditValueChanged;
            // 
            // repositoryItemComboBox1
            // 
            repositoryItemComboBox1.AutoHeight = false;
            repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            repositoryItemComboBox1.Items.AddRange(new object[] { "20", "50", "80", "100" });
            repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            repositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // lblSummary
            // 
            lblSummary.Caption = "没有加载数据";
            lblSummary.Id = 6;
            lblSummary.Name = "lblSummary";
            // 
            // barDockControlTop
            // 
            barDockControlTop.CausesValidation = false;
            barDockControlTop.Dock = DockStyle.Top;
            barDockControlTop.Location = new Point(0, 0);
            barDockControlTop.Manager = barManager1;
            barDockControlTop.Size = new Size(895, 0);
            // 
            // barDockControlBottom
            // 
            barDockControlBottom.CausesValidation = false;
            barDockControlBottom.Dock = DockStyle.Bottom;
            barDockControlBottom.Location = new Point(0, 0);
            barDockControlBottom.Manager = barManager1;
            barDockControlBottom.Size = new Size(895, 26);
            // 
            // barDockControlLeft
            // 
            barDockControlLeft.CausesValidation = false;
            barDockControlLeft.Dock = DockStyle.Left;
            barDockControlLeft.Location = new Point(0, 0);
            barDockControlLeft.Manager = barManager1;
            barDockControlLeft.Size = new Size(0, 0);
            // 
            // barDockControlRight
            // 
            barDockControlRight.CausesValidation = false;
            barDockControlRight.Dock = DockStyle.Right;
            barDockControlRight.Location = new Point(895, 0);
            barDockControlRight.Manager = barManager1;
            barDockControlRight.Size = new Size(0, 0);
            // 
            // PaginationControl
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(barDockControlLeft);
            Controls.Add(barDockControlRight);
            Controls.Add(barDockControlBottom);
            Controls.Add(barDockControlTop);
            Name = "PaginationControl";
            Size = new Size(895, 26);
            ((ISupportInitialize)barManager1).EndInit();
            ((ISupportInitialize)repositoryItemTextEdit1).EndInit();
            ((ISupportInitialize)repositoryItemComboBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnFirst;
        private DevExpress.XtraBars.BarButtonItem btnPrevious;
        private DevExpress.XtraBars.BarEditItem txtPage;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarButtonItem btnNext;
        private DevExpress.XtraBars.BarButtonItem btnLast;
        private DevExpress.XtraBars.BarEditItem cmbPageSize;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraBars.BarStaticItem lblSummary;
    }
}
