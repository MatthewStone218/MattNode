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
        public static float size = 1.0f;
        private static float sizeGoal = 1.0f;
        private bool CameraResized = false;
        private Point PositionPrev = new Point(0, 0);
        public static Point Position = new Point(0, 0);
        public Camera()
        {
            InitializeComponent();

            Action<int> func = AdjustMouseSizeWithWheel;
            GlobalHooks.AddCallbackMouseWheel(func);
        }

        public void EnableStep()
        {
            Step.Enabled = true;
        }
        private void AdjustMouseSizeWithWheel(int _size)
        {
            if (GlobalHooks.KeyboardCtrlDown)
            {
                float delta = -(float)(_size >> 16) / 1200.0f;
                sizeGoal += delta;
                if (sizeGoal < 0.1f) { sizeGoal = 0.1f; }
            }
        }

        private void Camera_Load(object sender, EventArgs e)
        {

        }

        private void Camera_Tick(object sender, EventArgs e)
        {
            size += (sizeGoal - size) / 4.0f;
            if (Math.Abs((double)(sizeGoal - size)) < 0.02) { size = sizeGoal; CameraResized = false; } else { CameraResized = true; }
            Position = new Point(x,y);

            bool NeedUpdate = false;

            for (int i = 0; i < Instance.InstanceList.Count; i++)
            {
                bool CameraMoved = Position.X != PositionPrev.X || Position.Y != PositionPrev.Y || CameraResized;
                if (CameraMoved || Instance.InstanceList[i].Location.X != (int)((float)(Instance.InstanceList[i].x - x) / size) + (int)((float)Form1.WindowWidth / 2) || Instance.InstanceList[i].Location.Y != (int)((float)(Instance.InstanceList[i].y - y) / size) + (int)((float)Form1.WindowHeight / 2))
                {
                    NeedUpdate = true;
                    //MessageBox.Show(Position.X.ToString()+" "+ Position.Y.ToString());
                    Instance.InstanceList[i].Location = new Point((int)((float)(Instance.InstanceList[i].x - x) / size) + (int)((float)Form1.WindowWidth / 2), (int)((float)(Instance.InstanceList[i].y - y) / size) + (int)((float)Form1.WindowHeight / 2));
                    Instance.InstanceList[i].SetSize(size);
                    Instance.InstanceList[i].Refresh();
                }
            }

            if (NeedUpdate) { ScreenDragger.MainScreenDrager.UpdatePaint(); }
            PositionPrev = Position;
        }
    }
}
