using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Storage.Entity;

namespace WebSite.Managers
{
    public interface IProfileID
    {
        public bool Login(string login, string password);
        public string Register(string login, string password, string confirm);
    }
}
