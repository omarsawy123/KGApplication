using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Test.Forms;
using Test.Dates;
using Test.TimesTables;
using Abp.Runtime.Session;

namespace Test.ApplicationTimeDates
{
    [Table("ApplicationTimeDates")]

    public class ApplicationTimeDate : FullAuditedEntity, IMustHaveTenant
    {

        [ForeignKey("FormId")]
        public virtual Form FormFk { get; set; }

        [ForeignKey("DateId")]
        public virtual DatesTable DateFk { get; set; }

        [ForeignKey("TimeId")]
        public virtual TimesTable TimeFk { get; set; }

        public int UserId { get; set; }

        public int TenantId { get; set; }
    }
}
