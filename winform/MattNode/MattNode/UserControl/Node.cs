using Microsoft.VisualBasic.Logging;
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
using System.Xml.Linq;

namespace MattNode
{
    public partial class Node : Instance
    {
        public static long IdxCount = 0;
        public long Idx;

        private bool NoneTextFocus = false;

        public static Node? FocusedNode;
        public static Node? LinkingNode = null;
        public static short ClickedPos = 0;

        public List<LinkLine> LinkLines = new List<LinkLine>();
        public List<LinkLine> InvLinkLines = new List<LinkLine>();

        private bool clicked = false;
        private Point PositionToCursor;
        private bool MouseLeftDownPrev = false;

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

        private Point InitNameLabelLocation;
        private Size InitNameLabelSize;
        private float InitNameLabelFontSize;

        private Point InitTypeLabelLocation;
        private Size InitTypeLabelSize;
        private float InitTypeLabelFontSize;
        public Node()
        {
            Idx = IdxCount;
            IdxCount++;

            Initialize();
        }
        public Node(long idx)
        {
            Idx = idx;
            if (IdxCount < Idx) { IdxCount = Idx; }
            Initialize();
        }

        private void Initialize()
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

            InitNameLabelLocation = NameLabel.Location;
            InitNameLabelSize = NameLabel.Size;
            InitNameLabelFontSize = NameLabel.Font.Size;

            InitTypeLabelLocation = TypeLabel.Location;
            InitTypeLabelSize = TypeLabel.Size;
            InitTypeLabelFontSize = TypeLabel.Font.Size;
        }

        private void DestroyLink(int index)
        {
            LinkLines[index].Dispose();
        }

        private void DestroyInvLink(int index)
        {
            InvLinkLines[index].Dispose();
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

            NameLabel.Size = new Size((int)(((float)InitNameLabelSize.Width) / size), (int)(((float)InitNameLabelSize.Height) / size));
            NameLabel.Location = new Point((int)(((float)InitNameLabelLocation.X) / size), (int)(((float)InitNameLabelLocation.Y) / size));
            NameLabel.Font = new(NameLabel.Font.FontFamily, InitNameLabelFontSize / size);

            TypeLabel.Size = new Size((int)(((float)InitTypeLabelSize.Width) / size), (int)(((float)InitTypeLabelSize.Height) / size));
            TypeLabel.Location = new Point((int)(((float)InitTypeLabelLocation.X) / size), (int)(((float)InitTypeLabelLocation.Y) / size));
            TypeLabel.Font = new(TypeLabel.Font.FontFamily, InitTypeLabelFontSize / size);
        }

