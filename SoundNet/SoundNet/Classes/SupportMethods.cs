using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SoundNet.Classes
{
    public static class SupportMethods
    {
        public static BitmapImage UserProfilePicture { get; set; }

        public static void UpdateVisibilityAndButtonBackground(Panel panelToShow, Panel panelToHide, Button buttonToHighlight, Button buttonToReset)
        {
            Color highlightColor = (Color)ColorConverter.ConvertFromString("#9158ff");
            Color resetColor = (Color)ColorConverter.ConvertFromString("#444444");

            panelToShow.Visibility = Visibility.Visible;
            panelToHide.Visibility = Visibility.Collapsed;
            buttonToHighlight.Background = new SolidColorBrush(highlightColor);
            buttonToReset.Background = new SolidColorBrush(resetColor);
        }

        public static string RemoveCharacters(string input, int count)
        {
            var sb = new StringBuilder(input);

            while (count > 0 && sb.Length > 2)
            {
                sb.Length--;
                count--;
            }

            return sb.ToString();
        }

        public static byte[] ReadAudioFile(string filePath)
        {
            byte[] audioBytes;

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                audioBytes = new byte[fileStream.Length];
                fileStream.Read(audioBytes, 0, (int)fileStream.Length);
            }

            return audioBytes;
        }

        public static List<Genre> LoadGenresFromJson(string filePath)
        {
            List<Genre> genres = new List<Genre>();

            string jsonContent = File.ReadAllText(filePath);
            genres = JsonSerializer.Deserialize<List<Genre>>(jsonContent);

            return genres;
        }

        public static void LoadUserProfilePicture(Image profile)
        {
            ByteToImageConverter conv = new();
            if (App.GlobalResources.UserSignedIn.Avatar != null && App.GlobalResources.UserSignedIn.Avatar.Length > 0)
            {
                UserProfilePicture = conv.ConvertToImage(App.GlobalResources.UserSignedIn.Avatar);
                profile.Source = UserProfilePicture;  // Bind the Image control to the property
            }
            else
            {
                UserProfilePicture = new BitmapImage(new Uri("Resources/test.jpg", UriKind.Relative));
                profile.Source = UserProfilePicture;
            }
        }
    }

    public class Genre
    {
        public string Name { get; set; }
    }
}