using SoundNet.Classes;
using SoundNet.EFCore.Entities;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SoundNet
{
    public partial class AdminPanel : Window
    {
        public AdminPanel()
        {
            InitializeComponent();
            DataContext = DBMethods.GetMedia();
            MusicLibTab.IsSelected = true;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                if (e.AddedItems[0] is TabItem selectedTab)
                {
                    if (selectedTab == MusicLibTab)
                    {
                        MusicLib.ItemsSource = DBMethods.LoadMusicData();
                    }
                    else if (selectedTab == AuthorLibTab)
                    {
                        List<User> users = DBMethods.LoadAuthorData();
                        users.Remove(App.GlobalResources.UserSignedIn);
                        AuthorLib.ItemsSource = users;
                    }
                    else if (selectedTab == AlbumLibTab)
                    {
                        AlbumLib.ItemsSource = DBMethods.LoadAlbumData();
                    }
                    else if (selectedTab == PlaylistLibTab)
                    {
                        PlaylistLib.ItemsSource = DBMethods.LoadPlaylistData();
                    }
                }
            }
        }

        private void BtnChangeMusic_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Audio audio = button.Tag as Audio;
                if (audio != null)
                {
                    ChangeMusic changeWindow = new(audio);
                    changeWindow.Show();
                }
            }
        }

        private void BtnDeleteMusic_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Audio audio = button.Tag as Audio;
                if (audio != null)
                {
                    App.GlobalResources._dbContext.Remove(audio);
                    App.GlobalResources._dbContext.SaveChanges();
                }
            }
        }

        private void BtnDeleteAuthor_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                User author = button.Tag as User;
                if (author != null)
                {
                    App.GlobalResources._dbContext.Remove(author);
                    App.GlobalResources._dbContext.SaveChanges();
                }
            }
        }

        private void BtnChangeAuthor_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                User user = button.Tag as User;
                if (user != null)
                {
                    ChangeUser changeWindow = new(user);
                    changeWindow.Show();
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
    }
}