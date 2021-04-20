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
        //Profile user;
        public AuthModel(IndexContext context)
        {
            _context = context;
        }
        public async Task<Profile> Auth(string login,string password)
        {
            return await _context.Profiles.FirstOrDefaultAsync(x => x.login == login && x.password == password);
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
        public async Task Delete(string login)
        {
            /*Удаление пользователя по логину */
            var profileToDelete = await _context.Profiles.FirstOrDefaultAsync(x => x.login == login);

            _context.Profiles.Remove(profileToDelete);
            await _context.SaveChangesAsync();
        }
        public async Task<Profile> Update(string login, string password)
        {
            var profileToUpdate = await _context.Profiles.FirstOrDefaultAsync(x => x.login == login);

            //Proverka na null

            profileToUpdate.password = password;

            await _context.SaveChangesAsync();

            return profileToUpdate;
        }

        public async Task<Profile> Read(string login)
        {
            var profileToRead = await _context.Profiles.FirstOrDefaultAsync(x => x.login == login);
            await _context.SaveChangesAsync();
            return profileToRead;
        }
    }
}
