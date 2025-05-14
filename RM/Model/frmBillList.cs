using Bussiness_Layer;
using RM.Reports;
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

namespace RM.Model
{
    public partial class frmBillList : SampleAdd
    {
        private tblMainBL tblMainBL;
        public frmBillList()
        {
            InitializeComponent();
            LoadImage();
            tblMainBL = new tblMainBL();
        }

        public int MainID = 0;
        private void LoadImage()
        {
            guna2PictureBox1.Image = Properties.Resources.productPic;
            guna2PictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

        }

        private void frmBillList_Load(object sender, EventArgs e)
        {
            LoadData();

        }

        private void LoadData()
        {
            // Ẩn các cột nút khi tải dữ liệu
            //guna2DataGridView1.Columns["dgvedit"].Visible = false;
            //guna2DataGridView1.Columns["dgvdel"].Visible = false;

            var bills = tblMainBL.GetBillPending();
            guna2DataGridView1.DataSource = bills;

            // Ẩn các cột không cần thiết
            guna2DataGridView1.Columns["aDate"].Visible = false;
            guna2DataGridView1.Columns["tTime"].Visible = false;
            guna2DataGridView1.Columns["received"].Visible = false;
            guna2DataGridView1.Columns["Change"].Visible = false;
            guna2DataGridView1.Columns["aTime"].Visible = false;
            guna2DataGridView1.Columns["CustName"].Visible = false;
            guna2DataGridView1.Columns["DriverID"].Visible = false;
            guna2DataGridView1.Columns["CustPhone"].Visible = false;
            guna2DataGridView1.Columns["Month"].Visible = false;


            // Di chuyển cột "dgvedit" và "dgvdel" vào cuối DataGridView
            guna2DataGridView1.Columns["dgvedit"].DisplayIndex = guna2DataGridView1.Columns.Count - 2;  // Di chuyển "dgvedit" vào vị trí cuối
            guna2DataGridView1.Columns["dgvdel"].DisplayIndex = guna2DataGridView1.Columns.Count - 1;   // Di chuyển "dgvdel" vào vị trí cuối

            // Đảm bảo DataGridView được điền đầy đủ dữ liệu
            guna2DataGridView1.Refresh();
        }


        private void HideEmptyColumns()
        {
            foreach (DataGridViewColumn column in guna2DataGridView1.Columns)
            {
                bool isEmpty = true;

                foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                {
                    var cellValue = row.Cells[column.Name].Value;

                    if (cellValue != null)
                    {
                        // Kiểm tra giá trị số bằng 0
                        if (column.ValueType == typeof(int) || column.ValueType == typeof(float) || column.ValueType == typeof(double))
                        {
                            if (Convert.ToDouble(cellValue) != 0)
                            {
                                isEmpty = false;
                                break;
                            }
                        }
                        // Kiểm tra giá trị ngày không có dữ liệu
                        else if (column.ValueType == typeof(DateTime))
                        {
                            if (DateTime.TryParse(cellValue.ToString(), out DateTime dateValue))
                            {
                                if (dateValue != DateTime.MinValue) // Ngày hợp lệ khác DateTime.MinValue
                                {
                                    isEmpty = false;
                                    break;
                                }
                            }
                        }
                        //Kiểm tra giá trị string không rỗng
                        else if (!string.IsNullOrEmpty(cellValue.ToString()))
                        {
                            isEmpty = false;
                            break;
                        }
                    }
                }

                // Ẩn cột nếu tất cả các giá trị là null, rỗng, 0, hoặc ngày không có giá trị
                if (isEmpty)
                {
                    column.Visible = false;
                }
                else
                {
                    column.Visible = true;
                }
            }
        }


        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && guna2DataGridView1.Columns[e.ColumnIndex].Name == "dgvSno")
            {
                e.Value = e.RowIndex + 1; // Gán số thứ tự từ 1 đến n
            }
            // for searil no
            //int count = 0;

            //foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            //{
            //    count++;
            //    row.Cells[0].Value = count;
            //}
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                //this is changes as we have to set form text properties before open

                MainID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                this.Close();
               
            }

            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                MainID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                string qry = "Select * from tblMain m inner join tblDetails d on d.MainID = m.MainID inner join products p on p.pID = d.proID Where m.MainID = " + MainID + " ";

                SqlCommand cmd = new SqlCommand(qry, MainClass.con);
                MainClass.con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                MainClass.con.Close();
                rptBill cr = new rptBill();
                frmPrint frm = new frmPrint();

                cr.SetDatabaseLogon("sa", "Ngodongnguyen2004"); // Đây là tài khoản Windows của bạn, không cần mật khẩu nếu bạn không yêu cầu mật khẩu
                cr.SetDataSource(dt);
                //cr.SetParameterValue("MainID", tmp);

                frm.crystalReportViewer1.ReportSource = cr;
                frm.crystalReportViewer1.Refresh();
                frm.Show();
            }
        }
    }
}
