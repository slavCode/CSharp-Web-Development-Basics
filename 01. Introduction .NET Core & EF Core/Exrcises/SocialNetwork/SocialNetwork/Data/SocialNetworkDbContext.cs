namespace SocialNetwork.Data
{
    using Microsoft.EntityFrameworkCore;
    using SocialNetwork.Models;

    public class SocialNetworkDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<UserAlbum> UserAlbum { get; set; }     

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(
                @"Server=Y510P;Database=SocialNetwork;Integrated Security=True;");

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<UserFriend>()
                .HasKey(uf => new { uf.UserId, uf.FriendId });

            builder
                .Entity<UserFriend>()
                .HasOne(uf => uf.User)
                .WithMany(f => f.FriendshipsMade)
                .HasForeignKey(uf => uf.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<UserFriend>()
                .HasOne(uf => uf.Friend)
                .WithMany(f => f.FriendshipsAccepted)
                .HasForeignKey(uf => uf.FriendId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<PictureAlbum>()
                .HasKey(pa => new { pa.PictureId, pa.AlbumId });

            builder
                .Entity<Picture>()
                .HasMany(p => p.Albums)
                .WithOne(pa => pa.Picture)
                .HasForeignKey(p => p.PictureId);

            builder
                .Entity<Album>()
                .HasMany(a => a.Pictures)
                .WithOne(pa => pa.Album)
                .HasForeignKey(a => a.AlbumId);

            // Task 3 Create one-To-Many
            // Task 5 Delete
           
            //builder
            //    .Entity<User>()
            //    .HasMany(u => u.Albums)
            //    .WithOne(a => a.User)
            //    .HasForeignKey(u => u.UserId);

            builder
                .Entity<AlbumTag>()
                .HasKey(at => new { at.AlbumId, at.TagId });

            builder
                .Entity<Tag>()
                .HasMany(t => t.Albums)
                .WithOne(at => at.Tag)
                .HasForeignKey(t => t.TagId);

            builder
                .Entity<Album>()
                .HasMany(a => a.Tags)
                .WithOne(at => at.Album)
                .HasForeignKey(a => a.AlbumId);

            builder
                .Entity<UserAlbum>()
                .HasKey(ua => new { ua.UserId, ua.AlbumId });

            builder
                .Entity<User>()
                .HasMany(u => u.Albums)
                .WithOne(ua => ua.User)
                .HasForeignKey(u => u.UserId);

            builder
                .Entity<Album>()
                .HasMany(a => a.Users)
                .WithOne(ua => ua.Album)
                .HasForeignKey(a => a.AlbumId);

            base.OnModelCreating(builder);
        }
    }
}
