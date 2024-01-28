using Microsoft.Win32;
using SoundNet.Classes;
using SoundNet.EFCore.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SoundNet
{
    public partial class ChangeMusic : Window
    {
        private static string jsonFilePath = @"C:\Study\Курсач ООП\SoundNet\SoundNet\Resources\genres.json";
        private List<Genre> genres = new();
        private byte[] audioBytes;
        private byte[] imageBytes;
        private Audio currentAudio = new();

        public ChangeMusic(Audio openedAudio)
        {
            InitializeComponent();

            currentAudio = openedAudio;
            NameTextBox.Text = openedAudio.Name;

            genres = SupportMethods.LoadGenresFromJson(jsonFilePath);
            Genre selectedGenre = genres.FirstOrDefault(g => g.Name == openedAudio.Genre);

            GenreComboBox.ItemsSource = genres;
            GenreComboBox.DisplayMemberPath = "Name";

            if (selectedGenre != null)
            {
                GenreComboBox.SelectedItem = selectedGenre;
            }
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
            openFileDialog.Filter = "Audio Files|*.mp3";

            if (openFileDialog.ShowDialog() == true)
            {
                string audioPath = openFileDialog.FileName;
                AudioPathTextBox.Text = audioPath;
                audioBytes = SupportMethods.ReadAudioFile(audioPath);
            }
        }

        private void Change_Music_Click(object sender, RoutedEventArgs e)
        {
            ValidationMethods.validationErrors.Clear();

            if (currentAudio != null)
            {
                // Проверяем существование песни в базе данных
                bool audioExists = App.GlobalResources._dbContext.Music.Any(a => a.Id == currentAudio.Id);

                if (audioExists)
                {
                    if (ValidationMethods.ValidateSongName(NameTextBox.Text))
                    {
                        ValidationMethods.ClearErrorBorder(NameTextBox);

                        currentAudio.Name = NameTextBox.Text;
                        currentAudio.Genre = GenreComboBox.Text;

                        if (!string.IsNullOrEmpty(ImagePathTextBox.Text))
                            currentAudio.Image = imageBytes;
                        if (!string.IsNullOrEmpty(AudioPathTextBox.Text))
                            currentAudio.AudioFile = audioBytes;

                        App.GlobalResources._dbContext.Music.Update(currentAudio);
                        App.GlobalResources._dbContext.SaveChanges();

                        MessageBox.Show("Песня успешно изменена");
                    }
                    else
                    {
                        ValidationMethods.SetErrorBorder(NameTextBox);
                        MessageBox.Show(string.Join("\n", ValidationMethods.validationErrors), "Ошибки валидации");
                    }
                }
                else
                {
                    // Песня не найдена в базе данных. Возможно, она была удалена.
                    MessageBox.Show("Песня не найдена в базе данных. Невозможно сохранить изменения.");
                }
            }
            else
            {
                // currentAudio равен null. Возможно, у вас есть какая-то логика обработки этого случая.
                MessageBox.Show("Песня не найдена. Невозможно сохранить изменения.");
            }
        }
    }
}