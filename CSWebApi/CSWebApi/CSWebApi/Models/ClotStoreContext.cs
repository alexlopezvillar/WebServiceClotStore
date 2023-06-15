using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CSWebApi.Models;

namespace CSWebApi.Models;

public partial class ClotStoreContext : DbContext
{
    public ClotStoreContext()
    {
    }

    public ClotStoreContext(DbContextOptions<ClotStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carret> Carrets { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<Estil> Estils { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Preference> Preferences { get; set; }

    public virtual DbSet<Prenda> Prendas { get; set; }

    public virtual DbSet<Producte> Productes { get; set; }

    public virtual DbSet<ProducteCarret> ProducteCarrets { get; set; }

    public virtual DbSet<ColorsProducte> ColorsProducte { get; set; }

    public virtual DbSet<Temporada> Temporadas { get; set; }

    public virtual DbSet<Usuari> Usuaris { get; set; }
    
    public virtual DbSet<Talla> Talla { get; set; }

    public virtual DbSet<TallasProducte> TallasProducte { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Trusted_Connection=True; Encrypt=False; Database=ClotStore");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carret>(entity =>
        {
            entity.HasKey(e => e.IdCarret);

            entity.ToTable("Carret");

            entity.HasOne(d => d.IdUsuariNavigation).WithMany(p => p.Carrets)
                .HasForeignKey(d => d.IdUsuari)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Carret_Usuari");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria);

            entity.ToTable("Categoria");
            entity.Property(e => e.Imatge).IsUnicode(false);
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.IdColor);

            entity.ToTable("Color");

            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Estil>(entity =>
        {
            entity.HasKey(e => e.IdEstil);

            entity.ToTable("Estil");
            entity.Property(e => e.Imatge).IsUnicode(false);
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca);

            entity.ToTable("Marca");
            entity.Property(e => e.Imatge).IsUnicode(false);
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Preference>(entity =>
        {
            entity.HasKey(e => e.IdPreferences);

            entity.HasOne(d => d.CategoriaNavigation).WithMany(p => p.Preferences)
                .HasForeignKey(d => d.Categoria)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Preferences_Categoria");

            entity.HasOne(d => d.EstilNavigation).WithMany(p => p.Preferences)
                .HasForeignKey(d => d.Estil)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Preferences_Estil");

            entity.HasOne(d => d.IdColorNavigation).WithMany(p => p.Preferences)
                .HasForeignKey(d => d.IdColor)
                .HasConstraintName("FK_Preferences_Color");

            entity.HasOne(d => d.IdUsuariNavigation).WithMany(p => p.Preferences)
                .HasForeignKey(d => d.IdUsuari)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Preferences_Usuari");

            entity.HasOne(d => d.TemporadaNavigation).WithMany(p => p.Preferences)
                .HasForeignKey(d => d.Temporada)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Preferences_Temporada");
        });

        modelBuilder.Entity<Prenda>(entity =>
        {
            entity.HasKey(e => e.IdPrenda);

            entity.ToTable("Prenda");
            entity.Property(e => e.Imatge)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Talla>(entity =>
        {
            entity.HasKey(e => e.IdTalla);

            entity.ToTable("Talla");

            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Producte>(entity =>
        {
            entity.HasKey(e => e.IdProducte);


            entity.Property(e => e.Imatge).IsUnicode(false);
            entity.Property(e => e.Nom)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.CategoriaNavigation).WithMany(p => p.Productes)
                .HasForeignKey(d => d.Categoria)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Productes_Categoria");

            entity.HasOne(d => d.EstilNavigation).WithMany(p => p.Productes)
                .HasForeignKey(d => d.Estil)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Productes_Estil");

            entity.HasOne(d => d.MarcaNavigation).WithMany(p => p.Productes)
                .HasForeignKey(d => d.Marca)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Productes_Marca");

            entity.HasOne(d => d.PrendaNavigation).WithMany(p => p.Productes)
                .HasForeignKey(d => d.Prenda)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Productes_Prenda");

            entity.HasOne(d => d.TemporadaNavigation).WithMany(p => p.Productes)
                .HasForeignKey(d => d.Temporada)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Productes_Temporada");
        });

        modelBuilder.Entity<ProducteCarret>(entity =>
        {
            entity.HasKey(e => e.IdProducteCarret);

            entity.ToTable("ProducteCarret");

            entity.HasOne(d => d.IdCarretNavigation).WithMany(p => p.ProducteCarrets)
                .HasForeignKey(d => d.IdCarret)
                .HasConstraintName("FK_ProducteCarret_Carret");

            entity.HasOne(d => d.IdTallasProducteNavigation).WithMany(p => p.ProducteCarrets)
                .HasForeignKey(d => d.IdTallaProducte)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ProducteCarret_TallasProducte");
        });

        modelBuilder.Entity<TallasProducte>(entity =>
        {
            entity.HasKey(e => e.IdTallaProducte);

            entity.ToTable("TallasProducte");

            entity.Property(e => e.existencies).HasColumnName("existencies");

            entity.HasOne(d => d.IdTallaNavigation).WithMany(p => p.TallasProducte)
                .HasForeignKey(d => d.IdTalla)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_TallasProducte_Talla");

            entity.HasOne(d => d.IdProducteNavigation).WithMany(p => p.TallasProductes)
                .HasForeignKey(d => d.IdProducte)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_TallasProducte_Productes");
        });

        modelBuilder.Entity<ColorsProducte>(entity =>
        {
            entity.HasKey(e => e.IdColorProducte);

            entity.ToTable("ColorsProducte");

            entity.HasOne(d => d.IdColorNavigation).WithMany(p => p.ColorsProductes)
                .HasForeignKey(d => d.IdColor)
                .HasConstraintName("FK_ColorsProducte_Color");

            entity.HasOne(d => d.IdProducteNavigation).WithMany(p => p.ColorsProductes)
                .HasForeignKey(d => d.IdProducte)
                .HasConstraintName("FK_ColorsProducte_Productes");
        });

        modelBuilder.Entity<Temporada>(entity =>
        {
            entity.HasKey(e => e.IdTemporada);

            entity.ToTable("Temporada");
            entity.Property(e => e.Imatge).IsUnicode(false);
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuari>(entity =>
        {
            entity.HasKey(e => e.IdUsuari);

            entity.ToTable("Usuari");

            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Contra)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Tipus)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
