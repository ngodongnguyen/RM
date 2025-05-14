using Bussiness_Layer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Transfer_Object;

namespace RM.Model
{
    public partial class frmProductAdd : SampleAdd
    {
        private ProductBL productBL;
        private CategoryBL categoryBL;
        public frmProductAdd()
        {
            InitializeComponent();
            productBL = new ProductBL();
            categoryBL = new CategoryBL();
        }

        public int id = 0;
        public int cID = 0;
        private void frmProductAdd_Load(object sender, EventArgs e)
        {
            List<Category> categories = categoryBL.GetCategories();

            // Đảm bảo ComboBox trống trước khi thêm dữ liệu
            cbCat.DataSource = categories;

            cbCat.DisplayMember = "CatName";                  // Hiển thị tên danh mục
            cbCat.ValueMember = "Id";
        }


        string filePath;
        Byte[] imageByteArray;
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images(.jpg, .png)|*.png; *.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            { filePath = ofd.FileName;
                txtImage.Image = new Bitmap(filePath);
            }

        }
        public override void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text;
                string price = txtPrice.Text;
                string categoryID = cbCat.SelectedValue.ToString();
                Image temp = new Bitmap(txtImage.Image);
                MemoryStream ms = new MemoryStream();
                temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                imageByteArray = ms.ToArray();
                // Nếu có giá trị hợp lệ, tiến hành tạo sản phẩm
                Product product = new Product(name, price, categoryID,imageByteArray);
                // Kiểm tra SelectedValue trước khi sử dụng
                if (id==0)
                {

                    productBL.Add(product);

                    guna2MessageDialog1.Show("Saved successfully");
                }
                else
                {
                    product.pId = id.ToString();
                    productBL.Update(product);
                    guna2MessageDialog1.Show("Update succesfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }



    }
}
