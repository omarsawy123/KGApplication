using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Dto.Forms;

namespace Test.Forms
{
    public interface IFormAppService : IAsyncCrudAppService<FormDto>
    {
        Task<FormDto> GetForm(NullableIdDto input);
        Task CreateForm(FormDto input);
    }
}
