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

namespace MattNode
{
    /// <summary>
    /// Instance.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Instance : UserControl
    {
        public List<CollisionNode> Nodes = new List<CollisionNode>();
        public Instance()
        {
        }
        public Instance(double x, double y, double width, double height)
        {
            Margin = new Thickness(x, y, 0, 0);
            Width = width;
            Height = height;
        }

        public void InsertInCollisionTree()
        {
            CollisionTree.Instert(this);
        }
        public void DeleteFromCollisionTree()
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                Nodes[i].RemoveInstance(this);
            }
        }

        public virtual void Dispose()
        {
            DeleteFromCollisionTree();
            MainWindow._MainWindow.mainGrid.Children.Remove(this);
            MainWindow._MainWindow.mainCanvas.mainCanvas.Children.Remove(this);
        }
        public bool Intersects(Instance other)
        {
            return !(
                Margin.Left >= other.Margin.Left + other.Width ||
                Margin.Left + Width <= other.Margin.Left ||
                Margin.Top >= other.Margin.Top + other.Height ||
                Margin.Top + Height <= other.Margin.Top
                );
        }

        public bool Contains(Instance other)
        {
            return (
                Margin.Left < other.Margin.Left &&
                Margin.Left + Width > other.Margin.Left + other.Width &&
                Margin.Top < other.Margin.Top &&
                Margin.Top + Height > other.Margin.Top + other.Height
                );
        }
    }
}
