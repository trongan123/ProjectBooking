using System;
using System.Collections.Generic;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public partial class ASMBOOKINGContext : DbContext
    {
        public ASMBOOKINGContext()
        {
        }

        public ASMBOOKINGContext(DbContextOptions<ASMBOOKINGContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Bill> Bills { get; set; } = null!;
        public virtual DbSet<BookingRoomDetail> BookingRoomDetails { get; set; } = null!;
        public virtual DbSet<BookingSeviceDetail> BookingSeviceDetails { get; set; } = null!;
        public virtual DbSet<BookingTransportDetail> BookingTransportDetails { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<RoomType> RoomTypes { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<Transport> Transports { get; set; } = null!;
        public virtual DbSet<TypeTransport> TypeTransports { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DBBooking"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Idacc)
                    .HasName("PK__Account__93212858C94F18B9");

                entity.ToTable("Account");

                entity.Property(e => e.Idacc)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("IDACC");

                entity.Property(e => e.FullName).HasMaxLength(80);

                entity.Property(e => e.Mail)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.St).HasColumnName("ST");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.HasKey(e => e.Idbill)
                    .HasName("PK__Bill__23BDC1E616E54136");

                entity.ToTable("Bill");

                entity.Property(e => e.Idbill)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("IDBill");

                entity.Property(e => e.EndDay).HasColumnType("datetime");

                entity.Property(e => e.Idacc)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("IDACC");

                entity.Property(e => e.Idadmin)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("IDAdmin");

                entity.Property(e => e.Phone)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.StartDay).HasColumnType("datetime");

                entity.HasOne(d => d.IdaccNavigation)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.Idacc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bill_Account");
            });

            modelBuilder.Entity<BookingRoomDetail>(entity =>
            {
                entity.HasKey(e => e.IdbookingRoomDetail)
                    .HasName("PK__BookingR__3C61D7749A6AD541");

                entity.ToTable("BookingRoomDetail");

                entity.Property(e => e.IdbookingRoomDetail)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("IDBookingRoomDetail");

                entity.Property(e => e.Idbill)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("IDBill");

                entity.Property(e => e.Idroom)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("IDRoom");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.St).HasColumnName("ST");

                entity.HasOne(d => d.IdbillNavigation)
                    .WithMany(p => p.BookingRoomDetails)
                    .HasForeignKey(d => d.Idbill)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookingRoomDetail_Bill");

                entity.HasOne(d => d.IdroomNavigation)
                    .WithMany(p => p.BookingRoomDetails)
                    .HasForeignKey(d => d.Idroom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookingRoomDetail_Room");
            });

            modelBuilder.Entity<BookingSeviceDetail>(entity =>
            {
                entity.HasKey(e => e.IdbookingSeviceDetail)
                    .HasName("PK__BookingS__4E3A0E2A0ADFB36D");

                entity.ToTable("BookingSeviceDetail");

                entity.Property(e => e.IdbookingSeviceDetail)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("IDBookingSeviceDetail");

                entity.Property(e => e.Idbill)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("IDBill");

                entity.Property(e => e.Idsevice)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("IDSevice");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.St).HasColumnName("ST");

                entity.HasOne(d => d.IdbillNavigation)
                    .WithMany(p => p.BookingSeviceDetails)
                    .HasForeignKey(d => d.Idbill)
                    .HasConstraintName("FK_BookingSeviceDetail_Bill");

                entity.HasOne(d => d.IdseviceNavigation)
                    .WithMany(p => p.BookingSeviceDetails)
                    .HasForeignKey(d => d.Idsevice)
                    .HasConstraintName("FK_BookingSeviceDetail_Service");
            });

            modelBuilder.Entity<BookingTransportDetail>(entity =>
            {
                entity.HasKey(e => e.IdbookingTransportDetail)
                    .HasName("PK__BookingT__9638B7A553575659");

                entity.ToTable("BookingTransportDetail");

                entity.Property(e => e.IdbookingTransportDetail)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("IDBookingTransportDetail");

                entity.Property(e => e.Idbill)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("IDBill");

                entity.Property(e => e.Idtransport)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("IDTransport");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.St).HasColumnName("ST");

                entity.HasOne(d => d.IdbillNavigation)
                    .WithMany(p => p.BookingTransportDetails)
                    .HasForeignKey(d => d.Idbill)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookingTransportDetail_Bill");

                entity.HasOne(d => d.IdtransportNavigation)
                    .WithMany(p => p.BookingTransportDetails)
                    .HasForeignKey(d => d.Idtransport)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookingTransportDetail_Transport");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Idcomment)
                    .HasName("PK__Comment__C71D7B98D0C0CD73");

                entity.ToTable("Comment");

                entity.Property(e => e.Idcomment)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("IDComment");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Idacc)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("IDACC");

                entity.HasOne(d => d.IdaccNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Idacc)
                    .HasConstraintName("FK_Comment_Account");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.Idroom)
                    .HasName("PK__Room__A1BA0EAF4A07B4CB");

                entity.ToTable("Room");

                entity.Property(e => e.Idroom)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("IDRoom");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.IdroomType)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("IDRoomType");

                entity.Property(e => e.Image)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.NumberRoom)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Stroom).HasColumnName("STRoom");

                entity.HasOne(d => d.IdroomTypeNavigation)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.IdroomType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_RoomType");
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.HasKey(e => e.IdroomType)
                    .HasName("PK__RoomType__FCDD8EA67369815F");

                entity.ToTable("RoomType");

                entity.Property(e => e.IdroomType)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("IDRoomType");

                entity.Property(e => e.NameRoomType)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.Idservice)
                    .HasName("PK__Service__1B4EA93EFB0817C0");

                entity.ToTable("Service");

                entity.Property(e => e.Idservice)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("IDservice");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.NameService)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");
            });

            modelBuilder.Entity<Transport>(entity =>
            {
                entity.HasKey(e => e.Idtransport)
                    .HasName("PK__Transpor__73A8C95243EF9119");

                entity.ToTable("Transport");

                entity.Property(e => e.Idtransport)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("IDTransport");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.IdtypeTransport)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("IDTypeTransport");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.IdtypeTransportNavigation)
                    .WithMany(p => p.Transports)
                    .HasForeignKey(d => d.IdtypeTransport)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transport_TypeTransport");
            });

            modelBuilder.Entity<TypeTransport>(entity =>
            {
                entity.HasKey(e => e.IdtypeTransport)
                    .HasName("PK__TypeTran__188C66E42B698927");

                entity.ToTable("TypeTransport");

                entity.Property(e => e.IdtypeTransport)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("IDTypeTransport");

                entity.Property(e => e.NameTranspors)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
