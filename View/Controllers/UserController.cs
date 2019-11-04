using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using View.ViewModels;

namespace View.Controllers
{
    public class UserController : Controller
    {
        private UserLogic _userLogic = new UserLogic();
        public IActionResult Register()
        {
            UserViewModel user = new UserViewModel();
            return View(user);
        }

        [HttpPost]
        public IActionResult Register(UserViewModel user)
        {
            _userLogic.CreateUser(ToUserModel(user));
            return View();
        }

        private UserModel ToUserModel(UserViewModel userViewModel)
        {
            return new UserModel(userViewModel.Username, userViewModel.EmailAddress, userViewModel.Password);
        }
    }
}