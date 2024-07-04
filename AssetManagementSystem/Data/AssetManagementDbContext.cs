using AssetManagementSystem.Models;
using AssetManagementSystem.Models.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace AssetManagementSystem.Context
{
    public class AssetManagementDbContext : DbContext
    {
        public AssetManagementDbContext(DbContextOptions<AssetManagementDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Asset> Asset { get; set; }
        public DbSet<Contract> Contract { get; set; }
        public DbSet<OrderedAssetType> OrderedAssetType { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<SellingContract> SellingContract { get; set; }
        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<DisposalAssets> DisposalAssets { get; set; }
        public DbSet<AssetType> AssetType { get; set; }
    }
}
