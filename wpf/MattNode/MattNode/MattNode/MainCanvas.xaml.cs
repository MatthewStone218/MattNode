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
        private bool Dragging = false;
        private Point DragStartPoint = new Point(0, 0);
        private Point CanvasOffsetAtDragStart = new Point(0, 0);
        private float RenderSize = 1.0f;
        private DispatcherTimer Timer;

        private TranslateTransform _TranslateTransform;
        private ScaleTransform _ScaleTransform;

        public MainCanvas()
        {
            InitializeComponent();
            InitRanderTransform();
            Register_MouseWheelEvent();
        }

        private void Register_MouseWheelEvent()
        {
            MainWindow._MainWindow.MouseWheel += Global_MouseWheel;
        }
        private void Global_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            RenderSize += e.Delta / 200.0f;
            
        }
        private void InitRanderTransform()
        {
            var _transformGroup = new TransformGroup();

            _TranslateTransform = new TranslateTransform();
            _transformGroup.Children.Add(_TranslateTransform);

            _ScaleTransform = new ScaleTransform();
            _transformGroup.Children.Add(_ScaleTransform);

            this.RenderTransform = _transformGroup;
        }
        private void DragSpace_MouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            Dragging = true;
            DragStartPoint = MainWindow.GetMousePos();

            CanvasOffsetAtDragStart = new Point(_TranslateTransform.X, _TranslateTransform.Y);
        }

        private void DragSpace_MouseButtonLeave(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void DragSpace_MouseButtonMove(object sender, MouseEventArgs e)
        {
            if (Dragging)
            {
                _TranslateTransform.X = CanvasOffsetAtDragStart.X + MainWindow.GetMousePos().X - DragStartPoint.X;
                _TranslateTransform.Y = CanvasOffsetAtDragStart.Y + MainWindow.GetMousePos().Y - DragStartPoint.Y;
            }
        }

        private void DragSpace_MouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            Dragging = false;
        }
    }
}
