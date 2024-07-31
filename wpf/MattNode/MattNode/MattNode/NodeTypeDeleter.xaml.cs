using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MattNode
{
    /// <summary>
    /// NodeTypeDeleter.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NodeTypeDeleter : UserControl
    {
        public int Num;
        public NodeTypeDeleter(int num)
        {
            Num = num;
            InitializeComponent();
            DeleteNodesFromType(ProjectProperty.NodeTypes[Num].Name);
            ProjectProperty.NodeTypes.RemoveAt(Num);
            PropertyMenu.mainProperty.SetPropertyTypeNodes();
            CompositionTarget.Rendering += RenderTick;
        }

        private void DeleteNodesFromType(string name)
        {
            for(int i = 0; i < Node.NodeList.Count; i++)
            {
                if (Node.NodeList[i].typeComboBox.Text == name)
                {
                    Node.NodeList[i].Dispose();
                }
            }
        }

        private void RenderTick(object sender, EventArgs e)
        {
            if(Parent != null)
            {
                DoneWindow doneWindow = new DoneWindow();
                doneWindow.HorizontalAlignment = HorizontalAlignment.Left;
                doneWindow.VerticalAlignment = VerticalAlignment.Top;
                Canvas.SetTop(doneWindow, 0);
                Canvas.SetBottom(doneWindow, 0);
                Grid.SetZIndex(doneWindow, 4000);
                MainWindow._MainWindow.mainGrid.Children.Add(doneWindow);

                Dispose();
            }
        }

        private void Dispose()
        {
            NodeTypeDeletionAsk.mainWindow.Dispose();
            CompositionTarget.Rendering -= RenderTick;
            ((Grid)Parent).Children.Remove(this);
        }
    }
}
