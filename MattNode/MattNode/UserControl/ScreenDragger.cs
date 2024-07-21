using Accessibility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            MouseClickedPoint = Cursor.Position;
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
                Point delta = Point.Subtract(Cursor.Position, new Size(MouseClickedPoint));
                Form1.MainCamera.Location = Point.Subtract(CameraPositionAtClick, new Size((int)((float)delta.X * Camera.size), (int)((float)delta.Y * Camera.size)));
                Form1.MainCamera.x = Form1.MainCamera.Location.X;
                Form1.MainCamera.y = Form1.MainCamera.Location.Y;
            }
        }

        private void Step(object sender, EventArgs e)
        {
            if (Inspector.BboxRight < Cursor.Position.X)
            {
                if (!MouseLeftDownPrev && GlobalHooks.MouseLeftDown)
                {
                    MouseClickedPoint = Cursor.Position;
                    CameraPositionAtClick = Camera.Position;
                }

                if (GlobalHooks.KeyboardSpaceDown && GlobalHooks.MouseLeftDown)
                {
                    Point delta = Point.Subtract(Cursor.Position, new Size(MouseClickedPoint));
                    Form1.MainCamera.Location = Point.Subtract(CameraPositionAtClick, new Size((int)((float)delta.X * Camera.size), (int)((float)delta.Y * Camera.size)));
                    Form1.MainCamera.x = Form1.MainCamera.Location.X;
                    Form1.MainCamera.y = Form1.MainCamera.Location.Y;
                }
            }

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
            }
        }

        public void UpdatePaint()
        {
            this.Invalidate(); // 전체 폼을 무효화
            this.Update(); // 무효화된 영역을 즉시 다시 그림
        }
    }
}
