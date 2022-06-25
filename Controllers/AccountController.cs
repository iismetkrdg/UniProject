using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EduProject.Models;
using EduProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace EduProject.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly DbContext _context;

        
        public AccountController(DbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index","home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterUserViewModel x)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index","home");
            }
            if (ModelState.IsValid)
            {
                var user = _context.User.FirstOrDefault(p=>p.UserName==x.UserName);
                if(user==null)
                {
                    User reguser = new User
                    {
                        UserName = x.UserName,
                        Password = x.Password,
                        AtSignUp = DateTime.Now
                    };
                    _context.User.Add(reguser);
                    _context.SaveChanges();
                    return Redirect("/Home/Index");
                }else
                {
                    ViewBag.Errors = "Kullanıcı adı alınmış.";
                    return View();
                }
            }
            return View();
        }

        public IActionResult Login(string ReturnUrl = "" )
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index","home");
            }
            ViewBag.returnurl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(RegisterUserViewModel a, string ReturnUrl=null)
        {   
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index","home");
            }
            if(ModelState.IsValid)
            {
                var user = _context.User.FirstOrDefault(p=>p.UserName==a.UserName && p.Password == a.Password);
                if (user!=null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim("username",user.UserName),
                        new Claim(ClaimTypes.Role,"User"),
                    }; 
                    var claimsIdentity = new ClaimsIdentity(
                        claims,CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        RedirectUri = ReturnUrl,

                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                    
                }
            }
            return View();
        }
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            

            // Clear the existing external cookie
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("index","home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}