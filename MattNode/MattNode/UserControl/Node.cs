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

        private Point InitNameBoxLocation;
        private Size InitNameBoxSize;
        private float InitNameBoxFontSize;

        private Point InitTextBoxLocation;
        private Size InitTextBoxSize;
        private float InitTextBoxFontSize;

        private Point InitTypeBoxLocation;
        private Size InitTypeBoxSize;
        private float InitTypeBoxFontSize;
        public Node()
        {
            InitializeComponent();
            InitSize = Size;

            InitTextBoxLocation = textBox.Location;
            InitTextBoxSize = textBox.Size;
            InitTextBoxFontSize = textBox.Font.Size;

            InitNameBoxLocation = NameBox.Location;
            InitNameBoxSize = NameBox.Size;
            InitNameBoxFontSize = NameBox.Font.Size;

            InitTypeBoxLocation = TypeBox.Location;
            InitTypeBoxSize = TypeBox.Size;
            InitTypeBoxFontSize = TypeBox.Font.Size;
        }
        public override void SetSize(float size)
        {
            Size = new Size((int)(((float)InitSize.Width) / size), (int)(((float)InitSize.Height) / size));

            textBox.Size = new Size((int)(((float)InitTextBoxSize.Width) / size), (int)(((float)InitTextBoxSize.Height) / size));
            textBox.Location = new Point((int)(((float)InitTextBoxLocation.X) / size), (int)(((float)InitTextBoxLocation.Y) / size));
            textBox.Font = new(textBox.Font.FontFamily, InitTextBoxFontSize / size);

            NameBox.Size = new Size((int)(((float)InitNameBoxSize.Width) / size), (int)(((float)InitNameBoxSize.Height) / size));
            NameBox.Location = new Point((int)(((float)InitNameBoxLocation.X) / size), (int)(((float)InitNameBoxLocation.Y) / size));
            NameBox.Font = new(textBox.Font.FontFamily, InitNameBoxFontSize / size);

            TypeBox.Size = new Size((int)(((float)InitTypeBoxSize.Width) / size), (int)(((float)InitTypeBoxSize.Height) / size));
            TypeBox.Location = new Point((int)(((float)InitTypeBoxLocation.X) / size), (int)(((float)InitTypeBoxLocation.Y) / size));
            TypeBox.Font = new(textBox.Font.FontFamily, InitTypeBoxFontSize / size);
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

        private void textBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
