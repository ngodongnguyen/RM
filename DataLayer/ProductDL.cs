using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Transfer_Object;
using System.IO;  // Thêm dòng này vào đầu file
using static System.Net.Mime.MediaTypeNames;

namespace DataLayer
{
    public class ProductDL:DataProvider
    {
        public List<Product> GetProducts()
        {
            string id, name, imagePath;
            string price;
            string categoryId;
            List<Product> products = new List<Product>();
            string sql = "Select * from Products";
            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text);
                while (reader.Read())
                {
                    id = reader[0].ToString();
                    name = reader[1].ToString();
                    price = reader[2].ToString();
                    categoryId = reader[3].ToString();
                    byte[] pImage = reader[4] as byte[];  // Đảm bảo rằng cột chứa dữ liệu nhị phân
                    Product product = new Product(id,name,price,categoryId,pImage);
                    products.Add(product);
                }
                reader.Close();
                return products;
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
        public int Add(Product product)
        {
            string sql = "INSERT INTO Products (pName, pPrice, categoryID, pImage) VALUES (@pName, @pPrice, @categoryID, @pImage)";

            // Tạo danh sách các tham số SQL
            List<SqlParameter> parameters = new List<SqlParameter>
    {
        new SqlParameter("@pName", SqlDbType.NVarChar) { Value = product.pName },
        new SqlParameter("@pPrice", SqlDbType.Decimal) { Value = product.pPrice },
        new SqlParameter("@categoryID", SqlDbType.Int) { Value = product.categoryId },
        new SqlParameter("@pImage", SqlDbType.VarBinary) { Value = product.pImage }
    };

            try
            {
                // Gọi MyExecuteNonQuery để thực hiện câu lệnh SQL
                return MyExecuteNonQuery(sql, CommandType.Text, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(Product product)
        {
            string sql = "UPDATE Products SET pName = '" + product.pName + "', pPrice = '" + product.pPrice + "', categoryID = '" + product.categoryId + "' WHERE pId = " + product.pId;
            try
            {
                return MyExecuteNonQuery(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Delete(Product product)
        {
            string sql = "DELETE FROM Products WHERE pId =" + product.pId;
            try
            {
                return MyExecuteNonQuery(sql, CommandType.Text);
            }
            catch (Exception ex) { throw ex; }
        }

        public List<Product> GetProductsJoinCategory()
        {
            string id, name, imagePath;
            string price;
            string categoryId,catID,catName;
            List<Product> products = new List<Product>();
            string sql = "Select * from products inner join category on catID = categoryID";
            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text);
                while (reader.Read())
                {
                    id = reader[0].ToString();
                    name = reader[1].ToString();
                    price = reader[2].ToString();
                    categoryId = reader[3].ToString();
                    byte[] pImage = reader[4] as byte[];  // Đảm bảo rằng cột chứa dữ liệu nhị phân
                    catID = reader[5].ToString();
                    catName = reader[6].ToString();
                    Product product = new Product(id, name, price, categoryId, pImage);
                    products.Add(product);
                }
                reader.Close();
                return products;
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
