using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Transfer_Object;
using Bussiness_Layer;
namespace RM.Model
{
    public partial class frmCategoryAdd : SampleAdd
    {
        private CategoryBL categoryBL;
        public frmCategoryAdd()
        {
            InitializeComponent();
            categoryBL = new CategoryBL();

        }

        public int id = 0;

        public override void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text;  
                int categoryId = id;  

                Category category = new Category(name);  
                category.Id = categoryId.ToString();  

                // Kiểm tra xem Id có giá trị hay không để quyết định Add hoặc Update
                if (id == 0)  
                {
                    categoryBL.Add(category); 
                    guna2MessageDialog1.Show("Saved successfully");
                }
                else  // Nếu là cập nhật (Id khác 0)
                {
                    id = 0;
                    categoryBL.Update(category);  // Cập nhật đối tượng vào database
                    guna2MessageDialog1.Show("Updated successfully");
                }
            }
            catch (Exception ex)
            {
                // Nếu có lỗi, hiển thị thông báo lỗi
                guna2MessageDialog1.Show("Error: " + ex.Message);
            }
        }


        private void frmCategoryAdd_Load(object sender, EventArgs e)
        {
         
        }
    }
}
