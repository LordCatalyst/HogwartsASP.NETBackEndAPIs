using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HogwartsBackEndAPIs.Models;

public partial class DbhogwartsContext : DbContext
{
    public DbhogwartsContext()
    {
    }

    public DbhogwartsContext(DbContextOptions<DbhogwartsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<House> Houses { get; set; }

    public virtual DbSet<WizardRequest> WizardRequests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<House>(entity =>
        {
            entity.HasKey(e => e.HouseId).HasName("PK__Houses__3235AC0B03EC2A48");

            entity.Property(e => e.HouseName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<WizardRequest>(entity =>
        {
            entity.HasKey(e => e.WizardId).HasName("PK__Wizards__EB46AA851087D1D6");

            entity.ToTable("WizardRequest");

            entity.Property(e => e.WizardLastName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.WizardName)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.House).WithMany(p => p.WizardRequests)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("FK__Wizards__HouseId__1273C1CD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
