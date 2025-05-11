using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer_Object
{
    public class Tables
    {
        public string tId {  get; set; }
        public string tName { get; set; }
        public Tables(string tId, string tName)
        {
            this.tId = tId;
            this.tName = tName;
        }
        public Tables(string tName)
        {
            this.tName = tName;
        }
    }
}
