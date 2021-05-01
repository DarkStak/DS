﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebSite.Storage.Entity;


namespace WebSite.Storage
{
    public class IndexContext : DbContext
    {
        public IndexContext(DbContextOptions<IndexContext> options) : base(options)
        {

        }

        public DbSet<Profile> Profiles { get; set; } // Получается вся бада данных содержиться в Profiles?
        public DbSet<Account> Accounts { get; set; }
    }
}
