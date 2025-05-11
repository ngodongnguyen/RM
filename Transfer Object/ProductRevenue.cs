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
        public string pName {  get; set; }
        public double TotalRevenue { get; set; }
        public ProductRevenue(string pName, double totalRevenue)
        {
            this.pName = pName;
            TotalRevenue = totalRevenue;
        }
    }
}
