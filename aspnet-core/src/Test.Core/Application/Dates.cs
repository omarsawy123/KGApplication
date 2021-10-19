using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Test.Dates
{
    [Table("Dates")]

    public class Date:FullAuditedEntity, IMustHaveTenant
    {

        [Required]
        public string DateName { get; set; }

        [Required]
        public DateTime DateValue { get; set; }

        public bool IsStartDate { get; set; }
        public bool IsEndDate { get; set; }


        public int TenantId { get; set; }



    }
}
