using Abp.Authorization;
using Test.Authorization.Roles;
using Test.Authorization.Users;

namespace Test.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
