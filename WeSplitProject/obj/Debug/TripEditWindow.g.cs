﻿#pragma checksum "..\..\TripEditWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7A52FECD7825B10DBA5BEB7279CFBF7CD25073330B81A6925D04CD07142BEE30"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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
using WeSplitProject;


namespace WeSplitProject {
    
    
    /// <summary>
    /// CreateNewTrip
    /// </summary>
    public partial class CreateNewTrip : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid TitleBar;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Back;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tripNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox tripDestinationComboBox;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox tripStatusComboBox;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker tripDateBeginDatePicker;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker tripDateFinishDatePicker;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tripDescriptionTextBox;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image tripImage;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tripImageHint;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock totalExpenseTextBlock;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button doneBtn;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel SPExpense;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addExpenseBtn;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox visitLocDestinationComboBox;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker visitLocDateBeginDatePicker;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker visitLocDateFinishDatePicker;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox visitLocDescriptionTextBox;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image visitLocImage;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock visitLocImageHint;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox vitsitLocList;
        
        #line default
        #line hidden
        
        
        #line 132 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button deleteVisitLocBtn;
        
        #line default
        #line hidden
        
        
        #line 133 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addVisitLocBtn;
        
        #line default
        #line hidden
        
        
        #line 142 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox memberNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 145 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox memberPhoneTextBox;
        
        #line default
        #line hidden
        
        
        #line 148 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox memberEmailTextBox;
        
        #line default
        #line hidden
        
        
        #line 153 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image avatarImage;
        
        #line default
        #line hidden
        
        
        #line 154 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock avatarImageHint;
        
        #line default
        #line hidden
        
        
        #line 169 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addMemberBtn;
        
        #line default
        #line hidden
        
        
        #line 170 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button deleteMemberBtn;
        
        #line default
        #line hidden
        
        
        #line 174 "..\..\TripEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox memberListBox;
        
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
            System.Uri resourceLocater = new System.Uri("/WeSplitProject;component/tripeditwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\TripEditWindow.xaml"
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
            
            #line 11 "..\..\TripEditWindow.xaml"
            ((WeSplitProject.CreateNewTrip)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TitleBar = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.Back = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\TripEditWindow.xaml"
            this.Back.Click += new System.Windows.RoutedEventHandler(this.Back_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.tripNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.tripDestinationComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.tripStatusComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.tripDateBeginDatePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 8:
            this.tripDateFinishDatePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 9:
            this.tripDescriptionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.tripImage = ((System.Windows.Controls.Image)(target));
            
            #line 65 "..\..\TripEditWindow.xaml"
            this.tripImage.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.TripImage_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 65 "..\..\TripEditWindow.xaml"
            this.tripImage.Drop += new System.Windows.DragEventHandler(this.TripImage_Drop);
            
            #line default
            #line hidden
            return;
            case 11:
            this.tripImageHint = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 12:
            this.totalExpenseTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 13:
            this.doneBtn = ((System.Windows.Controls.Button)(target));
            
            #line 71 "..\..\TripEditWindow.xaml"
            this.doneBtn.Click += new System.Windows.RoutedEventHandler(this.doneBtn_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.SPExpense = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 15:
            this.addExpenseBtn = ((System.Windows.Controls.Button)(target));
            
            #line 79 "..\..\TripEditWindow.xaml"
            this.addExpenseBtn.Click += new System.Windows.RoutedEventHandler(this.addExpenseBtn_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            this.visitLocDestinationComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 17:
            this.visitLocDateBeginDatePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 18:
            this.visitLocDateFinishDatePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 19:
            this.visitLocDescriptionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 20:
            this.visitLocImage = ((System.Windows.Controls.Image)(target));
            
            #line 105 "..\..\TripEditWindow.xaml"
            this.visitLocImage.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.visitLocImage_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 105 "..\..\TripEditWindow.xaml"
            this.visitLocImage.Drop += new System.Windows.DragEventHandler(this.visitLocImage_Drop);
            
            #line default
            #line hidden
            return;
            case 21:
            this.visitLocImageHint = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 22:
            this.vitsitLocList = ((System.Windows.Controls.ListBox)(target));
            
            #line 110 "..\..\TripEditWindow.xaml"
            this.vitsitLocList.PreviewMouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.vitsitLocList_PreviewMouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 23:
            this.deleteVisitLocBtn = ((System.Windows.Controls.Button)(target));
            
            #line 132 "..\..\TripEditWindow.xaml"
            this.deleteVisitLocBtn.Click += new System.Windows.RoutedEventHandler(this.deleteVisitLocBtn_Click);
            
            #line default
            #line hidden
            return;
            case 24:
            this.addVisitLocBtn = ((System.Windows.Controls.Button)(target));
            
            #line 133 "..\..\TripEditWindow.xaml"
            this.addVisitLocBtn.Click += new System.Windows.RoutedEventHandler(this.addVisitLocBtn_Click);
            
            #line default
            #line hidden
            return;
            case 25:
            this.memberNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 26:
            this.memberPhoneTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 27:
            this.memberEmailTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 28:
            this.avatarImage = ((System.Windows.Controls.Image)(target));
            
            #line 153 "..\..\TripEditWindow.xaml"
            this.avatarImage.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.avatarImage_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 153 "..\..\TripEditWindow.xaml"
            this.avatarImage.Drop += new System.Windows.DragEventHandler(this.avatarImage_Drop);
            
            #line default
            #line hidden
            return;
            case 29:
            this.avatarImageHint = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 30:
            this.addMemberBtn = ((System.Windows.Controls.Button)(target));
            
            #line 169 "..\..\TripEditWindow.xaml"
            this.addMemberBtn.Click += new System.Windows.RoutedEventHandler(this.addMemberBtn_Click);
            
            #line default
            #line hidden
            return;
            case 31:
            this.deleteMemberBtn = ((System.Windows.Controls.Button)(target));
            
            #line 170 "..\..\TripEditWindow.xaml"
            this.deleteMemberBtn.Click += new System.Windows.RoutedEventHandler(this.deleteMemberBtn_Click);
            
            #line default
            #line hidden
            return;
            case 32:
            this.memberListBox = ((System.Windows.Controls.ListBox)(target));
            
            #line 174 "..\..\TripEditWindow.xaml"
            this.memberListBox.PreviewMouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.memberListBox_PreviewMouseDoubleClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

