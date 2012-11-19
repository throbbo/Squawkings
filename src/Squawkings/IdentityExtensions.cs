using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Squawkings
{
    public static class IdentityExtensions
    {
        public static int Id (this IIdentity identity)
        {
            if (identity != null && !string.IsNullOrEmpty(identity.Name) && identity.Name.IsNumeric())
                return Convert.ToInt32(identity.Name);

            return -1;
        }
    }
}