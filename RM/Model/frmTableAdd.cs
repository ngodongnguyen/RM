using Bussiness_Layer;
using CrystalDecisions.CrystalReports.Engine;
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
    public partial class frmTableAdd : SampleAdd
    {
        private TableBL tableBL;
        public frmTableAdd()
        {
            InitializeComponent();
            tableBL = new TableBL();
        }
        public int id = 0;
        public override void btnSave_Click(object sender, EventArgs e)
        {
            string name=txtName.Text;
            Transfer_Object.Tables table = new Transfer_Object.Tables(name);
            if (id == 0)
            {
                tableBL.Add(table);
                guna2MessageDialog1.Show("Saved successfully");
            }
            else
            {
                table.tId = id.ToString();
                tableBL.Update(table);
                guna2MessageDialog1.Show("Update successfully");
            }
           

        }
    }
}
