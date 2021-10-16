using System.Threading.Tasks;
using Abp.Application.Services;
using Test.Sessions.Dto;

namespace Test.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
