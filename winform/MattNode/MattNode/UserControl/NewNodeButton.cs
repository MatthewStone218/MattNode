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
        public static WeakReference testCache;

        public NewNodeButton()
        {
            InitializeComponent();
        }

        private void newNodeButton_Click(object sender, EventArgs e)
        {
            Node node = new Node();
            node.Location = Point.Subtract(Form1.MainForm.PointToClient(Cursor.Position),new Size(50,70));
            node.Size = new Size(413, 299);
            Form1.MainForm.Controls.Add(node);
            node.BringToFront();
            node.SetPosition();
            node.EnableMouseTracking();

            UI.BringUiToFront();
        }

        private void testStep(object sender, EventArgs e)
        {
            if(testCache == null || !testCache.IsAlive) 
            {
                MessageBox.Show("1");
            }
        }

    }
}
