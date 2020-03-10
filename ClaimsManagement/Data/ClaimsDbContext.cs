using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ClaimsDbContext : IdentityDbContext<User>
    {
        public ClaimsDbContext(DbContextOptions<ClaimsDbContext> options) : base(options)
        {
        }

        public DbSet<Claim> Claims { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ClaimConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
