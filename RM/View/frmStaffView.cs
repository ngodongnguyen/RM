﻿using Bussiness_Layer;
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
    public partial class frmStaffView : SampleView
    {
        private StaffBL staffBL;
        public frmStaffView()
        {
            InitializeComponent();
            staffBL = new StaffBL();
        }

        private void frmStaffView_Load(object sender, EventArgs e)
        {
            GetData();
        }

        public void GetData()
        {
            try
            {
                // Nạp dữ liệu vào DataGridView
                guna2DataGridView1.DataSource = staffBL.GetStaff();


                // Kiểm tra cột "Role" có tồn tại trong DataGridView
                if (guna2DataGridView1.Columns.Contains("dgvrole"))
                {
                    // Tìm chỉ mục của cột "Role"
                    int roleColumnIndex = guna2DataGridView1.Columns["dgvrole"].Index;
                    guna2DataGridView1.Columns["dgvrole"].DisplayIndex = roleColumnIndex - 2;
                    // Di chuyển "dgvedit" và "dgvdel" vào sau cột "Role"
                    guna2DataGridView1.Columns["dgvedit"].DisplayIndex = roleColumnIndex;
                    guna2DataGridView1.Columns["dgvdel"].DisplayIndex = roleColumnIndex +1;
                }
                else
                {
                    MessageBox.Show("Cột 'sRole' không tồn tại trong DataGridView.");
                }
                guna2DataGridView1.Refresh();

                // Đảm bảo DataGridView được điền đầy đủ dữ liệu
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public override void btnAdd_Click(object sender, EventArgs e)
        {
            MainClass.BlurBackground(new Model.frmStaffAdd());
            //frmCategoryAdd frm = new frmCategoryAdd();
            //frm.ShowDialog();
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
                frmStaffAdd frm = new frmStaffAdd();
                frm.id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                frm.txtName.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvName"].Value);
                frm.txtPhone.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvPhone"].Value);
                frm.cbRole.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvRole"].Value);

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
                    string id = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                    string name = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvName"].Value);
                    string role = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvRole"].Value);
                    string phone = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvPhone"].Value);
                    Transfer_Object.Staff staff = new Transfer_Object.Staff(name,role,phone);
                    staff.staffId = id;
                    staffBL.Delete(staff);
                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Show("Delete successfully");
                    GetData();
                }

            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
