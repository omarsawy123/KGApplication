using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Test.Authorization.Roles;
using Test.Authorization.Users;
using Test.MultiTenancy;
using Test.Forms;
using Test.Dates;
using Test.TimesTables;
using Test.ApplicationTimeDates;
using Test.Settings;

namespace Test.EntityFrameworkCore
{
    public class TestDbContext : AbpZeroDbContext<Tenant, Role, User, TestDbContext>
    {
        /* Define a DbSet for each entity of the application */
        

        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Form> Forms { get; set; }
        public virtual DbSet<DatesTable> Dates { get; set; }
        public virtual DbSet<TimesTable> TimesTables { get; set; }
        public virtual DbSet<ApplicationTimeDate> ApplicationTimeDates { get; set; }

        public virtual DbSet<DefaultSettings> DefaultSettings { get; set; }




    }
}
