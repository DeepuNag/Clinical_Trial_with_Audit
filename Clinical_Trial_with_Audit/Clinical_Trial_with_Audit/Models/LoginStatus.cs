using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinical_Trial_with_Audit.Models
{
    public class LoginStatus
    {
        public bool Success { get; set; }
        //public string Message { get; set; }
        public string TargetURL { get; set; }
    }
}