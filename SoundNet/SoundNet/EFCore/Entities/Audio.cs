using SoundNet.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoundNet.EFCore.Entities
{
    [Table("Audio")]
    public class Audio : IMedia
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string Genre { get; set; }
        public byte[] AudioFile { get; set; }
        public DateTime UploadDate { get; set; }

        public Guid UserId { get; set; } // Внешний ключ
        public User User { get; set; } // Навигационное свойство к пользователю

        public ICollection<Audio> AudioFavorites { get; set; }

        public Audio()
        {
            AudioFavorites = new List<Audio>();
        }
    }

    [Table("FavoritesAudio")]
    public class FavoritesAudio
    {
        public Guid UserId { get; set; } // Внешний ключ
        public User User { get; set; } // Навигационное свойство к пользователю

        public Guid AudioId { get; set; } // Внешний ключ
        public Audio Audio { get; set; } // Навигационное свойство к аудиофайлу
    }
}