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
using System.Xml.Linq;

namespace MattNode
{
    /// <summary>
    /// Instance.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Instance : UserControl
    {
        public static List<Instance> EnabledInstanceList = new List<Instance>();
        public List<CollisionNode> Nodes = new List<CollisionNode>();
        public bool _IsEnabled = true;
        public Instance()
        {
        }
        public Instance(double x, double y, double width, double height)
        {
            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
            Width = width;
            Height = height;
        }
        public void ReregisterCollisionTree()
        {
            DeleteFromCollisionTree();
            InsertInCollisionTree();
        }
        public void InsertInCollisionTree()
        {
            CollisionTree.Instert(this);
        }
        public void DeleteFromCollisionTree()
        {
            while(Nodes.Count > 0)
            {
                Nodes[0].RemoveInstance(this);
            }
        }

        public virtual void Dispose()
        {
            DeleteFromCollisionTree();
            EnabledInstanceList.Remove(this);
            if (MainWindow._MainWindow.mainGrid.Children.Contains(this)) { MainWindow._MainWindow.mainGrid.Children.Remove(this); }
            if (MainWindow._MainWindow.mainCanvas.mainCanvas.Children.Contains(this)) { MainWindow._MainWindow.mainCanvas.mainCanvas.Children.Remove(this); }
        }
        public bool Intersects(Instance other)
        {
            return !(
                Canvas.GetLeft(this) >= Canvas.GetLeft(other) + other.Width ||
                Canvas.GetLeft(this) + Width <= Canvas.GetLeft(other) ||
                Canvas.GetTop(this) >= Canvas.GetTop(other) + other.Height ||
                Canvas.GetTop(this) + Height <= Canvas.GetTop(other)
                );
        }

        public bool Contains(Instance other)
        {
            return (
                Canvas.GetLeft(this) < Canvas.GetLeft(other) &&
                Canvas.GetLeft(this) + Width > Canvas.GetLeft(other) + other.Width &&
                Canvas.GetTop(this) < Canvas.GetTop(other) &&
                Canvas.GetTop(this) + Height > Canvas.GetTop(other) + other.Height
                );
        }
    }
}
