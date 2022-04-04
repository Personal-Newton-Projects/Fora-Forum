using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fora.Server.Data
{
    public class AuthDbContext : IdentityDbContext
    {

        public DbSet<IdentityUser> IdentityUsers { get; set; }

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }
        
    }
}
