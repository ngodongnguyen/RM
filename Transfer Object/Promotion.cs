using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer_Object
{
    public class Promotion
    {
        public int promotionId { get; set; }
        public string promotionName { get; set;}
        public float discount_value { get; set; }
        public string status { get; set; }
        public Promotion(int promotionId, string promotionName, float discount_value, string status)
        {
            this.promotionId = promotionId;
            this.promotionName = promotionName;
            this.discount_value = discount_value;
            this.status = status;
        }
        public Promotion( string promotionName, float discount_value, string status)
        {
            this.promotionName = promotionName;
            this.discount_value = discount_value;
            this.status = status;
        }
        public Promotion()
        {

        }

    }
}
