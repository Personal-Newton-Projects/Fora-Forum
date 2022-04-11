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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var hasher = new PasswordHasher<IdentityUser>();
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                UserName = "admin",
                PasswordHash = hasher.HashPassword(null, "Password1234!"),
                Email = "admin@admin.com",

            });

            
        }
    }
}
