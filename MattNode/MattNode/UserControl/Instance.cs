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

        ~Instance()
        {
            InstanceList.Remove(this);
        }

        public void SetPosition()
        {
            x = Location.X;
            y = Location.Y;
        }
    }
}
