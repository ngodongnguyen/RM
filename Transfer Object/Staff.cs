using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer_Object
{
    public class Staff
    {
        public string staffId {  get; set; }
        public string sName { get; set;}
        public string sRole { get; set;}
        public string sPhone { get; set;}

        public Staff(string sName, string sRole, string sPhone)
        {
            this.sName = sName;
            this.sRole = sRole;
            this.sPhone = sPhone;
        }
        public Staff(string id,string sName, string sRole, string sPhone)
        {
            this.staffId = id;
            this.sName = sName;
            this.sRole = sRole;
            this.sPhone = sPhone;
        }
        public Staff(string id, string sName)
        {
            this.staffId = id;
            this.sName = sName;
        }
    }
}
