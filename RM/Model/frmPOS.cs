using Guna.UI2.WinForms;
using System;
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
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.VisualStyles;
using System.Collections;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Bussiness_Layer;
using Transfer_Object;

namespace RM.Model
{
    public partial class frmPOS : Form
    {
        private ProductBL productBL;
        private CategoryBL categoryBL;
        private tblMainBL tblMainBL;
        private tblDetailsBL tblDetailsBL;
        public frmPOS()
        {
            InitializeComponent();
            productBL = new ProductBL();
            categoryBL = new CategoryBL();
            tblMainBL = new tblMainBL();
            tblDetailsBL = new tblDetailsBL();
            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "dgvDelete";
            deleteButtonColumn.HeaderText = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            guna2DataGridView1.Columns.Add(deleteButtonColumn);

            // Đăng ký sự kiện CellClick cho DataGridView
            guna2DataGridView1.CellClick += guna2DataGridView1_CellClick;
        }

        public int MainID = 0;
        public string OrderType = "";
        public int driverID = 0;
        public string customerName = "";
        public string customerPhone = "";
        public double total,change,receive;
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPOS_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.BorderStyle = BorderStyle.FixedSingle;
            AddCategory();
            ProductPanel.Controls.Clear();
            LoadProduct();
        }

        private void AddCategory()
        {
            CategoryPanel.Controls.Clear();

            // Thêm nút "All Categories" đầu tiên
            Guna2Button btnAll = new Guna2Button();
            btnAll.Text = "All Categories";
            btnAll.Tag = "all"; // Đánh dấu đặc biệt
            btnAll.FillColor = Color.FromArgb(50, 55, 89);
            btnAll.Size = new Size(134, 45);
            btnAll.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btnAll.Checked = true; // Mặc định được chọn
            btnAll.Click += b_Click;
            CategoryPanel.Controls.Add(btnAll);
            // Giả sử CategoryDL là đối tượng để tương tác với cơ sở dữ liệu
            CategoryBL categoryBL = new CategoryBL();
            List<Category> categories=categoryBL.GetCategories();


            // Duyệt qua danh sách các danh mục và thêm nút cho từng danh mục
            foreach (var category in categories)
            {
                Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                b.FillColor = Color.FromArgb(50, 55, 89);
                b.Size = new Size(134, 45);
                b.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
                b.Text = category.CatName;

                // Sự kiện khi nhấn nút danh mục
                b.Click += new EventHandler(b_Click);
                CategoryPanel.Controls.Add(b);
            }
        }


        private void b_Click(object sender, EventArgs e)
        {

            Guna.UI2.WinForms.Guna2Button b = (Guna.UI2.WinForms.Guna2Button)sender;
            string selectedCategory = b.Text.Trim().ToLower();  // Loại bỏ khoảng trắng và chuyển thành chữ thường
            string id_find=categoryBL.Find(new Category(selectedCategory));
            string selectedValue = b.Tag?.ToString();

            if (selectedValue == "all")
            {
                foreach (var item in ProductPanel.Controls)
                {
                    var pro = (usProduct)item;
                    pro.Visible = true; // Hiển thị tất cả sản phẩm
                }
                return;
            }

            foreach (var item in ProductPanel.Controls)
            {
                var pro = (usProduct)item;
                string productCategory = pro.PCategory?.Trim().ToLower();  


                if (productCategory != null && productCategory.Contains(id_find))
                {
                    pro.Visible = true;
                }
                else
                {
                    pro.Visible = false;
                }
            }
        }





        private void AddItems(string id, String proID, string name, string cat, string price, Image pimage)
        {
            var w = new usProduct
            {
                PName = name,
                PPrice = price,
                PCategory = cat,
                PImage = pimage,
                id = Convert.ToInt32(proID)
            };

            ProductPanel.Controls.Add(w);

            w.onSelect += (ss, ee) =>
            {
                var wdg = (usProduct)ss;

                foreach (DataGridViewRow item in guna2DataGridView1.Rows)
                {
                    //this will check it product already there then a one to quantity and update price
                    if (Convert.ToInt32(item.Cells["dgvproID"].Value) == wdg.id)
                    {
                        item.Cells["dgvQty"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) + 1;
                        item.Cells["dgvAmount"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) *
                                                         double.Parse(item.Cells["dgvPrice"].Value.ToString());
                        return;
                    }
                }
                //this line add new product First for sr# and 2nd 0 from id
                guna2DataGridView1.Rows.Add(new object[] { 0, 0, wdg.id, wdg.PName, 1, wdg.PPrice, wdg.PPrice });
                GetTotal();
            };
        }
        private void guna2DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu là ô số lượng hoặc giá bị thay đổi
            if (e.ColumnIndex == guna2DataGridView1.Columns["dgvQty"].Index || e.ColumnIndex == guna2DataGridView1.Columns["dgvPrice"].Index)
            {
                DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];

