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
    public class StaffBL
    {
        private StaffDL staffDL;
        public StaffBL()
        {
            staffDL = new StaffDL();
        }
        public List<Staff> GetStaff()
        {
            try
            {
                return staffDL.GetStaff();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Add(Staff staff)
        {
            try
            {
                return staffDL.Add(staff);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Update(Staff staff)
        {
            try
            {
                return staffDL.Update(staff);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Delete(Staff staff)
        {
            try
            {
                return staffDL.Delete(staff);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public List<Staff> GetWaiter()
        {
            try
            {
                return staffDL.GetWaiter();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public List<Staff> GetDriver()
        {
            try
            {
                return staffDL.GetDriver();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
