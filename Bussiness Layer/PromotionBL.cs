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
    public class PromotionBL
    {
        private PromotionDL promotionDL;
        public PromotionBL() {
        promotionDL = new PromotionDL();
        }
        public List<Promotion> GetPromotions()
        {
            try
            {
                return promotionDL.GetPromotions();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Add(Promotion promotion)
        {
            try
            {
                return promotionDL.Add(promotion);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Update(Promotion promotion)
        {
            try
            {
                return promotionDL.Update(promotion);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Delete(Promotion promotion)
        {
            try
            {
                return promotionDL.Delete(promotion);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

    }
}
