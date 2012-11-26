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
         UserDisp UserDispGetProfileByUserName(string userName, int currentUserId);
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
            var squawks = _db.Fetch<SquawkDisp>(@"select top 10 * from squawks s 
	inner join Users u on u.UserId = s.UserId
where username = @0
order by CreatedAt desc", userName);

            return squawks;
        }

        public UserDisp UserDispGetProfileByUserName(string userName, int currentUserId)
        {
            var userDisp = _db.FirstOrDefault<UserDisp>(@"select *, 
    (select count(1)
		from Followers f
	where u.UserId = f.FollowingUserId) Followers, 
	(select count(1) 
		from Followers f
	where u.UserId = f.UserId) Following, 
	(select count(1) 
		from Followers f
	where f.FollowingUserId = u.UserId 
	and f.UserId = @0) IsFollowing
from users u 
where username = @1", currentUserId, userName);

            return userDisp ?? new UserDisp();
        }

    }
}