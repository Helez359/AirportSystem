﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Userame { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
