using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MattNode
{
    public partial class LinkLine : UserControl
    {
        public static List<LinkLine> LinkLines = new List<LinkLine>();

        public short Pos1;
        public short Pos2;
        public Point Point1;
        public Point Point2;
        public long LinkedNum1;
        public long LinkedNum2;
        public Node LinkedNode1;
        public Node LinkedNode2;

        private short LineStartPoint = 0;


        public LinkLine(short pos1, Node linked_node1, short pos2, Node linked_node2)
        {
            Pos1 = pos1;
            LinkedNode1 = linked_node1;
            Pos2 = pos2;
            LinkedNode2 = linked_node2;
            InitializeComponent();
            EnableStep();

            LinkLines.Add(this);
            Visible = false;

            step_Step();

            ScreenDragger.MainScreenDrager.UpdatePaint();
        }

        private void step_Step(object sender, EventArgs e)
        {
            step_Step();
        }
        private void step_Step()
        {
            int sep = 20;
            switch (Pos1)
            {
                case 1: Point1 = new Point(LinkedNode1.Location.X - sep, LinkedNode1.Location.Y - sep); break;
                case 2: Point1 = new Point(LinkedNode1.Location.X - sep, LinkedNode1.Location.Y + LinkedNode1.Height / 2); break;
                case 3: Point1 = new Point(LinkedNode1.Location.X - sep, LinkedNode1.Location.Y + LinkedNode1.Height + sep); break;
                case 4: Point1 = new Point(LinkedNode1.Location.X + LinkedNode1.Width / 2, LinkedNode1.Location.Y + LinkedNode1.Height + sep); break;
                case 5: Point1 = new Point(LinkedNode1.Location.X + LinkedNode1.Width + sep, LinkedNode1.Location.Y + LinkedNode1.Height + sep); break;
                case 6: Point1 = new Point(LinkedNode1.Location.X + LinkedNode1.Width + sep, LinkedNode1.Location.Y + LinkedNode1.Height / 2); break;
                case 7: Point1 = new Point(LinkedNode1.Location.X + LinkedNode1.Width + sep, LinkedNode1.Location.Y - sep); break;
                case 8: Point1 = new Point(LinkedNode1.Location.X + LinkedNode1.Width / 2, LinkedNode1.Location.Y - sep); break;
            }

            switch (Pos2)
            {
                case 1: Point2 = new Point(LinkedNode2.Location.X - sep, LinkedNode2.Location.Y - sep); break;
                case 2: Point2 = new Point(LinkedNode2.Location.X - sep, LinkedNode2.Location.Y + LinkedNode2.Height / 2); break;
                case 3: Point2 = new Point(LinkedNode2.Location.X - sep, LinkedNode2.Location.Y + LinkedNode2.Height + sep); break;
                case 4: Point2 = new Point(LinkedNode2.Location.X + LinkedNode2.Width / 2, LinkedNode2.Location.Y + LinkedNode2.Height + sep); break;
                case 5: Point2 = new Point(LinkedNode2.Location.X + LinkedNode2.Width + sep, LinkedNode2.Location.Y + LinkedNode2.Height + sep); break;
                case 6: Point2 = new Point(LinkedNode2.Location.X + LinkedNode2.Width + sep, LinkedNode2.Location.Y + LinkedNode2.Height / 2); break;
                case 7: Point2 = new Point(LinkedNode2.Location.X + LinkedNode2.Width + sep, LinkedNode2.Location.Y - sep); break;
                case 8: Point2 = new Point(LinkedNode2.Location.X + LinkedNode2.Width / 2, LinkedNode2.Location.Y - sep); break;
            }
        }

        private void EnableStep()
        {
            step.Enabled = true;
        }
    }
}
