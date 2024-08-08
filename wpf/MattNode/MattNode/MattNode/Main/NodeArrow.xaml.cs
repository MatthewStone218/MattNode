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
    /// NodeArrow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NodeArrow : Arrow
    {
        public Node StartNode;
        public Node EndNode;
        public NodeArrow(Node startNode, Node endNode)
        {
            EnabledInstanceList.Add(this);
            StartNode = startNode;
            EndNode = endNode;
            InitializeComponent();
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;

            ArrowLine = arrowLine;
            ArrowHead1 = arrowHead1;
            ArrowHead2 = arrowHead2;

            SetArrow();
        }
        public override void Dispose()
        {
            base.Dispose();
            StartNode.ArrowsFromMe.Remove(this);
            EndNode.ArrowsFromOther.Remove(this);
            EnabledInstanceList.Remove(this);
        }

        public void SetArrow()
        {
            double x1 = Canvas.GetLeft(StartNode) + StartNode.Width / 2;
            double y1 = Canvas.GetTop(StartNode) + StartNode.Height / 2;

            double x2 = Canvas.GetLeft(EndNode) + EndNode.Width / 2;
            double y2 = Canvas.GetTop(EndNode) + EndNode.Height / 2;

            double margin = 10;

            double left = Canvas.GetLeft(EndNode) - margin;
            double right = Canvas.GetLeft(EndNode) + EndNode.Width + margin;
            double top = Canvas.GetTop(EndNode) - margin;
            double bottom = Canvas.GetTop(EndNode) + EndNode.Height + margin;

            double goalX = x2;
            double goalY = y2;

            if (x1 < x2)
            {
                double yAtLeft = GetYInLine(left, x1, y1, x2, y2);
                if (top <= yAtLeft && yAtLeft <= bottom)
                {
                    goalX = left;
                    goalY = yAtLeft;
                }
                else
                {
                    if(y1 < y2)
                    {
                        goalX = GetXInLine(top, x1, y1, x2, y2);
                        goalY = top;
                    }
                    else
                    {
                        goalX = GetXInLine(bottom, x1, y1, x2, y2);
                        goalY = bottom;
                    }
                }
            }
            else
            {
                double yAtRight = GetYInLine(right, x1, y1, x2, y2);
                if (top <= yAtRight && yAtRight <= bottom)
                {
                    goalX = right;
                    goalY = yAtRight;
                }
                else
                {
                    if (y1 < y2)
                    {
                        goalX = GetXInLine(top, x1, y1, x2, y2);
                        goalY = top;
                    }
                    else
                    {
                        goalX = GetXInLine(bottom, x1, y1, x2, y2);
                        goalY = bottom;
                    }
                }
            }

            SetArrow(x1, y1, goalX, goalY);
        }

        private Point GetLocalPosFromMainCanvas(Point point)
        {
            return new Point(point.X - Canvas.GetLeft(this), point.Y - Canvas.GetTop(this));
        }

        private double GetYInLine(double x, double x1, double y1, double x2, double y2)
        {
            return (x-x1)*(y2-y1)/(x2-x1) + y1;
        }
        private double GetXInLine(double y, double x1, double y1, double x2, double y2)
        {
            return (y-y1)*(x2-x1)/(y2-y1) + x1;
        }
    }
}
