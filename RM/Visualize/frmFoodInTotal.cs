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
using System.Windows.Forms.DataVisualization.Charting;
using Transfer_Object;

namespace RM.Visualize
{
    public partial class frmFoodInTotal : Form
    {
        private tblDetailsBL tblDetailsBL;
        public frmFoodInTotal()
        {
            tblDetailsBL = new tblDetailsBL();
            InitializeComponent();
            LoadPieChart();
        }
        private void LoadPieChart()
        {
            // Dữ liệu mẫu về các sản phẩm và phần trăm doanh thu của chúng
            List<ProductRevenue> productRevenues = tblDetailsBL.GetProductRevenues();
            double total=0;
            foreach (var product in productRevenues)
            {
                total += product.TotalRevenue;
            }
            // Chuyển đổi giá trị thành đơn vị trăm và tính tỷ lệ phần trăm cho từng phần
            foreach (var product in productRevenues)
            {
                product.TotalRevenue = (product.TotalRevenue / total); // Tính phần trăm
            }
            // Tạo đối tượng Chart
            Chart chart = new Chart();
            chart.Dock = DockStyle.Fill;
            this.Controls.Add(chart);

            // Cấu hình ChartArea
            ChartArea chartArea = new ChartArea();
            chart.ChartAreas.Add(chartArea);

            // Tạo Series để vẽ biểu đồ tròn
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

            var colors = new List<System.Drawing.Color>
    {
        System.Drawing.Color.Orange,  // Màu cho "Food in total"
        System.Drawing.Color.Blue,    // Màu cho "Amount of order"
        System.Drawing.Color.Green,   // Màu cho "Additional Item"
        System.Drawing.Color.Red      // Màu cho "Discount"
    };

            // Gán màu cho từng phần của biểu đồ
            for (int i = 0; i < series.Points.Count; i++)
            {
                series.Points[i].Color = colors[i % colors.Count]; // Gán màu theo thứ tự và lặp lại khi hết màu
            }

            // Hiển thị tiêu đề cho biểu đồ
            chart.Titles.Add("Total Revenue Breakdown");
        }

    }
    }
