using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CookieAuthenticationDemo.Models;
using CookieAuthenticationDemo.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace CookieAuthenticationDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserViewModel userModel)
        {
            string returnUrl = @"/Home/Permission";
            UserModel user = UserModel.Login(userModel.Account, userModel.Password);
            if (user != null)
            {
                this.SetUser(user);
                return Redirect(returnUrl);
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Permission()
        {
            var claims = HttpContext.User.Claims;
            string content = string.Empty;

            foreach (var claim in claims)
            {
                content += claim.Value + " ";
            }

            return Content(content);
        }
    }
}
