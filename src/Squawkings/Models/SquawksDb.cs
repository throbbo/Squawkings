using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Web;
using NPoco;

namespace Squawkings.Models
{
    public interface ISquawksDb
    {
        List<SquawkDisp> GetProfileSquawks(string userName);
         UserDisp UserDispGetProfileByUserName(string userName);
    }

    public class SquawksDb : ISquawksDb
    {
        private readonly IDatabase _db;

        public SquawksDb()
            : this(new Database("Squawkings"))
        {
            
        }
        public SquawksDb(IDatabase db)
        {
            _db = db;
        }

        public List<SquawkDisp> GetProfileSquawks(string userName)
        {
            var squawks = _db.Fetch<SquawkDisp>(@"select * from squawks s 
	inner join Users u on u.UserId = s.UserId
where username = @0", userName);

            return squawks;
        }

        public UserDisp UserDispGetProfileByUserName(string userName)
        {
            var userDisp = _db.FirstOrDefault<UserDisp>("select * from users where username = @0",userName);

            return userDisp ?? new UserDisp();
        }

    }
}