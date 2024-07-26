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
    /// Node.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Node : Instance
    {
        public static Node FocusedNode = null;
        public static List<Node> NodeList = new List<Node>();
        DispatcherTimer Timer;
        private bool MbLeftclicked = false;
        private Point DeltaMousePoint = new Point(0,0);
        public Node(bool followMouse, Point deltaMousePoint)
        {
            NodeList.Add(this);
            InitializeComponent();
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;

            if (followMouse)
            { 
                DeltaMousePoint = deltaMousePoint;
                CompositionTarget.Rendering += UpdatePosition;
                FocusedNode = this;
            }
        }
        public Node()
        {
            NodeList.Add(this);
            InitializeComponent();
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
        }
         
        public void Dispose()
        {
            DetachUpdateEvent();
            if(FocusedNode == this) { FocusedNode = null; }
        }

        private void UpdatePosition(object sender, EventArgs e)
        {
            Margin = new Thickness(MainCanvas.GetMousePos().X+ DeltaMousePoint.X, MainCanvas.GetMousePos().Y+ DeltaMousePoint.Y, 0, 0);
            if (MbLeftclicked && Mouse.LeftButton != MouseButtonState.Pressed)
            {
                DeleteFromCollisionTree();
                InsertInCollisionTree();
                DetachUpdateEvent();
                FocusedNode = null;
            }

            MbLeftclicked = Mouse.LeftButton == MouseButtonState.Pressed;
        }

        private void DetachUpdateEvent()
        {
            CompositionTarget.Rendering -= UpdatePosition;
        }
    }
}
