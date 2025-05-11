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
    public partial class frmTotalRevenue : Form
    {
        private tblMainBL tblMainBL;
        public frmTotalRevenue()
        {
            tblMainBL = new tblMainBL();

            InitializeComponent();
            LoadAndDisplayChart();
        }
        private void LoadAndDisplayChart()
        {
            // Gọi phương thức LoadEntries để lấy dữ liệu
            List<tblMain> dateAmountList = tblMainBL.GetTotal();

            // Tạo một đối tượng Chart
            Chart chart = new Chart();
            chart.Dock = DockStyle.Fill; // Chart sẽ chiếm hết không gian form
            this.Controls.Add(chart);

            // Cấu hình ChartArea
            ChartArea chartArea = new ChartArea();
            chart.ChartAreas.Add(chartArea);

            // Tạo một Series để vẽ biểu đồ cột
            Series series = new Series
            {
                Name = "Daily Sales",
                ChartType = SeriesChartType.Column, // Biểu đồ cột
                IsValueShownAsLabel = true
            };

            // Thêm dữ liệu vào Series
            foreach (var entry in dateAmountList)
            {
                // X: Ngày (aDate), Y: Tổng tiền (totalAmount)
                series.Points.AddXY(entry.aDate.ToString("yyyy-MM-dd"), entry.Total);
            }

            // Thêm Series vào Chart
            chart.Series.Add(series);
        }
    }
}
