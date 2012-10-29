using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPoco;

namespace Squawkings.Models
{
    [TableName("UserSecurityInfo"), PrimaryKey("UserId")]
    public class UserSecurityInfo
    {
        [Column("UserId")]
        public int Userid { get; set; }
        [Column("Password")]
        public string Password { get; set; }
    }
}