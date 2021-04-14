using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Storage.Entity;

namespace WebSite.Managers
{
    public interface IProfileID
    {
        public void Add(Profile profile);
        public ICollection<Profile> GetAll();
        public void Delete(string login);
    }
}
