using System;
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
    /// NodeTypeDeletionAsk.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NodeTypeDeletionAsk : UserControl
    {
        public int Num;
        public static NodeTypeDeletionAsk ?mainWindow = null;
        private PropertyTypeNode TypeNode;
        public NodeTypeDeletionAsk(int num, PropertyTypeNode typeNode)
        {
            mainWindow = this;
            Num = num;
            InitializeComponent();
            TypeNode = typeNode;
        }

        public void Dispose()
        {
            mainWindow = null;
            originalTypeLabel.Loaded -= originalTypeLabel_Loaded;
            newTypeComboBox.Loaded -= newTypeComboBox_Loaded;
            cancelButton.Click -= cancelButton_Click;
            migrateButton.Click -= migrateButton_Click;
            ((Grid)Parent).Children.Remove(this);
        }

        private void originalTypeLabel_Loaded(object sender, RoutedEventArgs e)
        {
            originalTypeLabel.Content = ProjectProperty.NodeTypes[Num].Name;
        }

        private void newTypeComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            for(var i = 0; i < ProjectProperty.NodeTypes.Count; i++)
            {
                if(i != Num)
                {
                    newTypeComboBox.Items.Add(ProjectProperty.NodeTypes[i].Name);
                }
            }

            if(newTypeComboBox.Items.Count < 0)
            {
                mainCanvas.Children.Remove(migrateButton);
            }
            else
            {
                newTypeComboBox.SelectedIndex = 0;
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Dispose();
        }

        private void migrateButton_Click(object sender, RoutedEventArgs e)
        {
            int newNum = -1;

            for(int i = 0; i < ProjectProperty.NodeTypes.Count; i++)
            {
                if (ProjectProperty.NodeTypes[i].Name == newTypeComboBox.Text)
                {
                    newNum = i;
                    break;
                }
            }

            if(newNum == -1) { MessageBox.Show("Can't Find new type."); return; }

            NodeTypeMigrateAsk migrateWindow = new NodeTypeMigrateAsk(Num, newNum, TypeNode);
            migrateWindow.HorizontalAlignment = HorizontalAlignment.Left;
            migrateWindow.VerticalAlignment = VerticalAlignment.Top;
            Canvas.SetTop(migrateWindow, 0);
            Canvas.SetBottom(migrateWindow, 0);
            Grid.SetZIndex(migrateWindow, 4000);
            MainWindow._MainWindow.mainGrid.Children.Add(migrateWindow);
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            NodeTypeDeletionAsk2 deletionAskWindow2 = new NodeTypeDeletionAsk2(Num);
            deletionAskWindow2.HorizontalAlignment = HorizontalAlignment.Left;
            deletionAskWindow2.VerticalAlignment = VerticalAlignment.Top;
            Canvas.SetTop(deletionAskWindow2, 0);
            Canvas.SetBottom(deletionAskWindow2, 0);
            Grid.SetZIndex(deletionAskWindow2, 3000);
            MainWindow._MainWindow.mainGrid.Children.Add(deletionAskWindow2);
        }
    }
}
