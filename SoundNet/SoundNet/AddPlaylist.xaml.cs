using Microsoft.Win32;
using SoundNet.Classes;
using SoundNet.EFCore.Entities;
using System.Collections.Generic;
using System.Windows;

namespace SoundNet
{
    public partial class AddPlaylist : Window
    {
        private byte[] imageBytes;
        private User Author = new();
        private List<Audio> audioList = new();
        private List<Audio> playlistAudioList = new();

        public AddPlaylist(User author)
        {
            InitializeComponent();
            Author = author;
            audioList = DBMethods.LoadMusicDataForUser(author);

            GenreComboBox.ItemsSource = audioList;
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

        private void Add_Song_Button_Click(object sender, RoutedEventArgs e)
        {
            if (GenreComboBox.SelectedItem != null)
            {
                Audio selectedAudio = (Audio)GenreComboBox.SelectedItem;

                if (!playlistAudioList.Contains(selectedAudio))
                {
                    playlistAudioList.Add(selectedAudio);
                    AudioPathTextBox.Text += $"{selectedAudio.Name}\n";
                }
                else
                {
                    MessageBox.Show("Песня уже присутствует в плейлисте.", "Внимание");
                }
            }
            else
            {
                MessageBox.Show("Выберите песню перед добавлением в плейлист.", "Внимание");
            }
        }

        private void CreatePlaylist_Click(object sender, RoutedEventArgs e)
        {
            ValidationMethods.ClearErrorBorder(NameTextBox);
            ValidationMethods.ClearErrorBorder(ImagePathTextBox);
            ValidationMethods.ClearErrorBorder(AudioPathTextBox);
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Невозможно создать плейлист. Введите название плейлиста.", "Предупреждение");
                ValidationMethods.SetErrorBorder(NameTextBox);
                ValidationMethods.SetErrorBorder(ImagePathTextBox);
                ValidationMethods.SetErrorBorder(AudioPathTextBox);
            }
            else
            {
                if (string.IsNullOrEmpty(ImagePathTextBox.Text))
                {
                    MessageBox.Show("Невозможно создать плейлист. Выберите обложку плейлисту.", "Предупреждение");
                    ValidationMethods.SetErrorBorder(ImagePathTextBox);
                    ValidationMethods.SetErrorBorder(AudioPathTextBox);
                }
                else
                {
                    if (playlistAudioList.Count > 0)
                    {
                        Playlists newPlaylist = new();

                        newPlaylist.Name = NameTextBox.Text;
                        newPlaylist.Author = App.GlobalResources.UserSignedIn;
                        newPlaylist.Image = imageBytes;
                        newPlaylist.UploadDate = System.DateTime.UtcNow;

                        App.GlobalResources._dbContext.Playlists.Add(newPlaylist);
                        App.GlobalResources._dbContext.SaveChanges();

                        foreach (var audio in playlistAudioList)
                        {
                            PlaylistAudio playlistAudio = new PlaylistAudio
                            {
                                PlaylistId = newPlaylist.Id,
                                AudioId = audio.Id
                            };

                            App.GlobalResources._dbContext.PlaylistAudio.Add(playlistAudio);
                            App.GlobalResources._dbContext.SaveChanges();
                        }

                        MessageBox.Show("Плейлист создан успешно.", "Успех");
                    }
                    else
                    {
                        MessageBox.Show("Невозможно создать плейлист. Плейлист пуст.", "Предупреждение");
                        ValidationMethods.SetErrorBorder(AudioPathTextBox);
                    }
                }
            }
        }
    }
}