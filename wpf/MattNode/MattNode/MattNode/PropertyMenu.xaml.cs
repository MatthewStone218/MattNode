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
        private List<ExportFile> ExportFilesBackup = ProjectProperty.ExportFiles;
        private List<NodeType> NodeTypesBackup = ProjectProperty.NodeTypes;
        private List<PropertyOutputNode> PropertyOutputNodes = new List<PropertyOutputNode>();
        private List<PropertyTypeNode> PropertyTypeNodes = new List<PropertyTypeNode>();
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
        }
        private void RenderNodes(object sender, EventArgs e)
        {
            double height = 0;
            for(int i = 0; i < PropertyTypeNodes.Count; i++)
            {
                height = PropertyTypeNodes[i].Step(height);
            }
        }
        public void Dispose()
        {
            CompositionTarget.Rendering -= RenderNodes;
            saveButton.Click -= CloseMenuWithSave;
            cancelButton.Click -= CloseMenuWithoutSave;
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

        private void CloseMenuWithoutSave(object sender, RoutedEventArgs e)
        {
            CloseMenu();
        }

        private void CloseMenuWithSave(object sender, RoutedEventArgs e)
        {
            ProjectProperty.ExportFiles = ExportFilesBackup;
            ProjectProperty.NodeTypes = NodeTypesBackup;
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
