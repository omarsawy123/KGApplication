using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Test.EntityFrameworkCore;
using Test.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Test.Web.Tests
{
    [DependsOn(
        typeof(TestWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class TestWebTestModule : AbpModule
    {
        public TestWebTestModule(TestEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TestWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(TestWebMvcModule).Assembly);
        }
    }
}