using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Application.Dto;
using Test.Dates;
using Test.Dto.Dates;
using Test.Dto.Forms;
using Test.TimesTables;

namespace Test.Forms
{
    public interface IFormAppService : IAsyncCrudAppService<FormDto>
    {
        Task<FormDto> GetForm(NullableIdDto input);
        Task CreateForm(FormDto input);
        Task<List<TimeTableDto>> GetAllTimes(int dateId);

         Task<List<DatesDto>> GetAllDates();
    }
}
