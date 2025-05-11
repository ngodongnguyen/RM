using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer_Object
{
    public class Category
    {
        public string Id { get; set; }
        public string CatName { get; set; }

        public Category(string id, string name)
        {
            this.Id = id;
            this.CatName = name;
        }
        public Category(string name)
        {
            this.CatName = name;
        }
    }
}
