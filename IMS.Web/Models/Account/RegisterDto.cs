using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IMS.Web.Models.Account
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }


        [Required]
        [PasswordPropertyText]
        public string ConfitmPassword { get; set; }




    }
}
