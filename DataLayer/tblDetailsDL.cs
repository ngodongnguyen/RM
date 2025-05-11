using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
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
        public List<ProductRevenue> GetProductRevenues()
        {
            string pName;
            double total;
            string sql = "SELECT \r\n    p.pName,\r\n    SUM(od.amount) AS TotalRevenue\r\nFROM \r\n    tblDetails od\r\nJOIN \r\n    Products p ON od.proID = p.pID\r\nJOIN \r\n    tblMain t ON od.MainID = t.MainID\r\nWHERE \r\n    t.status='Paid' and od.MainID!=0\r\nGROUP BY \r\n    p.pID, p.pName, p.pPrice";
            List<ProductRevenue> productRevenues = new List<ProductRevenue>();
            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text);
                while (reader.Read())
                {
                    pName = reader[0].ToString();
                    total = Convert.ToSingle(reader[1]);
                    ProductRevenue productRevenue = new ProductRevenue(pName, total);
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
