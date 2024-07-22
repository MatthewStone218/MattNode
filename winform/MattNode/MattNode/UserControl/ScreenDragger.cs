using Accessibility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MattNode
{
    public partial class ScreenDragger : UserControl
    {
        public static ScreenDragger? MainScreenDrager;

        private bool clicked = false;
        private Point MouseClickedPoint;
        private Point CameraPositionAtClick;
        private bool MouseLeftDownPrev = false;
        public ScreenDragger()
        {
            InitializeComponent();
            MainScreenDrager = this;
        }

        private void Drag_MouseDown(object sender, MouseEventArgs e)
        {
            Node.LinkingNode = null;

            clicked = true;
            MouseClickedPoint = Form1.MainForm.PointToClient(Cursor.Position);
            CameraPositionAtClick = Camera.Position;
            Inspector.MainInspector.DisableInspector();
        }

        private void Drag_MouseLeave(object sender, EventArgs e)
        {
            clicked = false;
        }

        private void Drag_MouseUp(object sender, EventArgs e)
        {
            clicked = false;
        }

        private void Drag_MouseMove(object sender, MouseEventArgs e)
        {
            if (clicked && !GlobalHooks.KeyboardSpaceDown)
            {
                Point delta = Point.Subtract(Form1.MainForm.PointToClient(Cursor.Position), new Size(MouseClickedPoint));
                Form1.MainCamera.Location = Point.Subtract(CameraPositionAtClick, new Size((int)((float)delta.X * Camera.size), (int)((float)delta.Y * Camera.size)));
                Form1.MainCamera.x = Form1.MainCamera.Location.X;
                Form1.MainCamera.y = Form1.MainCamera.Location.Y;
            }
        }

        private void Step(object sender, EventArgs e)
        {
            if (Inspector.BboxRight < Form1.MainForm.PointToClient(Cursor.Position).X)
            {
                if (!MouseLeftDownPrev && GlobalHooks.MouseLeftDown)
                {
                    MouseClickedPoint = Form1.MainForm.PointToClient(Cursor.Position);
                    CameraPositionAtClick = Camera.Position;
                }

                if (GlobalHooks.KeyboardSpaceDown && GlobalHooks.MouseLeftDown)
                {
                    Point delta = Point.Subtract(Form1.MainForm.PointToClient(Cursor.Position), new Size(MouseClickedPoint));
                    Form1.MainCamera.Location = Point.Subtract(CameraPositionAtClick, new Size((int)((float)delta.X * Camera.size), (int)((float)delta.Y * Camera.size)));
                    Form1.MainCamera.x = Form1.MainCamera.Location.X;
                    Form1.MainCamera.y = Form1.MainCamera.Location.Y;
                }
            }

            if (Node.LinkingNode != null) { UpdatePaint(); }

            MouseLeftDownPrev = GlobalHooks.MouseLeftDown;
        }

        public void EnableStep()
        {
            Step1.Enabled = true;
        }

        private void Arrow_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            using (Pen pen = new Pen(Color.White, 2))
            {
                for (int i = 0; i < LinkLine.LinkLines.Count; i++)
                {
                    pen.CustomEndCap = new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5);
                    g.DrawLine(pen, LinkLine.LinkLines[i].Point1.X, LinkLine.LinkLines[i].Point1.Y, LinkLine.LinkLines[i].Point2.X, LinkLine.LinkLines[i].Point2.Y);
                }

                if(Node.LinkingNode != null)
                {
                    Point Point1 = new Point(0,0);
                    int sep = 20;
                    switch (Node.ClickedPos)
                    {
                        case 1: Point1 = new Point(Node.LinkingNode.Location.X - sep, Node.LinkingNode.Location.Y - sep); break;
                        case 2: Point1 = new Point(Node.LinkingNode.Location.X - sep, Node.LinkingNode.Location.Y + Node.LinkingNode.Height / 2); break;
                        case 3: Point1 = new Point(Node.LinkingNode.Location.X - sep, Node.LinkingNode.Location.Y + Node.LinkingNode.Height + sep); break;
                        case 4: Point1 = new Point(Node.LinkingNode.Location.X + Node.LinkingNode.Width / 2, Node.LinkingNode.Location.Y + Node.LinkingNode.Height + sep); break;
                        case 5: Point1 = new Point(Node.LinkingNode.Location.X + Node.LinkingNode.Width + sep, Node.LinkingNode.Location.Y + Node.LinkingNode.Height + sep); break;
                        case 6: Point1 = new Point(Node.LinkingNode.Location.X + Node.LinkingNode.Width + sep, Node.LinkingNode.Location.Y + Node.LinkingNode.Height / 2); break;
                        case 7: Point1 = new Point(Node.LinkingNode.Location.X + Node.LinkingNode.Width + sep, Node.LinkingNode.Location.Y - sep); break;
                        case 8: Point1 = new Point(Node.LinkingNode.Location.X + Node.LinkingNode.Width / 2, Node.LinkingNode.Location.Y - sep); break;
                    }

                    pen.CustomEndCap = new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5);
                    g.DrawLine(pen, Point1.X, Point1.Y, Form1.MainForm.PointToClient(Cursor.Position).X, Form1.MainForm.PointToClient(Cursor.Position).Y);
                }
            }
        }

        public void UpdatePaint()
        {
            this.Invalidate(); // 전체 폼을 무효화
            this.Update(); // 무효화된 영역을 즉시 다시 그림
        }
    }
}
