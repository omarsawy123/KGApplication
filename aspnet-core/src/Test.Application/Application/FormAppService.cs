using Abp.Application.Services;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Forms;
using Test.Dto.Forms;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using System.Linq;
using Abp.UI;
using Test.Authorization;
using Abp.Authorization;
using Test.Dates;
using Test.TimesTables;
using Test.ApplicationTimeDates;

namespace Test.Forms
{
    [AbpAuthorize(PermissionNames.Pages_Application)]

    public class FormAppService : AsyncCrudAppService<Form, FormDto>, IFormAppService
    {

        private readonly IRepository<Form, int> _formRepository;
        private readonly IRepository<Date, int> _dateRepository;
        private readonly IRepository<TimesTable, int> _timeRepository;
        private readonly IRepository<ApplicationTimeDate, int> _appRepository;

        public FormAppService(IRepository<Form, int> repository
            , IRepository<Date, int> dateRepository
            , IRepository<TimesTable, int> timeRepository
            , IRepository<ApplicationTimeDate, int> appRepository
            ) : base(repository)
        {
            this._formRepository = repository;
            this._dateRepository = dateRepository;
            this._timeRepository = timeRepository;
            this._appRepository = appRepository;
        }

        public async Task CreateForm(FormDto input)
        {

            if (_formRepository.GetAll().Where(s => s.StudentNationalId == input.StudentNationalId).FirstOrDefault() == null)
            {

                var form = ObjectMapper.Map<Form>(input);

                var newForm = await _formRepository.InsertAsync(form);

                var AppTimeDate = new ApplicationTimeDate();

                AppTimeDate.TimeFk = _timeRepository.Get((int)input.TimeId);
                AppTimeDate.DateFk = _dateRepository.Get((int)input.DateId);
                AppTimeDate.FormFk = newForm;
                AppTimeDate.TenantId = 1;
                AppTimeDate.IsDeleted = false;
                AppTimeDate.CreationTime = DateTime.Now;

                await _appRepository.InsertAsync(AppTimeDate);

                

            }

            else {

                throw new UserFriendlyException("Student NationalID Already Exists!");

            }

        }


        public async Task<FormDto> GetForm(NullableIdDto input)
        {
            if (input.Id == null) throw new UserFriendlyException("Please Login Before Viewing Application");

            else
            {
                var form = await _formRepository.GetAsync((int)input.Id);
                var result = ObjectMapper.Map<FormDto>(form);

                return result;

            }
        }
    }
}
