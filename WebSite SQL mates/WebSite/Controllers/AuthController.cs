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
        public ActionResult reg(string login, string password,string confirm)
        {
            if (_manager.Register(login, password, confirm))
            {

            }
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
