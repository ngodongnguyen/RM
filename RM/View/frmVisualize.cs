using Bussiness_Layer;
using RM.Visualize;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Transfer_Object;

namespace RM.View
{
    public partial class btnTotalRevenue : Form
    {
        public btnTotalRevenue()
        {
            InitializeComponent();
        }


        private void btnRevenue_Click(object sender, EventArgs e)
        {
            frmTotalRevenue frm=new frmTotalRevenue();
            frm.StartPosition = FormStartPosition.CenterScreen;  // Đặt vị trí form ở giữa màn hình
            frm.Show();
        }

        private void btnFood_Click(object sender, EventArgs e)
        {
            frmFoodInTotal frm = new frmFoodInTotal();
            frm.StartPosition = FormStartPosition.CenterScreen;  // Đặt vị trí form ở giữa màn hình
            frm.Show();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            frmAmountOfOrder frm= new frmAmountOfOrder();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }
    }
}
