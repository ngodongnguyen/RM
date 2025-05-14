using Bussiness_Layer;
using Guna.UI2.WinForms;
using RM.Model;
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

namespace RM.View
{
    public partial class frmSettings : SampleView
    {
        private AccountBL accountBL;
        public frmSettings()
        {
            InitializeComponent();
            accountBL = new AccountBL();
        }
        private void GetData()
        {
            guna2DataGridView1.DataSource = accountBL.GetAccounts();
        }

        private void frmPromotion_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && guna2DataGridView1.Columns[e.ColumnIndex].Name == "dgvSno")
            {
                e.Value = e.RowIndex + 1; // Gán số thứ tự từ 1 đến n
            }
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            MainClass.BlurBackground(new frmAddAccount());
            GetData();
        }

        private void guna2DataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                frmAddAccount frm = new frmAddAccount();
                frm.id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                //frm. = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvcatID"].Value);
                MainClass.BlurBackground(frm);
                GetData();
            }
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                // need to confirm before deleting
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question;
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;
                if (guna2MessageDialog1.Show("Are you sure you want to delete?") == DialogResult.Yes)
                {
                    string name = (guna2DataGridView1.CurrentRow.Cells["dgvName"].Value).ToString();
                    Account account = new Account();
                    account.UserName = name;
                    accountBL.Delete(name);
                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Show("Delete successfully");
                    GetData();
                }

            }
        }
    }
}
