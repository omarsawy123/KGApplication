﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Dates;
using Test.Forms;
using Test.TimesTables;

namespace Test.Dto.Forms
{
    [AutoMap(typeof(Form))]
    public class FormDto : EntityDto
    {

        
        public string StudentName { get; set; }

        
        public string StudentNameAr { get; set; }

        
        public DateTime StudentBirthDate { get; set; }

        
        public string StudentReligion { get; set; }

        
        public string FatherJob { get; set; }

        
        public string MotherJob { get; set; }

        
        public string FatherMobile { get; set; }

        
        public string MotherMobile { get; set; }

        
        public string Telephone { get; set; }

        
        public string Email { get; set; }

        public bool HasRelatives { get; set; }
        public bool MotherSchoolGraduate { get; set; }
        public string MotherGraduationYear { get; set; }

        public long StudentNationalId { get; set; }

        public string StudentRelativeName { get; set; }
        public string StudentRelativeGrade { get; set; }
        public string JoiningSchool { get; set; }

        public long DateId { get; set; }
        public long TimeId { get; set; }

        public int TenantId { get; set; }


    }
}