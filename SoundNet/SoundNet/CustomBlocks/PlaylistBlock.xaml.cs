using SoundNet.Classes;
using SoundNet.EFCore.Entities;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SoundNet.CustomBlocks
{
    public partial class PlaylistBlock : UserControl
    {

        public PlaylistBlock()
        {
            InitializeComponent();
        }

        private void OnMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Playlists currentPlaylist = DBMethods.GetPlaylistById(Guid.Parse(PlaylistId.Text));

            //а дальше?
        }

        private void BtnDeletePlaylist_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnChangePlaylist_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}