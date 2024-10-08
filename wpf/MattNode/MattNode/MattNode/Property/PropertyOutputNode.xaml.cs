﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MattNode
{
    /// <summary>
    /// PropertyOutputNode.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PropertyOutputNode : UserControl
    {
        private int Num;
        public bool InitTextFocus = false;
        public PropertyOutputNode(int num)
        {
            InitializeComponent();
            Num = num;
            nameTextBox.Text = ProjectProperty.ExportFiles[Num].Name;
            if (ProjectProperty.ExportFiles[Num].Extension == ".csv")
            {
                extensionComboBox.SelectedIndex = 0;
            }
            else if (ProjectProperty.ExportFiles[Num].Extension == ".txt(Structure containing functions)")
            {
                extensionComboBox.SelectedIndex = 1;
            }
            else if (ProjectProperty.ExportFiles[Num].Extension == ".txt(Structure)")
            {
                extensionComboBox.SelectedIndex = 2;
            }
            else if (ProjectProperty.ExportFiles[Num].Extension == ".txt(Structure without indexing)")
            {
                extensionComboBox.SelectedIndex = 3;
            }
            else if (ProjectProperty.ExportFiles[Num].Extension == ".txt(Script containing functions)")
            {
                extensionComboBox.SelectedIndex = 4;
            }
            else if (ProjectProperty.ExportFiles[Num].Extension == ".txt(Script without indexing)")
            {
                extensionComboBox.SelectedIndex = 5;
            }
        }

        public void Dispose()
        {
            nameTextBox.TextChanged -= nameTextbox_TextChanged;
            nameTextBox.Loaded -= nameTextBox_Loaded;
            extensionComboBox.SelectionChanged -= extensionComboBox_SelectionChanged;
            deleteButton.MouseDown -= deleteButton_MouseDown;
            ((Canvas)Parent).Children.Remove(this);
        }

        private void extensionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!PropertyMenu.SettingNodes)
            {
                ProjectProperty.ModifyExportFile(Num, ProjectProperty.ExportFiles[Num].Name, ((ComboBoxItem)(extensionComboBox.SelectedItem)).Content.ToString());
                PropertyMenu.mainProperty.SetPropertyTypeNodes();
            }
        }

        private void nameTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (nameTextBox.IsFocused && !PropertyMenu.SettingNodes)
            {
                ProjectProperty.ModifyExportFile(Num, nameTextBox.Text, ((ComboBoxItem)(extensionComboBox.SelectedItem)).Content.ToString());
                PropertyMenu.mainProperty.SetPropertyTypeNodes();
            }
        }
        private void nameTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (InitTextFocus) { nameTextBox.Focus(); }
            nameTextBox.CaretIndex = nameTextBox.Text.Length;
        }

        private void deleteButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ProjectProperty.RemoveExportFile(Num);
            PropertyMenu.mainProperty.SetPropertyNodes();
        }
        private void control_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (PropertyMenu.ChangingNodeType)
            {
                e.Handled = true;
                MainWindow._MainWindow.focusCatcher.Focus();
            }
        }
    }
}
