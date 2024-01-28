using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoundNet
{
    /// <summary>
    /// Логика взаимодействия для MusicPlayer.xaml
    /// </summary>
    public partial class MusicPlayer : UserControl
    {
        public MusicPlayer()
        {
            InitializeComponent();
        }

        private bool isPlaying = false;

        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (isPlaying)
            {
                // Логика при паузе
                PlayPauseButton.Content = "Play";
                // Остановка проигрывания
            }
            else
            {
                // Логика при проигрывании
                PlayPauseButton.Content = "Pause";
                // Начало проигрывания
            }

            isPlaying = !isPlaying;
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Логика изменения громкости
            double volume = volumeSlider.Value;
            // Установка громкости
        }

        private void PositionSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Логика изменения громкости
            double position = positionSlider.Value;
            // Установка громкости
        }
    }
}
