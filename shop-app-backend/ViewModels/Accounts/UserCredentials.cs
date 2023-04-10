﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Accounts
{
    public class UserCredentials
    {
        [Required] 
        [EmailAddress] 
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
