using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AtmAppAsp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            seedData(builder);
        }

        private void seedData(ModelBuilder builder)
        {

            #region Users
            var user1 = new IdentityUser()
            {
                Id = "7f9d962a-59f5-44d0-a73e-297314544d91",
                Email = "user8@test.com",
                NormalizedEmail = "USER8@TEST.COM",
                UserName = "user8@test.com",
                NormalizedUserName = "USER8@TEST.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            var admin1 = new IdentityUser()
            {
                Id = "87de95a5-c410-4c44-9cde-d76d58c20759",
                Email = "Admin@Admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                UserName = "Admin@Admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var passwordHasher = new PasswordHasher<IdentityUser>();
            user1.PasswordHash = passwordHasher.HashPassword(user1, "User8-123");
            admin1.PasswordHash = passwordHasher.HashPassword(admin1, "Admin-123");

            builder.Entity<IdentityUser>().HasData(user1);
            builder.Entity<IdentityUser>().HasData(admin1);

            #endregion

            #region Roles
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "aabc7a43-eed9-49d4-bcd3-aca50ad87bdc", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
                new IdentityRole() { Id = "20c51aac-0a5a-4008-8862-04132e33ac29", Name = "User", ConcurrencyStamp = "2", NormalizedName = "USER" }
                );

            #endregion

            #region Assign roles to users
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "20c51aac-0a5a-4008-8862-04132e33ac29", UserId = "7f9d962a-59f5-44d0-a73e-297314544d91" },
                new IdentityUserRole<string>() { RoleId = "aabc7a43-eed9-49d4-bcd3-aca50ad87bdc", UserId = "87de95a5-c410-4c44-9cde-d76d58c20759" }


                );
            #endregion
        }
    }

}
