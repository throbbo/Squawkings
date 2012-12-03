using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Web;
using NPoco;

namespace Squawkings.Models
{
    public interface IHomeDb
    {
        List<SquawkDisp> GetHomeSquawks(int userId);
        void AddSquawk(int userId, string content);
    }

    public class HomeDb : IHomeDb
    {
        private readonly IDatabase _db;

        public HomeDb()
            : this(new Database("Squawkings"))
        {
            
        }
        public HomeDb(IDatabase db)
        {
            _db = db;
        }

        public List<SquawkDisp> GetHomeSquawks(int userId)
        {
            var squawks = _db.Fetch<SquawkDisp>(@"select * from squawks s
	inner join (
select UserId, AvatarUrl, UserName, FirstName, LastName, Email, IsGravatar from Users where UserId=@0
union
select f.followinguserid, uf.AvatarUrl, uf.UserName, uf.FirstName, uf.LastName, uf.Email, uf.IsGravatar 
from Followers f 
	inner join Users uf on f.FollowingUserId = uf.UserId
where f.UserId=@0 ) h on h.userid = s.userid order by CreatedAt desc", userId);

            return squawks;
        }

        public void AddSquawk(int userId, string content)
        {
            var squawk = new Squawk
                             {
                                 Content = content,
                                 CreatedAt = DateTime.UtcNow,
                                 UserId = userId
                             };
            //var sql = "insert into squawks (UserId,CreatedAt,Content) values (@0, @1, @2) ";
            _db.Insert(squawk);
        }
    }
}