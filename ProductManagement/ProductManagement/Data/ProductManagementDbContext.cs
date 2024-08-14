using Microsoft.EntityFrameworkCore;

namespace ProductManagement.Data
{
    public class ProductManagementDbContext : DbContext
    {
        public ProductManagementDbContext(DbContextOptions<ProductManagementDbContext> options):base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .ToTable("Product");
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .IsUnicode(true);
            modelBuilder.Entity<Product>()
                .Property(p => p.Description)
                .IsUnicode(true);
            modelBuilder.Entity<Product>()
                .HasOne(p => p.CreatedUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedUserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Product>()
                .HasOne(p => p.LastUpdatedUser)
                .WithMany()
                .HasForeignKey(p => p.LastUpdatedUserId)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<User>()
                .ToTable("User");
            modelBuilder.Entity<User>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .IsRequired()
                .IsUnicode(false);
            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired();

        }
    }
}
