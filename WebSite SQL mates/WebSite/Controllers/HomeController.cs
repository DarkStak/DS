using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Managers;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
        private IProfileID _manager;

        public HomeController(IProfileID manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
