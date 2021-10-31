using System;
using System.Collections.Generic;
using System.Text;
using Test.Dto.Forms;
using Test.Forms;

namespace Test.Dto.ViewForm
{
    public class ViewFormDto
    {
        public FormDto Form { get; set; }

        public string DateName { get; set; }
        public string TimeName { get; set; }

        public int Days { get; set; }
        public int Months { get; set; }
        public int Years { get; set; }


    }
}
