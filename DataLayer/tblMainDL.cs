using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Transfer_Object;
using Microsoft.SqlServer.Server;
using System.Reflection;
using System.Security.Cryptography;
using System.Web;
using System.Diagnostics;

namespace DataLayer
{
    public class tblMainDL:DataProvider
    {
        public List<tblMain> GetBillPending()
        {
            string TableName, WaiterName, orderType, status;
            int MainID;
            float total;
            List<tblMain> tblMains = new List<tblMain>();

            string sql = "select MainID, TableName, WaiterName, orderType, status, total from tblMain where status <> 'Pending' ";
            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text);
                while (reader.Read())
                {
                    MainID =Convert.ToInt32( reader[0]);
                    TableName = reader[1].ToString();
                    WaiterName = reader[2].ToString();
                    orderType = reader[3].ToString();
                    status = reader[4].ToString();
                    total = Convert.ToSingle(reader[5]);
                    tblMain tblMain = new tblMain(TableName,WaiterName,status,MainID,orderType,total);
                    tblMains.Add(tblMain);
                }
                reader.Close();
                return tblMains;
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
        public List<tblMain> GetTables_Pending()
        {
            int mainId,driverId;
            string aTime, tTime, TableName,WaiterName,status,orderType,CustName,CustPhone;
            DateTime aDate;
            double total, received, change;
            List<tblMain> tblMains = new List<tblMain>();
            string sql = "Select * from tblMain where status = 'Pending' ";
            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text);
                while (reader.Read())
                {
                    mainId = Convert.ToInt32(reader[0]);
                    aDate = Convert.ToDateTime(reader[1]);
                    tTime = reader[2].ToString();
                    TableName = reader[3].ToString();
                    WaiterName = reader[4].ToString();
                    status = reader[5].ToString();
                    orderType = reader[6].ToString();
                    total = Convert.ToSingle(reader[7]);
                    received = Convert.ToSingle(reader[8]);
                    change = Convert.ToSingle(reader[9]);
                    aTime = reader[10].ToString();
                    driverId = Convert.ToInt32(reader[11]);
                    CustName= reader[12].ToString();
                    CustPhone = reader[13].ToString();
                    tblMain tbl = new tblMain(aDate, tTime, TableName, WaiterName, status, mainId,orderType, total, received, change, aTime, driverId, CustName, CustPhone);
                    tblMains.Add(tbl);

                }
                reader.Close();
                return tblMains;
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
        public List<MDPC> GetMDPCs(DateTime startDate, DateTime endDate)
        {
            DateTime aDate;
            string pName,catName;
            int qty,MainID,proID,categoryID,catID;
            double amount, price;
            List<SqlParameter> parameters = new List<SqlParameter>
    {
        new SqlParameter("@sdate", SqlDbType.DateTime) { Value = startDate },
        new SqlParameter("@edate", SqlDbType.DateTime) { Value = endDate } };
            List<MDPC> mDPCs = new List<MDPC>();
            string sql = @"select * from  tblMain m inner join tblDetails d on m.MainID = d.MainID inner join products p on p.pID = d.proID inner join category c on c.catID =p.categoryID where m.aDate between @sdate and @edate";
            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text,parameters);
                while (reader.Read())
                {
                    MainID = Convert.ToInt32(reader[0]);
                    proID= Convert.ToInt32(reader[16]);
                    categoryID= Convert.ToInt32(reader[23]);
                    catID= Convert.ToInt32(reader[25]);
                    aDate = Convert.ToDateTime(reader[1]);
                    pName = reader[21].ToString();
                    qty = Convert.ToInt32( reader[17]);
                    price = Convert.ToSingle( reader[18]);
                    amount = Convert.ToSingle(reader[19]);
                    catName = reader[26].ToString();

                    MDPC mdpc = new MDPC(aDate, pName,  qty, price, amount,catName,MainID,proID,categoryID,catID);
                    mDPCs.Add(mdpc);
                }
                reader.Close();
                return mDPCs;
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
        public List<tblMain> GetTotal()
        {
            DateTime dateTime;
            double total;
            string sql = "SELECT aDate, SUM(total) AS totalAmount FROM tblMain WHERE status = 'Paid' GROUP BY aDate ORDER BY aDate";
            List<tblMain> tblMains = new List<tblMain>();
            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text);
                while (reader.Read())
                {
                    dateTime = Convert.ToDateTime(reader[0]);
                    total = Convert.ToSingle(reader[1]);
                    tblMain tblMain = new tblMain(dateTime, total);
                    tblMains.Add(tblMain);
                }
                reader.Close();
                return tblMains;
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
        public List<tblMain> GetTables()
        {
            string tableName, status;
            string sql = "SELECT t.tName, m.status\r\nFROM Tables t\r\nLEFT JOIN tblMain m ON t.tName = m.TableName\r\nWHERE m.status = 'Paid' or m.status = 'complete' OR t.tName NOT IN (SELECT TableName FROM tblMain);";
            List<tblMain> tblMains= new List<tblMain>();
            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text);
                while(reader.Read())
                {
                    tableName = reader[0].ToString();
                    status = reader[1].ToString();
                    tblMain tblMain = new tblMain(tableName, status);
                    tblMains.Add(tblMain);
                }    
                reader.Close();
                return tblMains;
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
        public List<tblMain> GetAmountOfOrder()
        {
            DateTime dateTime;
            int MainID;
            string sql = "SELECT \r\n    aDate,\r\n    COUNT(MainID) AS OrderCount\r\nFROM \r\n    tblMain\r\nWHERE \r\n    status = 'Paid'\r\nGROUP BY \r\n    aDate\r\n";
            List<tblMain> tblMains= new List<tblMain>();
            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text);
                while(reader.Read())
                {
                    dateTime = Convert.ToDateTime(reader[0]);
                    MainID = Convert.ToInt32(reader[1]);
                    tblMain tblMain = new tblMain(dateTime, MainID);
                    tblMains.Add(tblMain);
                }    
                reader.Close();
                return tblMains;
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

