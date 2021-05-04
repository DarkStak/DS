using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Storage.Entity;
using WebSite.Storage;
using Microsoft.EntityFrameworkCore;

namespace WebSite.Models
{
    public class AuthModel: IAuthModel
    {

        private IndexContext _context;
        //private AccountContext _accounttext;
        //Profile user;
        public AuthModel(IndexContext context)
        {
            _context = context;
        }
        public async Task<Profile> Auth(string login,string password)
        {
            Profile User = await _context.Profiles.FirstOrDefaultAsync(x => x.login == login);
            if (User != null)
            {
                bool Result = BCrypt.Net.BCrypt.Verify(password, User.password);
                if (Result)
                    return User;
                else
                    return null;
            }
            else
                return null;
        }
        public async Task<Profile> Create(string login, string password)
        {
            /*Создание пользователя с логином и паролем, получение количества пользователей и запись его в id+1*/

            Profile newProfile = new Profile();

            newProfile.login = login;
            newProfile.password = password;

            _context.Profiles.Add(newProfile);
            await _context.SaveChangesAsync();

            return newProfile;
        }

        public async Task<Account> CreateAccount(string login, string password)
        {
            Account newAccount = new Account();

            newAccount.login = login;
            newAccount.password = password;
            newAccount.Avatar = "/Stock/zeroUser.jpg";
            newAccount.vkPurchases = "";
            newAccount.gamePurchases = "";
            newAccount.scanPurchases = "";
            newAccount.coinsPurchases = "";

            _context.Accounts.Add(newAccount);
            await _context.SaveChangesAsync();

            return newAccount;
        }
        public async Task<Profile> Delete(string login)
        {
            /*Удаление пользователя по логину */
            var profileToDelete = await _context.Profiles.FirstOrDefaultAsync(x => x.login == login);

            _context.Profiles.Remove(profileToDelete);
            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<Account> DeleteAccount(string login)
        {
            var accountToDelete = await _context.Accounts.FirstOrDefaultAsync(x => x.login == login);

            _context.Accounts.Remove(accountToDelete);
            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<Profile> Update(Profile User)
        {
            //var profileToUpdate = await _context.Profiles.FirstOrDefaultAsync(x => x.login == login);

            //User.password = BCrypt.Net.BCrypt.HashPassword(password);
            _context.Profiles.Attach(User);
            await _context.SaveChangesAsync();
            return User;
        }
        public async Task<Account> UpdateAccount(Account User)
        {
            /*User.password = Updated.password;

            User.Avatar = Updated.Avatar;
            User.vkPurchases = Updated.vkPurchases;
            User.gamePurchases = Updated.gamePurchases;
            User.scanPurchases = Updated.scanPurchases;
            User.coinsPurchases = Updated.coinsPurchases;*/
            _context.Accounts.Attach(User);

            await _context.SaveChangesAsync();
            return User;
        }

        public async Task<Profile> Read(string login)
        {
            var profileToRead = await _context.Profiles.FirstOrDefaultAsync(x => x.login == login);
            await _context.SaveChangesAsync();
            return profileToRead;
        }
        public async Task<Account> ReadAccount(string login)
        {
            var accountToRead = await _context.Accounts.FirstOrDefaultAsync(x => x.login == login);
            await _context.SaveChangesAsync();
            return accountToRead;
        }
    }
}
