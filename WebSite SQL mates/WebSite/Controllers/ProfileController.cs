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
        public IActionResult Index(Account User)
        {
            return View(User);
        }

        [HttpGet]
        [HttpPost]
        public IActionResult purchases()
        {

            return View();
        }

        [HttpGet]
        [HttpPost]
        public IActionResult purchased(Account User)
        {

            return View(User);
        }

        [HttpGet]
        [HttpPost]
        public IActionResult change(Account User)
        {
            ProfileReg profileReg = new ProfileReg();
            if (Request.HasFormContentType == true)
            {
                ViewBag.answer = _manager.ChangePassword(HttpContext.Session.Get<Account>("user"), Request.Form["login"], Request.Form["password"], Request.Form["confirm"]).result;
                profileReg.login = Request.Form["login"];
                profileReg.password = Request.Form["password"];
                profileReg.confirm = Request.Form["confirm"];
            }
            else
            {
                profileReg.login = "";
                profileReg.password = "";
                profileReg.confirm = "";
            }
            return View(profileReg);
        }
    }
}
