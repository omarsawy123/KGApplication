using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Test.Configuration.Dto;

namespace Test.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : TestAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
