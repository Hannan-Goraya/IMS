using IMS.BLL.Users;
using IMS.Domain.Users;
using IMS.Web.Models.Account;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

using IMS.BLL.Role;
using static System.Net.WebRequestMethods;

namespace IMS.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRoleServices _role;
        private readonly IUserServices _user;

        public AccountController(IUserServices user, IRoleServices role)
        {
            _role = role;
            _user = user;
        }

        [ValidateAntiForgeryToken]
        public IActionResult Register() => View();

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Register(RegisterDto dto)
        {
            if (dto.ConfitmPassword != dto.Password)
            {
                return BadRequest("Password does not match");
            }
            if (ModelState.IsValid)
            {
                Guid guid = new Guid();
                string token = guid.ToString();

                var user = new AppUsers
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Password = _user.CreatePasswordHash(dto.Password),
                    Image = "NoImage",
                    IsVerify = false,
                    Token = token,
                    CreateDate = DateTime.UtcNow,


                };

                var result = _user.AddUser(user);

                if(result > 0)
                {
                    return RedirectToAction("Confirm");
                }

                else
                {
                    return BadRequest("Some Thing  gone wrong!!!!");
                }


            }

            return BadRequest(ModelState);
           

        }




        [AutoValidateAntiforgeryToken]
        [ValidateAntiForgeryToken]
        public IActionResult Confirm(string token)
        {
            OTP otp = new OTP
            {
                Token = token
            };
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Confirm(OTP otp,string token)
        {
            string email = (string)TempData["Email"];
            var user = _user.GetUserByEmail(email);

            if (user.CreateDate.AddMinutes(30) < DateTime.UtcNow.AddMinutes(30))
            {
                if (user.Token == token)
                {
                    bool verify = true;
                    _user.UpdateStatus(email, verify);
                    if (user.IsVerify = true)
                    {
                        return LocalRedirect("~/");
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            return BadRequest("Email Confirmation Link has been expired. kindly resend the link.");
        }




        [ValidateAntiForgeryToken]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginDto dto) 
        {
            if (ModelState.IsValid)
            {
                var user = _user.GetUserByEmail(dto.Email);
                if (user == null)
                {
                    return BadRequest("No Any User Is Registered on this email");
                }
               


                    if (user.IsVerify = false)
                    {
                        return BadRequest("Sorry! user not verify user self yet.. ");
                    }

                    if (!_user.VirifyPassword(dto.Password, user.Password))
                    {
                       return BadRequest("Your password does not match");
                    }
                    else
                     {



                    int userId = user.Id;

                    var UserRole = _role.GetAllRole(userId).ToArray();



                    var claims = new List<Claim>()
                    { 
                    new Claim(ClaimTypes.NameIdentifier, user.Email ),
                    };

                    foreach (var item in UserRole)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item.RoleName));
                    }



                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);



                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                              new ClaimsPrincipal(identity),
                              new AuthenticationProperties
                              {   ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
                                  IsPersistent = true,
                              });


                    return RedirectToRoute("Default");

                }
              
            }
            else
            {
                return BadRequest(ModelState);
            }
        }





















        private void SignInUser(AppUsers currentUser, bool isPersistent)
        {
            //Initialization
            var claims = new List<Claim>();

            try
            {
                //setting
                claims.Add(new Claim(ClaimTypes.Name, currentUser.Name));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, currentUser.Id.ToString()));
                //custom claims
                claims.Add(new Claim("Id", currentUser.Id.ToString()));
                claims.Add(new Claim("Name", currentUser.Name));
                claims.Add(new Claim("Email", currentUser.Email));
                claims.Add(new Claim("Image", currentUser.Image.ToString()));
                // Id Profile Picutue


                var identity = new ClaimsIdentity(claims, "DDLO");

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                         new ClaimsPrincipal(identity),
                         new AuthenticationProperties
                         {
                             ExpiresUtc = DateTime.UtcNow.AddMinutes(10),
                             IsPersistent = true
                         });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ClaimIdentities(string username, bool isPersistent)
        {
            //Initialization
            var claims = new List<Claim>();
            try
            {
                //setting
                claims.Add(new Claim(ClaimTypes.Name, username));
                var ClaimIdenties = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
