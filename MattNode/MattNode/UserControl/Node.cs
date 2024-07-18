using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MattNode
{
    public partial class Node : Instance
    {
        private bool clicked = false;
        private Point MousePrevPoint;
        public Node()
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
                x += Cursor.Position.X - MousePrevPoint.X;
                y += Cursor.Position.Y - MousePrevPoint.Y;
                MousePrevPoint = Cursor.Position;
            }
        }
    }
}
