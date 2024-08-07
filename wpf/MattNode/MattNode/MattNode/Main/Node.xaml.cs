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
        DispatcherTimer Timer;
        private bool MbLeftclicked = false;
        private Point DeltaMousePoint = new Point(0,0);
        private bool FollowingMouse = false;
        private bool SettingNodeTypeItems = false;
        public List<NodeArrow> ArrowsFromMe = new List<NodeArrow>();
        public List<NodeArrow> ArrowsFromOther = new List<NodeArrow>();
        public Node(bool followMouse, Point deltaMousePoint)
        {
            InitializeComponent();
            Init();

            Canvas.SetLeft(this, 0);
            Canvas.SetTop(this, 0);

            if (followMouse)
            { 
                DeltaMousePoint = deltaMousePoint;
                CompositionTarget.Rendering += UpdatePosition;
                node_GotFocus();
                FollowingMouse = true;
            }
            else
            {
                ReregisterCollisionTree();
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
            EnabledInstanceList.Add(this);
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;

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
            if (FocusedNode == this) { FocusedNode = null; }
            NodeList.Remove(this);
            EnabledInstanceList.Remove(this);

            topRectangle.MouseDown -= node_MouseDown;

            deleteButton.MouseDown -= deleteButton_MouseDown;

            contentTextBox.TextChanged -= contentTextBox_TextChanged;
            contentTextBox.GotFocus -= node_GotFocus;

            typeComboBox.GotFocus -= node_GotFocus;
            typeComboBox.SelectionChanged -= typeComboBox_SelectionChanged;

            bgCanvas.MouseDown -= node_MouseDown;
            bgCanvas.GotFocus -= node_GotFocus;

            resizeThumb1.DragDelta -= resizeThumb1_DragDelta;
            resizeThumb1.DragCompleted -= resizeThumb_DragCompleted;
            resizeThumb2.DragDelta -= resizeThumb2_DragDelta;
            resizeThumb2.DragCompleted -= resizeThumb_DragCompleted;
            resizeThumb3.DragDelta -= resizeThumb3_DragDelta;
            resizeThumb3.DragCompleted -= resizeThumb_DragCompleted;
            resizeThumb4.DragDelta -= resizeThumb4_DragDelta;
            resizeThumb4.DragCompleted -= resizeThumb_DragCompleted;
            resizeThumb5.DragDelta -= resizeThumb5_DragDelta;
            resizeThumb5.DragCompleted -= resizeThumb_DragCompleted;
            resizeThumb6.DragDelta -= resizeThumb6_DragDelta;
            resizeThumb6.DragCompleted -= resizeThumb_DragCompleted;
            resizeThumb7.DragDelta -= resizeThumb7_DragDelta;
            resizeThumb7.DragCompleted -= resizeThumb_DragCompleted;
            resizeThumb8.DragDelta -= resizeThumb8_DragDelta;
            resizeThumb8.DragCompleted -= resizeThumb_DragCompleted;

            bgCanvas.PreviewMouseRightButtonDown -= Control_MouseRightButtonDown;
            typeComboBox.PreviewMouseRightButtonDown -= Control_MouseRightButtonDown;
            contentTextBox.PreviewMouseRightButtonDown -= Control_MouseRightButtonDown;
            deleteButton.PreviewMouseRightButtonDown -= Control_MouseRightButtonDown;
            topRectangle.PreviewMouseRightButtonDown -= Control_MouseRightButtonDown;
            focusRectangle1.PreviewMouseRightButtonDown -= Control_MouseRightButtonDown;
            resizeThumb1.PreviewMouseRightButtonDown -= Control_MouseRightButtonDown;
            resizeThumb2.PreviewMouseRightButtonDown -= Control_MouseRightButtonDown;
            resizeThumb3.PreviewMouseRightButtonDown -= Control_MouseRightButtonDown;
            resizeThumb4.PreviewMouseRightButtonDown -= Control_MouseRightButtonDown;
            resizeThumb5.PreviewMouseRightButtonDown -= Control_MouseRightButtonDown;
            resizeThumb6.PreviewMouseRightButtonDown -= Control_MouseRightButtonDown;
            resizeThumb7.PreviewMouseRightButtonDown -= Control_MouseRightButtonDown;
            resizeThumb8.PreviewMouseRightButtonDown -= Control_MouseRightButtonDown;

            for (int i = 0; i < ArrowsFromMe.Count; i++)
            {
                ArrowsFromMe[i].Dispose();
                i--;//화살표의 dispose가 ArrowFromMe에서 그 화살표를 빼게 하므로, i값을 땡겨야함.
            }
            for (int i = 0; i < ArrowsFromOther.Count; i++)
            {
                ArrowsFromOther[i].Dispose();
                i--;//화살표의 dispose가 ArrowFromOther에서 그 화살표를 빼게 하므로, i값을 땡겨야함.
            }
        }

        private void UpdatePosition(object sender, EventArgs e)
        {
            Canvas.SetLeft(this, MainCanvas.GetMousePos().X + DeltaMousePoint.X);
            Canvas.SetTop(this, MainCanvas.GetMousePos().Y + DeltaMousePoint.Y);

            SetArrowPos();

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

        private void RepositionElements(object sender, EventArgs e)
        {
            RepositionElements();
        }
        public void RepositionElements()
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
            Canvas.SetTop(deleteButton, 60);

            focusRectangle1.Width = Width;
            focusRectangle1.Height = Height;

            topRectangle.Width = Width;

            typeComboBox.Width = Width-79;

            contentTextBox.Width = Width-30;
            contentTextBox.Height = Height-105;

            SetArrowPos();
        }

        private void resizeThumb1_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double newWidth = this.Width - e.HorizontalChange;
            double newHeight = this.Height - e.VerticalChange;

            if (newWidth > 250)
            {
                Canvas.SetLeft(this, Canvas.GetLeft(this) + e.HorizontalChange);
                Width = newWidth;
            }

            if (newHeight > 150)
            {
                Canvas.SetTop(this, Canvas.GetTop(this) + e.VerticalChange);
                Height = newHeight;
            }

            RepositionElements();
        }
        private void resizeThumb2_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double newWidth = this.Width + e.HorizontalChange;
            double newHeight = this.Height - e.VerticalChange;

            if (newWidth > 250)
            {
                Width = newWidth;
            }

            if (newHeight > 150)
            {
                Canvas.SetTop(this, Canvas.GetTop(this) + e.VerticalChange);
                Height = newHeight;
            }
            RepositionElements();
        }
        private void resizeThumb3_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double newWidth = this.Width - e.HorizontalChange;
            double newHeight = this.Height + e.VerticalChange;

            if (newWidth > 250)
            {
                Canvas.SetLeft(this, Canvas.GetLeft(this) + e.HorizontalChange);
                Width = newWidth;
            }

            if (newHeight > 150)
            {
                Height = newHeight;
            }
            RepositionElements();
        }
        private void resizeThumb4_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double newWidth = this.Width + e.HorizontalChange;
            double newHeight = this.Height + e.VerticalChange;

            if (newWidth > 250)
            {
                Width = newWidth;
            }

            if (newHeight > 150)
            {
                Height = newHeight;
            }
            RepositionElements();
        }
        private void resizeThumb5_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double newWidth = this.Width - e.HorizontalChange;

            if (newWidth > 250)
            {
                Canvas.SetLeft(this, Canvas.GetLeft(this) + e.HorizontalChange);
                Width = newWidth;
            }

            RepositionElements();
        }
        private void resizeThumb6_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double newHeight = this.Height + e.VerticalChange;

            if (newHeight > 150)
            {
                Height = newHeight;
            }
            RepositionElements();
        }
        private void resizeThumb7_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double newWidth = this.Width + e.HorizontalChange;

            if (newWidth > 250)
            {
                Width = newWidth;
            }
            RepositionElements();
        }
        private void resizeThumb8_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double newHeight = this.Height - e.VerticalChange;

            if (newHeight > 150)
            {
                Canvas.SetTop(this, Canvas.GetTop(this) + e.VerticalChange);
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
                DeltaMousePoint = new Point(Canvas.GetLeft(this) - MainCanvas.GetMousePos().X, Canvas.GetTop(this) - MainCanvas.GetMousePos().Y);
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

        public void SetType(object selectedValue)
        {
            typeComboBox.SelectedValue = selectedValue;
        }

        public void SetContent(string text)
        {
            contentTextBox.Text = text;
        }
        private void typeComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (typeComboBox.IsFocused)
            {
                Inspector.SetType(typeComboBox.SelectedValue);
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
            if (!SettingNodeTypeItems)
            {
                if (FocusedNode != null && FocusedNode.typeComboBox.SelectedIndex >= 0)
                {
                    FocusedNode.focusRectangle1.Stroke = ProjectProperty.NodeTypes[FocusedNode.typeComboBox.SelectedIndex].Color;
                }
                SetTypeItems();
                Inspector.SetType(typeComboBox.SelectedValue);
                Inspector.SetContent(contentTextBox.Text);

                FocusedNode = this;
                SetColor();
            }
        }

        public void SetTypeItems()
        {
            SettingNodeTypeItems = true;

            object obj = null;
            if (typeComboBox.SelectedIndex >= 0)
            {
                obj = typeComboBox.Items[typeComboBox.SelectedIndex];
            }
            string name = null;

            if (obj != null)
            {
                name = obj.ToString();
            }

            for (int i = 0; i < typeComboBox.Items.Count; i++)
            {
                typeComboBox.Items.RemoveAt(i);
                i--;
            }

            for (int i = 0; i < ProjectProperty.NodeTypes.Count; i++)
            {
                typeComboBox.Items.Add(ProjectProperty.NodeTypes[i].Name);
            }

            if(name != null)
            {
                typeComboBox.SelectedValue = name;
            }

            SettingNodeTypeItems = false;
        }
        public void node_GotFocus(object sender, RoutedEventArgs e)
        {
            node_GotFocus();
        }

        private void typeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (typeComboBox.IsFocused)
            {
                Inspector.SetType(typeComboBox.SelectedValue);
            }

            SetColor();
        }

        public void SetColor()
        {
            if (typeComboBox.SelectedIndex >= 0)
            {
                SolidColorBrush color = ProjectProperty.NodeTypes[typeComboBox.SelectedIndex].Color;
                bgCanvas.Background = color;
                topRectangle.Fill = new SolidColorBrush(Color.FromArgb(255, (Byte)((double)color.Color.R * 0.8), (Byte)((double)color.Color.G * 0.8), (Byte)((double)color.Color.B * 0.8)));

                if (FocusedNode == this)
                {
                    focusRectangle1.Stroke = new SolidColorBrush(Color.FromArgb(255, (Byte)((double)color.Color.R * 0.3), (Byte)((double)color.Color.G * 0.3), (Byte)((double)color.Color.B * 0.3)));
                }
                else
                {
                    focusRectangle1.Stroke = color;
                }
            }
        }

        private void Control_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(FocusedNode != this && FocusedNode != null)
            {
                NodeArrow nodeArrow = new NodeArrow(FocusedNode,this);
                Panel.SetZIndex(nodeArrow, 90);

                MainWindow._MainWindow.mainCanvas.mainCanvas.Children.Add(nodeArrow);
                ArrowsFromOther.Add(nodeArrow);
                FocusedNode.ArrowsFromMe.Add(nodeArrow);
            }

            node_GotFocus();
            e.Handled = true;
        }

        private void SetArrowPos()
        {
            for (int i = 0; i < ArrowsFromMe.Count; i++)
            {
                ArrowsFromMe[i].SetArrow();
            }

            for (int i = 0; i < ArrowsFromOther.Count; i++)
            {
                ArrowsFromOther[i].SetArrow();
            }
        }
    }
}
