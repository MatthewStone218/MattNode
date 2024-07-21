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
    public partial class Inspector : UI
    {
        public static Inspector MainInspector;
        public static int BboxRight = 0;
        private bool Clicked = false;
        private int InitDragpanelX;
        private int InitTextBoxWidth;
        private int InitTypeBoxWidth;
        private int InitNameBoxWidth;
        public Inspector()
        {
            Visible = false;
            MainInspector = this;
            InitializeComponent();
            InitDragpanelX = dragPanel.Location.X;
            InitTextBoxWidth = textBox.Width;
            InitTypeBoxWidth = typeBox.Width;
            InitNameBoxWidth = nameBox.Width;
        }

        private void dragPanel_MouseDown(object sender, MouseEventArgs e)
        {
            Clicked = true;
        }
        private void dragPanel_MouseUp(object sender, MouseEventArgs e)
        {
            Clicked = false;
        }
        private void dragPanel_MouseLeave(object sender, EventArgs e)
        {
            Clicked = false;
        }
        private void StepTick(object sender, EventArgs e)
        {
            if (Clicked)
            {
                dragPanel.Location = new Point(Cursor.Position.X-dragPanel.Size.Width/2,0);
                Size = new Size(dragPanel.Location.X + dragPanel.Size.Width, Form1.WindowHeight);

                int DWidth = dragPanel.Location.X - InitDragpanelX;

                textBox.Width = InitTextBoxWidth + DWidth;
                typeBox.Width = InitTypeBoxWidth + DWidth;
                nameBox.Width = InitNameBoxWidth + DWidth;
            }

            Size = new Size(Size.Width, Form1.WindowHeight);
            BboxRight = dragPanel.Location.X + dragPanel.Size.Width;
        }

        public void EnableStep()
        {
            Step.Enabled = true;
        }

        public void EnableInspector()
        {
            Visible = true;
        }
        public void DisableInspector()
        {
            Visible = false;
        }

        private void NameBox_TextChanged(object sender, EventArgs e)
        {
            if (nameBox.Focused)
            {
                Node.FocusedNode.SetName(nameBox.Text);
            }
        }

        public void SetName(string text)
        {
            nameBox.Text = text;
        }
        private void TypeBox_TextChanged(object sender, EventArgs e)
        {
            if (typeBox.Focused)
            {
                Node.FocusedNode.SetType(typeBox.Text);
            }
        }

        public void SetType(string text)
        {
            typeBox.Text = text;
        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (textBox.Focused)
            {
                Node.FocusedNode.SetText(textBox.Text);
            }
        }

        public void SetText(string text)
        {
            textBox.Text = text;
        }
    }
}
