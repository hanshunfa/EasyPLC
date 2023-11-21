using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPlc.Entry.ChrildrenForms.Adapter.PLC
{
    public partial class MoveForm : DevExpress.XtraEditors.XtraForm
    {
        public MoveForm()
        {
            InitializeComponent();
        }

        public int Rlt = 0;

        /// <summary>
        /// 上方
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Rlt = 1;
            DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// 下方
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Rlt = 2;
            DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// 对换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Rlt = 3;
            DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}