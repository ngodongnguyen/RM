using Bussiness_Layer;
using RM.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RM.View
{
    public partial class frmTableView : SampleView
    {
        private TableBL tableBL;
        public frmTableView()
        {
            InitializeComponent();
            tableBL = new TableBL();
        }

        private void frmTableView_Load(object sender, EventArgs e)
        {
            GetData();
        }
        public void GetData()
        {
            try
            {
                guna2DataGridView1.DataSource = tableBL.GetTables();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public override void btnAdd_Click(object sender, EventArgs e)
        {
            //adding blue effect
            //frmTableAdd frm = new frmTableAdd();
            //frm.ShowDialog();
           
            MainClass.BlurBackground(new frmTableAdd());
            GetData();
        }

        public override void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")
            {

                frmTableAdd frm = new frmTableAdd();
                frm.id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                frm.txtName.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvName"].Value);
                //frm.ShowDialog();
                MainClass.BlurBackground(frm);
                GetData();


            }
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                // need to confrim before deleting

                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question;
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;
                if (guna2MessageDialog1.Show("Are you sure you want to delete?") == DialogResult.Yes)
                {
                    string id = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                    string name = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvName"].Value);
                    Transfer_Object.Tables table = new Transfer_Object.Tables(name);
                    table.tId = id;
                    tableBL.Delete(table);
                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Show("Delete sucessfully");
                    GetData();
                }

            }
        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && guna2DataGridView1.Columns[e.ColumnIndex].Name == "dgvSno")
            {
                e.Value = e.RowIndex + 1; // Gán số thứ tự từ 1 đến n
            }
        }
    }
}
