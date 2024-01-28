using Microsoft.EntityFrameworkCore;
using SoundNet.Classes;
using SoundNet.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SoundNet.CustomBlocks
{
    public partial class MusicBlock : UserControl
    {

        public Guid CurrentUserId { get; set; }

        public MusicBlock()
        {
            InitializeComponent();
            CurrentUserId = Guid.Empty;
        }

        private void OnMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(MusicId.Text);
        }

        // Вызов событий при нажатии соответствующих кнопо
    }
}