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
        private Point MousePrevPoint;
        public static Camera? MainCamera;
        public ScreenDragger()
        {
            InitializeComponent();
        }

        private void Drag_MouseDown(object sender, MouseEventArgs e)
        {
            clicked = true;
            MousePrevPoint = Cursor.Position;
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

            if (clicked)
            {
                MainCamera.Location = Point.Subtract(MainCamera.Location, new Size(Point.Subtract(Cursor.Position, new Size(MousePrevPoint))));
                MainCamera.x = MainCamera.Location.X;
                MainCamera.y = MainCamera.Location.Y;
                MousePrevPoint = Cursor.Position;
            }
        }
    }
}
