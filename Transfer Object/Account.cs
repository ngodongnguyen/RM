using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer_Object
{
    public class Account
    {
        public String UserName {  get; set; }
        public String Password { get; set; }

        public String uName { get; set; }

        public String uPhone { get; set; }

        public Account(string userName, string password, string uName, string uPhone)
        {
            this.UserName = userName;
            this.Password = password;
            this.uName = uName;
            this.uPhone = uPhone;
        }
        public Account(string uName,string password)
        {
            this.UserName= uName;
            this.Password = password;
        }
    }
}
