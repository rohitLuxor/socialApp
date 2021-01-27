using Microsoft.EntityFrameworkCore;
using SocialApp.DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialApp.DAL.Context
{
    public class SocialContext : DbContext
    {
        public SocialContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserTable> UserTables { get; set; }
        public DbSet<EntityTable> EntityTable { get; set; }
        public DbSet<DynamicTable> DynamicTable { get; set; }
    }
}
