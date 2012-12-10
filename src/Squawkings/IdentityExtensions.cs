using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using NPoco;
using Squawkings.Models;
using StructureMap;

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

		public static string GetName(this IIdentity identity)
		{
			if (!identity.IsAuthenticated) return null;

			var db = ObjectFactory.GetInstance<IDatabase>();
			if (db != null) return null;

			return db.SingleById<User>(identity.Id()).UserName;
		}

    }
}