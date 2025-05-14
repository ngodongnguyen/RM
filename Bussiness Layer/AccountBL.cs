using DataLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer_Object;

namespace Bussiness_Layer
{
    public class AccountBL
    {
        private AccountDL accountDL;
        public AccountBL()
        {
            accountDL = new AccountDL();
        }
        public List<Account> GetAccounts()
        {
            try
            {
                return accountDL.GetAccounts();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Add(Account account)
        {
            try
            {
                return accountDL.Add(account);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Delete(string account)
        {
            try
            {
                return accountDL.Delete(account);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Update(Account account)
        {
            try
            {
                return accountDL.Update(account);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
