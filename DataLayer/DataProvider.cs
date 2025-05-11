using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DataProvider
    {
        public SqlConnection cn;
        public DataProvider()
        {
            string cnStr = "Data Source=NGUYEN;Initial Catalog = RM; Integrated Security = True";
            cn = new SqlConnection(cnStr);
        }

        public void Connect()
        {
            try
            {
                if (cn != null && cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public void Disconnect()
        {
            try
            {
                if (cn != null && cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public object MyExecuteScalar(string sql, CommandType type)
        {
            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandType = type;
                return (cmd.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }
        }
        public SqlDataReader MyExecuteReader(string sql, CommandType type, List<SqlParameter> parameters = null)
        {
            try
            {
                // Tạo lệnh SQL
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandType = type;

                // Kiểm tra và thêm tham số nếu có
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                }

                // Thực thi câu lệnh và trả về SqlDataReader
                return cmd.ExecuteReader();
            }
            catch (SqlException ex)
            {
                throw ex;  // Ném lại lỗi
            }
        }

        public int MyExecuteNonQuery(string sql, CommandType type, List<SqlParameter> parameters=null)
        {
            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandType = type;
                if(parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                }

                return cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally { Disconnect(); }
        }

    }
}

