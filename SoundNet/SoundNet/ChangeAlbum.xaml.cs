using SoundNet.Classes;
using SoundNet.EFCore.Entities;
using System.Linq;
using System.Windows;

namespace SoundNet
{
    public partial class ChangeAlbum : Window
    {
        private Albums _album;

        public ChangeAlbum(Albums album)
        {
            InitializeComponent();
            NameTextBox.Text = album.Name;
            _album = album;
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            ValidationMethods.validationErrors.Clear();

            if (_album != null)
            {
                // Проверяем существование альбома в базе данных
                bool albumExists = App.GlobalResources._dbContext.Albums.Any(a => a.Id == _album.Id);

                if (albumExists)
                {
                    if (ValidationMethods.ValidateSongName(NameTextBox.Text))
                    {
                        ValidationMethods.ClearErrorBorder(NameTextBox);
                        _album.Name = NameTextBox.Text;
                        App.GlobalResources._dbContext.Albums.Update(_album);
                        App.GlobalResources._dbContext.SaveChanges();

                        MessageBox.Show("Альбом успешно изменен");
                    }
                    else
                    {
                        ValidationMethods.SetErrorBorder(NameTextBox);
                        MessageBox.Show(string.Join("\n", ValidationMethods.validationErrors), "Ошибки валидации");
                    }
                }
                else
                {
                    // Альбом не найден в базе данных. Возможно, он был удален.
                    MessageBox.Show("Альбом не найден в базе данных. Невозможно сохранить изменения.");
                }
            }
            else
            {
                // _album равен null. Возможно, у вас есть какая-то логика обработки этого случая.
                MessageBox.Show("Альбом не найден. Невозможно сохранить изменения.");
            }
        }


    }
}