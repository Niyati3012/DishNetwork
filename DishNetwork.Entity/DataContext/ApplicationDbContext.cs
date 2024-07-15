using System;
using System.Collections.Generic;
using DishNetwork.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace DishNetwork.Entity.DataContext;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<FileLog> FileLogs { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Reseller> Resellers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleMenu> RoleMenus { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserWiseMenu> UserWiseMenus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("User ID = postgres;Password=1936;Server=localhost;Port=5432;Database=DishNetwork;Integrated Security=true;Pooling=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("Admin_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.AspNetUser).WithMany(p => p.AdminAspNetUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Admin_AspNetUserId_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.AdminCreatedByNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Admin_CreatedBy_fkey");
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasKey(e => e.AspNetRoleId).HasName("AspNetRoles_pkey");
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasKey(e => e.AspNetUserId).HasName("AspNetUser_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<AspNetUserRole>(entity =>
        {
            entity.HasKey(e => e.AspNetUserRolesId).HasName("AspNetUserRoles_pkey");

            entity.HasOne(d => d.AspNetRole).WithMany(p => p.AspNetUserRoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AspNetUserRoles_AspNetRoleId_fkey");

            entity.HasOne(d => d.AspNetUser).WithMany(p => p.AspNetUserRoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AspNetUserRoles_AspNetUserId_fkey");
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.DeviceId).HasName("Device_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Devices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Device_CreatedBy_fkey");

            entity.HasOne(d => d.ReSeller).WithMany(p => p.Devices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Device_ReSellerId_fkey");
        });

        modelBuilder.Entity<FileLog>(entity =>
        {
            entity.HasKey(e => e.FileLogId).HasName("FileLog_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Device).WithMany(p => p.FileLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FileLog_DeviceId_fkey");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("Log_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Logs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Log_CreatedBy_fkey");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("Menu_pkey");
        });

        modelBuilder.Entity<Reseller>(entity =>
        {
            entity.HasKey(e => e.ResellerId).HasName("Reseller_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.AspNetUser).WithMany(p => p.ResellerAspNetUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Reseller_AspNetUserId_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ResellerCreatedByNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Reseller_CreatedBy_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("Role_pkey");
        });

        modelBuilder.Entity<RoleMenu>(entity =>
        {
            entity.HasKey(e => e.RoleMenuId).HasName("RoleMenu_pkey");

            entity.HasOne(d => d.Menu).WithMany(p => p.RoleMenus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RoleMenu_MenuId_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleMenus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RoleMenu_RoleId_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("User_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.AspNetUser).WithMany(p => p.UserAspNetUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_AspNetUserId_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.UserCreatedByNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_CreatedBy_fkey");

            entity.HasOne(d => d.ReSeller).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_ReSellerId_fkey");
        });

        modelBuilder.Entity<UserWiseMenu>(entity =>
        {
            entity.HasKey(e => e.UserWiseMenuId).HasName("UserWiseMenu_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.UserWiseMenus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserWiseMenu_CreatedBy_fkey");

            entity.HasOne(d => d.Menu).WithMany(p => p.UserWiseMenus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserWiseMenu_MenuId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserWiseMenus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserWiseMenu_UserId_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
