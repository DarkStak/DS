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

namespace WebSite.Controllers
{
    public class AuthController : Controller
    {
        private IProfileID _manager;

        public AuthController(IProfileID manager)
        {
            _manager = manager;
        }

        [HttpPost]
        public ActionResult log(string login, string password)
        {
            if (_manager.Login(login, password))
            {

            }
            return View();
        }

        [HttpPost]
        public ActionResult reg(string nulled)
        {

            /*if (_manager.Register(Request.Form["login"], Request.Form["password"], Request.Form["confirm"]) == "Регистрация прошла успешно!")
            {
                
            }*/
            //return View();
            ViewBag.Head = "Привет мир!";
            return View();
        }

        public IActionResult log()
        {

            return View();
        }

        public IActionResult reg()
        {

            return View();
        }

    }
}
