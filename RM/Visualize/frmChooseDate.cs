using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RM.Visualize
{
    public partial class frmChooseDate : Form
    {
        public frmChooseDate()
        {
            InitializeComponent();
        }

        private void btnVisual_Click(object sender, EventArgs e)
        {
            DateTime startDate = sDate.Value;
            DateTime endDate = eDate.Value;
            frmTotalRevenue frm = new frmTotalRevenue(startDate,endDate);
            frm.StartPosition = FormStartPosition.CenterScreen;  // Đặt vị trí form ở giữa màn hình
            frm.ShowDialog();
        }
    }
}
