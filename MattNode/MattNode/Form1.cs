using System.Diagnostics.PerformanceData;

namespace MattNode
{
    public partial class Form1 : Form
    {
        public static Camera? MainCamera;
        public static int WindowWidth;
        public static int WindowHeight;
        protected bool clicked = false;
        protected Point MousePrevPoint;
        public Form1()
        {
            GlobalHooks.Start();
            InitializeComponent();
            SetInstancesPosition();
            MainCamera = MainCamera1;
            EnableStep();
            MainCamera.EnableStep();
            screenDragger1.EnableStep();
        }

        private void SetInstancesPosition()
        {
            for (int i = 0; i < Instance.InstanceList.Count; i++)
            {
                Instance.InstanceList[i].SetPosition();
            }
        }

        private void dragSpacePanel1_Load(object sender, EventArgs e)
        {

        }

        private void dragableControl1_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_Close(object sender, FormClosedEventArgs e)
        {
            GlobalHooks.Stop();
        }

        private void Step(object sender, EventArgs e)
        {
            WindowWidth = this.Width;
            WindowHeight = this.Height;
        }

        public void EnableStep()
        {
            Step1.Enabled = true;
        }
    }
}