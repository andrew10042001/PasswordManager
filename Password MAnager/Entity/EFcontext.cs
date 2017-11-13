using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password_MAnager.Entity
{
    public class EFcontext : DbContext
    {
        public EFcontext() : base("PasswordDb")
        {

        }
        public DbSet<User> Users { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<ExtraField> ExtraFields { get; set; }
    }
}
