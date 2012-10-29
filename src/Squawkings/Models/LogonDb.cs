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
        private readonly IDatabase _db;

        public LogonDb()
            : this(new Database("Squawkings"))
        {
            
        }
        public LogonDb(IDatabase db)
        {
            _db = db;
        }

        public List<User> GetUsers()
        {
            var users = _db.Fetch<User>("select * from users");
            return users;
        }

        public string GetPasswordByUserName(string userName)
        {
            var pw = _db.ExecuteScalar<string>(@"select usi.password from users u
	inner join usersecurityinfo usi on u.UserId = usi.UserId
and u.UserName= @0", userName);
            return pw;
        }
        
        public bool IsUserNameValid(string userName)
        {
            var isUserValid = GetUsers().Any(x => x.Username == userName);
            return isUserValid;
        }
    }
}