using System.Collections.Generic;
using Squawkings.Models;

namespace Squawkings.Controllers
{
    public class SquawkDispsViewModel
    {
        public SquawkDispsViewModel()
        {
            OtherStuff = "Hello World";
            SquawkDisps = new List<SquawkDisp>();
        }
        public List<SquawkDisp> SquawkDisps { get; set; }
        public string OtherStuff { get; set; }
    }
}

public class SquawkDispsViewModel
{
    public List<SquawkDisp> SquawkDisps { get; set; }
    public string OtherStuff { get; set; }
}