using System;
using NPoco;

namespace Squawkings.Models
{
    public interface ILogonDb
    {
        System.Collections.Generic.List<User> GetUsers();
        IDatabase GetDb();
        string GetPasswordByUserName(string userName);
    }
}
