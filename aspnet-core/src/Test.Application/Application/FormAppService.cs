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
using Microsoft.EntityFrameworkCore;
using Test.Dto.Dates;
using Test.Application.Dto;
using Abp.Runtime.Session;
using Test.Dto.ViewForm;
using Test.Settings;

namespace Test.Forms
{
    [AbpAuthorize(PermissionNames.Pages_Application)]

    public class FormAppService : AsyncCrudAppService<Form, FormDto>, IFormAppService
    {

        private readonly IRepository<Form, int> _formRepository;
        private readonly IRepository<DatesTable, int> _dateRepository;
        private readonly IRepository<TimesTable, int> _timeRepository;
        private readonly IRepository<DefaultSettings, int> _settingsRepository;
        private readonly IRepository<ApplicationTimeDate, int> _appRepository;
        private readonly IAbpSession _abpSession;


        public FormAppService(IRepository<Form, int> repository
            , IRepository<DatesTable, int> dateRepository
            , IRepository<TimesTable, int> timeRepository
            , IRepository<ApplicationTimeDate, int> appRepository
            , IRepository<DefaultSettings,int> settingsRepository
            , IAbpSession abpSession
            ) : base(repository)
        {
            this._formRepository = repository;
            this._dateRepository = dateRepository;
            this._timeRepository = timeRepository;
            this._appRepository = appRepository;
            this._abpSession = abpSession;
            this._settingsRepository = settingsRepository;
        }


        public async Task<FormDto> CreateForm(FormDto input)
        {

            if (_formRepository.GetAll().Where(s => s.StudentNationalId == input.StudentNationalId).FirstOrDefault() == null)
            {

                var form = ObjectMapper.Map<Form>(input);

                var newForm = await _formRepository.InsertAsync(form);


                var AppTimeDate = new ApplicationTimeDate();

                if (_appRepository.GetAll().Where(a => a.DateFk.Id == input.DateId).Count() == 59)
                {
                    var d = await _dateRepository.GetAsync((int)input.DateId);
                    d.IsEnabled = false;
                    await _dateRepository.UpdateAsync(d);

                }

                AppTimeDate.DateFk = await _dateRepository.GetAsync((int)input.DateId);

                //if (_appRepository.FirstOrDefault(a => a.TimeFk.Id == input.TimeId) == null)
                //{
                //    var t = await _timeRepository.GetAsync((int)input.TimeId);
                //    t.IsEnabled = false;
                //    await _timeRepository.UpdateAsync(t);

                //    AppTimeDate.TimeFk = await _timeRepository.GetAsync((int)input.TimeId);

                //}


               
                AppTimeDate.TimeFk = await _timeRepository.GetAsync((int)input.TimeId);

                //var t = await _timeRepository.GetAsync((int)input.TimeId);
                //t.IsEnabled = false;
                //await _timeRepository.UpdateAsync(t);

                AppTimeDate.FormFk = newForm;
                AppTimeDate.TenantId = 1;
                AppTimeDate.IsDeleted = false;
                AppTimeDate.CreationTime = DateTime.Now;
                AppTimeDate.UserId = (int)_abpSession.UserId.Value;

                await _appRepository.InsertAsync(AppTimeDate);

                return ObjectMapper.Map<FormDto>(newForm);

            }

            else {

                throw new UserFriendlyException("Student NationalID Already Exists!");

            }

        }

