using Assignment_Sa.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment_Sa.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Customer> Customers { get; set; }
        public DbSet<Models.Product> Products { get; set; }
        public DbSet<Models.SaleMaster> SaleMasters { get; set; }
        public DbSet<Models.SaleDetail> SaleDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("Users");
                e.HasKey(u => u.UserId);
                e.HasIndex(u => u.Username).IsUnique();
                e.Property(u => u.Username).HasMaxLength(50).IsRequired();
                e.Property(u => u.PasswordHash).IsRequired();
                e.Property(u => u.FullName).HasMaxLength(100);
                e.Property(u => u.Role).HasMaxLength(20);
            });

            modelBuilder.Entity<Customer>(e =>
            {
                e.ToTable("Customers");
                e.HasKey(c => c.CustomerId);
                e.Property(c => c.Name).HasMaxLength(100);
                e.Property(c => c.Email).HasMaxLength(100);
                e.Property(c => c.Phone).HasMaxLength(20);
                e.Property(c => c.Address).HasMaxLength(200);
            });

            modelBuilder.Entity<Product>(e =>
            {
                e.ToTable("Products");
                e.HasKey(p => p.ProductId);
                e.Property(p => p.Name).HasMaxLength(100);
                e.Property(p => p.UnitPrice).HasColumnType("decimal(18,2)");
                e.Property(p => p.Stock).IsRequired();
            });

            modelBuilder.Entity<SaleMaster>(e =>
            {
                e.ToTable("SalesMaster");
                e.HasKey(s => s.SaleId);
                e.Property(s => s.TotalAmount).HasColumnType("decimal(18,2)");
                e.Property(s => s.SaleDate).HasDefaultValueSql("GETDATE()");


              
                e.HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);


                
                e.HasOne(s => s.CreatedByUser)
                .WithMany(u => u.SalesCreated)
                .HasForeignKey(s => s.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
            });



            modelBuilder.Entity<SaleDetail>(e =>
            {
                e.ToTable("SalesDetail");
                e.HasKey(d => d.DetailId);
                e.Property(d => d.UnitPrice).HasColumnType("decimal(18,2)");
                e.Property(d => d.SubTotal).HasColumnType("decimal(18,2)");


                 // Foreign key SaleMaster (cascade delete: when a sale is removed, remove its details)
                e.HasOne(d => d.SaleMaster)
                .WithMany(s => s.SalesDetails)
                .HasForeignKey(d => d.SaleId)
                .OnDelete(DeleteBehavior.Cascade);


                
                e.HasOne(d => d.Product)
                .WithMany(p => p.SalesDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            });

        }


    }
}
