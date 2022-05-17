using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebAdmin.Models
{
    public partial class WebBanHangDB : DbContext
    {
        public WebBanHangDB()
            : base("name=WebBanHangDB")
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Categorie> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Custormer> Custormers { get; set; }
        public virtual DbSet<Employer> Employers { get; set; }
        public virtual DbSet<Img> Imgs { get; set; }
        public virtual DbSet<Option_Product> Option_Product { get; set; }
        public virtual DbSet<Permisstion> Permisstions { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Role_Permisstion> Role_Permisstion { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }
        public virtual DbSet<VoucherDetail> VoucherDetails { get; set; }
        public virtual DbSet<VoucherOrder> VoucherOrders { get; set; }
        public virtual DbSet<VoucherOrderDetail> VoucherOrderDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .Property(e => e.intomoney)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Categorie>()
                .Property(e => e.image)
                .IsFixedLength();

            modelBuilder.Entity<Categorie>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Categorie)
                .HasForeignKey(e => e.category_id);

            modelBuilder.Entity<Comment>()
                .HasMany(e => e.Comment11)
                .WithOptional(e => e.Comment2)
                .HasForeignKey(e => e.IdReply);

            modelBuilder.Entity<Custormer>()
                .Property(e => e.telephone)
                .IsFixedLength();

            modelBuilder.Entity<Employer>()
                .Property(e => e.telephone)
                .IsFixedLength();

            modelBuilder.Entity<Img>()
                .Property(e => e.Url)
                .IsFixedLength();

            modelBuilder.Entity<Permisstion>()
                .HasMany(e => e.Role_Permisstion)
                .WithOptional(e => e.Permisstion)
                .HasForeignKey(e => e.idPermisstion);

            modelBuilder.Entity<Permisstion>()
                .HasMany(e => e.Role_Permisstion1)
                .WithOptional(e => e.Permisstion1)
                .HasForeignKey(e => e.idPermisstion);

            modelBuilder.Entity<Product>()
                .Property(e => e.discount)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.price_before_discount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.price_min_before_discount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Carts)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.product_id);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Comments)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.idProduct);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Imgs)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.IdProduct);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Option_Product)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.IdProduct);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.VoucherDetails)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.IdProduct);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.VoucherOrderDetails)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.product_id);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Role_Permisstion)
                .WithOptional(e => e.Role)
                .HasForeignKey(e => e.idRole);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Role_Permisstion1)
                .WithOptional(e => e.Role1)
                .HasForeignKey(e => e.idRole);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Role)
                .HasForeignKey(e => e.idRole);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users1)
                .WithOptional(e => e.Role1)
                .HasForeignKey(e => e.idRole);

            modelBuilder.Entity<Voucher>()
                .Property(e => e.voucher_code)
                .IsFixedLength();

            modelBuilder.Entity<Voucher>()
                .Property(e => e.discount_value)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Voucher>()
                .HasMany(e => e.VoucherDetails)
                .WithOptional(e => e.Voucher)
                .HasForeignKey(e => e.IdVoucher);

            modelBuilder.Entity<VoucherOrder>()
                .Property(e => e.telephone)
                .IsFixedLength();

            modelBuilder.Entity<VoucherOrder>()
                .Property(e => e.grossAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<VoucherOrder>()
                .Property(e => e.discountAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<VoucherOrder>()
                .Property(e => e.shiper)
                .HasPrecision(18, 0);

            modelBuilder.Entity<VoucherOrder>()
                .HasMany(e => e.VoucherOrderDetails)
                .WithRequired(e => e.VoucherOrder)
                .HasForeignKey(e => e.voucherId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VoucherOrderDetail>()
                .Property(e => e.grossAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<VoucherOrderDetail>()
                .Property(e => e.discountAmount)
                .HasPrecision(18, 0);
        }
    }
}
