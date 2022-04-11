using Microsoft.EntityFrameworkCore;

namespace Fora.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<InterestModel> Interests { get; set; }
        public DbSet<UserInterestModel> UserInterests { get; set; }
        public DbSet<ThreadModel> Threads { get; set; }
        public DbSet<MessageModel> Messages { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<UserRoleModel> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many to many (users can have many interests that in turns have many users)
            modelBuilder.Entity<UserInterestModel>()
                .HasKey(ui => new { ui.UserId, ui.InterestId });
            modelBuilder.Entity<UserInterestModel>()
                .HasOne(ui => ui.User)
                .WithMany(u => u.UserInterests)
                .HasForeignKey(ui => ui.UserId);
            modelBuilder.Entity<UserInterestModel>()
                .HasOne(ui => ui.Interest)
                .WithMany(i => i.UserInterests)
                .HasForeignKey(ui => ui.InterestId);

            modelBuilder.Entity<UserRoleModel>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<UserRoleModel>()
                .HasOne(ur => ur.User)
                .WithOne(u => u.UserRole) // One-to-one
                .HasForeignKey<UserRoleModel>(ur => ur.UserId);
            modelBuilder.Entity<UserRoleModel>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles) // Many-to-one
                .HasForeignKey(r => r.RoleId);

            // Restrict deletion of interest on user delete (set user to null instead)
            modelBuilder.Entity<InterestModel>()
                .HasOne(i => i.User)
                .WithMany(u => u.Interests)
                .HasForeignKey(i => i.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
            //Seeds database with different interests
            #region InterestSeed

            modelBuilder.Entity<InterestModel>().HasData(
                new InterestModel { Id = 1, Name = "Animals", Threads = new List<ThreadModel>() },
                new InterestModel { Id = 2, Name = "Gaming", Threads = new List<ThreadModel>() },
                new InterestModel { Id = 3, Name = "Philosophy", Threads = new List<ThreadModel>() });
            #endregion

            #region RoleSeed
            modelBuilder.Entity<RoleModel>().HasData(
                new RoleModel { Id = 1, Role = RoleEnum.USER },
                new RoleModel { Id = 2, Role = RoleEnum.ADMIN });
            #endregion



            #region CreateAdmin
            modelBuilder.Entity<UserModel>(u =>
            {
                u.HasData(
                    new UserModel()
                    {
                        Id = 1,
                        Username = "admin",
                        Banned = false,
                        Deleted = false,
                        Interests = new List<InterestModel>(),
                        UserInterests = new List<UserInterestModel>(),
                        Threads = new List<ThreadModel>(),
                        Messages = new List<MessageModel>(),
                    });
                //u.OwnsOne(ur => ur.UserRole)
                //.HasData(new UserRoleModel()
                //{
                //    UserId = 1,
                //    RoleId = 2
                //});
            });
            #endregion

            modelBuilder.Entity<UserRoleModel>().HasData(
    new UserRoleModel { UserId = 1, RoleId = 2 });

            // Restrict deletion of thread on user delete (set user to null instead)
            modelBuilder.Entity<ThreadModel>()
                .HasOne(i => i.User)
                .WithMany(u => u.Threads)
                .HasForeignKey(i => i.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);


            // Restrict deletion of thread on message delete (set user to null instead)
            modelBuilder.Entity<MessageModel>()
                .HasOne(i => i.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(i => i.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
