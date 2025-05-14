using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Transfer_Object;

namespace DataLayer
{
    public class tblDetailsDL:DataProvider
    {
        public int Add(tblDetails tblDetails)
        {
            string sql =  "INSERT INTO tblDetails (MainID, proID, qty, price, amount) " +
            "Values (" + tblDetails.MainID + ", " + tblDetails.ProID + ", " + tblDetails.Qty + ", " + tblDetails.Price + ", " + tblDetails.Amount + ")";

            try
            {
                return MyExecuteNonQuery(sql, CommandType.Text);
            }
            catch (Exception ex) { throw ex; }
        }
        public int Update(tblDetails tblDetails)
        {
            string sql = "Update tblDetails Set proID = " + tblDetails.ProID + ", qty = " + tblDetails.Qty + ", price = " + tblDetails.Price + ", amount = " + tblDetails.Amount +
             " where DetailID = " + tblDetails.DetailID;
            try
            {
                return MyExecuteNonQuery(sql, CommandType.Text);
            }
            catch (Exception ex) { throw ex; }
        }
        public List<ProductRevenue> GetProductRevenues(string categoryName)
        {
            string pName;
            double total;
            string cName;
            string sql = "SELECT    c.catName,p.pName,    SUM(od.amount) AS TotalRevenue \r\nFROM     tblDetails od JOIN   Products p ON od.proID = p.pID \r\nJOIN   tblMain t ON od.MainID = t.MainID \r\njoin category c on c.catID=p.categoryID\r\nWHERE    t.status='Paid' and od.MainID!=0 and c.catName=@catName\r\nGROUP BY    p.pID, p.pName, p.pPrice,c.catName";
            List<ProductRevenue> productRevenues = new List<ProductRevenue>();
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter catName = new SqlParameter();
            catName.ParameterName = "@catName";
            catName.SqlDbType = SqlDbType.NVarChar;  // Đặt kiểu dữ liệu là Int
            catName.Value = categoryName;  // Truyền giá trị của id vào tham số
            parameters.Add(catName);
            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text,parameters);
                while (reader.Read())
                {
                    cName = reader[0].ToString();
                    pName = reader[1].ToString();
                    total = Convert.ToSingle(reader[2]);
                    ProductRevenue productRevenue = new ProductRevenue(cName,pName, total);
                    productRevenues.Add(productRevenue);
                }
                reader.Close();
                return productRevenues;
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
