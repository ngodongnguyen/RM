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
using Transfer_Object;

namespace RM.View
{
    public partial class frmProductView : SampleView
    {
        private ProductBL productBL;
        public frmProductView()
        {
            InitializeComponent();
            productBL = new ProductBL();
        }

        private void frmProductView_Load(object sender, EventArgs e)
        {
            GetData();

        }

        public void GetData()
        {
            try
            {
                var products = productBL.GetProducts();  // Lấy danh sách sản phẩm
                if (products != null && products.Count > 0)
                {
                    guna2DataGridView1.DataSource = products;
                }
                else
                {
                    MessageBox.Show("No data available.");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public override void btnAdd_Click(object sender, EventArgs e)
        {
            MainClass.BlurBackground(new Model.frmProductAdd());
            //frmCategoryAdd frm = new frmAdd();
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
                frmProductAdd frm = new frmProductAdd();
                frm.id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                frm.cID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvcatID"].Value);
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
                    string price = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvPrice"].Value);
                    string pName = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvName"].Value);
                    string categoryID = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvcatID"].Value);
                    Product product=new Product(pName,price,categoryID);
                    product.pId = id;
                    productBL.Delete(product);
                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Show("Delete successfully");
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
