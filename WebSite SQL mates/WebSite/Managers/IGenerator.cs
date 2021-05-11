using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Managers
{
    public interface IGenerator
    {
        public string emailGenerator();
        public string passwordGenerator();
        public string keyGenerator();
        public string urlGenerator();
        public string CookieGenerator();
    }
}
