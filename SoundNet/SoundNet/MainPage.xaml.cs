using System.Windows;
using System.Windows.Controls;

namespace SoundNet
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            if (App.GlobalResources.UserSignedIn.Role != 1)
            {
                uved.Visibility = Visibility.Hidden;
                LoginUsernameTextBox.Visibility = Visibility.Hidden;
            }
            LoginUsernameTextBox.Text = App.GlobalResources.UserSignedIn.Description;
        }
    }
}