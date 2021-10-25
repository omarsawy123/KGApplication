using System;
using System.Collections.Generic;
using System.Text;
using Test.Authorization.Users;

namespace Test.Authorization.Accounts.Dto
{
    public class EmailConfirmationResult
    {
        public bool CanLogin { get; set; }
        public User userInfo { get; set; }

    }
}
