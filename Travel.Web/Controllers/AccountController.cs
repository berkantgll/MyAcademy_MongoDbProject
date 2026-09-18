using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Travel.Web.DTOs.UserDtos;
using Travel.Web.Entitites;
using Travel.Web.Services.UserServices;

namespace Travel.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher<User> _passwordHasher;


        public AccountController(
            IUserService userService,
            IPasswordHasher<User> passwordHasher)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return View(registerDto);
            }


            if (registerDto.Password != registerDto.ConfirmPassword)
            {
                ModelState.AddModelError(
                    "",
                    "Şifreler eşleşmiyor."
                );

                return View(registerDto);
            }


            var existingUser =
                await _userService.GetByEmailAsync(
                    registerDto.Email
                );


            if (existingUser != null)
            {
                ModelState.AddModelError(
                    "",
                    "Bu e-posta adresi zaten kayıtlı."
                );

                return View(registerDto);
            }


            var user = new User
            {
                NameSurname = registerDto.NameSurname,

                Email = registerDto.Email
                    .Trim()
                    .ToLowerInvariant(),

                Role = "User",

                CreatedDate = DateTime.Now
            };


            user.PasswordHash =
                _passwordHasher.HashPassword(
                    user,
                    registerDto.Password
                );


            await _userService.CreateAsync(user);


            return RedirectToAction(nameof(Login));
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }


            var email =
                loginDto.Email
                    .Trim()
                    .ToLowerInvariant();


            var user =
                await _userService.GetByEmailAsync(email);


            if (user == null)
            {
                ModelState.AddModelError(
                    "",
                    "E-posta veya şifre hatalı."
                );

                return View(loginDto);
            }


            var result =
                _passwordHasher.VerifyHashedPassword(
                    user,
                    user.PasswordHash,
                    loginDto.Password
                );


            if (result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError(
                    "",
                    "E-posta veya şifre hatalı."
                );

                return View(loginDto);
            }


            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id
                ),

                new Claim(
                    ClaimTypes.Name,
                    user.NameSurname
                ),

                new Claim(
                    ClaimTypes.Email,
                    user.Email
                ),

                new Claim(
                    ClaimTypes.Role,
                    user.Role
                )
            };


            var identity =
                new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                );


            var principal =
                new ClaimsPrincipal(identity);


            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );


            if (user.Role == "Admin")
            {
                return RedirectToAction(
                    "Index",
                    "Dashboard",
                    new
                    {
                        area = "Admin"
                    }
                );
            }


            return RedirectToAction(
                "Index",
                "Home"
            );
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            return RedirectToAction(nameof(Login));
        }


        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}