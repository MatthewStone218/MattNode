using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
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
    /// PropertyTypeNodeExportOptionNode.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PropertyTypeNodeExportOptionNode : UserControl
    {
        private int Num;
        private int OptionNum;

        public PropertyTypeNodeExportOptionNode(int num, int optionNum)
        {
            InitializeComponent();
            Num = num;
            OptionNum = optionNum;

            exportFileNameLabel.Content = ProjectProperty.ExportFiles[OptionNum].Name;
            typeCheckBox.IsChecked = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteType;
            textCheckBox.IsChecked = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteText;
            prevNodesCheckBox.IsChecked = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WritePrevNodes;
            nextNodesCheckBox.IsChecked = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteNextNodes;
        }
        public void Dispose()
        {
            ((Canvas)Parent).Children.Remove(this);

            textCheckBox.Checked -= textCheckBox_Checked;
            textCheckBox.Unchecked -= textCheckBox_Unchecked;

            typeCheckBox.Checked -= typeCheckBox_Checked;
            typeCheckBox.Unchecked -= typeCheckBox_Unchecked;
        }

        private void typeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!PropertyMenu.SettingNodes)
            {
                bool writeType = true;
                bool writeText = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteText;
                bool writePrevNodes = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WritePrevNodes;
                bool writeNextNodes = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteNextNodes;
                ProjectProperty.NodeTypes[Num].ExportOption[OptionNum] = new FileExportOption(writeType, writeText, writePrevNodes, writeNextNodes);
            }
        }

        private void typeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!PropertyMenu.SettingNodes)
            {
                bool writeType = false;
                bool writeText = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteText;
                bool writePrevNodes = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WritePrevNodes;
                bool writeNextNodes = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteNextNodes;
                ProjectProperty.NodeTypes[Num].ExportOption[OptionNum] = new FileExportOption(writeType, writeText, writePrevNodes, writeNextNodes);
            }
        }
        private void textCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!PropertyMenu.SettingNodes)
            {
                bool writeType = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteType;
                bool writeText = true;
                bool writePrevNodes = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WritePrevNodes;
                bool writeNextNodes = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteNextNodes;
                ProjectProperty.NodeTypes[Num].ExportOption[OptionNum] = new FileExportOption(writeType, writeText, writePrevNodes, writeNextNodes);
            }
        }
        private void textCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!PropertyMenu.SettingNodes)
            {
                bool writeType = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteType;
                bool writeText = false;
                bool writePrevNodes = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WritePrevNodes;
                bool writeNextNodes = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteNextNodes;
                ProjectProperty.NodeTypes[Num].ExportOption[OptionNum] = new FileExportOption(writeType, writeText, writePrevNodes, writeNextNodes);
            }
        }

        private void prevNodesCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            bool writeType = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteType;
            bool writeText = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteText;
            bool writePrevNodes = true;
            bool writeNextNodes = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteNextNodes;
            ProjectProperty.NodeTypes[Num].ExportOption[OptionNum] = new FileExportOption(writeType, writeText, writePrevNodes, writeNextNodes);
        }

        private void prevNodesCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            bool writeType = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteType;
            bool writeText = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteText;
            bool writePrevNodes = false;
            bool writeNextNodes = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteNextNodes;
            ProjectProperty.NodeTypes[Num].ExportOption[OptionNum] = new FileExportOption(writeType, writeText, writePrevNodes, writeNextNodes);
        }
        private void nextNodesCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            bool writeType = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteType;
            bool writeText = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteText;
            bool writePrevNodes = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WritePrevNodes;
            bool writeNextNodes = true;
            ProjectProperty.NodeTypes[Num].ExportOption[OptionNum] = new FileExportOption(writeType, writeText, writePrevNodes, writeNextNodes);
        }
        private void nextNodesCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            bool writeType = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteType;
            bool writeText = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteText;
            bool writePrevNodes = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WritePrevNodes;
            bool writeNextNodes = false;
            ProjectProperty.NodeTypes[Num].ExportOption[OptionNum] = new FileExportOption(writeType, writeText, writePrevNodes, writeNextNodes);
        }
    }
}
