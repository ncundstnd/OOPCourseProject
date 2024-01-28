using SoundNet.Classes;
using SoundNet.Classes.Interfaces;
using SoundNet.EFCore.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SoundNet
{
    public partial class SearchPage : Page
    {
        private List<IMedia> allSearchResults;
        private List<User> SearchResultsAuthors;

        public SearchPage(List<IMedia> searchResults, List<User> searchAuthors)
        {
            InitializeComponent();
            DataContext = searchAuthors;
            DataContext = searchResults;
            allSearchResults = searchResults;
            SearchResultsAuthors = searchAuthors;
            SearchResultsAuthors.RemoveAll(user => user.Id == App.GlobalResources.UserSignedIn.Id);

            TestListBox.ItemsSource = searchResults;
            // Подписываемся на событие изменения выбранного элемента в ComboBox
            FilterComboBox.SelectionChanged += FilterComboBox_SelectionChanged;
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Применяем выбранный фильтр
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            var selectedFilter = ((ComboBoxItem)FilterComboBox.SelectedItem).Content.ToString();

            switch (selectedFilter)
            {
                case "Все результаты":
                    TestListBox.ItemsSource = allSearchResults;
                    break;

                case "Плейлисты":
                    TestListBox.ItemsSource = allSearchResults.OfType<Playlists>().ToList();
                    break;

                case "Альбомы":
                    TestListBox.ItemsSource = allSearchResults.OfType<Albums>().ToList();
                    break;

                case "Песни":
                    TestListBox.ItemsSource = allSearchResults.OfType<Audio>().ToList();
                    break;

                case "Исполнители":
                    TestListBox.ItemsSource = SearchResultsAuthors.ToList();
                    break;
            }
        }

        private void BtnAddAuthorToFavorites_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                User author = button.Tag as User;
                if (author != null)
                {
                    // Вызов метода добавления в избранное
                    DBMethods.AddAuthorToSubscriptions(author);
                }
            }
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