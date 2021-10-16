using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Test.Authorization;

namespace Test
{
    [DependsOn(
        typeof(TestCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class TestApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<TestAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(TestApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
