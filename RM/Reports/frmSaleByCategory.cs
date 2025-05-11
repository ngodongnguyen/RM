using Bussiness_Layer;
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

namespace RM.Reports
{
    public partial class frmSaleByCategory : Form
    {
        private tblMainBL tblMainBL;
        public frmSaleByCategory()
        {
            InitializeComponent();
            tblMainBL = new tblMainBL();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {

            List<MDPC> mDPCs = tblMainBL.GetMDPCs(Convert.ToDateTime(dateTimePicker1.Value).Date, Convert.ToDateTime(dateTimePicker2.Value).Date);

            // Chuyển danh sách sản phẩm thành DataTable để hiển thị trên Crystal Report
            DataTable dt = new DataTable();
            dt.Columns.Add("MainID");
            dt.Columns.Add("proID");
            dt.Columns.Add("categoryID");
            dt.Columns.Add("catID");
            dt.Columns.Add("aDate");
            dt.Columns.Add("pName");
            dt.Columns.Add("qty");
            dt.Columns.Add("price");
            dt.Columns.Add("amount");
            dt.Columns.Add("catName");


            // Điền dữ liệu vào DataTable
            foreach (var mdpc in mDPCs)
            {
                DataRow row = dt.NewRow();
                row["MainID"] = mdpc.MainID;
                row["proID"] = mdpc.proID;
                row["categoryID"] = mdpc.categoryID;
                row["catID"] = mdpc.catID;
                row["aDate"] = mdpc.aDate;
                row["pName"] = mdpc.pName;
                row["qty"] = mdpc.qty;
                row["price"] = mdpc.price;
                row["amount"] = mdpc.amount;
                row["catName"] = mdpc.catName;


                dt.Rows.Add(row);
            }
            frmPrint frm = new frmPrint();
            rpSaleByCategory cr = new rpSaleByCategory();

            // Cấu hình lại kết nối cơ sở dữ liệu trong Crystal Report
            cr.SetDatabaseLogon("sa", "Ngodongnguyen2004", "NGUYEN", "RM");
            cr.SetDataSource(dt);
            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.Refresh();
            frm.Show();
        }
    }
}
