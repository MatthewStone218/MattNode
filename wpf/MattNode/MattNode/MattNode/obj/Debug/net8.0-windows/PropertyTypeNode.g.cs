﻿#pragma checksum "..\..\..\PropertyTypeNode.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BB19431D47A99203923A38F1E5CA3D55E6D7855E"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

using MattNode;
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


namespace MattNode {
    
    
    /// <summary>
    /// PropertyTypeNode
    /// </summary>
    public partial class PropertyTypeNode : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\PropertyTypeNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid mainGrid;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\PropertyTypeNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas dropDownCanvas;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\PropertyTypeNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button leftButton;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\PropertyTypeNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label typeNameLabel;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\PropertyTypeNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox typeNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\PropertyTypeNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button foldButton;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\PropertyTypeNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image deleteButton;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\PropertyTypeNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button upButton;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\PropertyTypeNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button downButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MattNode;component/propertytypenode.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\PropertyTypeNode.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.mainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.dropDownCanvas = ((System.Windows.Controls.Canvas)(target));
            return;
            case 3:
            this.leftButton = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.typeNameLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.typeNameTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 17 "..\..\..\PropertyTypeNode.xaml"
            this.typeNameTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.typeNameTextBox_TextChanged);
            
            #line default
            #line hidden
            
            #line 17 "..\..\..\PropertyTypeNode.xaml"
            this.typeNameTextBox.Loaded += new System.Windows.RoutedEventHandler(this.typeNameTextBox_Loaded);
            
            #line default
            #line hidden
            return;
            case 6:
            this.foldButton = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\PropertyTypeNode.xaml"
            this.foldButton.Click += new System.Windows.RoutedEventHandler(this.foldButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.deleteButton = ((System.Windows.Controls.Image)(target));
            
            #line 20 "..\..\..\PropertyTypeNode.xaml"
            this.deleteButton.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.deleteButton_MouseDown);
            
            #line default
            #line hidden
            return;
            case 8:
            this.upButton = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\PropertyTypeNode.xaml"
            this.upButton.Click += new System.Windows.RoutedEventHandler(this.upButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.downButton = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\PropertyTypeNode.xaml"
            this.downButton.Click += new System.Windows.RoutedEventHandler(this.downButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

