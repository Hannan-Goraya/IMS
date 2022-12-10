using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Users
{
    public partial class  AppUserRolePartial
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

    public partial class AppUserRolePartial
    {
        public int UId { get; set; }

        public int RId { get; set; }

    }
    public partial class AppUserRolePartial
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }
    }


}
