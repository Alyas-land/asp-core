using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tqmrin2.Data;
using Tqmrin2.Models;

namespace Tqmrin2.Controllers
{
    public class AuthController : Controller
    {
        private readonly MyDbContext _context;

        public AuthController(MyDbContext context)
        {
             _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "اطلاعات صحیح نمی باشد.";
                return View(model);
            }

            var user = _context.Users.SingleOrDefault( u => u.Username == model.Username);
            if (user == null ) {
                TempData["error"] = "شما حساب کاربری ندارید، لطفا ابتدا قبت نام فرمایید";
                return RedirectToAction("Register");
      
            }

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.Password, model.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                TempData["PassError"] = "پسورد اشتباه می باشد";
                return View(model);
            }
            

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.NationalId),
                new Claim(ClaimTypes.Name, user.Name),
                
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
            TempData["Success"] = "ورود موفقیت آمیز";
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "لطفا در وارد کردن اطلاعات دقت فرمایید";
                return View(model);
            }
            var checking = _context.Users.FirstOrDefault(i=> i.NationalId == model.NationalCode);
            if(checking != null)
            {
                TempData["Logined"] = "شما قبلا حساب کاربری ساخته اید";
                return RedirectToAction("Login");
            }

            var hasher = new PasswordHasher<User>();

            var newUser = new User
            {
                NationalId = model.NationalCode,
                Name = model.Name,
                Username = model.Username,
                Password = hasher.HashPassword(null, model.Password),
                CreatedAccount = DateTime.Now
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, newUser.NationalId),
                new Claim(ClaimTypes.Name, newUser.Name),
                

            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
            TempData["Success"] = "ورود موفقیت آمیز";
            return RedirectToAction("Index", "Home");

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
            TempData["Success"] = "ورود موفقیت آمیز";
            return RedirectToAction("Index", "Home");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
