using SoundNet.Classes;
using SoundNet.Classes.Interfaces;
using SoundNet.EFCore.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SoundNet
{
    public partial class ContentPage : Page
    {
        public ContentPage()
        {
            InitializeComponent();
            List<IMedia> mediaList = DBMethods.GetMedia();

            mediaList = mediaList.OrderByDescending(m => m.UploadDate).ToList();

            TestListBox.ItemsSource = mediaList;
        }

        private void BtnAddAlbumToFavorites_Click_1(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Albums album = button.Tag as Albums;
                if (album != null)
                {
                    // Вызов метода удаления из избранного
                    DBMethods.AddAlbumToFavorites(album);
                }
            }
        }

        private void BtnAddPlaylistToFavorites_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Playlists playlist = button.Tag as Playlists;
                if (playlist != null)
                {
                    // Вызов метода удаления из избранного
                    DBMethods.AddPlaylistToFavorites(playlist);
                }
            }
        }

        private void BtnAddToFavorites_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Audio audio = button.Tag as Audio;
                if (audio != null)
                {
                    // Открытие страницы профиля с передачей идентификатора автора
                    DBMethods.AddAudioToFavorites(audio);
                }
            }
        }

        private void Song_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            if (image != null)
            {
                Audio audio = image.Tag as Audio;
                if (audio != null)
                {
                    // Открытие страницы профиля с передачей идентификатора автора
                    MessageBox.Show($"Ты нажал на песню{audio.Name}");
                }
            }
        }

        private void Author_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            if (image != null)
            {
                User author = image.Tag as User;
                if (author != null)
                {
                    // Открытие страницы профиля с передачей идентификатора автора
                    AuthorProfilePage authorInfo = new AuthorProfilePage(author.Id);
                    authorInfo.Show();
                }
            }
        }

        private void Playlist_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }

        private void Album_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }
    }
}