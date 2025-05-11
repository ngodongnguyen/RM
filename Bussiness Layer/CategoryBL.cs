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
    public class CategoryBL
    {
        private CategoryDL categoryDL;
        public CategoryBL()
        {
            categoryDL = new CategoryDL();
        }
        public List<Category>GetCategories()
        {
            try
            {
                return categoryDL.GetCategories();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Add(Category category)
        {
            try
            {
                return categoryDL.Add(category);
            }
            catch(SqlException ex)
            {
                throw ex;
            }
        }
        public int Delete(Category category)
        {
            try
            {
                return categoryDL.Delete(category);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Update(Category category)
        {
            try
            {
                return categoryDL.Update(category);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public string Find(Category category)
        {
            try
            {
                return categoryDL.Find(category);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