        private void SetPositionToCursor()
        {
            PositionToCursor = new Point(x - (int)((float)(Form1.MainForm.PointToClient(Cursor.Position).X - Camera.Position.X - Form1.WindowWidth / 2) * Camera.size), y - (int)((float)(Form1.MainForm.PointToClient(Cursor.Position).Y - Camera.Position.Y - Form1.WindowHeight / 2) * Camera.size));
        }
        private void Drag_MouseDown()
        {
            if (!GlobalHooks.KeyboardSpaceDown)
            {
                clicked = true;
                SetPositionToCursor();
            }
            ShowInspector();
            Focused();
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
                x = (int)((float)(Form1.MainForm.PointToClient(Cursor.Position).X - Camera.Position.X - Form1.WindowWidth / 2) * Camera.size) + PositionToCursor.X;
                y = (int)((float)(Form1.MainForm.PointToClient(Cursor.Position).Y - Camera.Position.Y - Form1.WindowHeight / 2) * Camera.size) + PositionToCursor.Y;
            }
        }

        private void ShowInspector()
        {
            FocusedNode = this;
            Inspector.MainInspector.SetName(NameBox.Text);
            Inspector.MainInspector.SetType(TypeBox.Text);
            Inspector.MainInspector.SetText(textBox.Text);
            Inspector.MainInspector.EnableInspector();
        }
        private void ShowInspector(object sender, EventArgs e)
        {
            ShowInspector();
            Focused();
        }

        public void SetName(string text)
        {
            NameBox.Text = text;
        }

        private void NameBox_TextChanged(object sender, EventArgs e)
        {
            if (NameBox.Focused)
            {
                Inspector.MainInspector.SetName(NameBox.Text);
            }
        }
        public void SetType(string text)
        {
            TypeBox.Text = text;
        }

        private void TypeBox_TextChanged(object sender, EventArgs e)
        {
            if (TypeBox.Focused)
            {
                Inspector.MainInspector.SetType(TypeBox.Text);
            }
        }
        public void SetText(string text)
        {
            textBox.Text = text;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (textBox.Focused)
            {
                Inspector.MainInspector.SetText(textBox.Text);
            }
        }

        private void NameLabel_Click(object sender, EventArgs e)
        {

        }

        private void Focused()
        {
            BringToFront();
            UI.BringUiToFront();
        }
        private void Focused(object sender, EventArgs e)
        {
            Focused();
        }
        public void EnableMouseTracking()
        {
            step.Enabled = true;
            SetPositionToCursor();
        }
        private void step_Step(object sender, EventArgs e)
        {
            if (!GlobalHooks.MouseLeftDown && MouseLeftDownPrev)
            {
                step.Enabled = false;
            }
            else
            {
                x = (int)((float)(Form1.MainForm.PointToClient(Cursor.Position).X - Camera.Position.X - Form1.WindowWidth / 2) * Camera.size) + PositionToCursor.X;
                y = (int)((float)(Form1.MainForm.PointToClient(Cursor.Position).Y - Camera.Position.Y - Form1.WindowHeight / 2) * Camera.size) + PositionToCursor.Y;
            }

            MouseLeftDownPrev = GlobalHooks.MouseLeftDown;
        }

        private void LinkButton_Click(short pos)
        {
            if (LinkingNode == null)
            {
                StartLink(pos);
            }
            else
            {
                Link(pos);
            }
        }
        private void StartLink(short pos)
        {
            LinkingNode = this;
            ClickedPos = pos;
        }

        private void Link(short pos)
        {
            if (LinkingNode != this)
            {
                bool AlreadyLinked = false;
                for (int i = 0; i < LinkingNode.LinkLines.Count; i++)
                {
                    if (LinkingNode.LinkLines[i].LinkedNode2 == this)
                    {
                        LinkingNode.LinkLines[i].Dispose();
                        break;
                    }
                }

                LinkLine linkLinke = new LinkLine(ClickedPos, LinkingNode, pos, this);
                linkLinke.Location = new Point(0, 0);
                linkLinke.Size = new Size(300, 300);
                Form1.MainForm.Controls.Add(linkLinke);
                linkLinke.BringToFront();

                UI.BringUiToFront();

                LinkingNode.LinkLines.Add(linkLinke);
                InvLinkLines.Add(linkLinke);

                linkLinke.BringToFront();
                Instance.BringInstancesToFront();
                UI.BringUiToFront();

                LinkingNode = null;

                ScreenDragger.MainScreenDrager.UpdatePaint();
            }
            else
            {
                LinkingNode = null;
            }
        }

        private void linkButtonTop_Click(object sender, EventArgs e)
        {
            LinkButton_Click(8);
        }

        private void linkButtonLeftTop_Click(object sender, EventArgs e)
        {
            LinkButton_Click(1);
        }

        private void linkButtonLeft_Click(object sender, EventArgs e)
        {
            LinkButton_Click(2);
        }

        private void linkButtonBottomLeft_Click(object sender, EventArgs e)
        {
            LinkButton_Click(3);
        }

        private void linkButtonBottom_Click(object sender, EventArgs e)
        {
            LinkButton_Click(4);
        }

        private void linkButtonBottomRight_Click(object sender, EventArgs e)
        {
            LinkButton_Click(5);
        }

        private void linkButtonRight_Click(object sender, EventArgs e)
        {
            LinkButton_Click(6);
        }

        private void linkButtonTopRight_Click(object sender, EventArgs e)
        {
            LinkButton_Click(7);
        }

        private void NoneTextFocused(object sender, MouseEventArgs e)
        {
            this.BackColor = Color.FromArgb(235, 235, 250);
            NoneTextFocus = true;
            Focused();
            Focus();
            Drag_MouseDown();
        }
        private void NoneTextFocusLeaved()
        {
            this.BackColor = Color.FromArgb(240, 240, 240); ;
            NoneTextFocus = false;
        }
        private void NoneTextFocusLeaved(object sender, EventArgs e)
        {
            NoneTextFocusLeaved();
        }

        private void node_KeyDown(object sender, KeyEventArgs e)
        {
            if (NoneTextFocus || e.KeyCode == Keys.Delete)
            {
                Dispose();
                GC.Collect();
            }
        }

        private void NoneTextFocused(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(235, 235, 250);
            NoneTextFocus = true;
            Focused();
            Drag_MouseDown();
        }
    }
}
