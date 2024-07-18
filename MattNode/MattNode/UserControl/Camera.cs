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
    public partial class Camera : UserControl
    {
        public int x = 0;
        public int y = 0;
        public float size = 1.0f;
        public Camera()
        {
            InitializeComponent();
        }

        public void EnableStep()
        {
            Step.Enabled = true;
        }

        private void Camera_Load(object sender, EventArgs e)
        {

        }

        private void Camera_Tick(object sender, EventArgs e)
        {
            for(int i = 0; i < Instance.InstanceList.Count; i++)
            {
                Instance.InstanceList[i].Location = new Point((int)((float)(Instance.InstanceList[i].x-x)/size), (int)((float)(Instance.InstanceList[i].y-y)/size));
            }
        }
    }
}
