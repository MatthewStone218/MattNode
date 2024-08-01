using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
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
    /// PropertyMenu.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PropertyMenu : UserControl
    {
        public static bool SettingNodes = false;
        public static PropertyMenu ?mainProperty = null;
        private List<PropertyOutputNode> PropertyOutputNodes = new List<PropertyOutputNode>();
        private List<PropertyTypeNode> PropertyTypeNodes = new List<PropertyTypeNode>();
        private double TypeNodeHeight = 0;
        public PropertyMenu()
        {
            mainProperty = this;
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            CompositionTarget.Rendering += RenderNodes;
            SetPropertyNodes();
            MainWindow._MainWindow.MouseWheel += ScrollMenu;
        }
        private void RenderNodes(object sender, EventArgs e)
        {
            double height = 0;
            for(int i = 0; i < PropertyTypeNodes.Count; i++)
            {
                height = PropertyTypeNodes[i].Step(height);
            }

            TypeNodeHeight = height;

            double top = Canvas.GetTop(Canvas2);

            if (top + TypeNodeHeight < nodeTypeCanvas.Height)
            {
                top = nodeTypeCanvas.Height - TypeNodeHeight;
            }

            if (top > 0) { top = 0; }

            Canvas.SetTop(Canvas2, top);
        }
        public void Dispose()
        {
            MainWindow._MainWindow.MouseWheel -= ScrollMenu;
            CompositionTarget.Rendering -= RenderNodes;
            saveButton.Click -= CloseMenuWithSave;
            addOutputFileButton.Click -= addOutputFileButton_Click;
            addNodeTypeButton.Click -= addNodeTypeButton_Click;

            for (int i = 0; i < PropertyOutputNodes.Count; i++)
            {
                PropertyOutputNodes[i].Dispose();
            }

            PropertyOutputNodes = null;

            for (int i = 0; i < PropertyTypeNodes.Count; i++)
            {
                PropertyTypeNodes[i].Dispose();
            }

            PropertyTypeNodes = null;

            ((Grid)Parent).Children.Remove(this);

            mainProperty = null;
        }

        private void ScrollMenu(object sender, MouseWheelEventArgs e)
        {
            if (outputFileCanvas.IsMouseOver)
            {
                double newTop = Canvas.GetTop(Canvas1) + e.Delta / 2;

                if(newTop + PropertyOutputNodes.Count * 150 < outputFileCanvas.Height)
                {
                    newTop = outputFileCanvas.Height - PropertyOutputNodes.Count * 150;
                }
                
                if (newTop > 0) { newTop = 0; }

                Canvas.SetTop(Canvas1, newTop);
            }
            else if (nodeTypeCanvas.IsMouseOver)
            {
                double newTop = Canvas.GetTop(Canvas2) + e.Delta / 2;

                if (newTop + TypeNodeHeight < nodeTypeCanvas.Height)
                {
                    newTop = nodeTypeCanvas.Height - TypeNodeHeight;
                }
                
                if (newTop > 0) { newTop = 0; }

                Canvas.SetTop(Canvas2, newTop);
            }
        }

        public void SetPropertyNodes()
        {
            SettingNodes = true;

            SetPropertyOutputNodes();

            SetPropertyTypeNodes();

            SettingNodes = false;
        }

        public void SetPropertyOutputNodes()
        {
            SettingNodes = true;

            for (int i = 0; i < PropertyOutputNodes.Count; i++)
            {
                PropertyOutputNodes[i].Dispose();
            }

            PropertyOutputNodes = new List<PropertyOutputNode>();

            for (int i = 0; i < ProjectProperty.ExportFiles.Count; i++)
            {
                PropertyOutputNode exportFileNode = new PropertyOutputNode(i);
                PropertyOutputNodes.Add(exportFileNode);
                Canvas1.Children.Add(exportFileNode);
                exportFileNode.Margin = new Thickness(0, i * 200, 0, 0);
                exportFileNode.HorizontalAlignment = HorizontalAlignment.Left;
                exportFileNode.VerticalAlignment = VerticalAlignment.Top;
            }

            SettingNodes = false;
        }
        public void SetPropertyTypeNodes()
        {
            SettingNodes = true;

            List<bool> typeNodeIsFold = new List<bool>();

            for (int i = 0; i < PropertyTypeNodes.Count; i++)
            {
                typeNodeIsFold.Add(PropertyTypeNodes[i].GetFold());
                PropertyTypeNodes[i].Dispose();
            }

            PropertyTypeNodes = new List<PropertyTypeNode>();
            for (int i = 0; i < ProjectProperty.NodeTypes.Count; i++)
            {
                bool fold = true;
                if (typeNodeIsFold.Count > i)
                {
                    fold = typeNodeIsFold[i];
                }
                PropertyTypeNode typeNode = new PropertyTypeNode(i, fold);

                PropertyTypeNodes.Add(typeNode);
                Canvas2.Children.Add(typeNode);
                typeNode.Margin = new Thickness(0, 0, 0, 0);
                typeNode.HorizontalAlignment = HorizontalAlignment.Left;
                typeNode.VerticalAlignment = VerticalAlignment.Top;
                Canvas.SetTop(typeNode, i * 150);
            }

            SettingNodes = false;
        }

        public void SwapNodeTypeFold(int num, int newNum)
        {
            bool temp = PropertyTypeNodes[num].GetFold();
            PropertyTypeNodes[num].SetFold(PropertyTypeNodes[newNum].GetFold());
            PropertyTypeNodes[newNum].SetFold(temp);
        }

        private void CloseMenuWithoutSave(object sender, RoutedEventArgs e)
        {
            CloseMenu();
        }

        private void CloseMenuWithSave(object sender, RoutedEventArgs e)
        {
            CloseMenu();
        }

        private void CloseMenu()
        {
            Dispose();
        }

        private void addOutputFileButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectProperty.AddExportFile();
            SetPropertyNodes();
        }

        private void addNodeTypeButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectProperty.AddNodeType();
            SetPropertyNodes();
        }
    }
}
