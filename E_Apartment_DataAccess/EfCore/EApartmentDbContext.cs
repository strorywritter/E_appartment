using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace E_Apartment_DataAccess.EfCore;

public partial class EApartmentDbContext : DbContext
{
    public EApartmentDbContext()
    {
    }

    public EApartmentDbContext(DbContextOptions<EApartmentDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Apartment> Apartments { get; set; }

    public virtual DbSet<ApartmentStatus> ApartmentStatuses { get; set; }

    public virtual DbSet<ApartmentType> ApartmentTypes { get; set; }

    public virtual DbSet<Building> Buildings { get; set; }

    public virtual DbSet<LeaseDetail> LeaseDetails { get; set; }

    public virtual DbSet<LeaseExtension> LeaseExtensions { get; set; }

    public virtual DbSet<LeaseStatus> LeaseStatuses { get; set; }

    public virtual DbSet<OccupierDetail> OccupierDetails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersType> UsersTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAKSHMADU\\SQLEXPRESS; Initial Catalog=e_apartment;User ID=SQL-ADMIN;Password=admin@123;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Apartment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("apartment_pk");

            entity.ToTable("apartment");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.FlowNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("flow_no");
            entity.Property(e => e.IsDelete).HasColumnName("is_delete");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Status).WithMany(p => p.Apartments)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("apartment_fk_2");

            entity.HasOne(d => d.Type).WithMany(p => p.Apartments)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("apartment_fk_3");
        });

        modelBuilder.Entity<ApartmentStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("apartment_status_pk");

            entity.ToTable("apartment_status");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ApartmentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("apartment_type_pk");

            entity.ToTable("apartment_types");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Building>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("building_pk");

            entity.ToTable("building");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.IsDelete).HasColumnName("is_delete");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<LeaseDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("lease_details_pk");

            entity.ToTable("lease_details");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.ApartmentId).HasColumnName("apartment_id");
            entity.Property(e => e.FromDate)
                .HasColumnType("date")
                .HasColumnName("from_date");
            entity.Property(e => e.IsDelete).HasColumnName("is_delete");
            entity.Property(e => e.LeaseStatusId).HasColumnName("lease_status_id");
            entity.Property(e => e.MonthlyFee)
                .HasColumnType("numeric(12, 2)")
                .HasColumnName("monthly_fee");
            entity.Property(e => e.OccupierId).HasColumnName("occupier_id");
            entity.Property(e => e.ToDate)
                .HasColumnType("date")
                .HasColumnName("to_date");

            entity.HasOne(d => d.Apartment).WithMany(p => p.LeaseDetails)
                .HasForeignKey(d => d.ApartmentId)
                .HasConstraintName("lease_details_fk");

            entity.HasOne(d => d.LeaseStatus).WithMany(p => p.LeaseDetails)
                .HasForeignKey(d => d.LeaseStatusId)
                .HasConstraintName("lease_details_fk2");

            entity.HasOne(d => d.Occupier).WithMany(p => p.LeaseDetails)
                .HasForeignKey(d => d.OccupierId)
                .HasConstraintName("lease_details_fk1");
        });

        modelBuilder.Entity<LeaseExtension>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("lease_extension_pk");

            entity.ToTable("lease_extension");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.FromDate)
                .HasColumnType("date")
                .HasColumnName("from_date");
            entity.Property(e => e.IsDelete).HasColumnName("is_delete");
            entity.Property(e => e.LeaseDetailsId).HasColumnName("lease_details_id");
            entity.Property(e => e.LeaseStatusId).HasColumnName("lease_status_id");
            entity.Property(e => e.ToDate)
                .HasColumnType("date")
                .HasColumnName("to_date");

            entity.HasOne(d => d.LeaseDetails).WithMany(p => p.LeaseExtensions)
                .HasForeignKey(d => d.LeaseDetailsId)
                .HasConstraintName("lease_extension_fk");

            entity.HasOne(d => d.LeaseStatus).WithMany(p => p.LeaseExtensions)
                .HasForeignKey(d => d.LeaseStatusId)
                .HasConstraintName("lease_extension_fk2");
        });

        modelBuilder.Entity<LeaseStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("lease_statuses_pk");

            entity.ToTable("lease_statuses");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<OccupierDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("occupier_details_pk");

            entity.ToTable("occupier_detail");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.AlternateAddress)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("alternate_address");
            entity.Property(e => e.ApartmentId).HasColumnName("apartment_id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.ContactNo)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("contact_no");
            entity.Property(e => e.IsDelete).HasColumnName("is_delete");
            entity.Property(e => e.IsIncludeServant).HasColumnName("is_include_servant");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NicOrPassportNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nic_or_passport_no");

            entity.HasOne(d => d.Apartment).WithMany(p => p.OccupierDetails)
                .HasForeignKey(d => d.ApartmentId)
                .HasConstraintName("occupier_detail_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pk");

            entity.ToTable("users");

            entity.HasIndex(e => e.Username, "user_unique_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.IsDelete).HasColumnName("is_delete");
            entity.Property(e => e.Password)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.Type).WithMany(p => p.Users)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("users_fk");
        });

        modelBuilder.Entity<UsersType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_type_pk");

            entity.ToTable("users_type");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
