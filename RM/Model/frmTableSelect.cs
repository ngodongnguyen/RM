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

namespace RM.Model
{
    public partial class frmTableSelect : Form
    {
        private TableBL tableBL;
        private tblMainBL mainBL;
        public frmTableSelect()
        {
            InitializeComponent();
            tableBL = new TableBL();
            mainBL = new tblMainBL();
        }

        public string TableName;

        private void frmTableSelect_Load(object sender, EventArgs e)
        {
            List<Transfer_Object.Tables>tables = tableBL.GetTables();
            List<tblMain> tblMains = mainBL.GetTables();
            bool hasAvailableTable = false; // Biến kiểm tra xem có bàn nào hợp lệ hay không

            foreach (var table in tables)
            {
                Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                b.Text = table.tName.ToString();
                b.Width = 150;
                b.Height = 50;
                b.FillColor = Color.FromArgb(241, 85, 126);
                b.HoverState.FillColor = Color.FromArgb(50, 55, 89);
                var matchingTable = tblMains.FirstOrDefault(t => t.TableName == table.tName);

                // Nếu bàn có trong tblMain và trạng thái là "Paid", cho phép nhấn
                if (matchingTable != null)
                {
                    b.Enabled = true;
                    hasAvailableTable = true; // Đánh dấu là có bàn hợp lệ

                }
                else
                {
                    // Nếu bàn không có trong tblMain hoặc trạng thái không phải "Paid", vô hiệu hóa nút
                    b.Enabled = false;
                    b.FillColor = Color.Gray;  // Màu xám cho bàn không cho phép chọn
                }

                //event for click
                b.Click += new EventHandler(b_Click);
                flowLayoutPanel1.Controls.Add(b);
            }
            if (!hasAvailableTable)
            {
                MessageBox.Show("Không có bàn nào có trạng thái 'Paid'.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void b_Click(object sender, EventArgs e)
        {
            TableName = (sender as Guna.UI2.WinForms.Guna2Button).Text.ToString();
            this.Close();
        }
    }
}
