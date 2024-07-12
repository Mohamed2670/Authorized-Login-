using Microsoft.EntityFrameworkCore;
using MwTesting.Model;

namespace MwTesting.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> opt) : base(opt)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserPerm> UserPerms { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserPerm>(entity =>
            {
                entity.HasKey(up => new { up.UserId, up.permission });
                entity.HasOne(up => up.User)
                      .WithMany(u => u.UserPerms)
                      .HasForeignKey(up => up.UserId);
            });
        }
    }
}