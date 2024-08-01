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
    /// NodeTypeMigrateAsk.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NodeTypeMigrateAsk : UserControl
    {
        public int Num, NewNum;
        public NodeTypeMigrateAsk(int num, int newNum)
        {
            Num = num;
            NewNum = newNum;
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
            NodeTypeMigrator migrateWindow = new NodeTypeMigrator(Num,NewNum);
            migrateWindow.HorizontalAlignment = HorizontalAlignment.Left;
            migrateWindow.VerticalAlignment = VerticalAlignment.Top;
            Canvas.SetTop(migrateWindow, 0);
            Canvas.SetBottom(migrateWindow, 0);
            Grid.SetZIndex(migrateWindow, 4000);
            MainWindow._MainWindow.mainGrid.Children.Add(migrateWindow);

            Dispose();
        }
        private void cautionText_Loaded(object sender, RoutedEventArgs e)
        {
            cautionTextBlock.Text = $"Are you sure you want to Migrate ALL \"{ProjectProperty.NodeTypes[Num].Name}\" type nodes to \"{ProjectProperty.NodeTypes[NewNum].Name}\"?\n\nTHIS IS NOT UNDOABLE ACTION.";
        }
    }
}
