﻿#pragma checksum "..\..\..\..\CustomBlocks\AlbumBlock.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "EF7B2744000E4E528B3D4F99E97EC83257DB92D1"
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


namespace SoundNet.CustomBlocks {
    
    
    /// <summary>
    /// AlbumBlock
    /// </summary>
    public partial class AlbumBlock : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 53 "..\..\..\..\CustomBlocks\AlbumBlock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock AlbumId;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\CustomBlocks\AlbumBlock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddToFavorites;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\CustomBlocks\AlbumBlock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRemoveFromFavorites;
        
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
            System.Uri resourceLocater = new System.Uri("/SoundNet;component/customblocks/albumblock.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\CustomBlocks\AlbumBlock.xaml"
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
            
            #line 51 "..\..\..\..\CustomBlocks\AlbumBlock.xaml"
            ((System.Windows.Controls.Image)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.OnMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.AlbumId = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.btnAddToFavorites = ((System.Windows.Controls.Button)(target));
            
            #line 60 "..\..\..\..\CustomBlocks\AlbumBlock.xaml"
            this.btnAddToFavorites.Click += new System.Windows.RoutedEventHandler(this.btnAddToFavorites_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnRemoveFromFavorites = ((System.Windows.Controls.Button)(target));
            
            #line 61 "..\..\..\..\CustomBlocks\AlbumBlock.xaml"
            this.btnRemoveFromFavorites.Click += new System.Windows.RoutedEventHandler(this.btnRemoveFromFavorites_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

