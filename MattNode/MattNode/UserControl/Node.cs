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
        private Point PositionToCursor;
        private Size InitSize;
        private Point InitLabelLocation;
        private Size InitLabelSize;
        private float InitLabelFontSize;
        public Node()
        {
            InitializeComponent();
            InitSize = Size;
            InitLabelSize = label1.Size;
            InitLabelLocation = label1.Location;
            InitLabelFontSize = label1.Font.Size;
        }
        public override void SetSize(float size)
        {
            Size = new Size((int)(((float)InitSize.Width) / size), (int)(((float)InitSize.Height) / size));
            label1.Size = new Size((int)(((float)InitLabelSize.Width) / size), (int)(((float)InitLabelSize.Height) / size));
            label1.Location = new Point((int)(((float)InitLabelLocation.X) / size), (int)(((float)InitLabelLocation.Y) / size));
            label1.Font = new(label1.Font.FontFamily, InitLabelFontSize / size);
        }

        private void Drag_MouseDown(object sender, MouseEventArgs e)
        {
            if (!GlobalHooks.KeyboardSpaceDown)
            {
                clicked = true;
                PositionToCursor = new Point(x - (int)((float)Cursor.Position.X * Camera.size), y - (int)((float)Cursor.Position.Y * Camera.size));
            }
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
                x = (int)((float)Cursor.Position.X * Camera.size) + PositionToCursor.X;
                y = (int)((float)Cursor.Position.Y * Camera.size) + PositionToCursor.Y;
            }
        }
    }
}
