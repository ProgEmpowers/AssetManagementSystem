using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace AuthService.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var AdminRoleId = "e5a00264-e2a2-4c67-a568-70f48d9aa34f";
            var AssetMRoleId = "30415502-99f1-49cc-95b3-d0081b0e638f";
            var VendorMRoleId = "6f58a48e-40a0-45a5-bbe3-15dcb68c8f09";
            var EmployeRoleId = "e4b140ec-d7eb-4690-804d-e879f583c751";

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
                    Id = AssetMRoleId,
                    ConcurrencyStamp = AssetMRoleId,
                    Name = "AssetManager",
                    NormalizedName = "AssetManager".ToUpper(),
                },
                new IdentityRole
                {
                    Id = VendorMRoleId,
                    ConcurrencyStamp = VendorMRoleId,
                    Name = "VendorManager",
                    NormalizedName = "VendorManager".ToUpper(),
                },
                new IdentityRole
                {
                    Id = EmployeRoleId,
                    ConcurrencyStamp = EmployeRoleId,
                    Name = "Employe",
                    NormalizedName = "Employe".ToUpper(),
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
