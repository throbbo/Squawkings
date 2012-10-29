using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPoco;

namespace Squawkings.Models
{
    [System.Web.DynamicData.TableName("Users"), PrimaryKey("UserId")]
    public class User
    {
        [Column("UserId")]
        public int Userid { get; set; }
        [Column("UserName")]
        public string Username { get; set; }
        [Column("FirstName")]
        public string Firstname { get; set; }
        [Column("LastName")]
        public string Lastname { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("AvatarUrl")]
        public string Avatarurl { get; set; }
    }
}