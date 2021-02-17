using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UserAuthentication.Models
{
    public class AuthenticationContext : DbContext, IAuthenticationContext
    {
        public AuthenticationContext(DbContextOptions options) : base(options) {
            //this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; }
    }
}
