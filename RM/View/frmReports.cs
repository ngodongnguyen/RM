using Bussiness_Layer;
using RM.Reports;
using System;
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
    public partial class frmReports : Form
    {
        private StaffBL staffBL;
        private ProductBL productBL;
        public frmReports()
        {
            InitializeComponent();
            staffBL = new StaffBL();
            productBL = new ProductBL();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy danh sách nhân viên
                List<Staff> staffList = staffBL.GetStaff();

                // Kiểm tra xem có dữ liệu không
                if (staffList.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu nhân viên!");
                    return;
                }

                // Tạo DataTable để chứa dữ liệu
                DataTable dt = new DataTable();
                dt.Columns.Add("sName");
                dt.Columns.Add("sRole");
                dt.Columns.Add("sPhone");

                // Thêm dữ liệu vào DataTable
                foreach (var staff in staffList)
                {
                    DataRow row = dt.NewRow();
                    row["sName"] = staff.sName;
                    row["sRole"] = staff.sRole;
                    row["sPhone"] = staff.sPhone;
                    dt.Rows.Add(row);
                }

                // Tạo đối tượng form và báo cáo
                frmPrint frm = new frmPrint();
                rptStaffList cr = new rptStaffList();

                // Kết nối với cơ sở dữ liệu
                cr.SetDatabaseLogon("sa", "huynhtrongnguyen739904");

                // Gán DataTable vào báo cáo
                cr.SetDataSource(dt);

                // Đưa báo cáo vào crystalReportViewer
                frm.crystalReportViewer1.ReportSource = cr;
                frm.crystalReportViewer1.Refresh();

                // Hiển thị form
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }


        private void btnMenu_Click(object sender, EventArgs e)
        {
            List<Product> productList = productBL.GetProducts();

            // Chuyển danh sách sản phẩm thành DataTable để hiển thị trên Crystal Report
            DataTable dt = new DataTable();
            dt.Columns.Add("pName");
            dt.Columns.Add("pPrice");
            dt.Columns.Add("pImage", typeof(byte[])); // Cột hình ảnh là kiểu byte[] để lưu dữ liệu hình ảnh

            // Điền dữ liệu vào DataTable
            foreach (var product in productList)
            {
                DataRow row = dt.NewRow();
                row["pName"] = product.pName;
                row["pPrice"] = product.pPrice;
                row["pImage"] = product.pImage; // Cột hình ảnh lưu dưới dạng byte[]
                dt.Rows.Add(row);
            }
            frmPrint frm = new frmPrint();
            rptMenu cr = new rptMenu();

            cr.SetDatabaseLogon("sa", "huynhtrongnguyen739904");
            cr.SetDataSource(dt);
            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.Refresh();
            frm.Show();
        }

        private void btnSaleCat_Click(object sender, EventArgs e)
        {
            frmSaleByCategory frm = new frmSaleByCategory();
            frm.ShowDialog();
        }
    }
}
