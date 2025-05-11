using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer_Object
{
    public class tblMainDetail
    {
        // Thuộc tính từ tblMain
        public int MainID { get; set; }
        public string TableName { get; set; }
        public string WaiterName { get; set; }
        public string Status { get; set; }
        public string OrderType { get; set; }
        public double Total { get; set; }

        // Thuộc tính từ tblDetails
        public int DetailID { get; set; }
        public int ProID { get; set; }
        public string ProName { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }

        public tblMainDetail(string TableName,string WaiterName,string orderType,int DetailID, int ProID, string ProName, int Qty, double Price, double Amount)
        {
            this.TableName = TableName;
            this.WaiterName= WaiterName;
            this.OrderType = orderType;
            this.DetailID = DetailID;
            this.ProID = ProID;
            this.ProName = ProName;
            this.Qty = Qty;
            this.Price = Price;
            this.Amount = Amount;
        }
    }
}
