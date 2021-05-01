using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Managers;
using WebSite.Storage.Entity;

namespace WebSite.Controllers
{
    public class ProfileController : Controller
    {
        private IProfileID _manager;

        public ProfileController(IProfileID manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Index(Profile User)
        {
            ViewBag.answer = User.login;
            if (Request.HasFormContentType == true)
            {
                //ViewBag.answer = _manager.ChangePassword(profile, Request.Form["password"], Request.Form["newpassword"], Request.Form["newconfirm"]);
                
            }
            return View(User);
        }

        [HttpGet]
        [HttpPost]
        public IActionResult purchases()
        {

            return View();
        }

    }
}
