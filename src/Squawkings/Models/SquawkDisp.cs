using System;

namespace Squawkings.Models
{
    public class SquawkDisp
    {
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Time { get; set; }
        public string TimeDisplay { get; set; }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; } 
        }
        public string AvatarUrl { get; set; }
    }
}