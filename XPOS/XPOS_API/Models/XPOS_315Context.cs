using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace XPOS_API.Models
{
    public partial class XPOS_315Context : DbContext
    {
        public XPOS_315Context()
        {
        }

        public XPOS_315Context(DbContextOptions<XPOS_315Context> options)
            : base(options)
        {
        }

        public virtual DbSet<TblCategory> TblCategories { get; set; } = null!;
        public virtual DbSet<TblCustomer> TblCustomers { get; set; } = null!;
        public virtual DbSet<TblOrderDetail> TblOrderDetails { get; set; } = null!;
        public virtual DbSet<TblOrderHeader> TblOrderHeaders { get; set; } = null!;
        public virtual DbSet<TblProduct> TblProducts { get; set; } = null!;
        public virtual DbSet<TblRole> TblRoles { get; set; } = null!;
        public virtual DbSet<TblUser> TblUsers { get; set; } = null!;
        public virtual DbSet<TblVariant> TblVariants { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=ETG-XALNV00021-;Initial Catalog=XPOS_315;user id=sa;Password=xsis123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblCategory>(entity =>
            {
                entity.ToTable("TblCategory");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.NameCategory)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblCustomer>(entity =>
            {
                entity.ToTable("TblCustomer");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.NameCustomer)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblOrderDetail>(entity =>
            {
                entity.ToTable("TblOrderDetail");

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdateDate).HasColumnType("date");
            });

            modelBuilder.Entity<TblOrderHeader>(entity =>
            {
                entity.ToTable("TblOrderHeader");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CodeTransaction).HasMaxLength(20);

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.UpdateDate).HasColumnType("date");
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.ToTable("TblProduct");

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.NameProduct)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdateDate).HasColumnType("date");
            });

            modelBuilder.Entity<TblRole>(entity =>
            {
                entity.ToTable("TblRole");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.ToTable("TblUser");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblVariant>(entity =>
            {
                entity.ToTable("TblVariant");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameVariant)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
