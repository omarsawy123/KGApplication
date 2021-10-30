using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Dto.FormsList
{
    public class FormListDto : EntityDto<long>
    {
        public string StudentName { get; set; }
        public string StudentNameAr { get; set; }
        public DateTime FormDate { get; set; }
        public DateTime FormTime { get; set; }

        public int TenantId { get; set; }


    }
}
