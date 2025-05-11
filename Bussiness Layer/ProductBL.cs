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
    public class ProductBL
    {
        private ProductDL productDL;
        public ProductBL()
        {
            productDL = new ProductDL();
        }
        public List<Product> GetProducts()
        {
            try
            {
                return productDL.GetProducts();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Add(Product product)
        {
            try
            {
                return productDL.Add(product);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Update(Product product)
        {
            try
            {
                return productDL.Update(product);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Delete(Product product)
        {
            try
            {
                return productDL.Delete(product);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public List<Product> GetProductsJoinCategory()
        {
            try
            {
                return productDL.GetProductsJoinCategory();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
