using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Test.Authorization.Roles;
using Test.Authorization.Users;
using Test.MultiTenancy;
using Test.Forms;

namespace Test.EntityFrameworkCore
{
    public class TestDbContext : AbpZeroDbContext<Tenant, Role, User, TestDbContext>
    {
        /* Define a DbSet for each entity of the application */
        

        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Form> BinaryObjects { get; set; }

    }
}
