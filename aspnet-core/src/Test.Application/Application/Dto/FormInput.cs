using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Application.Dto
{
    public class FormInput: PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public long DateId { get; set; }
        public long TimeId { get; set; }

    }
}
