using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CookieAuthenticationDemo.Models
{
    public class UserModel
    {
        public UserModel()
        {

        }

        public static UserModel Login(string account, string password)
        {
            UserModel user = null;

            if (account == "admin" && password == "yourpwd")
            {
                user = new UserModel();
                user.Email = "admin@domain.com";
                user.UserName = "admin";
                user.UserId = "admin";
            }

            return user;
        }

        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
    }

    public static class ControllerExtensionClass
    {
        public async static void SetUser(this Controller controller, UserModel user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("email", user.Email));
            claims.Add(new Claim(nameof(user.UserId), user.UserId));
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await controller.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = true
                });
        }
    }
}
