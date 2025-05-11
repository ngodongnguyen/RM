using Bussiness_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Transfer_Object;

namespace RM.Model
{
    public partial class frmEmailInput : SampleAdd
    {
        public string EmailAddress { get; set; }

        public frmEmailInput()
        {
            InitializeComponent();
        }
        public override void btnSave_Click(object sender, EventArgs e)
        {
            EmailAddress = txtEmail.Text.Trim();

            if (IsValidEmail(EmailAddress))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid email address. Please enter a valid email.");
            }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
