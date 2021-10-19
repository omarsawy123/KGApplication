using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Test.TimesTables;

namespace Test.Application.Dto
{
    [AutoMap(typeof(TimesTable))]

    public class TimeTableDto : EntityDto
    {
        public string TimeName { get; set; }

        public DateTime TimeValue { get; set; }

        public int TenantId { get; set; }
    }
}
