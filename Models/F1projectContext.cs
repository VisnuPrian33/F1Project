using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace F1Project.Models;

public partial class F1projectContext : DbContext
{
    public F1projectContext()
    {
    }

    public F1projectContext(DbContextOptions<F1projectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<F1driver> F1drivers { get; set; }

    public virtual DbSet<F1team> F1teams { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WorldChampion> WorldChampions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(" data source=DESKTOP-Q4LIC46;database=F1Project;integrated security=true;trustservercertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<F1driver>(entity =>
        {
            entity.HasKey(e => e.DriverNumber).HasName("PK__F1Driver__F81F68F87E208265");

            entity.ToTable("F1Driver");

            entity.Property(e => e.DriverNumber).ValueGeneratedNever();
            entity.Property(e => e.DriverName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Nationality)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.TeamId).HasColumnName("TeamID");
            entity.Property(e => e.TeamName)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Team).WithMany(p => p.F1drivers)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__F1Driver__TeamNa__398D8EEE");
        });

        modelBuilder.Entity<F1team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("PK__F1Team__123AE7B90CF970CB");

            entity.ToTable("F1Team");

            entity.Property(e => e.TeamId)
                .ValueGeneratedNever()
                .HasColumnName("TeamID");
            entity.Property(e => e.BaseCountry)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TeamName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TeamPrincipal)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C77395653");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.Password)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.UserNavigation).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__Role__46E78A0C");
        });

        modelBuilder.Entity<WorldChampion>(entity =>
        {
            entity.HasKey(e => e.ChampionId).HasName("PK__WorldCha__2C14F2C31ED3F9BA");

            entity.ToTable("WorldChampion");

            entity.Property(e => e.ChampionId)
                .ValueGeneratedNever()
                .HasColumnName("ChampionID");
            entity.Property(e => e.DriverName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TeamName)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.DriverNumberNavigation).WithMany(p => p.WorldChampions)
                .HasForeignKey(d => d.DriverNumber)
                .HasConstraintName("FK__WorldCham__Point__440B1D61");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
