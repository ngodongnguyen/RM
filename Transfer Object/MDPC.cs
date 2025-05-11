using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer_Object
{
    public class MDPC
    {
        public DateTime aDate { get; set; }

        public string pName { get; set; }
        public int qty { get; set; }
        public double price { get; set; }
        public double amount { get; set; }

        public string catName { get; set; }

        public int MainID { get; set; }
        public int proID { get; set; }
        public int categoryID { get; set; }

        public int catID { get; set; }
        public MDPC(DateTime aDate, string pName, int qty, double price, double amount, string catName, int mainID, int proID, int categoryID, int catID) 
        {
            MainID = mainID;
            this.proID = proID;
            this.categoryID = categoryID;
            this.catID = catID;
            this.aDate = aDate;
            this.pName = pName;
            this.qty = qty;
            this.price = price;
            this.amount = amount;
            this.catName = catName;
        }

        //public MDPC(DateTime aDate,string pName,int qty,double price, double amount,string catName) { 

        //}
    }
}
