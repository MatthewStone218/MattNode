using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MattNode
{
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
    /// <summary>
    /// Arrow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Arrow : Instance
    {
        protected Line ArrowLine;
        protected Line ArrowHead1;
        protected Line ArrowHead2;
        public Arrow()
        {
        }

        public void SetArrow(double x1, double y1, double x2, double y2)
        {
            if (x1 < x2)
            {
                Canvas.SetLeft(this, x1 - 5);
                Width = x2 - x1 + 10;
            }
            else
            {
                Canvas.SetLeft(this, x2 - 5);
                Width = x1 - x2 + 10;
            }

            if (y1 < y2)
            {
                Canvas.SetTop(this, y1 - 5);
                Height = y2 - y1 + 10;
            }
            else
            {
                Canvas.SetTop(this, y2 - 5);
                Height = y1 - y2 + 10;
            }

            Point startPoint = GetLocalPosFromMainCanvas(new Point(x1, y1));
            Point endPoint = GetLocalPosFromMainCanvas(new Point(x2, y2));

            ArrowLine.X1 = startPoint.X;
            ArrowLine.Y1 = startPoint.Y;
            ArrowLine.X2 = endPoint.X;
            ArrowLine.Y2 = endPoint.Y;
            
            ArrowHead1.X1 = endPoint.X;
            ArrowHead1.Y1 = endPoint.Y;

            ArrowHead1.X2 = ArrowHead1.X1 + Math.Cos(Math.Atan((ArrowLine.Y2 - ArrowLine.Y1) / (ArrowLine.X2 - ArrowLine.X1)) + (x1 < x2 ? (Math.PI * 3 / 4) : (Math.PI * 1 / 4))) * 15;
            ArrowHead1.Y2 = ArrowHead1.Y1 + Math.Sin(Math.Atan((ArrowLine.Y2 - ArrowLine.Y1) / (ArrowLine.X2 - ArrowLine.X1)) + (x1 < x2 ? (Math.PI * 3 / 4) : (Math.PI * 1 / 4))) * 15;
            
            ArrowHead2.X1 = endPoint.X;
            ArrowHead2.Y1 = endPoint.Y;

            ArrowHead2.X2 = ArrowHead2.X1 + Math.Cos(Math.Atan((ArrowLine.Y2 - ArrowLine.Y1) / (ArrowLine.X2 - ArrowLine.X1)) - (x1 < x2 ? (Math.PI * 3 / 4) : (Math.PI * 1 / 4))) * 15;
            ArrowHead2.Y2 = ArrowHead2.Y1 + Math.Sin(Math.Atan((ArrowLine.Y2 - ArrowLine.Y1) / (ArrowLine.X2 - ArrowLine.X1)) - (x1 < x2 ? (Math.PI * 3 / 4) : (Math.PI * 1 / 4))) * 15;

            ReregisterCollisionTree();
        }

        private Point GetLocalPosFromMainCanvas(Point point)
        {
            return new Point(point.X - Canvas.GetLeft(this), point.Y - Canvas.GetTop(this));
        }
    }
}
