using Bussiness_Layer;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Transfer_Object;

namespace RM
{
    public partial class frmLogin : Form
    {
        private LoginBL loginBL;
        public frmLogin()
        {
            InitializeComponent();
            loginBL = new LoginBL();
        }
        bool UserLogin(Account account)
        {
            try
            {
                return loginBL.Login(account);
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user, pass;
            user=txtUser.Text;
            pass=txtPass.Text;
            Account account=new Account(user, pass);
            //create database and user
            if (UserLogin(account)==true)
            {
                this.Hide();
                frmMain frm = new frmMain();
                frm.Show();
                
            }
            else
            {
                guna2MessageDialog1.Show("Tài khoản hoặc mật khẩu không đúng!");
                return;
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