                // Lấy giá trị ô số lượng và giá
                var qtyCell = row.Cells["dgvQty"];
                var priceCell = row.Cells["dgvPrice"];
                var amountCell = row.Cells["dgvAmount"];

                // Kiểm tra nếu giá trị trong ô số lượng và giá hợp lệ
                if (int.TryParse(qtyCell.Value.ToString(), out int qty) && double.TryParse(priceCell.Value.ToString(), out double price))
                {
                    // Tính lại số tiền (amount) cho sản phẩm
                    amountCell.Value = qty * price;

                    // Gọi hàm GetTotal() để tính lại tổng giỏ hàng
                    GetTotal();
                }
                else
                {
                    // Nếu giá trị không hợp lệ, hiển thị thông báo lỗi
                    MessageBox.Show("Số lượng hoặc giá không hợp lệ.");
                }
            }
        }


        private void LoadProduct()
        {
            // Gọi phương thức GetProductsJoinCategory từ Business Logic Layer (BLL)
            List<Product> products = productBL.GetProductsJoinCategory();

            // Duyệt qua danh sách sản phẩm trả về
            foreach (var product in products)
            {
                // Kiểm tra xem hình ảnh có tồn tại hay không
                if (product.pImage != null && product.pImage.Length > 100) // Kiểm tra nếu mảng byte lớn hơn 100 byte
                {
                    try
                    {
                        using (MemoryStream ms = new MemoryStream(product.pImage))
                        {
                            // Chuyển đổi mảng byte thành hình ảnh
                            Image image = Image.FromStream(ms);
                            // Thêm sản phẩm vào danh sách Items
                            AddItems("0", product.pId, product.pName, product.categoryId, product.pPrice, image);
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }



        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            foreach(var item in ProductPanel.Controls)
            {
                var pro = (usProduct)item;
                pro.Visible = pro.PName.ToLower().Contains(txtSearch.Text.Trim().ToLower());
            }
        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // for searil no
            int count = 0;

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        private void GetTotal()
        {
            double tot = 0;
            lblTotal.Text = "";
            //foreach (DataGridViewRow item in guna2DataGridView1.Rows)
            //{
            //    tot += double.Parse(item.Cells["dgvAmount"].Value.ToString());
            //}
            foreach (DataGridViewRow item in guna2DataGridView1.Rows)
            {
                if (item.Cells["dgvAmount"].Value != null) // Kiểm tra null trước khi sử dụng
                {
                    double amount;
                    if (double.TryParse(item.Cells["dgvAmount"].Value.ToString(), out amount))
                    {
                        tot += amount;
                    }
                }
            }

            lblTotal.Text = tot.ToString("N2");
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblWaiter.Visible = false;
            lblTable.Visible = false;
            guna2DataGridView1.Rows.Clear();
            MainID = 0;
            lblTotal.Text = "00";
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {

            lblTable.Text = "";
            lblWaiter.Text = "";
            lblWaiter.Visible = false;
            lblTable.Visible = false;
            OrderType = "Delivery";

            frmAddCusTomer frm = new frmAddCusTomer();
            frm.mainID = MainID;
            frm.OrderType= OrderType;
            MainClass.BlurBackground(frm);

            if (frm.txtName.Text != "") // as take away did not have driver info
            {
                driverID = frm.driverID;
                lblDriverName.Text = "Customer Name: " + frm.txtName.Text + "Phone: " + frm.txtPhone.Text + "Driver: " + frm.cbDriver.Text;
                lblDriverName.Visible = true;
                customerName = frm.txtName.Text;
                customerPhone = frm.txtPhone.Text;
            }
        }

        private void btnTake_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblWaiter.Visible = false;
            lblTable.Visible = false;
            OrderType = "Take Away";

            frmAddCusTomer frm = new frmAddCusTomer();
            frm.mainID = MainID;
            frm.OrderType= OrderType;
            MainClass.BlurBackground(frm);

            if (frm.txtName.Text != "") // as take away did not have driver info
            {
                driverID = frm.driverID;
                lblDriverName.Text="Customer Name: " + frm.txtName.Text + "Phone: " + frm.txtPhone.Text;
                lblDriverName.Visible = true;
                customerName = frm.txtName.Text;
                customerPhone = frm.txtPhone.Text;
            }
        }

        private void btnDin_Click(object sender, EventArgs e)
        {
            OrderType = "Din In";
            lblTable.Visible = false;
            //need to create form for table selection and waiter selection
            frmTableSelect frm = new frmTableSelect();
            MainClass.BlurBackground(frm);
            if (frm.TableName != "")
            {
                lblTable.Text=frm.TableName;
                lblTable.Visible=true;
            }
            else
            {
                lblTable.Text = "";
                lblTable.Visible = false;
            }
            frmWaiterSelect frm2 = new frmWaiterSelect();
            MainClass.BlurBackground(frm2);
            if(frm2.WaiterName != "")
            {
                lblWaiter.Text = frm2.WaiterName;
                lblWaiter.Visible = true;
            }
            else
            {
                lblWaiter.Text = "";
                lblWaiter.Visible = false;
            }
        }

        private void btnKot_Click(object sender, EventArgs e)
        {
            // Create tblMain object to store data
            tblMain main = new tblMain
            {
                aDate = DateTime.Now.Date,
                aTime = DateTime.Now.ToShortTimeString(),
                TableName = lblTable.Text,
                WaiterName = lblWaiter.Text,
                Status = "Pending",
                OrderType = OrderType,
                Total = Convert.ToDouble(lblTotal.Text),
                Received = 0, // Initial received value, will update later
                Change = 0, // Change value to be updated later
                DriverID = driverID,
                CustName = customerName,
                CustPhone = customerPhone
            };

            int MainID = 0;

            if (MainID == 0) // Insert operation
            {
                // Use Add method to insert a new record into tblMain
                MainID = tblMainBL.Add(main);  // Assume Add method returns the inserted ID
            }
            else // Update operation
            {
                main.MainID = MainID;  // Set MainID for update
                tblMainBL.Update(main); // Use Update method to update tblMain
            }

            // Insert or update tblDetails based on each row in the DataGridView
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                int detailID = Convert.ToInt32(row.Cells["dgvid"].Value);
                tblDetails detail = new tblDetails
                {
                    MainID = MainID,
                    ProID = Convert.ToInt32(row.Cells["dgvproID"].Value),
                    Qty = Convert.ToInt32(row.Cells["dgvQty"].Value),
                    Price = Convert.ToDouble(row.Cells["dgvPrice"].Value),
                    Amount = Convert.ToDouble(row.Cells["dgvAmount"].Value)
                };
                if (detailID == 0) //Insert
                {
                    tblDetailsBL.Add(detail);
                }

                else //Update
                {
                    detail.DetailID = detailID; // Set the DetailID for updating the record
                    tblDetailsBL.Update(detail);
                }

            }
                // Show success message
                guna2MessageDialog1.Show("Saved successfully");

                // Reset variables for next operation
                MainID = 0;
                guna2DataGridView1.Rows.Clear();
                lblTable.Text = "";
                lblWaiter.Text = "";
                lblWaiter.Visible = false;
                lblTable.Visible = false;
                lblTotal.Text = "00";
                lblDriverName.Text = "";
        }

        public int id = 0;
        private void btnBill_Click(object sender, EventArgs e)
        {
            frmBillList frm = new frmBillList();
            MainClass.BlurBackground(frm);

            if (frm.MainID >0)
            {
                id = frm.MainID;
                MainID = frm.MainID;
                LoadEntries(id);
            }
        }

        private void LoadEntries(int id)
        {

            List<Transfer_Object.tblMainDetail> tblMainList = tblMainBL.LoadEntries(id);

            if (tblMainList.Count > 0)
            {
                tblMainDetail mainDetail = tblMainList.First(); // Lấy phần tử đầu tiên

                // Xử lý các thông tin từ tblMain
                if (mainDetail.OrderType == "Delivery")
                {
                    btnDelivery.Checked = true;
                    lblWaiter.Visible = false;
                    lblTable.Visible = false;
                }
                else if (mainDetail.OrderType == "Take away")
                {
                    btnTake.Checked = true;
                    lblWaiter.Visible = false;
                    lblTable.Visible = false;
                }
                else
                {
                    btnDin.Checked = true;
                    lblWaiter.Visible = true;
                    lblTable.Visible = true;
                }

                guna2DataGridView1.Rows.Clear();

                foreach (var item in tblMainList)
                {
                    lblTable.Text = item.TableName;
                    lblWaiter.Text = item.WaiterName;

                    // Thêm các thông tin vào DataGridView
                    object[] obj = {0, item.DetailID, item.ProID, item.ProName, item.Qty, item.Price, item.Amount };
                    guna2DataGridView1.Rows.Add(obj);
                }

                GetTotal();
            }
            else
                guna2DataGridView1.Rows.Clear();


        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            total = Convert.ToSingle (lblTotal.Text);
            frmCheckout frm = new frmCheckout(customerName,total);
            frm.MainID = id;
            frm.amt = Convert.ToDouble(lblTotal.Text);
            MainClass.BlurBackground(frm);

            MainID = 0;
            guna2DataGridView1.Rows.Clear();
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblWaiter.Visible = false;
            lblTable.Visible = false;
            lblTotal.Text = "00";

        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == guna2DataGridView1.Columns["dgvDelete"].Index && e.RowIndex >= 0 && e.RowIndex < guna2DataGridView1.Rows.Count)
            {
                DataGridViewRow selectedRow = guna2DataGridView1.Rows[e.RowIndex];

                if (MessageBox.Show("Confirm delete", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    guna2DataGridView1.Rows.Remove(selectedRow);

                    int count = 0;
                    foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                    {
                        count++;
                        row.Cells[0].Value = count;
                    }

                    GetTotal();
                }
            }
        }

        private void btnHold_Click(object sender, EventArgs e)
        {

            int detailID = 0;

            if (OrderType == "")
            {
                guna2MessageDialog1.Show("Please select order type");
                return;
            }

            // Create tblMain object to store data
            tblMain main = new tblMain
            {
                aDate = DateTime.Now.Date,
                aTime = DateTime.Now.ToShortTimeString(),
                TableName = lblTable.Text,
                WaiterName = lblWaiter.Text,
                Status = "Hold",
                OrderType = OrderType,
                Total = Convert.ToDouble(lblTotal.Text),
                Received = 0, // Initial received value, will update later
                Change = 0, // Change value to be updated later
                DriverID = driverID,
                CustName = customerName,
                CustPhone = customerPhone
            };

            int MainID = 0;

            if (MainID == 0) // Insert operation
            {
                // Use Add method to insert a new record into tblMain
                MainID = tblMainBL.Add(main);  // Assume Add method returns the inserted ID
            }
            else // Update operation
            {
                main.MainID = MainID;  // Set MainID for update
                tblMainBL.Update(main); // Use Update method to update tblMain
            }

            // Insert or update tblDetails based on each row in the DataGridView
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                detailID = Convert.ToInt32(row.Cells["dgvid"].Value);
                tblDetails detail = new tblDetails
                {
                    MainID = MainID,
                    ProID = Convert.ToInt32(row.Cells["dgvproID"].Value),
                    Qty = Convert.ToInt32(row.Cells["dgvQty"].Value),
                    Price = Convert.ToDouble(row.Cells["dgvPrice"].Value),
                    Amount = Convert.ToDouble(row.Cells["dgvAmount"].Value)
                };
                if (detailID == 0) //Insert
                {
                    tblDetailsBL.Add(detail);
                }

                else //Update
                {
                    detail.DetailID = detailID; // Set the DetailID for updating the record
                    tblDetailsBL.Update(detail);
                }

            }
            // Show success message
            guna2MessageDialog1.Show("Saved successfully");

            // Reset variables for next operation
            MainID = 0;
            guna2DataGridView1.Rows.Clear();
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblWaiter.Visible = false;
            lblTable.Visible = false;
            lblTotal.Text = "00";
            lblDriverName.Text = "";
        }


        private void guna2DataGridView1_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra RowIndex hợp lệ
            if (e.RowIndex >= 0)
            {
                // Kiểm tra xem cột có phải là dgvPrice không (cột giá có index là 5)
                if (e.ColumnIndex == 4)
                {
                    DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];

                    // Kiểm tra xem ô giá và số lượng có giá trị hợp lệ không
                    var qtyCell = row.Cells["dgvQty"];
                    var priceCell = row.Cells["dgvPrice"];
                    var amountCell = row.Cells["dgvAmount"];

                    if (qtyCell.Value != null && priceCell.Value != null)
                    {
                        // Kiểm tra nếu giá trị có thể chuyển đổi được
                        if (int.TryParse(qtyCell.Value.ToString(), out int qty) && double.TryParse(priceCell.Value.ToString(), out double price))
                        {
                            amountCell.Value = qty * price;  // Tính lại số tiền
                            GetTotal();  // Tính lại tổng giỏ hàng
                        }
                        else
                        {
                            MessageBox.Show("Số lượng hoặc giá không hợp lệ.");
                        }
                    }
                }
            }
        }


    }
}