        public async Task<FormDto> UpdateForm(FormDto input)
        {
            if (_formRepository.GetAll().Where(s => s.StudentNationalId == input.StudentNationalId).Count() == 1)
            {

                var form = ObjectMapper.Map<Form>(input);

                var updatedForm = await _formRepository.UpdateAsync(form);


                var AppTimeDate = await _appRepository.FirstOrDefaultAsync(a => a.FormFk.Id == input.Id);

                if (_appRepository.GetAll().Where(a => a.DateFk.Id == input.DateId).Count() == 59)
                {
                    var d = await _dateRepository.GetAsync((int)input.DateId);
                    d.IsEnabled = false;
                    await _dateRepository.UpdateAsync(d);

                }

                AppTimeDate.DateFk = await _dateRepository.GetAsync((int)input.DateId);

                //if (_appRepository.FirstOrDefault(a => a.TimeFk.Id == input.TimeId) == null)
                //{
                //    var t = await _timeRepository.GetAsync((int)input.TimeId);
                //    t.IsEnabled = false;
                //    await _timeRepository.UpdateAsync(t);

                //    AppTimeDate.TimeFk = await _timeRepository.GetAsync((int)input.TimeId);

                //}



                AppTimeDate.TimeFk = await _timeRepository.GetAsync((int)input.TimeId);

                //var t = await _timeRepository.GetAsync((int)input.TimeId);
                //t.IsEnabled = false;
                //await _timeRepository.UpdateAsync(t);

                //AppTimeDate.FormFk = updatedForm;
                AppTimeDate.TenantId = 1;
                AppTimeDate.IsDeleted = false;
                AppTimeDate.LastModificationTime = DateTime.Now;
                AppTimeDate.UserId = (int)_abpSession.UserId.Value;

                await _appRepository.UpdateAsync(AppTimeDate);

                return ObjectMapper.Map<FormDto>(updatedForm);

            }

            else
            {

                throw new UserFriendlyException("Student NationalID Already Exists!");

            }
        }



        public async Task <List<DatesDto>> GetAllDates()
        {

            var date = await _dateRepository.GetAll().ToListAsync();
            var dates = ObjectMapper.Map<List<DatesDto>>(date);

            foreach (var da in dates)
            {
                if (da.IsEnabled)
                {
                    var subselect = (from ap in _appRepository.GetAll().Where(a => a.DateFk.Id == da.Id) select ap.TimeFk.Id).ToList();
                    var result = await (from tm in _timeRepository.GetAll() where !subselect.Contains(tm.Id) select tm).ToListAsync();



                    da.TimeTable = ObjectMapper.Map<List<TimeTableDto>>(result);


                }
            }



            return dates;

        }

        public async Task<List<TimeTableDto>> GetAllTimes(int dateId)
        {
            var result = await _timeRepository.GetAll().ToListAsync();
            return ObjectMapper.Map<List<TimeTableDto>>(result);
        }


        public bool CheckApplicationStartEndDate(DateTime dateValue)
        {

            var date = _settingsRepository.FirstOrDefault(s => s.IsDefault);

            return date.ApplicationStartDate <= dateValue && date.ApplicationEndDate >= dateValue;

        }

        public string GetStudentBirthDateMinMax()
        {
            var date = _settingsRepository.FirstOrDefault(s => s.IsDefault);

            return "مواليد تاريخ " + date.StudentBirthDateMin.ToShortDateString() + " الي مواليد تاريخ " + date.StudentBirthDateMax.ToShortDateString();

        }

        public bool CheckStudentBirthDate(DateTime dateValue)
        {
            var date = _settingsRepository.FirstOrDefault(s => s.IsDefault);

            return date.StudentBirthDateMin <= dateValue && date.StudentBirthDateMax >= dateValue;

        }

        public async Task<int> CheckUserApplication()
        {

            var userId = _abpSession.UserId.Value;
            var app = await _appRepository.GetAll().Where(a => a.UserId == userId).Include(a => a.FormFk).FirstOrDefaultAsync();

            if (app != null)
            {

                return app.FormFk.Id;

            }
            else
            {
                return 0;
            }
        }

        public async Task<ViewFormDto> GetForm(NullableIdDto input)
       {
            if (input.Id == null) throw new UserFriendlyException("Please Login Before Viewing Application");

            else
            {
                var form = await _formRepository.GetAsync((int)input.Id);

                var app = await _appRepository.GetAll().Where(a => a.FormFk.Id == input.Id)
                    .Include(a => a.TimeFk).Include(a => a.DateFk).FirstOrDefaultAsync();

                ViewFormDto result = new ViewFormDto();
                
                result.Form = ObjectMapper.Map<FormDto>(form);
                result.Form.DateId = app.DateFk.Id;
                result.Form.TimeId = app.TimeFk.Id;

                result.DateName = app.DateFk.DateName;
                result.TimeName = app.TimeFk.TimeName;

                return result;

            }
        }

        
    }
}
