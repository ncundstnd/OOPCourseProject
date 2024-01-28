using Microsoft.Win32;
using SoundNet.Classes;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SoundNet
{
    public partial class ProfilePage : Page
    {
        public event EventHandler AvatarChanged;

        public ProfilePage()
        {
            InitializeComponent();
            DataContext = App.GlobalResources.UserSignedIn;
            if (App.GlobalResources.UserSignedIn.Role == 3)
            {
                Admin_Button.Visibility = Visibility.Visible;
                Add_Playlist_Button.Visibility = Visibility.Hidden;
            }
            else if (App.GlobalResources.UserSignedIn.Role == 2)
            {
                Author_Button.Visibility = Visibility.Visible;
                Author_Add_Album_Button.Visibility = Visibility.Visible;
                Author_Add_Song_Button.Visibility = Visibility.Visible;
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true;
            ValidationMethods.validationErrors.Clear();

            isValid &= ValidationMethods.ValidateAndSetErrorBorder(PasswordTextBox, ValidationMethods.ValidatePassword);
            isValid &= ValidationMethods.ValidateAndSetErrorBorder(NameTextBox, ValidationMethods.ValidateName);
            isValid &= ValidationMethods.ValidateAndSetErrorBorder(LastNameTextBox, ValidationMethods.ValidateLastName);
            isValid &= ValidationMethods.ValidateAndSetErrorBorder(LoginTextBox, ValidationMethods.ValidateLogin);
            isValid &= ValidationMethods.ValidateAndSetErrorBorder(EmailTextBox, ValidationMethods.ValidateEmail);
            isValid &= ValidationMethods.ValidateAndSetErrorBorder(PhoneTextBox, ValidationMethods.ValidatePhoneNumber);

            bool isValidUniq = ValidationMethods.AreFieldsUniqueChange(LoginTextBox, EmailTextBox, PhoneTextBox);

            if (isValid && isValidUniq)
            {
                App.GlobalResources.UserSignedIn.Name = NameTextBox.Text;
                App.GlobalResources.UserSignedIn.LastName = LastNameTextBox.Text;
                App.GlobalResources.UserSignedIn.Login = LoginTextBox.Text;
                App.GlobalResources.UserSignedIn.Email = EmailTextBox.Text;
                App.GlobalResources.UserSignedIn.Phone = PhoneTextBox.Text;
                App.GlobalResources.UserSignedIn.Password = PasswordTextBox.Text;

                App.GlobalResources._dbContext.Users.Update(App.GlobalResources.UserSignedIn);
                App.GlobalResources._dbContext.SaveChanges();

                OnAvatarChanged();
                MessageBox.Show("Изменения сохранены");
            }
            else
            {
                MessageBox.Show(string.Join("\n", ValidationMethods.validationErrors), "Ошибки валидации");
            }
        }

        protected virtual void OnAvatarChanged()
        {
            AvatarChanged?.Invoke(this, EventArgs.Empty);
        }

        private void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                ImagePathTextBox.Text = imagePath;
                App.GlobalResources.UserSignedIn.Avatar = System.IO.File.ReadAllBytes(imagePath);
            }
        }

        private void Admin_Button_Click(object sender, RoutedEventArgs e)
        {
            var adminPanel = new AdminPanel();
            adminPanel.Show();
        }

        private void Author_Button_Click(object sender, RoutedEventArgs e)
        {
            var authorProfile = new AuthorProfilePage(App.GlobalResources.UserSignedIn);
            authorProfile.Show();
        }

        private void Author_Add_Album_Button_Click(object sender, RoutedEventArgs e)
        {
            var addAlbum = new AddAlbum(App.GlobalResources.UserSignedIn);
            addAlbum.Show();
        }

        private void Author_Add_Song_Button_Click(object sender, RoutedEventArgs e)
        {
            var addSong = new AddMusic(App.GlobalResources.UserSignedIn);
            addSong.Show();
        }

        private void Add_Playlist_Button_Click(object sender, RoutedEventArgs e)
        {
            var addPlaylist = new AddPlaylist(App.GlobalResources.UserSignedIn);
            addPlaylist.Show();
        }
    }
}