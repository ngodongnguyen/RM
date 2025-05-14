using Bussiness_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Transfer_Object;

namespace RM.Visualize
{
    public partial class frmFoodInTotal : Form
    {
        private tblDetailsBL tblDetailsBL;

        // Khai báo biểu đồ
        private Chart chart;

        public frmFoodInTotal()
        {
            tblDetailsBL = new tblDetailsBL();
            InitializeComponent();
            InitializeComboBox(); // Gọi hàm này để khởi tạo và hiển thị ComboBox
            InitializeChart(); // Khởi tạo chart một lần duy nhất
            LoadPieChart("Appetizers"); // Hiển thị chart với dữ liệu mặc định (ví dụ "Appetizers")
        }

        // Khởi tạo ComboBox cho các danh mục
        private void InitializeComboBox()
        {
            // Tạo ComboBox
            ComboBox categoryComboBox = new ComboBox();
            categoryComboBox.Items.Add("Appetizers");
            categoryComboBox.Items.Add("Main Course");
            categoryComboBox.Items.Add("Dessert");
            categoryComboBox.Items.Add("Drink");
            categoryComboBox.Items.Add("Special Dish");

            // Đặt vị trí ComboBox trên form
            categoryComboBox.Location = new System.Drawing.Point(20, 20);
            categoryComboBox.SelectedIndexChanged += new EventHandler(CategoryComboBox_SelectedIndexChanged);
            // Thêm ComboBox vào form
            this.Controls.Add(categoryComboBox);

        }

        // Xử lý sự kiện khi người dùng chọn một danh mục từ ComboBox
        private void CategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                // Gọi LoadPieChart với tên danh mục được chọn
                LoadPieChart(comboBox.SelectedItem.ToString());
            }
        }

        // Khởi tạo chart một lần duy nhất
        private void InitializeChart()
        {
            chart = new Chart();
            chart.Dock = DockStyle.Fill;
            this.Controls.Add(chart);

            // Cấu hình ChartArea
            ChartArea chartArea = new ChartArea();
            chart.ChartAreas.Add(chartArea);
        }

        // Cập nhật pie chart theo danh mục
        private void LoadPieChart(string categoryName)
        {
            // Lấy dữ liệu sản phẩm theo danh mục từ tblDetailsBL
            List<ProductRevenue> productRevenues = tblDetailsBL.GetProductRevenues(categoryName);

            if (productRevenues == null || productRevenues.Count == 0)
            {
                MessageBox.Show("No data found for the selected category.");
                return;
            }

            // Tính tổng doanh thu
            double total = productRevenues.Sum(p => p.TotalRevenue);

            // Tính phần trăm cho mỗi sản phẩm
            foreach (var product in productRevenues)
            {
                product.TotalRevenue = (product.TotalRevenue / total); // Tính phần trăm
            }

            // Xóa Series cũ nếu có
            chart.Series.Clear();
            chart.Legends.Clear();

            // Tạo Series mới cho biểu đồ tròn
            Series series = new Series
            {
                Name = "Sales",
                ChartType = SeriesChartType.Pie,  // Biểu đồ tròn
                IsValueShownAsLabel = true,  // Hiển thị giá trị phần trăm trên biểu đồ
                LabelFormat = "#0.##%",  // Hiển thị phần trăm với 2 chữ số sau dấu phẩy
                LegendText = "#VALX" // Chú thích cho tên sản phẩm trong Legend
            };

            // Thêm dữ liệu vào Series
            foreach (var product in productRevenues)
            {
                series.Points.AddXY(product.pName, product.TotalRevenue); // X: Tên sản phẩm, Y: Phần trăm doanh thu
            }

            // Thêm Series vào Chart
            chart.Series.Add(series);
            chart.Legends.Add(new Legend() { Docking = Docking.Top });

            // Đặt màu sắc cho từng phần của biểu đồ
            var colors = new List<System.Drawing.Color>
            {
                System.Drawing.Color.Orange,
                System.Drawing.Color.Blue,
                System.Drawing.Color.Green,
                System.Drawing.Color.Red,
                System.Drawing.Color.Purple
            };

            // Gán màu cho từng phần của biểu đồ
            for (int i = 0; i < series.Points.Count; i++)
            {
                series.Points[i].Color = colors[i % colors.Count]; // Gán màu theo thứ tự và lặp lại khi hết màu
            }

            // Hiển thị tiêu đề cho biểu đồ
            chart.Titles.Clear(); // Xóa tiêu đề cũ
            chart.Titles.Add("Total Revenue Breakdown for " + categoryName);
        }
    }
}
