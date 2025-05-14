using Bussiness_Layer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Transfer_Object;

namespace RM.Model
{
    public partial class frmCheckout : SampleAdd
    {
        private tblMainBL tblMainBL;
        public string customerName;
        public double total;
        private PromotionBL promotionBL;
        public frmCheckout()
        {
            InitializeComponent();
            tblMainBL = new tblMainBL();
            promotionBL=new PromotionBL();
            LoadImage();
        }
        public frmCheckout(string customerName, double total)
        {
            this.customerName = customerName;
            this.total = total;
            InitializeComponent();
            tblMainBL = new tblMainBL();
            promotionBL = new PromotionBL();
            LoadImage();
        }

        private void frmCheckout_Load(object sender, EventArgs e)
        {
            txtBillAmount.Text = amt.ToString();
            var promotions = promotionBL.GetPromotions();
            var activePromotions = promotions.Where(p => p.status == "Active").ToList();

            // Thêm "None" vào đầu danh sách chương trình khuyến mãi
            activePromotions.Insert(0, new Promotion { promotionId = 0, promotionName = "None" });

            // Cập nhật DataSource của ComboBox
            cbPromotion.DataSource = activePromotions;
            cbPromotion.DisplayMember = "promotionName";  // Hiển thị tên chương trình khuyến mãi
            cbPromotion.ValueMember = "promotionId";
            cbPromotion.SelectedIndexChanged += cbPromotion_SelectedIndexChanged;

        }

        private void LoadImage()
        {
            guna2PictureBox1.Image = Properties.Resources.productPic;
            guna2PictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

        }

        public double amt;
        public int MainID = 0;
        public double change;
        private void txtReceived_TextChanged(object sender, EventArgs e)
        {
            double amt = 0;
            double receipt = 0;

            double.TryParse(txtBillAmount.Text, out amt);
            double.TryParse(txtReceived.Text, out receipt);

            change =Math.Abs(amt - receipt); //always positive
            txtChange.Text = change.ToString();
        }

        public override void btnSave_Click(object sender, EventArgs e)
        {
            double amt = 0;
            double receipt = 0;

            double.TryParse(txtBillAmount.Text, out amt);
            double.TryParse(txtReceived.Text, out receipt);

            // Kiểm tra nếu số tiền nhận được nhỏ hơn tổng tiền cần thanh toán
            if (receipt < amt)
            {
                // Hiển thị thông báo khi số tiền nhận được không đủ
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Show("The received amount is not enough to make payment. Please check again.");
                return; // Dừng lại không thực hiện lưu nếu số tiền nhận được nhỏ hơn
            }

            tblMain tbl = new tblMain();
            tbl.MainID = MainID;
            tbl.Received=Convert.ToSingle(txtReceived.Text);
            tbl.Total = Convert.ToSingle(txtBillAmount.Text);
            tbl.Change = change;

            tblMainBL.Update_Bill(tbl);
            guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
            guna2MessageDialog1.Show("Saved successfully");
            this.Close();
        }
        private void cbPromotion_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy giá trị ID của khuyến mãi được chọn
            int selectedPromotionId = Convert.ToInt32(cbPromotion.SelectedValue);

            // Kiểm tra nếu không chọn "None" (promotionId = 0)
            if (selectedPromotionId != 0)
            {
                // Lấy thông tin khuyến mãi từ danh sách (hoặc cơ sở dữ liệu)
                var selectedPromotion = promotionBL.GetPromotions().FirstOrDefault(p => p.promotionId == selectedPromotionId);

                if (selectedPromotion != null)
                {
                    // Trừ giá trị giảm giá từ tổng
                    double newAmount = amt - selectedPromotion.discount_value;
                    if (newAmount < 0)
                    {
                        newAmount = 0;
                    }
                    txtBillAmount.Text = newAmount.ToString();
                }
            }
            else
            {
                // Nếu không chọn khuyến mãi (chọn "None"), giữ nguyên giá trị ban đầu
                txtBillAmount.Text = amt.ToString();
            }
        }

        private void btnBankTransfer_Click(object sender, EventArgs e)
        {
            frmTransfer bankTransferForm = new frmTransfer(customerName,total,MainID,this);

            // Hiển thị form frmBankTransfer (chuyển sang form mới)
            bankTransferForm.Show();
        }
        public void btnSave_Click_Transfer(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
