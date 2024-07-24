using System.ComponentModel;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow _MainWindow;
        public MainWindow()
        {
            _MainWindow = this;
            InitializeComponent();
        }

        public static Point GetMousePos()
        {
            return Mouse.GetPosition(Application.Current.MainWindow);
        }

        public static double GetWindowWidth()
        {
            return _MainWindow.Width;
        }
        public static double GetWindowHeight()
        {
            return _MainWindow.Height;
        }
    }
}