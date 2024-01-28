using Microsoft.Win32;
using SoundNet.Classes;
using SoundNet.EFCore.Entities;
using System.Collections.Generic;
using System.Windows;

namespace SoundNet
{
    public partial class AddAlbum : Window
    {
        private static string jsonFilePath = @"C:\Study\Курсач ООП\SoundNet\SoundNet\Resources\genres.json";
        private List<Genre> genres = new();
        public List<Audio> audioList = new List<Audio>();
        private byte[] imageBytes;
        private User Author = new();

        public AddAlbum(User author)
        {
            InitializeComponent();
            Author = author;
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

        private void AddMusic_Click(object sender, RoutedEventArgs e)
        {
            ValidationMethods.ClearErrorBorder(NameTextBox);
            ValidationMethods.ClearErrorBorder(ImagePathTextBox);
            ValidationMethods.ClearErrorBorder(AudioPathTextBox);

            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Невозможно создать альбом. Введите название альбома.", "Предупреждение");
                ValidationMethods.SetErrorBorder(NameTextBox);
                ValidationMethods.SetErrorBorder(ImagePathTextBox);
                ValidationMethods.SetErrorBorder(AudioPathTextBox);
            }
            else
            {
                if (string.IsNullOrEmpty(ImagePathTextBox.Text))
                {
                    MessageBox.Show("Невозможно создать альбом. Выберите обложку альбому.", "Предупреждение");
                    ValidationMethods.SetErrorBorder(ImagePathTextBox);
                    ValidationMethods.SetErrorBorder(AudioPathTextBox);
                }
                else
                {
                    if (audioList.Count > 0)
                    {
                        if (GenreComboBox.SelectedItem == null)
                        {
                            MessageBox.Show("Выберит жанр для альбома");
                        }
                        else
                        {
                            Albums newAlbum = new();

                            newAlbum.Name = NameTextBox.Text;
                            newAlbum.Author = App.GlobalResources.UserSignedIn;
                            newAlbum.Image = imageBytes;
                            newAlbum.Genre = ((Genre)GenreComboBox.SelectedItem).Name;
                            newAlbum.UploadDate = System.DateTime.UtcNow;

                            App.GlobalResources._dbContext.Albums.Add(newAlbum);
                            App.GlobalResources._dbContext.SaveChanges();

                            foreach (var audio in audioList)
                            {
                                AlbumAudio albumAudio = new AlbumAudio
                                {
                                    AlbumId = newAlbum.Id,
                                    AudioId = audio.Id
                                };

                                App.GlobalResources._dbContext.AlbumAudio.Add(albumAudio);
                                App.GlobalResources._dbContext.SaveChanges();
                            }

                            MessageBox.Show("Альбом создан успешно.", "Успех");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Невозможно создать альбом. Альбом пуст.", "Предупреждение");
                        ValidationMethods.SetErrorBorder(AudioPathTextBox);
                    }
                }
            }
        }

        private void Author_Add_Song_Button_Click(object sender, RoutedEventArgs e)
        {
            AddMusic addMusicWindow = new AddMusic(Author, this);
            addMusicWindow.Show();
        }
    }
}