using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Managers;

namespace WebSite.Controllers
{
    public class ProfileController : Controller
    {
        private IProfileID _manager;

        public ProfileController(IProfileID manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            var profiles = _manager.GetAll();
            return View(profiles);
        }

    }
}
