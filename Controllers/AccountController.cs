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
using X.PagedList;

namespace EduProject.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly DbContext _context;

        
        public AccountController(DbContext context)
        {
            _context = context;
        }


        
        public IActionResult Users(int? page)
        {   
            if (!User.IsInRole("admin"))
            {
                return RedirectToAction("index","home");
            }
            var pageSize = 25;
            var pageNumber = page ?? 1;
            return View(_context.User.ToPagedList(pageNumber, pageSize));
        }
        public IActionResult AddAdmin()
        {
            if (!User.IsInRole("admin"))
            {
                return RedirectToAction("index","home");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAdmin(RegisterUserViewModel x)
        {
            if (!User.IsInRole("admin"))
            {
                return RedirectToAction("index","home");
            }
            if (ModelState.IsValid)
            {
                if (_context.User.FirstOrDefault(p=>p.UserName==x.UserName)==null)
                {
                    User user = new User
                    {
                        UserName = x.UserName,
                        Password = x.Password,
                        AtSignUp = DateTime.Now,
                        IsAdmin = true,
                    };
                    _context.Add(user);
                    _context.SaveChanges();

                }else
                {
                    ViewBag.Errors = "Kullanıcı adı alınmış.";
                    return View();
                }
            }else
            {
                ViewBag.Errors = "Bir hata oluştu";
                return View();
            }
            return View();
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
                        AtSignUp = DateTime.Now,
                        IsAdmin = false
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
                        new Claim(ClaimTypes.Role,user.IsAdmin==true?"admin":"user"),
                        new Claim(ClaimTypes.Name,user.UserName),
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