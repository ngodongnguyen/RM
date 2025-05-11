using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer_Object;
namespace DataLayer
{
    public class CategoryDL:DataProvider
    {
        public List<Category> GetCategories()
        {
            string id, name;
            List<Category> categories = new List<Category>();
            string sql = "Select * from Category";
            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text);
                while(reader.Read()) {
                    id = reader[0].ToString();
                    name = reader[1].ToString();
                    Category category = new Category(id,name);
                    categories.Add(category);
                }
                reader.Close();
                return categories;
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
        public int Add(Category category)
        {
            string sql = "Insert into Category (catName) Values('" + category.CatName + "')";
            try
            {
                return MyExecuteNonQuery(sql, CommandType.Text);
            }
            catch (Exception ex) { throw ex; }
        }
        public int Update(Category category)
        {
            string sql = "UPDATE Category SET catName = '" + category.CatName + "' WHERE CatID = " + category.Id;
            try
            {
                return MyExecuteNonQuery(sql, CommandType.Text);
            }
            catch (Exception ex) { throw ex; }
        }
        public int Delete(Category category)
        {
            string sql = "DELETE FROM Category WHERE CatID ="+category.Id;
            try
            {
                return MyExecuteNonQuery(sql, CommandType.Text);
            }
            catch (Exception ex) { throw ex; }
        }
        public string Find(Category category)
        {
            List<Category> categories = GetCategories();  // Lấy danh sách các danh mục từ cơ sở dữ liệu

            // Duyệt qua danh sách các danh mục và tìm kiếm theo tên
            foreach (var cat in categories)
            {
                if (cat.CatName.Equals(category.CatName, StringComparison.OrdinalIgnoreCase))  // So sánh tên danh mục
                {
                    return cat.Id;  // Trả về tên danh mục nếu tìm thấy
                }
            }
            return null;  // Trả về null nếu không tìm thấy danh mục
        }

    }
}
