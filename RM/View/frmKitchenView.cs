using Bussiness_Layer;
using Guna.UI2.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Transfer_Object;

namespace RM.View
{
    public partial class frmKitchenView : Form
    {
        private tblMainBL tblMainBL;

        public frmKitchenView()
        {
            InitializeComponent();
            tblMainBL = new tblMainBL();
        }

        private void frmKitchenView_Load(object sender, EventArgs e)
        {
            GetOrders();
        }

        private void GetOrders()
        {
            flowLayoutPanel1.Controls.Clear();
            List<tblMain> tbl = tblMainBL.GetTables_Pending();

            foreach (var order in tbl)
            {
                FlowLayoutPanel p1 = CreateFlowLayoutPanel();

                // Create and style the header panel (p2)
                FlowLayoutPanel p2 = CreateHeaderPanel(order);

                p1.Controls.Add(p2);

                // Add products to the order details
                List<tblMainDetail> tblMainDetails = tblMainBL.LoadEntries(order.MainID);

                foreach (var detail in tblMainDetails)
                {
                    Label lb5 = new Label
                    {
                        ForeColor = Color.Black,
                        Margin = new Padding(10, 5, 3, 0),
                        AutoSize = true,
                        Text = $"{tblMainDetails.IndexOf(detail) + 1} {detail.ProName} {detail.Qty}"
                    };
                    p1.Controls.Add(lb5);
                }

                // Add button to change order status
                Guna2Button completeButton = CreateCompleteButton(order.MainID);
                p1.Controls.Add(completeButton);

                flowLayoutPanel1.Controls.Add(p1);
            }
        }

        private FlowLayoutPanel CreateFlowLayoutPanel()
        {
            return new FlowLayoutPanel
            {
                AutoSize = true,
                Width = 230,
                Height = 350,
                FlowDirection = FlowDirection.TopDown,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10)
            };
        }

        private FlowLayoutPanel CreateHeaderPanel(tblMain order)
        {
            FlowLayoutPanel p2 = new FlowLayoutPanel
            {
                BackColor = Color.FromArgb(50, 55, 89),
                AutoSize = true,
                Width = 230,
                Height = 125,
                FlowDirection = FlowDirection.TopDown,
                Margin = new Padding(0)
            };

            p2.Controls.Add(CreateLabel("Table : ", order.TableName));
            p2.Controls.Add(CreateLabel("Waiter Name : ", order.WaiterName));
            p2.Controls.Add(CreateLabel("Order Time : ", order.tTime.ToString()));
            p2.Controls.Add(CreateLabel("Order Type : ", order.OrderType));

            return p2;
        }

        private Label CreateLabel(string labelText, string value)
        {
            return new Label
            {
                ForeColor = Color.White,
                Margin = new Padding(10, 5, 3, 0),
                AutoSize = true,
                Text = labelText + value
            };
        }

        private Guna2Button CreateCompleteButton(int mainId)
        {
            Guna2Button button = new Guna2Button
            {
                AutoRoundedCorners = true,
                Size = new Size(100, 35),
                FillColor = Color.FromArgb(241, 85, 126),
                Margin = new Padding(30, 5, 3, 10),
                Text = "Complete",
                Tag = mainId.ToString() // Store the MainID for later reference
            };
            button.Click += new EventHandler(b_click);

            return button;
        }

        private void b_click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32((sender as Guna2Button).Tag.ToString());

            guna2MessageDialog1.Icon = MessageDialogIcon.Question;
            guna2MessageDialog1.Buttons = MessageDialogButtons.YesNo;
            if (guna2MessageDialog1.Show("Are you sure you want to mark this order as complete?") == DialogResult.Yes)
            {
                int row = tblMainBL.Update_Kitchen(id);

                if (row > 0)
                {
                    guna2MessageDialog1.Buttons = MessageDialogButtons.OK;
                    guna2MessageDialog1.Show("Order status updated successfully.");
                }
                GetOrders();  // Refresh the orders list
            }
        }
    }
}
