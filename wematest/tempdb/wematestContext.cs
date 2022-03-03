//using System;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;
//using wematest.Data;

//#nullable disable

//namespace wematest.tempdb
//{
//    public partial class wematestContext : DbContext
//    {
//        public wematestContext()
//        {
//        }

//        public wematestContext(DbContextOptions<wematestContext> options)
//            : base(options)
//        {
//        }

//        public virtual DbSet<Otp> Otps { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=wematest; User ID=sa; password=Password1;");
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

//            modelBuilder.Entity<Otp>(entity =>
//            {
//                entity.ToTable("otps");

//                entity.Property(e => e.OtpId)
//                    .ValueGeneratedNever()
//                    .HasColumnName("otp_id");

//                entity.Property(e => e.OptCreated)
//                    .HasColumnType("datetime")
//                    .HasColumnName("opt_created");

//                entity.Property(e => e.OptStatus).HasColumnName("opt_status");

//                entity.Property(e => e.OptValue)
//                    .HasMaxLength(10)
//                    .IsUnicode(false)
//                    .HasColumnName("opt_value");

//                entity.Property(e => e.OtpCustomer).HasColumnName("otp_customer");
//            });

//            OnModelCreatingPartial(modelBuilder);
//        }

//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//    }
//}
