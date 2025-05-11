using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using Bussiness_Layer;
using Transfer_Object;

namespace RM.Model
{
    public partial class frmTransfer : SampleAdd
    {
        private tblMainBL tblMainBL;
        public string customerName;
        public double total;
        public int MainID = 0;
        private frmCheckout checkoutForm;

        public frmTransfer()
        {
            InitializeComponent();
            tblMainBL = new tblMainBL();
        }
        public frmTransfer(string customerName, double total,int mainId,frmCheckout checkoutForm)
        {
            this.customerName = customerName;
            this.total = total;
            this.MainID = mainId;
            InitializeComponent();
            tblMainBL = new tblMainBL();
            this.checkoutForm = checkoutForm;
        }

        // Phương thức gọi API
        private async Task CallApiGenerateQr(double amount, string memo)
        {
            // URL của Node.js API
            string apiUrl = "http://localhost:3001/generate_qr";

            // Tạo đối tượng yêu cầu với dữ liệu từ form
            var requestData = new
            {
                amount = amount,
                memo = memo
            };

            // Chuyển đối tượng yêu cầu thành chuỗi JSON
            string json = JsonConvert.SerializeObject(requestData);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            // Tạo HttpClient và gửi yêu cầu POST
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Gửi yêu cầu POST đến API
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                    response.EnsureSuccessStatusCode(); // Đảm bảo rằng phản hồi thành công

                    // Đọc nội dung phản hồi từ API
                    string responseBody = await response.Content.ReadAsStringAsync();
                    string fileName = JsonConvert.DeserializeObject<dynamic>(responseBody).filename.ToString();

                    // Tạo đường dẫn đến file ảnh QR
                    string qrImagePath = @"C:\Restaurant\RM\Resources\qr_codes\" + fileName;

                    // Tải hình ảnh mã QR từ file và hiển thị vào PictureBox
                    pictureBox1.Image = Image.FromFile(qrImagePath);
                    // Hiển thị hoặc xử lý kết quả ở đây
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu có
                    MessageBox.Show("Error calling API: " + ex.Message);
                }
            }
        }

        // Phương thức để gọi khi nhấn nút chuyển tiền

        private async void frmTransfer_Load(object sender, EventArgs e)
        {
            frmPOS posForm = new frmPOS();

            // Lấy dữ liệu từ form (số tiền và ghi chú)
            double amount = total;  // Giả sử có TextBox txtAmount
            string Name = customerName;  // Giả sử có TextBox txtCustomerName

            // Tạo chuỗi memo kết hợp với tên khách hàng
            string memo = $"Thanh toán cho khách hàng: {Name}";
            // Gọi API để tạo mã QR
            await CallApiGenerateQr(amount, memo);
        }
        public override void btnSave_Click(object sender, EventArgs e)
        {
            tblMain tbl = new tblMain();
            tbl.MainID = MainID;
            tblMainBL.Update_Transfer(tbl);
            guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
            guna2MessageDialog1.Show("Saved successfully");
            this.Close();
            checkoutForm.btnSave_Click_Transfer(sender, e);
        }
    }

}
