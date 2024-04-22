using AssetManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace AuthService.Data
{
    public class AuthDbContext : IdentityDbContext<User>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
     //   public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var AdminRoleId = "e5a00264-e2a2-4c67-a568-70f48d9aa34f";
            var VendorMRoleId = "30415502-99f1-49cc-95b3-d0081b0e638f";
            var AssetMRoleId = "a68e73fb-f0f6-452b-a861-b9bbad4713aa";
            var NormalUserRoleId = "5bc23f4f-7673-4a10-8b2f-81d11b2b631e";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = AdminRoleId,
                    ConcurrencyStamp = AdminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                },
                new IdentityRole
                {
                    Id = VendorMRoleId,
                    ConcurrencyStamp = VendorMRoleId,
                    Name = "VendorManeger",
                    NormalizedName = "VendorManeger".ToUpper(),
                },
                new IdentityRole
                {
                    Id = AssetMRoleId,
                    ConcurrencyStamp = AssetMRoleId,
                    Name = "AssetManeger",
                    NormalizedName = "AssetManeger".ToUpper(),
                },
                new IdentityRole
                {
                    Id = NormalUserRoleId,
                    ConcurrencyStamp = NormalUserRoleId,
                    Name = "NormalUser",
                    NormalizedName = "NormalUser".ToUpper(),
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

            var adminUserId = "75af95a9-9273-4c9b-86aa-0a80c76f32d6";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@codepulse.com",
                Email = "admin@codepulse.com",
                NormalizedEmail = "admin@codepulse.com".ToUpper(),
                NormalizedUserName = "admin@codepulse.com".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@12345");

            builder.Entity<IdentityUser>().HasData(admin);

            var adminRole = new IdentityUserRole<string>()
            {
                UserId = adminUserId,
                RoleId = AdminRoleId
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRole);
        }
    }
}
