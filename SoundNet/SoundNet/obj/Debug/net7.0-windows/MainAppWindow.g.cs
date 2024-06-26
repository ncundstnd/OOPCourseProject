﻿#pragma checksum "..\..\..\MainAppWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1C576395A4C4C5D9236D1A97D27BA6290F136674"
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
    /// MainAppWindow
    /// </summary>
    public partial class MainAppWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 74 "..\..\..\MainAppWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton MainButton;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\MainAppWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton ContentButton;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\MainAppWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton LibraryButton;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\MainAppWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchTextBox;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\MainAppWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image profilePic;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\MainAppWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame BodyFrame;
        
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
            System.Uri resourceLocater = new System.Uri("/SoundNet;component/mainappwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainAppWindow.xaml"
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
            this.MainButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 74 "..\..\..\MainAppWindow.xaml"
            this.MainButton.Click += new System.Windows.RoutedEventHandler(this.Main_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ContentButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 75 "..\..\..\MainAppWindow.xaml"
            this.ContentButton.Click += new System.Windows.RoutedEventHandler(this.Content_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.LibraryButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 76 "..\..\..\MainAppWindow.xaml"
            this.LibraryButton.Click += new System.Windows.RoutedEventHandler(this.Library_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SearchTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 81 "..\..\..\MainAppWindow.xaml"
            this.SearchTextBox.KeyDown += new System.Windows.Input.KeyEventHandler(this.SearchTextBox_KeyDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.profilePic = ((System.Windows.Controls.Image)(target));
            
            #line 86 "..\..\..\MainAppWindow.xaml"
            this.profilePic.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Profile_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.BodyFrame = ((System.Windows.Controls.Frame)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

