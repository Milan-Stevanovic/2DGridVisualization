﻿#pragma checksum "..\..\..\DrawingWindows\DrawPolygon.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1584EFF79DB69AFC8278DB6CE1A810A4C85635C16CE18E531D48FC53A3EE49CB"
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
using _2DGridVisualization.DrawingWindows;


namespace _2DGridVisualization.DrawingWindows {
    
    
    /// <summary>
    /// DrawPolygon
    /// </summary>
    public partial class DrawPolygon : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 39 "..\..\..\DrawingWindows\DrawPolygon.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbStroke;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\DrawingWindows\DrawPolygon.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtStrokeThickness;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\DrawingWindows\DrawPolygon.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbFill;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\DrawingWindows\DrawPolygon.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtOpacity;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\DrawingWindows\DrawPolygon.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtPolygonText;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\DrawingWindows\DrawPolygon.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbPolygonTextColor;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\DrawingWindows\DrawPolygon.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDrawPolygon;
        
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
            System.Uri resourceLocater = new System.Uri("/2DGridVisualization;component/drawingwindows/drawpolygon.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\DrawingWindows\DrawPolygon.xaml"
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
            this.txtPolygonText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.cmbPolygonTextColor = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.btnDrawPolygon = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\..\DrawingWindows\DrawPolygon.xaml"
            this.btnDrawPolygon.Click += new System.Windows.RoutedEventHandler(this.btnDrawPolygon_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

