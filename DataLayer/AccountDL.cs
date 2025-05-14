using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer_Object;

namespace DataLayer
{
    public class AccountDL:DataProvider
    {
        public List<Account> GetAccounts()
        {
            string userName, password;
            int staffId,userID;
            
            List<Account> accounts = new List<Account>();
            string sql = "SELECT * FROM users"; // Thay "Accounts" bằng tên bảng thực tế của bạn

            try
            {
                Connect(); // Giả sử phương thức Connect() đã được định nghĩa để mở kết nối đến cơ sở dữ liệu
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text); // Giả sử MyExecuteReader() thực hiện truy vấn và trả về SqlDataReader

                while (reader.Read())
                {
                    //userID= Convert.ToInt32(reader["userID"]);
                    userName = reader["username"].ToString(); // Lấy giá trị từ cột "UserName"
                    password = reader["upass"].ToString(); // Lấy giá trị từ cột "Password"
                    staffId = Convert.ToInt32(reader["staff_id"]); // Lấy giá trị từ cột "staff_id" và chuyển đổi sang kiểu int

                    //Account account = new Account(userID,userName, password, staffId); // Giả sử lớp Account có constructor nhận các tham số này
                    Account account = new Account(userName, password, staffId); // Giả sử lớp Account có constructor nhận các tham số này

                    accounts.Add(account);
                }

                reader.Close();
                return accounts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect(); // Giả sử phương thức Disconnect() đã được định nghĩa để đóng kết nối
            }
        }
        public int Add(Account account)
        {
            string sql = "INSERT INTO users (UserName, upass, staff_id) VALUES (@userName, @password, @staffId)";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
        new SqlParameter("@userName", SqlDbType.NVarChar, 50) { Value = account.UserName },
        new SqlParameter("@password", SqlDbType.NVarChar, 50) { Value = account.Password },
        new SqlParameter("@staffId", SqlDbType.Int) { Value = account.staff_id }
            };

            try
            {
                return MyExecuteNonQuery(sql, CommandType.Text, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Update(Account account)
        {
            string sql = "UPDATE users SET upass = @password, staff_id = @staffId WHERE UserName = @userName";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
        new SqlParameter("@password", SqlDbType.NVarChar, 50) { Value = account.Password },
        new SqlParameter("@staffId", SqlDbType.Int) { Value = account.staff_id },
        new SqlParameter("@userName", SqlDbType.NVarChar, 50) { Value = account.UserName } // Điều kiện WHERE
            };

            try
            {
                return MyExecuteNonQuery(sql, CommandType.Text, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Delete(string userName)
        {
            string sql = "DELETE FROM users WHERE UserName = @userName";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
        new SqlParameter("@userName", SqlDbType.NVarChar, 50) { Value = userName }
            };

            try
            {
                return MyExecuteNonQuery(sql, CommandType.Text, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
