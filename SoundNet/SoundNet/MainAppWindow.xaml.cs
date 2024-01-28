using SoundNet.Classes;
using SoundNet.Classes.Interfaces;
using SoundNet.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SoundNet
{
    public partial class MainAppWindow : Window
    {
        public MainAppWindow(Window window)
        {
            InitializeComponent();
            window.Close();
            Cursor = new Cursor(Application.GetResourceStream(new Uri("Resources/arrow.cur", UriKind.Relative)).Stream);
            SupportMethods.LoadUserProfilePicture(profilePic);
        }

        protected override void OnClosed(EventArgs e)
        {
            this.Close();
            MainWindow loginWindow = new();
            loginWindow.Show();
        }

        private void Main_Click(object sender, RoutedEventArgs e)
        {
            BodyFrame.NavigationService.Navigate(new Uri("./MainPage.xaml", UriKind.Relative));
            UncheckButton("MainButton");
        }

        private void Content_Click(object sender, RoutedEventArgs e)
        {
            ContentPage contentPage = new ContentPage();
            BodyFrame.NavigationService.Navigate(contentPage);
            UncheckButton("ContentButton");
        }

        private void Library_Click(object sender, RoutedEventArgs e)
        {
            LibraryPage libraryPage = new LibraryPage();
            BodyFrame.NavigationService.Navigate(libraryPage);
            UncheckButton("LibraryButton");
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            ProfilePage profilePage = new ProfilePage();
            profilePage.AvatarChanged += ProfilePage_AvatarChanged;

            BodyFrame.NavigationService.Navigate(profilePage);
            UncheckButton("ProfileButton");
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string searchTerm = SearchTextBox.Text;
                List<IMedia> searchResults = DBMethods.SearchMedia(searchTerm);
                List<User> searchResultsAuthors = DBMethods.SearchAuthors(searchTerm);

                SearchPage searchPage = new(searchResults, searchResultsAuthors);
                BodyFrame.NavigationService.Navigate(searchPage);

                UncheckButton("SearchButton");
            }
        }

        private void UncheckButton(string buttonName)
        {
            List<ToggleButton> buttons = new List<ToggleButton> { MainButton, ContentButton, LibraryButton };

            foreach (var button in buttons)
            {
                if (button.Name != buttonName)
                {
                    button.IsChecked = false;
                    button.IsEnabled = true;
                }
                else
                {
                    button.IsEnabled = false;
                }
            }
            if (!(buttonName == "SearchButton"))
            {
                SearchTextBox.Text = "";
            }
        }

        private void ProfilePage_AvatarChanged(object sender, EventArgs e)
        {
            SupportMethods.LoadUserProfilePicture(profilePic);
        }
    }
}