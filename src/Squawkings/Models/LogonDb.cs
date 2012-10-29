using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Web;
using NPoco;
namespace Squawkings.Models
{
    
    public class LogonDb : ILogonDb
    {
        public IDatabase GetDb()
        {
            IDatabase db = new Database("Squawkings");
            return db;
        }

        public List<User> GetUsers()
        {
            IDatabase db = GetDb();
            var users = db.Fetch<User>("select * from users");
            return users;
        }

        public string GetPasswordByUserName(string userName)
        {
            IDatabase db = GetDb();
            var pw = db.ExecuteScalar<string>(@"select usi.password from users u
	inner join usersecurityinfo usi on u.UserId = usi.UserId
and u.UserName= @0", userName);
            return pw;
        }
    }
}