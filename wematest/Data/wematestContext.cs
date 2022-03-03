using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace wematest.Data
{
    public partial class wematestContext : DbContext
    {
        public wematestContext()
        {
        }

        public wematestContext(DbContextOptions<wematestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Lga> Lgas { get; set; }
        public virtual DbSet<Stateofresidence> Stateofresidences { get; set; }
        public virtual DbSet<Otp> Otps { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=wematest; User ID=sa; password=Password1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustId);

                entity.ToTable("customers");

                entity.Property(e => e.CustId).HasColumnName("cust_id");

                entity.Property(e => e.CustEmail)
                    .HasMaxLength(150)
                    .HasColumnName("cust_email")
                    .IsFixedLength(true);

                entity.Property(e => e.CustLga).HasColumnName("cust_lga");

                entity.Property(e => e.CustPhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cust_phone_number");

                entity.Property(e => e.CustSor).HasColumnName("cust_sor");

                entity.HasOne(d => d.CustSorNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CustSor)
                    .HasConstraintName("FK_customers_stateofresidences");

                entity.Property(e => e.CustRegStatus)
                   .HasMaxLength(50)
                   .IsUnicode(false)
                   .HasColumnName("cust_reg_status");
            });

            modelBuilder.Entity<Lga>(entity =>
            {
                entity.ToTable("lgas");

                entity.Property(e => e.LgaId)
                    
                    .HasColumnName("lga_id");

                entity.Property(e => e.LgaName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lga_name");

                entity.Property(e => e.LgaState).HasColumnName("lga_state");

                entity.HasOne(d => d.LgaStateNavigation)
                    .WithMany(p => p.Lgas)
                    .HasForeignKey(d => d.LgaState)
                    .HasConstraintName("FK_lgas_lgas");
            });

            modelBuilder.Entity<Stateofresidence>(entity =>
            {
                entity.HasKey(e => e.StaId);

                entity.ToTable("stateofresidences");

                entity.Property(e => e.StaId).HasColumnName("sta_id");

                entity.Property(e => e.StaName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("sta_name");
            });


            modelBuilder.Entity<Otp>(entity =>
            {
                entity.ToTable("otps");

                entity.Property(e => e.OtpId).HasColumnName("otp_id");

                entity.Property(e => e.OptCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("opt_created");

                entity.Property(e => e.OptStatus).HasColumnName("opt_status");

                entity.Property(e => e.OptValue)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("opt_value");

                entity.Property(e => e.OtpCustomer).HasColumnName("otp_customer");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
