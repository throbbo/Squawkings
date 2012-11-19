using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPoco;

namespace Squawkings.Models
{
    public class UserDisp
    {
        public int Userid { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string FullName 
        {
            get { return Firstname + " " + Lastname; } 
        }
        public string Email { get; set; }
        public string Avatarurl { get; set; }
        public string Bio { get; set; }    
    }
}