        public int Add(tblMain main)
        {
            string sql = "INSERT INTO tblMain (aDate, aTime, TableName, WaiterName, status, orderType, total, received, change, driverID, CustName, CustPhone) " +
                        "VALUES ('" + main.aDate + "', '" + main.aTime + "', '" + main.TableName + "', '" + main.WaiterName + "', '" + main.Status + "', '" + main.OrderType + "', '" + main.Total + "', '" + main.Received + "', '" + main.Change + "', '" + main.DriverID + "', '" + main.CustName + "', '" + main.CustPhone + "');" +
                        "SELECT SCOPE_IDENTITY()";

            try
            {
                object result = MyExecuteScalar(sql,CommandType.Text);  // Lấy giá trị ID mới từ SCOPE_IDENTITY()
                return Convert.ToInt32(result);  // Trả về ID


            }
            catch (Exception ex) { throw ex; }
        }
        public int Update(tblMain main)
        {
            string sql = "Update tblMain Set status = '" + main.Status + "', total = '" + main.Total + "', received = '" + main.Received + "', change = '" + main.Change + "' where MainID = '" + main.MainID + "'";
            try
            {
                return MyExecuteNonQuery(sql, CommandType.Text);
            }
            catch (Exception ex) { throw ex; }
        }
        public int Update_Bill(tblMain main)
        {
            string sql =  "Update tblMain set total = " + main.Total + ", received = " + main.Received + ", change = " + main.Change + ", status = 'Paid' " +
             "Where MainID = " + main.MainID;
            ;
            try
            {
                return MyExecuteNonQuery(sql, CommandType.Text);
            }
            catch (Exception ex) { throw ex; }
        }
        public int Update_Transfer(tblMain main)
        {
            string sql = "UPDATE tblMain SET status = 'Paid' WHERE MainID = " + main.MainID;

            ;
            try
            {
                return MyExecuteNonQuery(sql, CommandType.Text);
            }
            catch (Exception ex) { throw ex; }
        }
        public int Update_Kitchen(int id)
        {
            string sql = "Update tblMain set status = 'complete' Where MainID = '"+id+"'";
            ;
            try
            {
                return MyExecuteNonQuery(sql, CommandType.Text);
            }
            catch (Exception ex) { throw ex; }
        }
        public List<tblMainDetail> LoadEntries(int id)
        {
            string proName,orderType,TableName,WaiterName;
            int detailID, proID, qty;
            double price, amount;
            List<tblMainDetail> tblMains = new List<tblMainDetail>();

            string sql = @"Select * from tblMain m inner join tblDetails d on m.MainID = d.MainID inner join products p on p.pID = d.proID Where m.MainID = @MainID";
            List<SqlParameter>parameters = new List<SqlParameter>();
            SqlParameter mainIDParam = new SqlParameter();
            mainIDParam.ParameterName = "@MainID";
            mainIDParam.SqlDbType = SqlDbType.Int;  // Đặt kiểu dữ liệu là Int
            mainIDParam.Value = id;  // Truyền giá trị của id vào tham số
            parameters.Add(mainIDParam);
            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text,parameters);
                while (reader.Read())
                {
                    TableName = reader[3].ToString();
                    WaiterName = reader[4].ToString();
                    orderType = reader[6].ToString();
                    detailID = Convert.ToInt32(reader[14]);
                    proID = Convert.ToInt32(reader[16]);
                    proName = reader[21].ToString();
                    qty = Convert.ToInt32(reader[17]);
                    price = Convert.ToSingle(reader[18]);
                    amount = Convert.ToSingle(reader[19]);
                    tblMainDetail tblMainDetail = new tblMainDetail(TableName,WaiterName,orderType,detailID, proID, proName, qty, price, amount);
                    tblMains.Add(tblMainDetail);
                }
                reader.Close();
                return tblMains;
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
