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
using System.Xml.Linq;
using Transfer_Object;

namespace RM.Model
{
    public partial class frmAddAccount : SampleAdd
    {
        private AccountBL accountBL;
        public frmAddAccount()
        {
            accountBL = new AccountBL();
            InitializeComponent();
        }
        public int id = 0;


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void frmAddAccount_Load(object sender, EventArgs e)
        {

        }
        public override void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtTK.Text;
                string pass=txtMK.Text;
                int staffid = Convert.ToInt32( txtStaffID.Text);
                Account account= new Account(name, pass, staffid);

                // Kiểm tra xem Id có giá trị hay không để quyết định Add hoặc Update
                if (id == 0)
                {
                    accountBL.Add(account);
                    guna2MessageDialog1.Show("Saved successfully");
                }
                else  // Nếu là cập nhật (Id khác 0)
                {
                    id = 0;
                    accountBL.Update(account);  // Cập nhật đối tượng vào database
                    guna2MessageDialog1.Show("Updated successfully");
                }
            }
            catch (Exception ex)
            {
                // Nếu có lỗi, hiển thị thông báo lỗi
                guna2MessageDialog1.Show("Error: " + ex.Message);
            }
        }
    }
}
