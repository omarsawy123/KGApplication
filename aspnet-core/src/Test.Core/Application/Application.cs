using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Test.Forms
{

    [Table("Forms")]
    public class Form: FullAuditedEntity, IMustHaveTenant
    {
        [Required]
        public string StudentName { get; set; }

        [Required]
        public string StudentNameAr { get; set; }

        [Required]
        public DateTime StudentBirthDate { get; set; }

        [Required]
        public string StudentReligion { get; set; }

        [Required]
        public string FatherJob { get; set; }

        [Required]
        public string MotherJob { get; set; }

        [Required]
        public string FatherMobile { get; set; }

        [Required]
        public string MotherMobile { get; set; }

        [Required]
        public string Telephone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public long StudentNationalId { get; set; }

        public string StudentRelativeName { get; set; }
        public string StudentRelativeGrade { get; set; }
        public string JoiningSchool { get; set; }

        public int TenantId { get; set ; }
    }
}
