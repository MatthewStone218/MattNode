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
    /// NodeTypeDeletionAsk2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NodeTypeDeletionAsk2 : UserControl
    {
        public int Num;
        public NodeTypeDeletionAsk2(int num)
        {
            Num = num;
            InitializeComponent();
        }

        public void Dispose()
        {
            noButton.Click -= noButton_Click;
            yesButton.Click -= yesButton_Click;
            cautionTextBlock.Loaded -= cautionText_Loaded;
            ((Grid)Parent).Children.Remove(this);
        }

        private void noButton_Click(object sender, RoutedEventArgs e)
        {
            Dispose();
        }

        private void yesButton_Click(object sender, RoutedEventArgs e)
        {
            NodeTypeDeleter deleteWindow = new NodeTypeDeleter(Num);
            deleteWindow.HorizontalAlignment = HorizontalAlignment.Left;
            deleteWindow.VerticalAlignment = VerticalAlignment.Top;
            Canvas.SetTop(deleteWindow, 0);
            Canvas.SetBottom(deleteWindow, 0);
            Grid.SetZIndex(deleteWindow, 4000);
            MainWindow._MainWindow.mainGrid.Children.Add(deleteWindow);

            Dispose();
        }
        private void cautionText_Loaded(object sender, RoutedEventArgs e)
        {
            cautionTextBlock.Text = $"Are you sure you want to DELETE ALL \"{ProjectProperty.NodeTypes[Num].Name}\" type NODES?\n\nTHIS IS NOT UNDOABLE ACTION.";
        }
    }
}
