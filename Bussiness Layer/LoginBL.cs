using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer_Object;

namespace Bussiness_Layer
{
    public class LoginBL
    {
        private LoginDL loginDL;
        public LoginBL() { 
        loginDL = new LoginDL();
        }
        public bool Login(Account account)
        {
            try
            {
                return loginDL.Login(account);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
