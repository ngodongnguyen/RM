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
    public class TableBL
    {
        private TableDL tableDL;
        public TableBL()
        {
            tableDL = new TableDL();
        }
        public List<Tables> GetTables()
        {
            try
            {
                return tableDL.GetTables();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Add(Tables table)
        {
            try
            {
                return tableDL.Add(table);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Update(Tables table)
        {
            try
            {
                return tableDL.Update(table);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Delete(Tables table)
        {
            try
            {
                return tableDL.Delete(table);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
