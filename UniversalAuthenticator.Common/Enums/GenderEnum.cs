﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Common.Enums
{
    public enum GenderEnum
    {
        [Display(Name ="Male")]
        Male,
        [Display(Name = "Female")]
        Female
    }
}
