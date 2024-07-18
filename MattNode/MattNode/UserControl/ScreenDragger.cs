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
        private bool clicked = false;
        private Point MouseClickedPoint;
        private Point CameraPositionAtClick;
        private bool MouseLeftDownPrev = false;
        public static Camera? MainCamera;
        public ScreenDragger()
        {
            InitializeComponent();
        }

        private void Drag_MouseDown(object sender, MouseEventArgs e)
        {
            clicked = true;
            MouseClickedPoint = Cursor.Position;
            CameraPositionAtClick = Camera.Position;
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
                MainCamera.Location = Point.Subtract(CameraPositionAtClick, new Size((int)((float)delta.X * Camera.size), (int)((float)delta.Y * Camera.size)));
                MainCamera.x = MainCamera.Location.X;
                MainCamera.y = MainCamera.Location.Y;
            }
        }

        private void Step(object sender, EventArgs e)
        {
            if(!MouseLeftDownPrev && GlobalHooks.MouseLeftDown)
            {
                MouseClickedPoint = Cursor.Position;
                CameraPositionAtClick = Camera.Position;
            }

            if (GlobalHooks.KeyboardSpaceDown && GlobalHooks.MouseLeftDown)
            {
                Point delta = Point.Subtract(Cursor.Position, new Size(MouseClickedPoint));
                MainCamera.Location = Point.Subtract(CameraPositionAtClick, new Size((int)((float)delta.X * Camera.size), (int)((float)delta.Y * Camera.size)));
                MainCamera.x = MainCamera.Location.X;
                MainCamera.y = MainCamera.Location.Y;
            }

            MouseLeftDownPrev = GlobalHooks.MouseLeftDown;
        }

        public void EnableStep()
        {
            Step1.Enabled = true;
        }
    }
}
