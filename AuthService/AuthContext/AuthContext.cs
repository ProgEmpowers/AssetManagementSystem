using AuthService.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace AuthService.AuthContext
{
    public class AuthContext: DbContext
    {
        public AuthContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
