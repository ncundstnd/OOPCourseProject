using SoundNet.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace SoundNet.Classes
{
    public class MediaTemplateSelector : DataTemplateSelector
    {
        public DataTemplate AlbumTemplate { get; set; }
        public DataTemplate PlaylistTemplate { get; set; }
        public DataTemplate MusicTemplate { get; set; }
        public DataTemplate AuthorTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is Albums)
                return AlbumTemplate;
            else if (item is Playlists)
                return PlaylistTemplate;
            else if (item is Audio)
                return MusicTemplate;
            else if (item is User)
                return AuthorTemplate;

            return base.SelectTemplate(item, container);
        }
    }
}
