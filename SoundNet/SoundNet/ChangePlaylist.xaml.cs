using SoundNet.Classes;
using SoundNet.EFCore.Entities;
using System.Linq;
using System.Windows;

namespace SoundNet
{
    public partial class ChangePlaylist : Window
    {
        private Playlists _playlist;

        public ChangePlaylist(Playlists playlist)
        {
            InitializeComponent();
            InitializeComponent();
            NameTextBox.Text = playlist.Name;
            _playlist = playlist;
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            ValidationMethods.validationErrors.Clear();

            if (_playlist != null)
            {
                // Проверяем существование плейлиста в базе данных
                bool playlistExists = App.GlobalResources._dbContext.Playlists.Any(p => p.Id == _playlist.Id);

                if (playlistExists)
                {
                    if (ValidationMethods.ValidateSongName(NameTextBox.Text))
                    {
                        ValidationMethods.ClearErrorBorder(NameTextBox);
                        _playlist.Name = NameTextBox.Text;
                        App.GlobalResources._dbContext.Playlists.Update(_playlist);
                        App.GlobalResources._dbContext.SaveChanges();

                        MessageBox.Show("Плейлист успешно изменен");
                    }
                    else
                    {
                        ValidationMethods.SetErrorBorder(NameTextBox);
                        MessageBox.Show(string.Join("\n", ValidationMethods.validationErrors), "Ошибки валидации");
                    }
                }
                else
                {
                    // Плейлист не найден в базе данных. Возможно, он был удален.
                    MessageBox.Show("Плейлист не найден в базе данных. Невозможно сохранить изменения.");
                }
            }
            else
            {
                // _playlist равен null. Возможно, у вас есть какая-то логика обработки этого случая.
                MessageBox.Show("Плейлист не найден. Невозможно сохранить изменения.");
            }
        }


    }
}