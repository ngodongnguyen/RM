using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer_Object
{
    public class ProductRevenue
    {
        public string catName { get; set; }
        public string pName {  get; set; }
        public double TotalRevenue { get; set; }
        public ProductRevenue(string pName, double totalRevenue)
        {
            this.pName = pName;
            TotalRevenue = totalRevenue;
        }
        public ProductRevenue(string catName,string pName, double totalRevenue)
        {
            this.catName = catName;
            this.pName = pName;
            TotalRevenue = totalRevenue;
        }
    }
}
