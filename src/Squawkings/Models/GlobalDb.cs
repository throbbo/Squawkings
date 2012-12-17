using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Web;
using NPoco;

namespace Squawkings.Models
{
    public interface IGlobalDb
    {
        List<SquawkDisp> GetGlobalSquawks();
    }

    public class GlobalDb : IGlobalDb
    {
        private readonly IDatabase _db;

        public GlobalDb(IDatabase db)
        {
            _db = db;
        }

        public List<SquawkDisp> GetGlobalSquawks()
        {
        	var template = new GlobalSquawk();

        	var squawks = _db.Fetch<SquawkDisp>(template.Template1);

            return squawks;
        }
    }
}