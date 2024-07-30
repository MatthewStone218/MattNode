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

            typeCheckBox.IsChecked = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteType;
            textCheckBox.IsChecked = ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteText;
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
                ProjectProperty.NodeTypes[Num].ExportOption[OptionNum] = new FileExportOption(true, ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteText);
            }
        }

        private void typeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!PropertyMenu.SettingNodes)
            {
                ProjectProperty.NodeTypes[Num].ExportOption[OptionNum] = new FileExportOption(false, ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteText);
            }
        }

        private void textCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!PropertyMenu.SettingNodes)
            {
                ProjectProperty.NodeTypes[Num].ExportOption[OptionNum] = new FileExportOption(ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteType, true);
            }
        }

        private void textCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!PropertyMenu.SettingNodes)
            {
                ProjectProperty.NodeTypes[Num].ExportOption[OptionNum] = new FileExportOption(ProjectProperty.NodeTypes[Num].ExportOption[OptionNum].WriteType, false);
            }
        }
    }
}
