﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer_Object;
using System.IO;
using System.Security.Principal;

namespace DataLayer
{
    public class StaffDL : DataProvider
    {
        public List<Staff> GetStaff()
        {
            string id, name, role, phone;
            List<Staff> staffs = new List<Staff>();
            string sql = "Select * from Staff";
            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text);
                while (reader.Read())
                {
                    id = reader[0].ToString();
                    name = reader[1].ToString();
                    phone = reader[2].ToString();
                    role = reader[3].ToString();
                    Staff staff = new Staff(id, name, role, phone);
                    staffs.Add(staff);
                }
                reader.Close();
                return staffs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }
        }
        public int Add(Staff staff)
        {
            string sql = "Insert into Staff (sName,sPhone,sRole) Values('" + staff.sName + "', '" + staff.sPhone + "', '" + staff.sRole + "')";
            try
            {
                return MyExecuteNonQuery(sql, CommandType.Text);
            }
            catch (Exception ex) { throw ex; }
        }
        public string GetRole(string name)
        {
            string sql = "SELECT staff.sRole FROM staff " +
                         "JOIN users u ON u.staff_id = staff.staffID " +
                         "WHERE u.username = @username";
            List<SqlParameter> parameters = new List<SqlParameter>
    {
        new SqlParameter("@username", SqlDbType.NVarChar, 50) { Value = name }
    };

            try
            {
                // Assuming you have a method like MyExecuteScalar that returns a single value
                object result = MyExecuteScalar(sql, CommandType.Text, parameters);

                // Check if the result is not null and convert it to a string
                if (result != null && result != DBNull.Value)
                {
                    return result.ToString();
                }
                else
                {
                    // Handle the case where no role is found (e.g., return null or an empty string)
                    return null; // Or return string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Update(Staff staff)
        {
            // Câu lệnh UPDATE với việc nối trực tiếp giá trị
            string sql = "UPDATE Staff SET sName = '" + staff.sName + "', sPhone = '" + staff.sPhone + "', sRole = '" + staff.sRole + "' WHERE staffID = " + staff.staffId;

            try
            {
                return MyExecuteNonQuery(sql, CommandType.Text);
            }
            catch (Exception ex) { throw ex; }
        }

        public int Delete(Staff staff)
        {
            // Câu lệnh DELETE
            string sql = "DELETE FROM Staff WHERE staffID = " + staff.staffId;

            try
            {
                return MyExecuteNonQuery(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Staff> GetWaiter()
        {
            string id, name, role, phone;
            List<Staff> staffs = new List<Staff>();
            string sql = "Select * from staff where sRole Like 'Waiter'";
            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text);
                while (reader.Read())
                {
                    id = reader[0].ToString();
                    name = reader[1].ToString();
                    phone = reader[2].ToString();
                    role = reader[3].ToString();
                    Staff staff = new Staff(id, name, role, phone);
                    staffs.Add(staff);
                }
                reader.Close();
                return staffs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }
        }
        public List<Staff> GetDriver()
        {
            string id, name, role, phone;
            List<Staff> staffs = new List<Staff>();
            string sql = "Select staffID 'id', sName 'name' from staff Where sRole = 'Driver'";
            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text);
                while (reader.Read())
                {
                    id = reader[0].ToString();
                    name = reader[1].ToString();
                    Staff staff = new Staff(id, name);
                    staffs.Add(staff);
                }
                reader.Close();
                return staffs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }
        }


    }
}
