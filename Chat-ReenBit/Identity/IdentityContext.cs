using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chat_ReenBit.Identity
{
    public class IdentityContext:IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser()
                {
                    UserName = "FirstUser",
                    NormalizedUserName = "FIRSTUSER",
                    PasswordHash = hasher.HashPassword(null, "Test123!")
                },new ApplicationUser()
                {
                    UserName = "SecondUser",
                    NormalizedUserName= "SECONDUSER",
                    PasswordHash = hasher.HashPassword(null, "Test123!")
                },new ApplicationUser()
                {
                    UserName = "ThirdUser",
                    NormalizedUserName = "THIRDUSER",
                    PasswordHash = hasher.HashPassword(null, "Test123!")
                }
            );
        }
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring (optionsBuilder);
        }

    }
}
