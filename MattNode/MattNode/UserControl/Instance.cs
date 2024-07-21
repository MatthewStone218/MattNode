using MattNode;
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
    public partial class Instance : UserControl
    {
        public static List<Instance> InstanceList = new List<Instance>();
        public int x;
        public int y;
        public Instance()
        {
            InitializeComponent();
            InstanceList.Add(this);
        }

        public static void BringInstancesToFront()
        {
            for (int i = 0; i < InstanceList.Count; i++)
            {
                InstanceList[i].BringToFront();
            }
        }

        public void SetPosition()
        {
            x = (int)((float)(Location.X - Form1.WindowWidth / 2) * Camera.size) + Camera.Position.X;
            y = (int)((float)(Location.Y - Form1.WindowHeight / 2) * Camera.size) + Camera.Position.Y;
            //x = Camera.Position.X - Form1.WindowWidth/2 + Location.X * Camera.size;
            //y = Camera.Position.Y - Form1.WindowHeight/2 + Location.Y * Camera.size;
        }

        public virtual void SetSize(float size)
        {
        }
    }
}
