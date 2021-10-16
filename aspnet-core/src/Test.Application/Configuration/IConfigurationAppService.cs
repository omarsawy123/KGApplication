using System.Threading.Tasks;
using Test.Configuration.Dto;

namespace Test.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
