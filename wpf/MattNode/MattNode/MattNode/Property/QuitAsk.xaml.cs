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

namespace MattNode.Property
{
    /// <summary>
    /// QuitAsk.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class QuitAsk : UserControl
    {
        public QuitAsk()
        {
            InitializeComponent();
        }

        public void Dispose()
        {
            noButton.Click -= noButton_Click;
            yesButton.Click -= yesButton_Click;
            ((Grid)Parent).Children.Remove(this);
        }
        private void noButton_Click(object sender, RoutedEventArgs e)
        {
            Dispose();
            MainWindow.CloseByCode = true;
            MainWindow.IsClosing = false;
            MainWindow._MainWindow.Close();
        }
        private void yesButton_Click(object sender, RoutedEventArgs e)
        {
            MenuBar.SaveAndCloseProject();
            Dispose();
        }
    }
}
