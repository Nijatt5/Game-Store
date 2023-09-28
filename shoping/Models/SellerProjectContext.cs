using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace shoping.Models
{
    public partial class SellerProjectContext : DbContext
    {
        public SellerProjectContext()
        {
        }

        public SellerProjectContext(DbContextOptions<SellerProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Basket> Baskets { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=NICAT\\SQLEXPRESS;Database=SellerProject;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Basket>(entity =>
            {
                entity.Property(e => e.BasketCount).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.BasketProduct)
                    .WithMany(p => p.Baskets)
                    .HasForeignKey(d => d.BasketProductId)
                    .HasConstraintName("FK__Baskets__BasketP__5070F446");

                entity.HasOne(d => d.BasketUser)
                    .WithMany(p => p.Baskets)
                    .HasForeignKey(d => d.BasketUserId)
                    .HasConstraintName("FK__Baskets__BasketU__52593CB8");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryName).HasMaxLength(30);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductConfirm).HasMaxLength(30);

                entity.Property(e => e.ProductImg).HasMaxLength(100);

                entity.Property(e => e.ProductName).HasMaxLength(30);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UsersId)
                    .HasName("PK__Users__A349B062368F7C8A");

                entity.Property(e => e.UsersMail).HasMaxLength(50);

                entity.Property(e => e.UsersName).HasMaxLength(30);

                entity.Property(e => e.UsersSurname).HasMaxLength(40);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
