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
    public class PromotionDL :DataProvider
    {
        public List<Promotion> GetPromotions()
        {
            string promotion_name, status;
            float promotion_discount;
            int promotion_id;
            List<Promotion> promotions=new List<Promotion>();
            string sql = "Select * from Promotion";
            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text);
                while (reader.Read())
                {
                    promotion_id = Convert.ToInt32(reader[0]);
                    promotion_name = reader[1].ToString();
                    promotion_discount = Convert.ToSingle(reader[2]);
                    status = reader[3].ToString();
                    Promotion promotion = new Promotion(promotion_id, promotion_name, promotion_discount, status);
                    promotions.Add(promotion);
                }
                reader.Close();
                return promotions;
            }
            catch(SqlException ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }
        }
        public int Add(Promotion promotion)
        {
            string sql= "Insert into Promotion Values('"+promotion.promotionName+"',"+promotion.discount_value+",'"+promotion.status+"')";
            try
            {
                return MyExecuteNonQuery(sql, CommandType.Text);
            }
            catch(SqlException ex)
            {
                throw ex;
            }
        }
        public int Update(Promotion promotion)
        {
            string sql = "UPDATE Promotion SET " +
                         "promotion_name = '" + promotion.promotionName + "', " +
                         "discount_value = " + promotion.discount_value + ", " +
                         "status = '" + promotion.status + "' " +
                         "WHERE promotion_id = " + promotion.promotionId;
            try
            {
                return MyExecuteNonQuery(sql, CommandType.Text);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Delete(Promotion promotion)
        {
            string sql = "DELETE FROM Promotion WHERE promotion_id = " + promotion.promotionId;
            try
            {
                return MyExecuteNonQuery(sql, CommandType.Text);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
