﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Users
{
    public class AppUsers
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Image { get; set; }

        public bool IsVerify { get; set; }

        public DateTime CreateDate { get; set; }

        public string Token { get; set; }

        
    }
}
