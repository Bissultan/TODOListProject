using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicTasks.Models
{
    public class AuthModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public string errMsg { get; set; }
    }
}