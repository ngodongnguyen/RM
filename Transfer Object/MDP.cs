using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer_Object
{
    public class MDP
    {
        public DateTime aDate { get; set; }
        public string tTime { get; set; }
        public string orderType { get; set; }
        public string custName { get; set; }
        public string TableName { get; set; }
        public string WaiterName { get; set; }
        public string pName { get; set; }
        public int qty { get; set; }
        public double price { get; set; }
        public double amount { get; set; }
        public double received { get; set; }
        public double change { get; set; }
        public MDP(DateTime aDate, string tTime, string orderType, string custName, string tableName, string waiterName, string pName, int qty, double price, double amount, double receive, double change)
        {
            this.aDate = aDate;
            this.tTime = tTime;
            this.orderType = orderType;
            this.custName = custName;
            this.TableName = tableName;
            this.WaiterName = waiterName;
            this.pName = pName;
            this.qty = qty;
            this.price = price;
            this.amount = amount;
            this.received = received;
            this.change = change;
        }
    }
}
