﻿using Bussiness_Layer;
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
    public partial class frmAmountOfOrder : Form
    {
        private tblMainBL tblMainBL;
        public frmAmountOfOrder()
        {
            tblMainBL = new tblMainBL();
            InitializeComponent();
            LoadLineChart();
        }
        private void LoadLineChart()
        {
            List<tblMain> tblMains = tblMainBL.GetAmountOfOrder();
            Chart chart = new Chart();
            chart.Dock = DockStyle.Fill;  // Đặt biểu đồ chiếm toàn bộ Form
            this.Controls.Add(chart);

            // Cấu hình ChartArea
            ChartArea chartArea = new ChartArea();
            chart.ChartAreas.Add(chartArea);

            // Tạo Series để vẽ biểu đồ đường
            Series series = new Series
            {
                Name = "Revenue",
                ChartType = SeriesChartType.Line,  // Biểu đồ đường
                BorderWidth = 3,  // Độ dày đường vẽ
                IsValueShownAsLabel = true,  // Hiển thị giá trị trên đường
                LabelFormat = "#0.##",  // Hiển thị giá trị của điểm dữ liệu
            };

            foreach (var main in tblMains)
            {
                // Sử dụng trực tiếp đối tượng DateTime cho trục X
                DateTime monthYear = main.aDate;
                double revenue = main.MainID;  // Giả sử MainID là tổng doanh thu

                // Thêm giá trị vào series
                series.Points.AddXY(monthYear, revenue);
            }

            chart.Series.Add(series);

            // Thêm tiêu đề cho biểu đồ
            chart.Titles.Add("Revenue per Month");

            // Cấu hình Legend (Chú thích)
            chart.Legends.Add(new Legend() { Docking = Docking.Top });

            // Cấu hình trục X (Tháng)
            chartArea.AxisX.LabelStyle.Format = "MMM-yyyy"; // Hiển thị tháng và năm
            chartArea.AxisX.IntervalType = DateTimeIntervalType.Months; // Tạo khoảng cách mỗi tháng
        }
    }
    }
