using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
        public static List<Node> EnabledNodeList = new List<Node>();
        DispatcherTimer Timer;
        private bool MbLeftclicked = false;
        private Point DeltaMousePoint = new Point(0,0);
        private bool FollowingMouse = false;
        public Node(bool followMouse, Point deltaMousePoint)
        {
            InitializeComponent();
            Init();

            if (followMouse)
            { 
                DeltaMousePoint = deltaMousePoint;
                CompositionTarget.Rendering += UpdatePosition;
                node_GotFocus();
                FollowingMouse = true;
            }
        }
        public Node()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            NodeList.Add(this);
            EnabledNodeList.Add(this);
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
            CompositionTarget.Rendering += RepositionElements;

            resizeThumb1.Opacity = 0;
            resizeThumb2.Opacity = 0;
            resizeThumb3.Opacity = 0;
            resizeThumb4.Opacity = 0;
            resizeThumb5.Opacity = 0;
            resizeThumb6.Opacity = 0;
            resizeThumb7.Opacity = 0;
            resizeThumb8.Opacity = 0;
        }
        
        public override void Dispose()
        {
            base.Dispose();
            DetachUpdateEvent();
            if(FocusedNode == this) { FocusedNode = null; }
            NodeList.Remove(this);
            EnabledNodeList.Remove(this);
        }

        private void UpdatePosition(object sender, EventArgs e)
        {
            Margin = new Thickness(MainCanvas.GetMousePos().X+ DeltaMousePoint.X, MainCanvas.GetMousePos().Y+ DeltaMousePoint.Y, 0, 0);
            if (MbLeftclicked && Mouse.LeftButton != MouseButtonState.Pressed)
            {
                ReregisterCollisionTree();
                DetachUpdateEvent();
                //FocusedNode = null;
                FollowingMouse = false;
            }

            MbLeftclicked = Mouse.LeftButton == MouseButtonState.Pressed;
        }

        private void DetachUpdateEvent()
        {
            CompositionTarget.Rendering -= UpdatePosition;
        }

        private void ReregisterCollisionTree()
        {
            DeleteFromCollisionTree();
            InsertInCollisionTree();
        }
        private void RepositionElements(object sender, EventArgs e)
        {
            RepositionElements();
        }
        private void RepositionElements()
        {
            bgCanvas.Width = Width;
            bgCanvas.Height = Height;

            Canvas.SetLeft(resizeThumb1, 0);
            Canvas.SetTop(resizeThumb1, 0);
            resizeThumb1.Width = 10 / MainCanvas.RenderSize;
            resizeThumb1.Height = 10 / MainCanvas.RenderSize;

            Canvas.SetLeft(resizeThumb2, Width - 10 / MainCanvas.RenderSize);
            Canvas.SetTop(resizeThumb2, 0);
            resizeThumb2.Width = 10 / MainCanvas.RenderSize;
            resizeThumb2.Height = 10 / MainCanvas.RenderSize;

            Canvas.SetLeft(resizeThumb3, 0);
            Canvas.SetTop(resizeThumb3, Height - 10 / MainCanvas.RenderSize);
            resizeThumb3.Width = 10 / MainCanvas.RenderSize;
            resizeThumb3.Height = 10 / MainCanvas.RenderSize;

            Canvas.SetLeft(resizeThumb4, Width - 10 / MainCanvas.RenderSize);
            Canvas.SetTop(resizeThumb4, Height - 10 / MainCanvas.RenderSize);
            resizeThumb4.Width = 10 / MainCanvas.RenderSize;
            resizeThumb4.Height = 10 / MainCanvas.RenderSize;
            
            Canvas.SetLeft(resizeThumb5, 0);
            Canvas.SetTop(resizeThumb5, 10 / MainCanvas.RenderSize);
            resizeThumb5.Width = 10 / MainCanvas.RenderSize;
            resizeThumb5.Height = Height - 20 / MainCanvas.RenderSize;

            Canvas.SetLeft(resizeThumb6, 10 / MainCanvas.RenderSize);
            Canvas.SetTop(resizeThumb6, Height - 10 / MainCanvas.RenderSize);
            resizeThumb6.Height = 10 / MainCanvas.RenderSize;
            resizeThumb6.Width = Width - 20 / MainCanvas.RenderSize;

            Canvas.SetLeft(resizeThumb7, Width - 10 / MainCanvas.RenderSize);
            Canvas.SetTop(resizeThumb7, 10 / MainCanvas.RenderSize);
            resizeThumb7.Width = 10 / MainCanvas.RenderSize;
            resizeThumb7.Height = Height - 20 / MainCanvas.RenderSize;

            Canvas.SetLeft(resizeThumb8, 10 / MainCanvas.RenderSize);
            Canvas.SetTop(resizeThumb8, 0);
            resizeThumb8.Height = 10 / MainCanvas.RenderSize;
            resizeThumb8.Width = Width - 20 / MainCanvas.RenderSize;

            Canvas.SetLeft(deleteButton, Width-35);
            Canvas.SetTop(deleteButton, 14);
        }

        private void resizeThumb1_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double newWidth = this.Width - e.HorizontalChange;
            double newHeight = this.Height - e.VerticalChange;

            if (newWidth > 100)
            {
                Margin = new Thickness(Margin.Left + e.HorizontalChange, Margin.Top, 0, 0);
                Width = newWidth;
            }

            if (newHeight > 100)
            {
                Margin = new Thickness(Margin.Left, Margin.Top + e.VerticalChange, 0, 0);
                Height = newHeight;
            }

            RepositionElements();
        }
        private void resizeThumb2_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double newWidth = this.Width + e.HorizontalChange;
            double newHeight = this.Height - e.VerticalChange;

            if (newWidth > 100)
            {
                Width = newWidth;
            }

            if (newHeight > 100)
            {
                Margin = new Thickness(Margin.Left, Margin.Top + e.VerticalChange, 0, 0);
                Height = newHeight;
            }
            RepositionElements();
        }
        private void resizeThumb3_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double newWidth = this.Width - e.HorizontalChange;
            double newHeight = this.Height + e.VerticalChange;

            if (newWidth > 100)
            {
                Margin = new Thickness(Margin.Left + e.HorizontalChange, Margin.Top, 0, 0);
                Width = newWidth;
            }

            if (newHeight > 100)
            {
                Height = newHeight;
            }
            RepositionElements();
        }
        private void resizeThumb4_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double newWidth = this.Width + e.HorizontalChange;
            double newHeight = this.Height + e.VerticalChange;

            if (newWidth > 100)
            {
                Width = newWidth;
            }

            if (newHeight > 100)
            {
                Height = newHeight;
            }
            RepositionElements();
        }
        private void resizeThumb5_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double newWidth = this.Width - e.HorizontalChange;

            if (newWidth > 100)
            {
                Margin = new Thickness(Margin.Left + e.HorizontalChange, Margin.Top, 0, 0);
                Width = newWidth;
            }

            RepositionElements();
        }
        private void resizeThumb6_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double newHeight = this.Height + e.VerticalChange;

            if (newHeight > 100)
            {
                Height = newHeight;
            }
            RepositionElements();
        }
        private void resizeThumb7_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double newWidth = this.Width + e.HorizontalChange;

            if (newWidth > 100)
            {
                Width = newWidth;
            }
            RepositionElements();
        }
        private void resizeThumb8_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double newHeight = this.Height - e.VerticalChange;

            if (newHeight > 100)
            {
                Margin = new Thickness(Margin.Left, Margin.Top + e.VerticalChange, 0, 0);
                Height = newHeight;
            }

            RepositionElements();
        }
        private void resizeThumb_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            ReregisterCollisionTree();
        }

        private void node_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!FollowingMouse)
            {
                DeltaMousePoint = new Point(Margin.Left - MainCanvas.GetMousePos().X, Margin.Top - MainCanvas.GetMousePos().Y);
                CompositionTarget.Rendering += UpdatePosition;
                node_GotFocus();
                FollowingMouse = true;
                contentTextBox.Focus();
            }
        }

        private void deleteButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dispose();
        }

        public void SetType(string text)
        {
            typeComboBox.Text = text;
        }

        public void SetContent(string text)
        {
            contentTextBox.Text = text;
        }
        private void typeComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)typeComboBox.Template.FindName("PART_EditableTextBox", typeComboBox);
            if (typeComboBox.IsFocused || textBox.IsFocused)
            {
                Inspector.SetType(typeComboBox.Text);
            }
        }
        private void contentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (contentTextBox.IsFocused)
            {
                Inspector.SetContent(contentTextBox.Text);
            }
        }

        public void node_GotFocus()
        {
            Inspector.SetType(typeComboBox.Text);
            Inspector.SetContent(contentTextBox.Text);
            FocusedNode = this;
        }
        public void node_GotFocus(object sender, RoutedEventArgs e)
        {
            node_GotFocus();
        }
    }
}
