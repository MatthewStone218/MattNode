using System.Diagnostics.PerformanceData;
using System.Windows.Forms;

namespace MattNode
{
    public partial class Form1 : Form
    {
        public static Form1? MainForm;
        public static Camera? MainCamera;
        public static int WindowWidth = 1920;
        public static int WindowHeight = 1080;

        protected bool clicked = false;
        protected Point MousePrevPoint;
        public Form1()
        {
            MainForm = this;
            GlobalHooks.Start();
            InitializeComponent();
            SetInstancesPosition();
            MainCamera = MainCamera1;
            EnableStep();
            MainCamera.EnableStep();
            screenDragger1.EnableStep();
            inspector.EnableStep();
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

        private void newNodeButton_Click(object sender, EventArgs e)
        {
            Node node = new Node();
            node.Location = new Point(0,0);
            node.Size = new Size(413, 299);
            Controls.Add(node);
            node.BringToFront();
            node.SetPosition();
            //node.Clicked();

            Inspector.MainInspector.BringToFront();
        }
    }
}