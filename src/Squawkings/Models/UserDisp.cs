using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NPoco;

namespace Squawkings.Models
{
    public class UserDisp
    {
        public int Userid { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName 
        {
            get { return FirstName + " " + LastName; } 
        }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public string Bio { get; set; }    
        public int Followers { get; set; }
        public int Following { get; set; }
        public bool IsFollowing { get; set; }
        public bool UnfollowButton { get; set; }
        public bool FollowButton { get; set; }
        public bool IsGravatar { get; set; }
        public string DisplayUrl { get; set; }
    }
}