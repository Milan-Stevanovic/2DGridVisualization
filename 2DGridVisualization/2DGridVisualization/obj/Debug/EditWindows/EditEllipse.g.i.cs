﻿#pragma checksum "..\..\..\EditWindows\EditEllipse.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "728321F95B1F6050AE80268BF8F7CCDAE423979AED8F776F9E2571378489550A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using _2DGridVisualization.EditWindows;


namespace _2DGridVisualization.EditWindows {
    
    
    /// <summary>
    /// EditEllipse
    /// </summary>
    public partial class EditEllipse : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\EditWindows\EditEllipse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbStroke;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\EditWindows\EditEllipse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtStrokeThickness;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\EditWindows\EditEllipse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbFill;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\EditWindows\EditEllipse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtOpacity;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\EditWindows\EditEllipse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbEllipseTextColor;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\EditWindows\EditEllipse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEditEllipse;
        
        #line default
        #line hidden
        
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
            System.Uri resourceLocater = new System.Uri("/2DGridVisualization;component/editwindows/editellipse.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\EditWindows\EditEllipse.xaml"
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
            this.cmbStroke = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.txtStrokeThickness = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.cmbFill = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.txtOpacity = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.cmbEllipseTextColor = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.btnEditEllipse = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\EditWindows\EditEllipse.xaml"
            this.btnEditEllipse.Click += new System.Windows.RoutedEventHandler(this.btnEditEllipse_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
