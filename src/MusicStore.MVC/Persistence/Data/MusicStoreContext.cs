using Microsoft.EntityFrameworkCore;
using MusicStore.MVC.Entities;

namespace MusicStore.MVC.Persistence.Data
{
  public class MusicStoreContext : DbContext
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
        .HasOne(s => s.Song)
        .WithMany(g => g.Genres)
        .HasForeignKey(g => g.GenreId);
      modelBuilder.Entity<GenreSongEntity>()
        .HasOne(g => g.Genre)
        .WithMany(s => s.Songs)
        .HasForeignKey(s => s.SongId);

      
    }
  }
}
