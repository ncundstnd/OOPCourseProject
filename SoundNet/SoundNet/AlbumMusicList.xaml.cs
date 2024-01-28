using SoundNet.Classes;
using SoundNet.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SoundNet
{
    public partial class AlbumMusicList : Window
    {
        private Albums playlists = new Albums();

        public AlbumMusicList(Albums playlist)
        {
            InitializeComponent();
            playlists = playlist;
            DataContext = DBMethods.GetMedia();
            authorName.Text = playlist.Name;
            ByteToImageConverter convert = new ByteToImageConverter();
            authorImage.Source = convert.ConvertToImage(playlist.Image);
            MusicLib.ItemsSource = GetPlaylistAudiosById(playlist.Id);
            List<Albums> list = App.GlobalResources._dbContext.Albums.Where(playlist => playlist.Author.Id == App.GlobalResources.UserSignedIn.Id).ToList();
            if (!list.Contains(playlist))
            {
                BtnChangePlaylist.Visibility = Visibility.Hidden;
                BtnDeletePlaylist.Visibility = Visibility.Hidden;
            }
        }

        private void BtnChangePlaylist_Click(object sender, RoutedEventArgs e)
        {
            ChangeAlbum changeWindow = new(playlists);
            changeWindow.Show();
        }

        private void BtnDeletePlaylist_Click(object sender, RoutedEventArgs e)
        {
            if (playlists != null)
            {
                var existingPlaylist = App.GlobalResources._dbContext.Albums.Find(playlists.Id);

                if (existingPlaylist != null)
                {
                    // Опционально: добавьте здесь код для подтверждения удаления (например, модальное окно с вопросом).
                    // Если пользователь подтверждает, продолжите с удалением.

                    App.GlobalResources._dbContext.Remove(existingPlaylist);
                    App.GlobalResources._dbContext.SaveChanges();
                    // Опционально: обновите ваш интерфейс или выполните другие действия после успешного удаления.
                }
                else
                {
                    // Плейлист не найден в базе данных. Возможно, он уже был удален.
                    // Добавьте соответствующую обработку ошибок или выведите сообщение пользователю.
                }
            }
            else
            {
                // playlists равен null. Возможно, у вас есть какая-то логика обработки этого случая.
            }
        }


        public List<Audio> GetPlaylistAudiosById(Guid playlistId)
        {
            var playlistAudios = App.GlobalResources._dbContext.AlbumAudio
                .Where(pa => pa.AlbumId == playlistId)
                .Select(pa => pa.Audio)
                .ToList();

            return playlistAudios;
        }
    }
}