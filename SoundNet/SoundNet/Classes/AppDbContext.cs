using Microsoft.EntityFrameworkCore;
using SoundNet.EFCore.Entities;

namespace SoundNet.Classes
{
    public class AppDbContext : DbContext
    {
        //private const string Connection = "Host=dpg-clvq5uud3nmc738fi260-a.frankfurt-postgres.render.com;Port=5432;Database=soundnet;Username=root;Password=ENJGFsKUsOLtXsXUhOMwVSaCqCnzcCd2";
        private const string Connection = "Host=localhost;Port=5432;Database=Rofl;Username=postgres;Password=1234";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Connection);
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSubscription>()
                .HasKey(us => new { us.SubscriberId, us.SubscriptionId });

            modelBuilder.Entity<UserSubscription>()
                .HasOne(us => us.Subscriber)
                .WithMany(u => u.Subscriptions)
                .HasForeignKey(us => us.SubscriberId);

            modelBuilder.Entity<UserSubscription>()
                .HasOne(us => us.Subscription)
                .WithMany(u => u.Subscribers)
                .HasForeignKey(us => us.SubscriptionId);

            modelBuilder.Entity<AlbumAudio>()
                .HasKey(aa => new { aa.AudioId, aa.AlbumId });

            modelBuilder.Entity<PlaylistAudio>()
            .HasKey(aa => new { aa.AudioId, aa.PlaylistId });

            modelBuilder.Entity<FavoritesAlbum>()
                .HasKey(fa => new { fa.UserId, fa.AlbumId });

            modelBuilder.Entity<FavoritesPlaylist>()
                .HasKey(fa => new { fa.UserId, fa.PlaylistId });

            modelBuilder.Entity<FavoritesAudio>()
                .HasKey(fa => new { fa.UserId, fa.AudioId });

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Login)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Phone)
                .IsUnique();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Audio> Music { get; set; }
        public DbSet<Playlists> Playlists { get; set; }
        public DbSet<Albums> Albums { get; set; }
        public DbSet<PlaylistAudio> PlaylistAudio { get; set; }
        public DbSet<UserSubscription> UserSubscription { get; set; }
        public DbSet<FavoritesAlbum> FavoritesAlbum { get; set; }
        public DbSet<AlbumAudio> AlbumAudio { get; set; }
        public DbSet<FavoritesPlaylist> FavoritesPlaylist { get; set; }
        public DbSet<FavoritesAudio> FavoritesAudio { get; set; }
    }
}