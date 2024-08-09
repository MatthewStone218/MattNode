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
        public static double RenderSize = 1.0f;
        private double RenderSizeGoal = 1.0f;
        private DispatcherTimer Timer;
        public double X = 0;
        public double Y = 0;

        private TranslateTransform _TranslateTransform;
        private ScaleTransform _ScaleTransform;

        public MainCanvas()
        {
            Canvas = this;
            InitializeComponent();
            InitRanderTransform();
            Register_MouseWheelEvent();
            CompositionTarget.Rendering += RenderTick;

            rect1.HorizontalAlignment = HorizontalAlignment.Left;
            rect1.VerticalAlignment = VerticalAlignment.Top;
        }

        public void Dispose()
        {
            CompositionTarget.Rendering -= RenderTick;
            MainWindow._MainWindow.MouseWheel -= Global_MouseWheel;
            dragSpace.MouseDown -= DragSpace_MouseButtonDown;
            dragSpace.MouseLeave -= DragSpace_MouseButtonLeave;
            dragSpace.MouseMove -= DragSpace_MouseButtonMove;
            dragSpace.MouseUp -= DragSpace_MouseButtonUp;

            ((Grid)Parent).Children.Remove(this);
        }
        private void RenderTick(object sender, EventArgs e)
        {
            RenderSize += (RenderSizeGoal - RenderSize) / 5;

            if (Math.Abs(RenderSize- RenderSizeGoal) < 0.005) { RenderSize = RenderSizeGoal; }

            _ScaleTransform.ScaleX = RenderSize;
            _ScaleTransform.ScaleY = RenderSize;
            _TranslateTransform.X = X + MainWindow.GetWindowWidth() / (RenderSize * 2);
            _TranslateTransform.Y = Y + MainWindow.GetWindowHeight() / (RenderSize * 2);


            rect1.Margin = new Thickness(-X - 960, -Y - 540, 0, 0);

            List<Instance> instances = CollisionTree.GetInstancesInBoundaryList(new Instance(-X - 960, -Y - 540, 1920, 1080));
            for (int i = 0; i < Instance.EnabledInstanceList.Count; i++)
            {
                Instance.EnabledInstanceList[i]._IsEnabled = false;
            }

            for (int i = 0; i < instances.Count; i++)
            {
                if (!Instance.EnabledInstanceList.Contains(instances[i]))
                {
                    Instance.EnabledInstanceList.Add(instances[i]);
                }
                instances[i]._IsEnabled = true;
            }

            if (Node.FocusedNode != null)
            {
                if (!Instance.EnabledInstanceList.Contains(Node.FocusedNode))
                {
                    Instance.EnabledInstanceList.Add(Node.FocusedNode);
                }
                Node.FocusedNode._IsEnabled = true;
            }

            for (int i = 0; i < Instance.EnabledInstanceList.Count; i++)
            {
                if (Instance.EnabledInstanceList[i]._IsEnabled)
                {
                    Instance.EnabledInstanceList[i].IsEnabled = true;
                    Instance.EnabledInstanceList[i].Visibility = Visibility.Visible;
                    if (!Canvas.mainCanvas.Children.Contains(Instance.EnabledInstanceList[i])) { Canvas.mainCanvas.Children.Add(Instance.EnabledInstanceList[i]); }
                }
                else
                {
                    Instance.EnabledInstanceList[i].IsEnabled = false;
                    Instance.EnabledInstanceList[i].Visibility = Visibility.Collapsed;
                    Instance.EnabledInstanceList[i]._IsEnabled = false;
                    if (Canvas.mainCanvas.Children.Contains(Instance.EnabledInstanceList[i])) { Canvas.mainCanvas.Children.Remove(Instance.EnabledInstanceList[i]); }
                    Instance.EnabledInstanceList.RemoveAt(i);
                    i--;
                }
            }
        }
        private void Register_MouseWheelEvent()
        {
            MainWindow._MainWindow.MouseWheel += Global_MouseWheel;
        }
        private void Global_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (PropertyMenu.mainProperty == null && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                RenderSizeGoal += e.Delta / 3000.0f;

                if (RenderSizeGoal < 0.2) { RenderSizeGoal = 0.2; }
                else if (RenderSizeGoal > 5) { RenderSizeGoal = 5; }
            }
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
        public void DragSpace_MouseButtonDown(object sender, MouseButtonEventArgs e)
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
        private void Control_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
