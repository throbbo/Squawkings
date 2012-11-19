using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPoco;

namespace Squawkings.Models
{
    public class UserLogon
    {
        public int Userid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}