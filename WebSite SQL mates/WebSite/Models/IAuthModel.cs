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
        public Task<Account> CreateAccount(string login, string password);
        public Task<Profile> Delete(string login);
        public Task<Account> DeleteAccount(string login);
        public Task<Profile> Update(Profile User);
        public Task<Account> UpdateAccount(Account User);
        public Task<Profile> Read(string login);
        public Task<Account> ReadAccount(string login);
    }
}
