﻿#pragma checksum "..\..\..\..\Property\PropertyTypeNodeExportOptionNode.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "FB3A659DF00395C94C3BC087FB4A26E9BD6DC897"
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
    /// PropertyTypeNodeExportOptionNode
    /// </summary>
    public partial class PropertyTypeNodeExportOptionNode : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\..\Property\PropertyTypeNodeExportOptionNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label exportFileNameLabel;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\Property\PropertyTypeNodeExportOptionNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox typeCheckBox;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\Property\PropertyTypeNodeExportOptionNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox textCheckBox;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\Property\PropertyTypeNodeExportOptionNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox prevNodesCheckBox;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\Property\PropertyTypeNodeExportOptionNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ___이름_없음_;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\Property\PropertyTypeNodeExportOptionNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox nextNodesCheckBox;
        
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
            System.Uri resourceLocater = new System.Uri("/MattNode;component/property/propertytypenodeexportoptionnode.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Property\PropertyTypeNodeExportOptionNode.xaml"
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
            this.exportFileNameLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.typeCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 16 "..\..\..\..\Property\PropertyTypeNodeExportOptionNode.xaml"
            this.typeCheckBox.Checked += new System.Windows.RoutedEventHandler(this.typeCheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 16 "..\..\..\..\Property\PropertyTypeNodeExportOptionNode.xaml"
            this.typeCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.typeCheckBox_Unchecked);
            
            #line default
            #line hidden
            
            #line 16 "..\..\..\..\Property\PropertyTypeNodeExportOptionNode.xaml"
            this.typeCheckBox.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.control_PreviewMouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.textCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 27 "..\..\..\..\Property\PropertyTypeNodeExportOptionNode.xaml"
            this.textCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.textCheckBox_Unchecked);
            
            #line default
            #line hidden
            
            #line 27 "..\..\..\..\Property\PropertyTypeNodeExportOptionNode.xaml"
            this.textCheckBox.Checked += new System.Windows.RoutedEventHandler(this.textCheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 27 "..\..\..\..\Property\PropertyTypeNodeExportOptionNode.xaml"
            this.textCheckBox.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.control_PreviewMouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.prevNodesCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 38 "..\..\..\..\Property\PropertyTypeNodeExportOptionNode.xaml"
            this.prevNodesCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.prevNodesCheckBox_Unchecked);
            
            #line default
            #line hidden
            
            #line 38 "..\..\..\..\Property\PropertyTypeNodeExportOptionNode.xaml"
            this.prevNodesCheckBox.Checked += new System.Windows.RoutedEventHandler(this.prevNodesCheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 38 "..\..\..\..\Property\PropertyTypeNodeExportOptionNode.xaml"
            this.prevNodesCheckBox.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.control_PreviewMouseDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.___이름_없음_ = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.nextNodesCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 49 "..\..\..\..\Property\PropertyTypeNodeExportOptionNode.xaml"
            this.nextNodesCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.nextNodesCheckBox_Unchecked);
            
            #line default
            #line hidden
            
            #line 49 "..\..\..\..\Property\PropertyTypeNodeExportOptionNode.xaml"
            this.nextNodesCheckBox.Checked += new System.Windows.RoutedEventHandler(this.nextNodesCheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 49 "..\..\..\..\Property\PropertyTypeNodeExportOptionNode.xaml"
            this.nextNodesCheckBox.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.control_PreviewMouseDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

