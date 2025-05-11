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
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Transfer_Object;

namespace RM.Model
{
    public partial class frmEmail : SampleAdd
    {
        private tblMainBL tblMainBL;
        public frmEmail()
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
            var bills = tblMainBL.GetBillPending();
            guna2DataGridView1.DataSource = bills;

            // Đảm bảo DataGridView được điền đầy đủ dữ liệu
            guna2DataGridView1.Refresh();

            // Lặp qua từng cột và ẩn cột không có dữ liệu
            HideEmptyColumns();


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
                        // Kiểm tra giá trị string không rỗng
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


            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "Email")
            {
                MainID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                string qry = "Select * from tblMain m inner join tblDetails d on d.MainID = m.MainID inner join products p on p.pID = d.proID Where m.MainID = " + MainID + " ";

                SqlCommand cmd = new SqlCommand(qry, MainClass.con);
                MainClass.con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                MainClass.con.Close();
                frmPrint frm = new frmPrint();
                rptBill cr = new rptBill();

                cr.SetDatabaseLogon("sa", "Ngodongnguyen2004"); // Đây là tài khoản Windows của bạn, không cần mật khẩu nếu bạn không yêu cầu mật khẩu
                cr.SetDataSource(dt);
                frm.crystalReportViewer1.ReportSource = cr;
                frm.crystalReportViewer1.Refresh();
                frmEmailInput emailInputForm = new frmEmailInput();
                if (emailInputForm.ShowDialog() == DialogResult.OK)
                {
                    string recipientEmail = emailInputForm.EmailAddress;

                    // Gửi email với báo cáo đính kèm
                    string pdfFilePath = "C:\\Restaurant\\Bill\\bill_" + MainID + ".pdf"; // Đường dẫn lưu file PDF
                    cr.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfFilePath);  // Xuất báo cáo ra PDF

                    SendEmailWithAttachment(recipientEmail, "Your Invoice", "Please find attached the invoice for your recent transaction.", pdfFilePath);
                }

            }
        }
        private void SendEmailWithAttachment(string recipientEmail, string subject, string body, string attachmentPath)
        {
            try
            {
                // Cấu hình SMTP client
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("ngodongnguyen27@gmail.com", "cnqy nawh opnm ykmb"),
                    EnableSsl = true
                };

                // Tạo đối tượng MailMessage
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("ngodongnguyen27@gmail.com"),
                    Subject = subject,
                    Body = body
                };

                // Thêm người nhận
                mailMessage.To.Add(recipientEmail);

                // Thêm file đính kèm
                Attachment attachment = new Attachment(attachmentPath);
                mailMessage.Attachments.Add(attachment);

                // Gửi email
                smtpClient.Send(mailMessage);

                MessageBox.Show("Email sent successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send email: " + ex.Message);
            }
        }
    }
}
