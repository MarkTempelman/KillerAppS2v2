using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace View.Controllers
{
    public class RatingController : Controller
    {
        [HttpPost]
        public IActionResult RateMovie()
        {
            return View();
        }
    }
}