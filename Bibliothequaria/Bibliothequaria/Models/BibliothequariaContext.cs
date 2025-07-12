using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Bibliothequaria.Models;

public partial class BibliothequariaContext : DbContext
{
    public BibliothequariaContext()
    {
    }

    public BibliothequariaContext(DbContextOptions<BibliothequariaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Clan> Clans { get; set; }

    public virtual DbSet<Knjiga> Knjigas { get; set; }

    public virtual DbSet<Radnik> Radniks { get; set; }

    public virtual DbSet<Transakcija> Transakcijas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-OV8R09A\\SQLEXPRESS;Database=BIBLIOTHEQUARIA;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Clan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clan__3214EC279F8B5A27");

            entity.ToTable("Clan");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Ime).HasMaxLength(30);
            entity.Property(e => e.Prezime).HasMaxLength(30);
        });

        modelBuilder.Entity<Knjiga>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Knjiga__3214EC27CF129EC8");

            entity.ToTable("Knjiga");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Autor).HasMaxLength(40);
            entity.Property(e => e.Naslov).HasMaxLength(100);
            entity.Property(e => e.Zanr).HasMaxLength(35);
        });

        modelBuilder.Entity<Radnik>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Radnik__3214EC27134E98F6");

            entity.ToTable("Radnik");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EMail)
                .HasMaxLength(1)
                .HasColumnName("E-mail");
            entity.Property(e => e.Ime).HasMaxLength(50);
            entity.Property(e => e.Prezime).HasMaxLength(50);
        });

        modelBuilder.Entity<Transakcija>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transakc__3214EC27E36D2990");

            entity.ToTable("Transakcija");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idclana).HasColumnName("IDClana");
            entity.Property(e => e.Idknjige).HasColumnName("IDKnjige");
            entity.Property(e => e.Iduposlenika).HasColumnName("IDUposlenika");

            entity.HasOne(d => d.IdclanaNavigation).WithMany(p => p.Transakcijas)
                .HasForeignKey(d => d.Idclana)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transakci__IDCla__3F466844");

            entity.HasOne(d => d.IdknjigeNavigation).WithMany(p => p.Transakcijas)
                .HasForeignKey(d => d.Idknjige)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transakci__IDKnj__3E52440B");

            entity.HasOne(d => d.IduposlenikaNavigation).WithMany(p => p.Transakcijas)
                .HasForeignKey(d => d.Iduposlenika)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transakci__IDUpo__403A8C7D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
