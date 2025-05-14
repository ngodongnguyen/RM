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
    public class tblDetailsBL
    {
        private tblDetailsDL tblDetailsDL;
        public tblDetailsBL()
        {
            tblDetailsDL = new tblDetailsDL();
        }
        public int Add(tblDetails tbl)
        {
            try
            {
                return tblDetailsDL.Add(tbl);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Update(tblDetails tbl)
        {
            try
            {
                return tblDetailsDL.Update(tbl);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public List<ProductRevenue> GetProductRevenues(string categoryName)
        {
            try
            {
                return tblDetailsDL.GetProductRevenues(categoryName);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
