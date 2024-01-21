using AssetManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace AssetManagementSystem.Context
{
    public class assetManegementDbContext : DbContext
    {
        public assetManegementDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {

        }
        public DbSet<Asset>Asset {  get; set; }
        public DbSet<User>User {  get; set; }
        public DbSet<Admin>Admin {  get; set; }
        public DbSet<Assignment>Assignment {  get; set; }
        public DbSet<Contract>Contract {  get; set; }
        public DbSet<Log>Log {  get; set; }
        public DbSet<SellingContract>SellingContract {  get; set; }
        public DbSet<Vendor>Vendor {  get; set; }
    }
}
