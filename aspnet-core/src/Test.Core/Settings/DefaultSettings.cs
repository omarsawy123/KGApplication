using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Test.Settings
{
    [Table("DefaultSettings")]
    public class DefaultSettings:FullAuditedEntity, IMustHaveTenant
    {
        [Required]
        public string DefaultHostName { get; set; }
        [Required]
        public string DefaultMailAddress { get; set; }
        [Required]
        public string DefaultMailPassword { get; set; }
        [Required]
        public DateTime ApplicationStartDate { get; set; }
        [Required]
        public DateTime ApplicationEndDate { get; set; }
        [Required]
        public DateTime StudentBirthDateMin { get; set; }
        [Required]
        public DateTime StudentBirthDateMax { get; set; }
        [Required]
        public bool IsDefault { get; set; }
        [Required]
        public int TenantId { get; set; }


    }
}
