using SoundNet.Classes;
using SoundNet.EFCore.Entities;
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

namespace SoundNet.CustomBlocks
{
    public partial class AuthorBlock : UserControl
    {
        public AuthorBlock()
        {
            InitializeComponent();

            //User globalUser = Application.Current.Resources["GlobalUser"] as User;

            //if (globalUser.Role == 1 || globalUser.Role == 2)
            //{
            //    btnChangeAuthor.Visibility = Visibility.Collapsed;
            //    BtnDeleteAuthor.Visibility = Visibility.Collapsed;
            //}
        }

        private void OnMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            User currentAuthor = DBMethods.GetUserByLogin(AuthorLogin.Text);
            var authorProfile = new AuthorProfilePage(currentAuthor, 1);
            authorProfile.Show();
        }

        private void BtnDeleteAuthor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnChangeAuthor_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
