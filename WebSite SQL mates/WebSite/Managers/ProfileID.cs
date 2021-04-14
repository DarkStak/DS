using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Storage;
using WebSite.Storage.Entity;

namespace WebSite.Managers
{
    public class ProfileID: IProfileID
    {
        private IndexContext _context;

        public ProfileID(IndexContext context)
        {
            _context = context;
        }
        public void Add(Profile profile)
        {
            _context.Profiles.Add(profile);
        }

        public ICollection<Profile> GetAll()
        {
            return _context.Profiles.ToList();
        }
        public void Delete(string login)
        {

        }
    }
}
