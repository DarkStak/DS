using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebSite.Storage.Entity
{
    public class Account: Profile
    {
        public string Avatar;
        //public List<string> Purchases;
    }
}
