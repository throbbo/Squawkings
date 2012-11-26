﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPoco;

namespace Squawkings.Models
{
    [System.Web.DynamicData.TableName("Users"), PrimaryKey("UserId")]
    public class User
    {
        public int Userid { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public string Bio { get; set; }    
        public bool IsGravatar { get; set; }    
    }
}