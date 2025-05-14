using DataLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Transfer_Object;

namespace Bussiness_Layer
{
    public class tblMainBL
    {
        private tblMainDL tblMainDL;
        public tblMainBL()
        {
            tblMainDL = new tblMainDL();
        }
        public List<tblMain> GetBillPending()
        {
            try
            {
                return tblMainDL.GetBillPending();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public List<tblMain> GetBillComplete()
        {
            try
            {
                return tblMainDL.GetBillComplete();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public List<tblMain> GetTotal( DateTime startDate, DateTime endDate)
        {
            try
            {
                return tblMainDL.GetTotal(startDate, endDate);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public List<tblMain> GetTables()
        {
            try
            {
                return tblMainDL.GetTables();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public List<MDP> GetBillList(string id)
        {
            try
            {
                return tblMainDL.GetBillList(id);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public List<tblMain> GetTables_Pending()
        {
            try
            {
                return tblMainDL.GetTables_Pending();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public List<tblMain> GetAmountOfOrder()
        {
            try
            {
                return tblMainDL.GetAmountOfOrder();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public List<tblMainDetail> LoadEntries(int id)
        {
            try
            {
                return tblMainDL.LoadEntries(id);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Add(tblMain tbl)
        {
            try
            {
                return tblMainDL.Add(tbl);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Update(tblMain tbl)
        {
            try
            {
                return tblMainDL.Update(tbl);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Update_Bill(tblMain tbl)
        {
            try
            {
                return tblMainDL.Update_Bill(tbl);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Update_Transfer(tblMain tbl)
        {
            try
            {
                return tblMainDL.Update_Transfer(tbl);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Update_Kitchen(int id)
        {
            try
            {
                return tblMainDL.Update_Kitchen(id);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public List<MDPC> GetMDPCs(DateTime startDate, DateTime endDate)
        {
            try
            {
                return tblMainDL.GetMDPCs(startDate, endDate);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
