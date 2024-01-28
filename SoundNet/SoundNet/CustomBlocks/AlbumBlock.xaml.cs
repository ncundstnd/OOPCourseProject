using SoundNet.EFCore.Entities;
using System.Windows;
using System.Windows.Controls;

namespace SoundNet.CustomBlocks
{
    public partial class AlbumBlock : UserControl
    {
        public AlbumBlock()
        {
            InitializeComponent();
            //User globalUser = Application.Current.Resources["GlobalUser"] as User;

            //if (globalUser.Role == 1)
            //{
            //    btnChangeAlbum.Visibility = Visibility.Collapsed;
            //    BtnDeleteAlbum.Visibility = Visibility.Collapsed;
            //}
        }

        private void OnMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(AlbumId.Text);
        }

        private void BtnDeleteAlbum_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnChangeAlbum_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
