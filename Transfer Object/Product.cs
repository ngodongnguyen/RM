using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer_Object
{
    public class Product
    {
        public string pId { get; set; }
        public string pName { get; set; }
        public string pPrice { get; set; }
        public string categoryId { get; set; }
        public byte[] pImage { get; set; }
        public Product(string pId, string pName, string pPrice, string categoryId, byte[] image)
        {
            this.pId = pId;
            this.pName = pName;
            this.pPrice = pPrice;
            this.categoryId = categoryId;
            this.pImage = image;
        }
        public Product(string pName, string pPrice, string categoryId, byte[] image)
        {
            this.pName = pName;
            this.pPrice = pPrice;
            this.categoryId = categoryId;
            this.pImage = image;
        }
        public Product( string pName, string pPrice, string categoryId)
        {
            this.pName = pName;
            this.pPrice = pPrice;
            this.categoryId = categoryId;
        }
        public Product(string pName)
        {
            this.pName = pName;
        }
    }
}
