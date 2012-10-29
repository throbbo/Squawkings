using System;
using System.Collections.Generic;
using NPoco;

namespace Squawkings.Models
{
    public interface ILogonDb
    {
        List<User> GetUsers();
        string GetPasswordByUserName(string userName);
        bool IsUserNameValid(string userName);
    }
}
