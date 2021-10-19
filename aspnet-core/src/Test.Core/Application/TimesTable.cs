using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.TimesTables
{
    [Table("TimesTables")]

    public class TimesTable:FullAuditedEntity, IMustHaveTenant
    {

        [Required]
        public string TimeName { get; set; }

        [Required]
        public DateTime TimeValue { get; set; }

        public int TenantId { get; set; }


    }
}
