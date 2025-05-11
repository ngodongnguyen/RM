using Bussiness_Layer;
using System;
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
    public partial class frmPromotionAdd : SampleAdd
    {
        private PromotionBL promotionBL;
        public frmPromotionAdd()
        {
            InitializeComponent();
            promotionBL = new PromotionBL();
        }
        public int id = 0;

        public override void btnSave_Click(object sender, EventArgs e)
        {
            string name=txtName.Text;
            float discount=Convert.ToSingle(txtDiscount.Text);
            string status=txtStatus.Text;
            Promotion promotion=new Promotion(name,discount, status);
            if (id == 0)
            {
                promotionBL.Add(promotion);
                guna2MessageDialog1.Show("Saved successfully");
            }
            else  // Nếu là cập nhật (Id khác 0)
            {
                promotion.promotionId = id;

                id = 0;
                promotionBL.Update(promotion);  // Cập nhật đối tượng vào database
                guna2MessageDialog1.Show("Updated successfully");
            }
        }
        private void frmPromotionAdd_Load(object sender, EventArgs e)
        {

        }
    }
}
