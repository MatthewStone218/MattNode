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
    public partial class NewNodeButton : UI
    {
        public NewNodeButton()
        {
            InitializeComponent();
        }

        private void newNodeButton_Click(object sender, EventArgs e)
        {
            Node node = new Node();
            node.Location = Point.Subtract(Cursor.Position,new Size(50,50));
            node.Size = new Size(413, 299);
            Form1.MainForm.Controls.Add(node);
            node.BringToFront();
            node.SetPosition();
            node.EnableMouseTracking();

            UI.BringUiToFront();
        }
    }
}
