using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService1
{
    public class User
    {
        public Guid Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string UserName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string city { get; set; }

   

    }
}