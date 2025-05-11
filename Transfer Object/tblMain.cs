using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer_Object
{
    public class tblMain
    {
        public DateTime aDate { get; set; }
        public string tTime { get; set; }
        public string TableName { get; set; }
        public string WaiterName { get; set; }
        public string Status { get; set; }
        public int MainID { get; set; }
        public string OrderType { get; set; }
        public double Total { get; set; }
        public double Received { get; set; }
        public double Change { get; set; }
        public string aTime { get; set; }
        public int DriverID { get; set; }
        public string CustName { get; set; }
        public string CustPhone { get; set; }
        public tblMain(DateTime aDate, string tTime, string tableName, string waiterName, string status, int mainID, string orderType, double total, double received, double change, string aTime, int driverID, string custName, string custPhone)
        {
            this.aDate = aDate;
            this.tTime = tTime;
            TableName = tableName;
            WaiterName = waiterName;
            Status = status;
            MainID = mainID;
            OrderType = orderType;
            Total = total;
            Received = received;
            Change = change;
            this.aTime = aTime;
            DriverID = driverID;
            CustName = custName;
            CustPhone = custPhone;
        }

        public tblMain(string tableName, string waiterName, string status, int mainID, string orderType, double total)
        {

            TableName = tableName;
            WaiterName = waiterName;
            Status = status;
            MainID = mainID;
            OrderType = orderType;
            Total = total;

        }
        public tblMain()
        {

        }
        public tblMain(DateTime date,double total)
        {
            this.aDate= date;
            this.Total = total;
        }
        public tblMain(DateTime date, int id)
        {
            this.aDate = date;
            this.MainID = id;
        }
        public tblMain(string tableName, string status)
        {
            this.TableName = tableName;
            this.Status = status;
        }
    }
}
