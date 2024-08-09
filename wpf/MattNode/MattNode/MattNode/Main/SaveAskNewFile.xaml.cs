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
    /// SaveAskNewFile.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SaveAskNewFile : UserControl
    {
        public SaveAskNewFile()
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
            MenuBar.NewProject();
        }
        private void yesButton_Click(object sender, RoutedEventArgs e)
        {
            MenuBar.SaveProject();
            MenuBar.NewProject();
            Dispose();
        }
    }
}
