using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPoco;

namespace Squawkings.Models
{
    [TableName("Followers"), PrimaryKey("UserId")]
    public class Follower
    {
        [Column("UserId")]
        public int Userid { get; set; }
        [Column("FollowingUserId")]
        public int Followinguserid { get; set; }
    }
}