using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Enums;
using Logic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
                if (_userLogic.DoesUserNameExist(user.Username))
                {
                    ModelState.AddModelError("Username", "This username is already taken");
                    return View(user);
                }

                if (_userLogic.DoesEmailAddressExist(user.EmailAddress))
                {
                    ModelState.AddModelError("EmailAddress", "This email address is already taken");
                    return View(user);
                }
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
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserViewModel userViewModel)
        {
            try
            {
                UserViewModel newUser = ModelToViewModel.ToUserViewModel(_userLogic.CheckUserValidity(userViewModel.Username, userViewModel.Password));

                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, newUser.Username),
                    new Claim(ClaimTypes.Email, newUser.EmailAddress),
                    new Claim(ClaimTypes.Role, newUser.AccountType.ToString()),
                    new Claim(ClaimTypes.Sid, newUser.Id.ToString()) 
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

        [Authorize]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Movie");
        }

        public IActionResult ManageUsers()
        {
            List<UserViewModel> userViewModels = new List<UserViewModel>();
            foreach (var user in _userLogic.GetAllUsersExceptCurrent(int.Parse(User.Claims
                .FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value)))
            {
                userViewModels.Add(ModelToViewModel.ToUserViewModel(user));
            }

            return View(userViewModels);
        }

        [Authorize(Policy = "admin")]
        public IActionResult DeleteUser(int id)
        {
            _userLogic.DeleteUser(id);
            return RedirectToAction("ManageUsers");
        }

        [Authorize(Policy = "admin")]
        public IActionResult MakeUserAdmin(int id)
        {
            _userLogic.SetUserAccountType(id, AccountType.Admin);
            return RedirectToAction("ManageUsers");
        }

        [Authorize(Policy = "admin")]
        public IActionResult MakeAdminUser(int id)
        {
            _userLogic.SetUserAccountType(id, AccountType.User);
            return RedirectToAction("ManageUsers");
        }
    }
}