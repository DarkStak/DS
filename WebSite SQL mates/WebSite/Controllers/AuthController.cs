using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Managers;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using WebSite.Storage.Entity;
using WebSite.Controllers;

namespace WebSite.Controllers
{
    public class Success
    {
        public string success { get; set; }
    }
    public class AuthController : Controller
    {
        private IProfileID _manager;

        public AuthController(IProfileID manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [HttpPost]
        public IActionResult log()
        {
            Profile profileLog = new Profile();
            if (Request.HasFormContentType == true)
            {
                ViewBag.answer = _manager.Login(Request.Form["login"], Request.Form["password"]);
                profileLog.login = Request.Form["login"];
                profileLog.password = Request.Form["password"];
                if (ViewBag.answer == "Авторизация прошла успешно!")
                {
                    ViewBag.path = "/Profile/Index";
                    return Redirect("/Profile/Index");
                }
                else
                    ViewBag.path = "/Auth/log"; 
            }
            else
            {
                profileLog.login = "";
                profileLog.password = "";
            }
            return View(profileLog);
        }

        [HttpGet]
        [HttpPost]
        public IActionResult reg()
        {
            ProfileReg profileReg = new ProfileReg();
            if (Request.HasFormContentType==true)
            {
                ViewBag.answer = _manager.Register(Request.Form["login"], Request.Form["password"], Request.Form["confirm"]);
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
