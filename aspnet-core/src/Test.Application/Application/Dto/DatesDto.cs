using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Dates;

namespace Test.Dto.Dates
{
    [AutoMap(typeof(DatesTable))]

    public class DatesDto : EntityDto
    {
        public string DateName { get; set; }

        public DateTime DateValue { get; set; }

        public bool IsStartDate { get; set; }
        public bool IsEndDate { get; set; }
        public bool IsEnabled { get; set; }


        public int TenantId { get; set; }
    }
}
