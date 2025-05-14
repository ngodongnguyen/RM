using Bussiness_Layer;
using Guna.UI2.WinForms;
using RM.Model;
using RM.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RM
{
    public partial class frmMain : Form
    {
        private string userName;
        private string role;
        private StaffBL staffBL; 
        public frmMain(string userName)
        {
            staffBL = new StaffBL();
            InitializeComponent();
            this.userName = userName;
            role = staffBL.GetRole(userName);
        }
        //for accessing frmMain
        static frmMain _obj;
        public static frmMain Instance
        {
            get
            {
                if (_obj == null)
                {
                    // The following line will cause an issue with the constructor
                    // because the Instance property doesn't know the userName.
                    // _obj = new frmMain();
                    // Consider a different approach for the Singleton if you need the userName.
                }
                return _obj;
            }
        }
        //Method to add controls in Main form
        public void AddControls(Form f)
        {
            CenterPanel.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            CenterPanel.Controls.Add(f);
            f.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblUser.Text = MainClass.USER;
            _obj = this;
            if (role == "Cashier")
            {
                btnTable.Visible = true;
                btnPos.Visible = true;
                btnKitchen.Visible = true;
                btnSendBill.Visible = true;
                btnHome.Visible = true;
                btnCategory.Visible = false; // Thêm nếu cashier có quyền
                btnProduct.Visible = false; // Thêm nếu cashier có quyền
                btnStaff.Visible = false;   // Ẩn nếu cashier không có quyền
                btnSetting.Visible = false;
                btnReport.Visible = false;
                btnChart.Visible = false;
                btnPromotion.Visible = false;

            }
            else if (role == "Driver")
            {
                btnSendBill.Visible = true;
                btnHome.Visible = true;
                btnTable.Visible = false;
                btnPos.Visible = false;
                btnKitchen.Visible = false;
                btnCategory.Visible = false;
                btnProduct.Visible = false;
                btnStaff.Visible = false;
                btnSetting.Visible = false;
                btnReport.Visible = false;
                btnChart.Visible = false;
                btnPromotion.Visible = false;
            }
            else if (role == "Waiter")
            {
                btnSendBill.Visible = true;
                btnHome.Visible = true;
                btnKitchen.Visible = true;
                btnPos.Visible = true;
                btnTable.Visible = true;
                btnCategory.Visible = false;
                btnProduct.Visible = false;
                btnStaff.Visible = false;
                btnSetting.Visible = false;
                btnReport.Visible = false;
                btnChart.Visible = false;
                btnPromotion.Visible = false;

            }
            else if (role == "Manager")
            {
                // Hiển thị tất cả các nút
                btnTable.Visible = true;
                btnPos.Visible = true;
                btnKitchen.Visible = true;
                btnSendBill.Visible = true;
                btnHome.Visible = true;
                btnCategory.Visible = true;
                btnProduct.Visible = true;
                btnStaff.Visible = true;
                btnSetting.Visible = true;
                btnReport.Visible = true;
                btnChart.Visible = true;
                btnPromotion.Visible = true;

            }
            else if (role == "Cleaning" || role == "Other")
            {
                btnHome.Visible = true;
                btnTable.Visible = false;
                btnPos.Visible = false;
                btnKitchen.Visible = false;
                btnSendBill.Visible = false;
                btnCategory.Visible = false;
                btnProduct.Visible = false;
                btnStaff.Visible = false;
                btnSetting.Visible = false;
                btnReport.Visible = false;
                btnChart.Visible = false;
                btnPromotion.Visible = false;

            }
            else
            {
                // Xử lý trường hợp vai trò không xác định, có thể ẩn tất cả hoặc hiển thị một số nút cơ bản
                btnHome.Visible = true;
                btnTable.Visible = false;
                btnPos.Visible = false;
                btnKitchen.Visible = false;
                btnSendBill.Visible = false;
                btnCategory.Visible = false;
                btnProduct.Visible = false;
                btnStaff.Visible = false;
                btnSetting.Visible = false;
                btnReport.Visible = false;
                btnChart.Visible = false;
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            AddControls(new frmHome());
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            AddControls(new frmCategoryView());
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            AddControls(new frmTableView());
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {

            AddControls(new frmStaffView());
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            AddControls(new frmProductView());
        }

        private void btnPos_Click(object sender, EventArgs e)
        {
            frmPOS frm = new frmPOS();
            frm.Show();
        }

        private void btnKitchen_Click(object sender, EventArgs e)
        {
            AddControls(new frmKitchenView());
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            AddControls(new frmReports());

        }

        private void btnChart_Click(object sender, EventArgs e)
        {
            AddControls(new btnTotalRevenue());

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AddControls(new frmPromotion());

        }

        private void btnSendBill_Click(object sender, EventArgs e)
        {
            AddControls(new frmEmail());

        }

        private void btnSetting_Click_1(object sender, EventArgs e)
        {
            AddControls(new frmSettings());

        }
    }
}
