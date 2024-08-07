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
            Init();
        }

        private void Init()
        {
            ProjectProperty.ApplyDefaultProperty();
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

        private void NodeCreateButton_Clicked(object sender, RoutedEventArgs e)
        {
            Node node = new Node(true,new Point(-50, -20));
            Canvas.SetLeft(node, MainCanvas.GetMousePos().X - 50);
            Canvas.SetTop(node, MainCanvas.GetMousePos().Y - 20);
            Panel.SetZIndex(node, 100);

            //mainCanvas.mainCanvas.Children.Add(node);
            node.Focus();
            node.typeComboBox.SelectedIndex = 0;
            node.node_GotFocus();
        }

        private void window_Closed(object sender, EventArgs e)
        {
        }

        private void TestPerformance()
        {
            for (int i = 0; i < 100; i++)
            {
                for (int ii = 0; ii < 100; ii++)
                {
                    Node node = new Node(false, new Point(0, 0));
                    Canvas.SetLeft(node, i*300);
                    Canvas.SetTop(node, ii*300);
                    Panel.SetZIndex(node, 100);

                    //MainWindow._MainWindow.mainCanvas.mainCanvas.Children.Add(node);
                    node.SetTypeItems();
                    node.typeComboBox.SelectedIndex = 0;
                    node.ReregisterCollisionTree();
                }
            }
        }

        private void TestPerformance(object sender, RoutedEventArgs e)
        {
            TestPerformance();
        }
    }
}