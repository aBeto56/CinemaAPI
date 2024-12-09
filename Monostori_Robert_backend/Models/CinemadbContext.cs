using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Monostori_Robert_backend.Models;

public partial class CinemadbContext : DbContext
{
    public CinemadbContext()
    {

    }

    public CinemadbContext(DbContextOptions<CinemadbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<FilmType> FilmTypes { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.ActorId).HasName("PRIMARY");

            entity.ToTable("actors");

            entity.Property(e => e.ActorId)
                .HasColumnType("int(11)")
                .HasColumnName("actor_id");
            entity.Property(e => e.ActorName)
                .HasMaxLength(100)
                .HasColumnName("actor_name");
        });

        modelBuilder.Entity<FilmType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PRIMARY");

            entity.ToTable("film_type");

            entity.Property(e => e.TypeId)
                .HasColumnType("int(11)")
                .HasColumnName("type_id");
            entity.Property(e => e.TypeName)
                .HasMaxLength(100)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PRIMARY");

            entity.ToTable("movies");

            entity.HasIndex(e => e.ActorId, "actor");

            entity.HasIndex(e => e.FilmTypeId, "film");

            entity.Property(e => e.MovieId)
                .HasColumnType("int(11)")
                .HasColumnName("movie_id");
            entity.Property(e => e.ActorId)
                .HasColumnType("int(11)")
                .HasColumnName("actor_id");
            entity.Property(e => e.FilmTypeId)
                .HasColumnType("int(11)")
                .HasColumnName("film_type_id");
            entity.Property(e => e.ReleaseDate)
                .HasColumnType("date")
                .HasColumnName("release_date");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");

            entity.HasOne(d => d.Actor).WithMany(p => p.Movies)
                .HasForeignKey(d => d.ActorId)
                .HasConstraintName("movies_ibfk_1");

            entity.HasOne(d => d.FilmType).WithMany(p => p.Movies)
                .HasForeignKey(d => d.FilmTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("movies_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
