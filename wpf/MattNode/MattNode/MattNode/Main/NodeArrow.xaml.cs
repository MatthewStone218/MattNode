﻿using System;
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
using Xceed.Wpf.AvalonDock.Controls;

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
            CompositionTarget.Rendering += CheckCursor;
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
            CompositionTarget.Rendering -= CheckCursor;
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

            double startX = x1;
            double startY = y1;

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

            left = Canvas.GetLeft(StartNode) - margin;
            right = Canvas.GetLeft(StartNode) + StartNode.Width + margin;
            top = Canvas.GetTop(StartNode) - margin;
            bottom = Canvas.GetTop(StartNode) + StartNode.Height + margin;

            if (x1 < x2)
            {
                double yAtRight = GetYInLine(right, x1, y1, x2, y2);
                if (top <= yAtRight && yAtRight <= bottom)
                {
                    startX = right;
                    startY = yAtRight;
                }
                else
                {
                    if (y1 < y2)
                    {
                        startX = GetXInLine(bottom, x1, y1, x2, y2);
                        startY = bottom;
                    }
                    else
                    {
                        startX = GetXInLine(top, x1, y1, x2, y2);
                        startY = top;
                    }
                }
            }
            else
            {
                double yAtLeft = GetYInLine(left, x1, y1, x2, y2);
                if (top <= yAtLeft && yAtLeft <= bottom)
                {
                    startX = left;
                    startY = yAtLeft;
                }
                else
                {
                    if (y1 < y2)
                    {
                        startX = GetXInLine(bottom, x1, y1, x2, y2);
                        startY = bottom;
                    }
                    else
                    {
                        startX = GetXInLine(top, x1, y1, x2, y2);
                        startY = top;
                    }
                }
            }

            SetArrow(startX, startY, goalX, goalY);

            numLabel.Content = StartNode.ArrowsFromMe.IndexOf(this) + 1;
            Canvas.SetLeft(numLabel, (arrowLine.X1 + arrowLine.X2) / 2 - numLabel.Width / 2);
            Canvas.SetTop(numLabel, (arrowLine.Y1 + arrowLine.Y2) / 2 - numLabel.Height / 2);
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

        private void CheckCursor(object sender, EventArgs e)
        {
            numLabel.Content = StartNode.ArrowsFromMe.IndexOf(this) + 1;
            if (CheckLineCircleCollision(
                Canvas.GetLeft(this) + ArrowLine.X1, 
                Canvas.GetTop(this) + ArrowLine.Y1, 
                Canvas.GetLeft(this) + ArrowLine.X2, 
                Canvas.GetTop(this) + ArrowLine.Y2,
                MainCanvas.GetMousePos().X,
                MainCanvas.GetMousePos().Y,
                10))
            {
                ArrowLine.Stroke = new SolidColorBrush(Color.FromRgb(150, 0, 0));
                ArrowHead1.Stroke = new SolidColorBrush(Color.FromRgb(150, 0, 0));
                ArrowHead2.Stroke = new SolidColorBrush(Color.FromRgb(150, 0, 0));
                if (Mouse.RightButton == MouseButtonState.Pressed)
                {
                    Dispose();
                }
            }
            else
            {
                ArrowLine.Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                ArrowHead1.Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                ArrowHead2.Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
        }
        private bool CheckLineCircleCollision(double x1, double y1, double x2, double y2, double cx, double cy, double radius)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;
            double a = dx * dx + dy * dy;

            // a가 충분히 작으면 선분이 매우 짧다고 가정하여 직접 계산
            if (Math.Abs(a) < 1e-10)
            {
                return ((x1 - cx) * (x1 - cx) + (y1 - cy) * (y1 - cy) <= radius * radius) ||
                       ((x2 - cx) * (x2 - cx) + (y2 - cy) * (y2 - cy) <= radius * radius);
            }

            double b = 2 * (dx * (x1 - cx) + dy * (y1 - cy));
            double c = (x1 - cx) * (x1 - cx) + (y1 - cy) * (y1 - cy) - radius * radius;

            double det = b * b - 4 * a * c;
            if (det < 0)
            {
                return false;
            }
            else
            {
                double sqrtDet = Math.Sqrt(det);
                double t1 = (-b - sqrtDet) / (2 * a);
                double t2 = (-b + sqrtDet) / (2 * a);
                return (t1 >= 0 && t1 <= 1) || (t2 >= 0 && t2 <= 1);
            }
        }

        private void arrowCollisionLine_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
