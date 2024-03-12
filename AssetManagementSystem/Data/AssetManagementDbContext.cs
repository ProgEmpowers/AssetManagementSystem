using AssetManagementSystem.Models;
using AssetManagementSystem.Models.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace AssetManagementSystem.Context
{
    public class AssetManagementDbContext : DbContext
    {
        public AssetManagementDbContext(DbContextOptions<AssetManagementDbContext> dbContextOptions): base(dbContextOptions)
        {

        }
        public DbSet<Asset>Asset {  get; set; }
        public DbSet<Contract>Contract {  get; set; }
        public DbSet<Log>Log {  get; set; }
        public DbSet<SellingContract>SellingContract {  get; set; }
        public DbSet<Vendor>Vendor {  get; set; }
    }
}
