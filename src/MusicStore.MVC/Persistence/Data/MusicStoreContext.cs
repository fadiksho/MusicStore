using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicStore.MVC.Entities;
using MusicStore.MVC.Models;

namespace MusicStore.MVC.Persistence.Data
{
  public class MusicStoreContext : IdentityDbContext<User>
  {
    public MusicStoreContext(DbContextOptions<MusicStoreContext> options)
      : base(options) { }

    public DbSet<SongEntity> Songs { get; set; }
    public DbSet<AlbumEntity> Albums { get; set; }
    public DbSet<GenreEntity> Genres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Configure optional relationship where each song has one album and each album has many songs
      // and the song are not requird to have an album
      modelBuilder.Entity<SongEntity>()
        .HasOne(s => s.Album)
        .WithMany(a => a.Songs)
        .HasForeignKey(a => a.AlbumId)
        .IsRequired(false)
        .OnDelete(DeleteBehavior.SetNull);

      // Configure many to many relationships between genre and songs
      modelBuilder.Entity<GenreSongEntity>()
        .HasKey(gs => new { gs.GenreId, gs.SongId });
      modelBuilder.Entity<GenreSongEntity>()
        .HasOne<SongEntity>(s => s.Song)
        .WithMany(g => g.GenreSong)
        .HasForeignKey(g => g.SongId);
      modelBuilder.Entity<GenreSongEntity>()
        .HasOne<GenreEntity>(g => g.Genre)
        .WithMany(s => s.GenreSong)
        .HasForeignKey(s => s.GenreId);
    }
  }
}
