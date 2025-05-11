using Bussiness_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Transfer_Object;

namespace RM.Model
{
    public partial class frmAddCusTomer : Form
    {
        private StaffBL staffBL;
        public frmAddCusTomer()
        {
            InitializeComponent();
            staffBL = new StaffBL();
        }

        public string OrderType = "";
        public int mainID = 0;
        public int driverID = 0;
        public string cusName = "";

        private void frmAddCusTomer_Load(object sender, EventArgs e)
        {
            if (OrderType == "Take Away")
            {
                lblDriver.Visible = false;
                cbDriver.Visible = false;
            }
            List<Staff>staff=new List<Staff>();
            staff = staffBL.GetDriver();
            cbDriver.DataSource = staff;
            cbDriver.DisplayMember = "sName"; // Hiển thị tên nhân viên trong ComboBox
            cbDriver.ValueMember = "staffId"; // Lưu giá trị staffId khi chọn

            if (mainID >0)
            {
                cbDriver.SelectedValue = driverID;
            }
        }

        // Khi ComboBox thay đổi lựa chọn
        private void cbDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDriver.SelectedItem != null)
            {
                // Chuyển đối tượng Staff từ SelectedItem
                Transfer_Object.Staff selectedStaff = (Transfer_Object.Staff)cbDriver.SelectedItem;

                // Lấy staffId từ đối tượng Staff
                driverID = Convert.ToInt32(selectedStaff.staffId);
            }
        }

    }
}
