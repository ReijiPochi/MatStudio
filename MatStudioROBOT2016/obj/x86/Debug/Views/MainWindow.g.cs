﻿#pragma checksum "..\..\..\..\Views\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4A533AB622D0C7C4072DA005E20F1F6A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Livet;
using Livet.Behaviors;
using Livet.Behaviors.ControlBinding;
using Livet.Behaviors.ControlBinding.OneWay;
using Livet.Behaviors.Messaging;
using Livet.Behaviors.Messaging.IO;
using Livet.Behaviors.Messaging.Windows;
using Livet.Commands;
using Livet.Converters;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.Messaging.Windows;
using MatGUI;
using MatStudioROBOT2016.ViewModels;
using MatStudioROBOT2016.Views;
using Microsoft.Expression.Interactivity.Core;
using Microsoft.Expression.Interactivity.Input;
using Microsoft.Expression.Interactivity.Layout;
using Microsoft.Expression.Interactivity.Media;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
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


namespace MatStudioROBOT2016.Views {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MatStudioROBOT2016;component/views/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 14 "..\..\..\..\Views\MainWindow.xaml"
            ((MatStudioROBOT2016.Views.MainWindow)(target)).Activated += new System.EventHandler(this.Window_Activated);
            
            #line default
            #line hidden
            
            #line 14 "..\..\..\..\Views\MainWindow.xaml"
            ((MatStudioROBOT2016.Views.MainWindow)(target)).Deactivated += new System.EventHandler(this.Window_Deactivated);
            
            #line default
            #line hidden
            
            #line 14 "..\..\..\..\Views\MainWindow.xaml"
            ((MatStudioROBOT2016.Views.MainWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 56 "..\..\..\..\Views\MainWindow.xaml"
            ((MatGUI.MatMenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.LoadLayout_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 57 "..\..\..\..\Views\MainWindow.xaml"
            ((MatGUI.MatMenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveLayoutAs_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 58 "..\..\..\..\Views\MainWindow.xaml"
            ((MatGUI.MatMenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveLayoutToInitial);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

