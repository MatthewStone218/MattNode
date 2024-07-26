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
using System.Windows.Threading;

namespace MattNode
{
    /// <summary>
    /// MainCanvas.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainCanvas : UserControl
    {
        public static MainCanvas Canvas;
        private bool Dragging = false;
        private Point DragStartPoint = new Point(0, 0);
        private Point PosAtDragStart = new Point(0, 0);
        private static double RenderSize = 1.0f;
        private double RenderSizeGoal = 1.0f;
        private DispatcherTimer Timer;
        private double X = 0;
        private double Y = 0;

        private TranslateTransform _TranslateTransform;
        private ScaleTransform _ScaleTransform;

        public MainCanvas()
        {
            Canvas = this;
            InitializeComponent();
            InitRanderTransform();
            Register_MouseWheelEvent();
            InitTimer();
            rect1.HorizontalAlignment = HorizontalAlignment.Left;
            rect1.VerticalAlignment = VerticalAlignment.Top;
        }

        private void InitTimer()
        {
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(1);
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            RenderSize += (RenderSizeGoal - RenderSize) / 10;
            _ScaleTransform.ScaleX = RenderSize;
            _ScaleTransform.ScaleY = RenderSize;
            _TranslateTransform.X = X + MainWindow.GetWindowWidth() / (RenderSize * 2);
            _TranslateTransform.Y = Y + MainWindow.GetWindowHeight() / (RenderSize * 2);

            rect1.Margin = new Thickness(-X - 960, -Y - 540, 0, 0);
            
            
            for(int i = 0; i < Node.NodeList.Count; i++)
            {
                Node.NodeList[i].IsEnabled = false;
                Node.NodeList[i].Visibility = Visibility.Collapsed;

                /*
                if(Node.NodeList[i].Intersects(new Instance(-X - 960, -Y - 540, 1920, 1080)))
                {
                    Node.NodeList[i].IsEnabled = true;
                    Node.NodeList[i].Visibility = Visibility.Visible;
                }
                */
            }
            
            
            List<Instance> instances = CollisionTree.GetInstancesInBoundaryList(new Instance(-X - 960, -Y - 540, 1920, 1080));
            
            for (int i = 0; i < instances.Count; i++)
            {
                instances[i].IsEnabled = true;
                instances[i].Visibility = Visibility.Visible;
            }

            if (Node.FocusedNode != null)
            {
                Node.FocusedNode.IsEnabled = true;
                Node.FocusedNode.Visibility = Visibility.Visible;
            }
        }
        private void Register_MouseWheelEvent()
        {
            MainWindow._MainWindow.MouseWheel += Global_MouseWheel;
        }
        private void Global_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            RenderSizeGoal += e.Delta / 3000.0f;

            if (RenderSizeGoal < 0.2) { RenderSizeGoal = 0.2; }
            else if (RenderSizeGoal > 5) { RenderSizeGoal = 5; }
        }
        private void InitRanderTransform()
        {
            var _transformGroup = new TransformGroup();

            _TranslateTransform = new TranslateTransform();
            _transformGroup.Children.Add(_TranslateTransform);

            _ScaleTransform = new ScaleTransform();
            _transformGroup.Children.Add(_ScaleTransform);

            mainCanvas.RenderTransform = _transformGroup;
        }

        public static Point GetMousePos()
        {
            return new Point( MainWindow.GetMousePos().X/RenderSize - Canvas.X - MainWindow.GetWindowWidth()/(RenderSize*2) , MainWindow.GetMousePos().Y/RenderSize - Canvas.Y - MainWindow.GetWindowHeight()/(RenderSize * 2));
        }
        private void DragSpace_MouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            Dragging = true;
            DragStartPoint = MainWindow.GetMousePos();

            PosAtDragStart = new Point(X, Y);
        }

        private void DragSpace_MouseButtonLeave(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void DragSpace_MouseButtonMove(object sender, MouseEventArgs e)
        {
            if (Dragging)
            {
                X = PosAtDragStart.X + (MainWindow.GetMousePos().X - DragStartPoint.X) / RenderSize;
                Y = PosAtDragStart.Y + (MainWindow.GetMousePos().Y - DragStartPoint.Y) / RenderSize;

                _TranslateTransform.X = X + MainWindow.GetWindowWidth() / (RenderSize * 2);
                _TranslateTransform.Y = Y + MainWindow.GetWindowHeight() / (RenderSize * 2);
            }
        }

        private void DragSpace_MouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            Dragging = false;
        }
    }
}
