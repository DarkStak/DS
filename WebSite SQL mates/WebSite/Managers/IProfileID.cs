using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Storage.Entity;
using Microsoft.AspNetCore.Http;

namespace WebSite.Managers
{
    public interface IProfileID
    {
        public LoginAnswer Login(string login, string password);
        public string Register(string login, string password, string confirm);
        public LoginAnswer ChangePassword(Account User, string password, string newpassword, string newconfirm);
        public bool UploadFile(IFormFile ufile);
        public LoginAnswer UpdateAvatar(Account User, IFormFile Avatar);
    }
}
