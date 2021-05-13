using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Managers;
using WebSite.Storage.Entity;

namespace WebSite.Controllers
{
    public class ProfileController : Controller
    {
        private IProfileID _manager;
        private IGenerator _generator;

        public ProfileController(IProfileID manager, IGenerator generator)
        {
            _manager = manager;
            _generator = generator;
        }

        [HttpGet]
        [HttpPost]
        [RequestSizeLimit(int.MaxValue)]
        public IActionResult Index(Account User)
        {
            if (Request.HasFormContentType == true)
            {
                var Avatar = HttpContext.Request.Form.Files["fileUpload"];
                var Res = _manager.UpdateAvatar(HttpContext.Session.Get<Account>("user"), Avatar);
                ViewBag.avatar = Res.result;
                User = Res.account;
                HttpContext.Session.Set<Account>("user", Res.account);
            }
            else
            {

            }
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
