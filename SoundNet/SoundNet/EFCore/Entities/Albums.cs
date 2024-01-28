using SoundNet.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoundNet.EFCore.Entities
{
    [Table("Albums")]
    public class Albums : IMedia
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime UploadDate { get; set; }
        public byte[] Image { get; set; }
        public string Genre { get; set; }
        public string? Description { get; set; }
        public User Author { get; set; }

        public ICollection<AlbumAudio> AlbumAudios { get; set; }
    }

    [Table("AlbumAudio")]
    public class AlbumAudio
    {
        public Guid AudioId { get; set; }
        public Audio Audio { get; set; }

        public Guid AlbumId { get; set; }
        public Albums Album { get; set; }
    }

    [Table("FavoritesAlbum")]
    public class FavoritesAlbum
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid AlbumId { get; set; }
        public Albums Album { get; set; }
    }
}