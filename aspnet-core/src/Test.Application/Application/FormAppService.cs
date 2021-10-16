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

namespace Test.Forms
{
    [AbpAuthorize(PermissionNames.Pages_Application)]

    public class FormAppService : AsyncCrudAppService<Form, FormDto>, IFormAppService
    {

        private readonly IRepository<Form, int> repository;
        public FormAppService(IRepository<Form, int> repository) : base(repository)
        {
            this.repository = repository;
        }

        public async Task CreateForm(FormDto input)
        {

            if (repository.GetAll().Where(s => s.StudentNationalId == input.StudentNationalId) == null)
            {

                var form = ObjectMapper.Map<Form>(input);

                await repository.InsertAsync(form);

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
                var form = await repository.GetAsync((int)input.Id);
                var result = ObjectMapper.Map<FormDto>(form);

                return result;

            }
        }
    }
}
