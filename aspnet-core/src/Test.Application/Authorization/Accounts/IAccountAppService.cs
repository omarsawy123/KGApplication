using System.Threading.Tasks;
using Abp.Application.Services;
using Test.Authorization.Accounts.Dto;

namespace Test.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
