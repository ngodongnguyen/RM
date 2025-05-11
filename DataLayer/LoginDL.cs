using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer_Object;

namespace DataLayer
{
    public class LoginDL:DataProvider
    {
        public bool Login(Account account) {
            string sql = "SELECT Count(username) FROM users WHERE username = '" + account.UserName + "' AND upass = '" + account.Password + "'";
            try
            {
                return (int)MyExecuteScalar(sql, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
