using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Squawkings
{
    public static class StringExtensions
    {
        public static Boolean IsNumeric(this string stringToTest)
        {
            int result;
            return int.TryParse(stringToTest, out result);
        }
    }
}