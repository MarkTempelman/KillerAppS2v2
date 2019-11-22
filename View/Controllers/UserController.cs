using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using View.Helpers;
using View.ViewModels;

namespace View.Controllers
{
    public class UserController : Controller
    {
        private readonly UserLogic _userLogic;

        public UserController(UserLogic userLogic)
        {
            _userLogic = userLogic;
        }
        public IActionResult Register()
        {
            UserViewModel user = new UserViewModel();
            return View(user);
        }

        [HttpPost]
        public IActionResult Register(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                _userLogic.CreateUser(ViewModelToModel.ToUserModel(user));
                return RedirectToAction("Login", "User");
            }

            return View(user);
        }

        public IActionResult Login()
        {
            UserViewModel user = new UserViewModel();
            return View(user);
        }
        [HttpPost]
        public IActionResult Login(UserViewModel userViewModel)
        {
            try
            {
                UserViewModel newUser = ModelToViewModel.ToUserViewModel(_userLogic.CheckUserValidity(userViewModel.Username, userViewModel.Password));

                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, newUser.Username),
                    new Claim(ClaimTypes.Email, newUser.EmailAddress),
                    new Claim(ClaimTypes.Role, newUser.AccountType.ToString()) 
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Movie");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ModelState.AddModelError("Username", "Password or username is incorrect");
                return View(userViewModel);
            }
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Movie");
        }
    }
}