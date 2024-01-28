using SoundNet.Classes;
using SoundNet.EFCore.Entities;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;

namespace SoundNet
{
    public partial class LibraryPage : Page
    {
        public LibraryPage()
        {
            InitializeComponent();
            DataContext = App.GlobalResources.UserSignedIn;
            MusicLibTab.IsSelected = true;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                if (e.AddedItems[0] is TabItem selectedTab)
                {
                    // Вызываем метод для обновления ListBox при изменении вкладки
                    UpdateListBox(selectedTab);
                }
            }
        }

        private void UpdateListBox(TabItem selectedTab)
        {
            ListBox selectedListBox = null;

            switch (selectedTab.Name)
            {
                case "MusicLibTab":
                    selectedListBox = MusicLib;
                    break;

                case "AuthorLibTab":
                    selectedListBox = AuthorLib;
                    break;

                case "AlbumLibTab":
                    selectedListBox = AlbumLib;
                    break;

                case "PlaylistLibTab":
                    selectedListBox = PlaylistLib;
                    break;
            }

            if (selectedListBox != null)
            {
                // Обновляем источник данных ListBox в зависимости от выбранной вкладки
                switch (selectedTab.Name)
                {
                    case "MusicLibTab":
                        MusicLib.ItemsSource = DBMethods.LoadMusicDataForUser(App.GlobalResources.UserSignedIn);
                        break;

                    case "AuthorLibTab":
                        AuthorLib.ItemsSource = DBMethods.LoadAuthorDataForUser(App.GlobalResources.UserSignedIn);
                        break;

                    case "AlbumLibTab":
                        AlbumLib.ItemsSource = DBMethods.LoadAlbumDataForUser(App.GlobalResources.UserSignedIn);
                        break;

                    case "PlaylistLibTab":
                        PlaylistLib.ItemsSource = DBMethods.LoadPlaylistDataForUser(App.GlobalResources.UserSignedIn);
                        break;
                }
            }
        }

        private void BtnRemoveAuthorFromFavorites_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                User author = button.Tag as User;
                if (author != null)
                {
                    // Вызов метода удаления из избранного
                    DBMethods.RemoveAuthorFromSubscriptions(author);
                    UpdateListBox(AuthorLibTab);
                }
            }
        }

        private void BtnDeleteAlbum_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Albums album = button.Tag as Albums;
                if (album != null)
                {
                    App.GlobalResources._dbContext.Remove(album);
                    App.GlobalResources._dbContext.SaveChanges();
                }
            }
        }

        private void BtnChangeAlbum_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Albums album = button.Tag as Albums;
                if (album != null)
                {
                    ChangeAlbum changeWindow = new(album);
                    changeWindow.Show();
                }
            }
        }

        private void BtnRemoveAlbumFromFavorites_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Albums album = button.Tag as Albums;
                if (album != null)
                {
                    // Вызов метода удаления из избранного
                    DBMethods.RemoveAlbumFromFavorites(album);
                    UpdateListBox(AlbumLibTab);
                }
            }
        }

        private void BtnChangePlaylist_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Playlists playlist = button.Tag as Playlists;
                if (playlist != null)
                {
                    ChangePlaylist changeWindow = new(playlist);
                    changeWindow.Show();
                }
            }
        }

        private void BtnDeletePlaylist_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Playlists playlist = button.Tag as Playlists;
                if (playlist != null)
                {
                    App.GlobalResources._dbContext.Remove(playlist);
                    App.GlobalResources._dbContext.SaveChanges();
                }
            }
        }

        private void BtnRemovePlaylistFromFavorites_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Playlists playlist = button.Tag as Playlists;
                if (playlist != null)
                {
                    // Вызов метода удаления из избранного
                    DBMethods.RemovePlaylistFromFavorites(playlist);
                    UpdateListBox(PlaylistLibTab);
                }
            }
        }

        private void BtnRemoveFromFavorites_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Audio audio = button.Tag as Audio;
                if (audio != null)
                {
                    // Открытие страницы профиля с передачей идентификатора автора
                    DBMethods.RemoveAudioFromFavorites(audio);
                    UpdateListBox(MusicLibTab);
                }
            }
        }

        private SoundPlayer player;
        private bool isPlaying = false;

        private void Song_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            if (image != null)
            {
                Audio audio = image.Tag as Audio;
                if (audio != null)
                {
                    if (isPlaying)
                    {
                        StopAudio();
                    }
                    else
                    {
                        PlayAudio(audio.AudioFile);
                    }
                }
            }
        }

        private void PlayAudio(byte[] audioBytes)
        {
            if (audioBytes != null && audioBytes.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(audioBytes))
                {
                    player = new SoundPlayer(ms);
                    player.Play();
                    isPlaying = true;
                }
            }
        }

        private void StopAudio()
        {
            if (player != null)
            {
                player.Stop();
                player.Dispose();
                isPlaying = false;
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
            Image button = sender as Image;
            if (button != null)
            {
                Playlists playlist = button.Tag as Playlists;
                if (playlist != null)
                {
                    PlaylistMusicList musicList = new(playlist);
                    musicList.Show();
                }
            }
        }

        private void Album_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Image button = sender as Image;
            if (button != null)
            {
                Albums playlist = button.Tag as Albums;
                if (playlist != null)
                {
                    AlbumMusicList musicList = new(playlist);
                    musicList.Show();
                }
            }
        }
    }
}