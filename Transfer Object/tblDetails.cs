using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer_Object
{
    public class tblDetails
    {
        public int DetailID { get; set; }
        public int MainID { get; set; }
        public int ProID { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }

        public tblDetails() { }

        public tblDetails(int mainID, int proID, int qty, double price, double amount)
        {
            this.MainID = mainID;
            this.ProID = proID;
            this.Qty = qty;
            this.Price = price;
            this.Amount = amount;
        }
    }
}
