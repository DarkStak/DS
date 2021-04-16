using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Managers;

namespace WebSite.Controllers
{
    public class AuthController : Controller
    {
        private IProfileID _manager;

        public AuthController(IProfileID manager)
        {
            _manager = manager;
        }

        public IActionResult log()
        {
            var profiles = _manager.GetAll();
            return View(profiles);
        }

        public IActionResult reg()
        {
            var profiles = _manager.GetAll();
            return View(profiles);
        }

    }
}
