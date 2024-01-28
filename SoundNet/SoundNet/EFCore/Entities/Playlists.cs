using SoundNet.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoundNet.EFCore.Entities
{
    [Table("Playlists")]
    public class Playlists : IMedia
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime UploadDate { get; set; }
        public byte[] Image { get; set; }
        public User Author { get; set; }

        public ICollection<PlaylistAudio> PlaylistAudios { get; set; }
    }

    [Table("PlaylistAudio")]
    public class PlaylistAudio
    {
        public Guid PlaylistId { get; set; }
        public Playlists Playlist { get; set; }

        public Guid AudioId { get; set; }
        public Audio Audio { get; set; }
    }

    [Table("FavoritesPlaylist")]
    public class FavoritesPlaylist
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid PlaylistId { get; set; }
        public Playlists Playlist { get; set; }
    }
}