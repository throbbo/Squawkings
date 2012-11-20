using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPoco;

namespace Squawkings.Models
{
    [TableName("squawks"), PrimaryKey("SquawkId")]
    public class Squawk
    {
        [Column("SquawkId")]
        public int SquawkId { get; set; }
        [Column("UserId")]
        public int UserId { get; set; }
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }
        [Column("Content")]
        public string Content { get; set; }
    }
}