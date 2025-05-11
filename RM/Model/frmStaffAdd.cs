using Bussiness_Layer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RM.Model
{
    public partial class frmStaffAdd : SampleAdd
    {
        private StaffBL staffBL;

        public frmStaffAdd()
        {
            InitializeComponent();
            staffBL= new StaffBL();
        }

        public int id = 0;

        private void frmStaffAdd_Load(object sender, EventArgs e)
        {

        }
        public override void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string phone=txtPhone.Text;
            string role = cbRole.Text;
            Transfer_Object.Staff staff = new Transfer_Object.Staff(name,role,phone);
            if (id == 0)
            {
                staffBL.Add(staff);
                guna2MessageDialog1.Show("Saved successfully");
            }
            else
            {
                staff.staffId = id.ToString();
                staffBL.Update(staff);
                guna2MessageDialog1.Show("Update successfully");
            }

        }
    }
}
