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
        //public int userID { get; set; }

        public int staff_id { get; set; }

        //public Account(int userID, string uName,string password,int staffid)
        //{
        //    this.userID=userID;
        //    this.UserName= uName;
        //    this.Password = password;
        //    this.staff_id = staffid;
        //}
        public Account(string uName, string password)
        {
            this.UserName = uName;
            this.Password = password;
        }
        public Account(string uName, string password,int staffid)
        {
            this.UserName = uName;
            this.Password = password;
            this.staff_id=staffid;
        }
        public Account()
        {

        }

    }
}
