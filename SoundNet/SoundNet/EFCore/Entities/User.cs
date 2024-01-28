using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security;

namespace SoundNet.EFCore.Entities
{
    [Table("User")]
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Role { get; set; }
        public string Password { get; set; }
        public byte[]? Avatar { get; set; }
        public string? Description { get; set; }

        public ICollection<Playlists> Playlists { get; set; }
        public ICollection<Albums> Albums { get; set; }
        public ICollection<Audio> Tracks { get; set; }

        public ICollection<FavoritesAudio> AudioFavorites { get; set; }
        public ICollection<FavoritesAlbum> AlbumFavorites { get; set; }
        public ICollection<FavoritesPlaylist> PlaylistFavorites { get; set; }
        public ICollection<UserSubscription> Subscriptions { get; set; }
        public ICollection<UserSubscription> Subscribers { get; set; }

        public User()
        {
            Role = 1;
            Playlists = new List<Playlists>();
            Albums = new List<Albums>();
            AudioFavorites = new List<FavoritesAudio>();
            AlbumFavorites = new List<FavoritesAlbum>();
            PlaylistFavorites = new List<FavoritesPlaylist>();
            Subscriptions = new List<UserSubscription>();
            Subscribers = new List<UserSubscription>();
            Tracks = new List<Audio>();
        }
    }

    [Table("UserSubscription")]
    public class UserSubscription
    {
        public Guid SubscriberId { get; set; }
        public User Subscriber { get; set; }

        public Guid SubscriptionId { get; set; }
        public User Subscription { get; set; }
    }
}