using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Test.EntityFrameworkCore
{
    public static class TestDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<TestDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<TestDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
