﻿#pragma checksum "..\..\..\PlaylistMusicList.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F6F6060F5D6DBC9A49342AE57A15D6FFE099E83B"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using SoundNet.Classes;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace SoundNet {
    
    
    /// <summary>
    /// PlaylistMusicList
    /// </summary>
    public partial class PlaylistMusicList : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 61 "..\..\..\PlaylistMusicList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock authorName;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\PlaylistMusicList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image authorImage;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\PlaylistMusicList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnChangePlaylist;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\PlaylistMusicList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnDeletePlaylist;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\PlaylistMusicList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox MusicLib;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SoundNet;component/playlistmusiclist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\PlaylistMusicList.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.authorName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.authorImage = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.BtnChangePlaylist = ((System.Windows.Controls.Button)(target));
            
            #line 64 "..\..\..\PlaylistMusicList.xaml"
            this.BtnChangePlaylist.Click += new System.Windows.RoutedEventHandler(this.BtnChangePlaylist_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.BtnDeletePlaylist = ((System.Windows.Controls.Button)(target));
            
            #line 65 "..\..\..\PlaylistMusicList.xaml"
            this.BtnDeletePlaylist.Click += new System.Windows.RoutedEventHandler(this.BtnDeletePlaylist_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.MusicLib = ((System.Windows.Controls.ListBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

