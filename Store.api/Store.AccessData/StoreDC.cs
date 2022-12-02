using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Store.AccessData.Entities;

#nullable disable

namespace Store.AccessData
{
    public partial class StoreDC : DbContext
    {
        public StoreDC()
        {
        }

        public StoreDC(DbContextOptions<StoreDC> options)
            : base(options)
        {
        }

        public virtual DbSet<BussinesAccount> BussinesAccounts { get; set; }
        public virtual DbSet<BussinesAccountHistory> BussinesAccountHistories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public virtual DbSet<SalesOrder> SalesOrders { get; set; }
        public virtual DbSet<SalesOrderItem> SalesOrderItems { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=galastore;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BussinesAccount>(entity =>
            {
                entity.ToTable("BussinesAccount");

                entity.Property(e => e.AccountName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<BussinesAccountHistory>(entity =>
            {
                entity.ToTable("BussinesAccountHistory");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.DocRefType).HasMaxLength(3);

                entity.Property(e => e.HistoryType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.HasKey(e => e.DocNum)
                    .HasName("PK__Purchase__420AEAF125967020");

                entity.ToTable("PurchaseOrder");

                entity.Property(e => e.CanceledBy).HasMaxLength(200);

                entity.Property(e => e.CandeledDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.DocDate).HasColumnType("date");

                entity.Property(e => e.DocStatus)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.DocTotal).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Supplier)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.SupplierNavigation)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.Supplier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PurchaseO__Suppl__36B12243");
            });

            modelBuilder.Entity<PurchaseOrderItem>(entity =>
            {
                entity.ToTable("PurchaseOrderItem");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.FactorRevenue).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.IsSold).HasColumnName("isSold");

                entity.Property(e => e.ItemBy)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.ItemCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PriceByGrs).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.PublicPrice).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Reference1).HasMaxLength(50);

                entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.WeightItem).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.DocNumNavigation)
                    .WithMany(p => p.PurchaseOrderItems)
                    .HasForeignKey(d => d.DocNum)
                    .HasConstraintName("FK__PurchaseO__DocNu__37A5467C");
            });

            modelBuilder.Entity<SalesOrder>(entity =>
            {
                entity.HasKey(e => e.DocNum)
                    .HasName("PK__SalesOrd__420AEAF1A7EF1F88");

                entity.ToTable("SalesOrder");

                entity.Property(e => e.CanceledBy)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CandeledDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Discount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.DocDate).HasColumnType("date");

                entity.Property(e => e.DocStatus)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.DocTotal).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.DocTotalFinal).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.MethodPayment)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.TotalDiscount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.SalesOrders)
                    .HasForeignKey(d => d.Customer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SalesOrde__Custo__38996AB5");
            });

            modelBuilder.Entity<SalesOrderItem>(entity =>
            {
                entity.ToTable("SalesOrderItem");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ItemCode).HasMaxLength(50);

                entity.Property(e => e.Reference1).HasMaxLength(50);

                entity.Property(e => e.Reference2).HasMaxLength(50);

                entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.DocNumNavigation)
                    .WithMany(p => p.SalesOrderItems)
                    .HasForeignKey(d => d.DocNum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SalesOrde__DocNu__398D8EEE");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.CardCode)
                    .HasName("PK__Supplier__3D5317061E99FAC2");

                entity.ToTable("Supplier");

                entity.Property(e => e.CardCode).HasMaxLength(20);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.SuplierName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.SupplierStatus)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
