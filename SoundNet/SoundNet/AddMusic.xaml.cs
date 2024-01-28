using Microsoft.Win32;
using SoundNet.Classes;
using SoundNet.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SoundNet
{
    public partial class AddMusic : Window
    {
        private static string jsonFilePath = @"C:\Study\Курсач ООП\SoundNet\SoundNet\Resources\genres.json";
        private List<Genre> genres = new();
        private byte[] audioBytes;
        private byte[] imageBytes;
        private User Author = new();
        private AddAlbum AddAlbum;

        public AddMusic(User author)
        {
            InitializeComponent();
            Author = author;
            MessageBox.Show(author.Login);
            genres = SupportMethods.LoadGenresFromJson(jsonFilePath);

            GenreComboBox.ItemsSource = genres;
            GenreComboBox.DisplayMemberPath = "Name";
        }

        public AddMusic(User author, AddAlbum addAlbum)
        {
            InitializeComponent();
            Author = author;
            AddAlbum = addAlbum;
            genres = SupportMethods.LoadGenresFromJson(jsonFilePath);

            GenreComboBox.ItemsSource = genres;
            GenreComboBox.DisplayMemberPath = "Name";
        }

        private void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                ImagePathTextBox.Text = imagePath;
                imageBytes = System.IO.File.ReadAllBytes(imagePath);
            }
        }

        private void UploadAudioButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Audio Files|*.wav";

            if (openFileDialog.ShowDialog() == true)
            {
                string audioPath = openFileDialog.FileName;
                AudioPathTextBox.Text = audioPath;
                audioBytes = SupportMethods.ReadAudioFile(audioPath);
            }
        }

        private void AddMusic_Click(object sender, RoutedEventArgs e)
        {
            ValidationMethods.validationErrors.Clear();

            if (ValidationMethods.ValidateSongName(NameTextBox.Text))
            {
                if (GenreComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Выберит жанр для песни");
                }
                else
                {
                    ValidationMethods.ClearErrorBorder(NameTextBox);

                    Audio newAudio = new Audio();
                    newAudio.Name = NameTextBox.Text;
                    newAudio.Genre = GenreComboBox.Text;
                    newAudio.UserId = Author.Id;
                    //newAudio.User = Author;
                    newAudio.UploadDate = System.DateTime.UtcNow;
                    newAudio.Image = imageBytes;
                    newAudio.AudioFile = audioBytes;

                    App.GlobalResources._dbContext.Music.Add(newAudio);
                    App.GlobalResources._dbContext.SaveChanges();

                    if (AddAlbum != null)
                    {
                        AddAlbum.AudioPathTextBox.Text += $"{newAudio.Name}\n";
                        AddAlbum.audioList.Add(newAudio);
                    }

                    List<User> subs = GetSubscribers(App.GlobalResources.UserSignedIn.Id);

                    foreach (User user in subs)
                    {
                        user.Description += $"Исполнитель {App.GlobalResources.UserSignedIn.Name} опубликовал {newAudio.UploadDate} новую песню {newAudio.Name}.\n";
                    }

                    MessageBox.Show("Песня успешно добавлена");
                }
            }
            else
            {
                ValidationMethods.SetErrorBorder(NameTextBox);
                MessageBox.Show(string.Join("\n", ValidationMethods.validationErrors), "Ошибки валидации");
            }
        }

        public List<User> GetSubscribers(Guid userId)
        {
            var subscribers = App.GlobalResources._dbContext.UserSubscription
                .Where(subscription => subscription.SubscriptionId == userId)
                .Select(subscription => subscription.Subscriber)
                .ToList();

            return subscribers;
        }
    }
}