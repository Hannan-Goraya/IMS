using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Users
{
    public class UserListWithRole
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Roles { get; set; }
    }
}
