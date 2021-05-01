using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Storage.Entity;

namespace WebSite.Models
{
    public interface IAuthModel
    {
        public Task<Profile> Auth(string login, string password);
        public Task<Profile> Create(string login, string password);
        public Task<Profile> CreateAccount(string login, string password);
        public Task Delete(string login);
        public Task<Profile> Update(Profile User, string password);
        public Task<Profile> Read(string login);
    }
}
