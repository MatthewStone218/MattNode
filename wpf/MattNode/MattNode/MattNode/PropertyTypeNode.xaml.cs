using System;
using System.Collections.Generic;
using System.Linq;
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
    /// PropertyTypeNode.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PropertyTypeNode : UserControl
    {
        public bool InitTextFocus = false;
        private bool Fold;
        private int Num;
        private List<PropertyTypeNodeExportOptionNode> ExportOptionNodes = new List<PropertyTypeNodeExportOptionNode>();
        public PropertyTypeNode(int num, bool fold)
        {
            InitializeComponent();

            Num = num;
            Fold = fold;
            Init();
        }

        public void Init()
        {
            typeNameTextBox.Text = ProjectProperty.NodeTypes[Num].Name;

            for (int i = 0; i < ProjectProperty.NodeTypes[Num].ExportOption.Count; i++)
            {
                PropertyTypeNodeExportOptionNode exportOptionNode = new PropertyTypeNodeExportOptionNode(Num, i);
                dropDownCanvas.Children.Add(exportOptionNode);
                exportOptionNode.Margin = new Thickness(40, i * 150, 0, 0);
                exportOptionNode.HorizontalAlignment = HorizontalAlignment.Left;
                exportOptionNode.VerticalAlignment = VerticalAlignment.Top;

                ExportOptionNodes.Add(exportOptionNode);
            }

            if (Fold)
            {
                Canvas.SetTop(dropDownCanvas, 150 - 150 * ProjectProperty.ExportFiles.Count);
            }
            else
            {
                Canvas.SetTop(dropDownCanvas, 150);
            }
        }
        public void Dispose()
        {
            for (int i = 0; i < ExportOptionNodes.Count; i++)
            {
                ExportOptionNodes[i].Dispose();
            }

            ExportOptionNodes = null;

            ((Canvas)Parent).Children.Remove(this);

            typeNameTextBox.TextChanged -= typeNameTextBox_TextChanged;
            deleteButton.MouseDown -= deleteButton_MouseDown;
        }
        public double Step(double height)
        {
            Canvas.SetTop(this, height);

            int goalY;
            if (Fold)
            {
                goalY = 150 - 150 * ProjectProperty.ExportFiles.Count;
            }
            else
            {
                goalY = 150;
            }

            bool a = Mouse.RightButton == MouseButtonState.Pressed;

            Canvas.SetTop(dropDownCanvas, Canvas.GetTop(dropDownCanvas) + (goalY - Canvas.GetTop(dropDownCanvas)) /4);

            Height = Canvas.GetTop(dropDownCanvas) + 150 * ProjectProperty.ExportFiles.Count;
            mainGrid.Height = Height;
            leftButton.Height = 150 * ProjectProperty.ExportFiles.Count;

            return height + Height;
        }

        private void foldButton_Click(object sender, RoutedEventArgs e)
        {
            Fold = !Fold;
        }

        private void typeNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (typeNameTextBox.IsFocused && !PropertyMenu.SettingNodes)
            {
                ProjectProperty.ModifyNodeType(Num, typeNameTextBox.Text, ProjectProperty.NodeTypes[Num].Color, ProjectProperty.NodeTypes[Num].ExportOption);
            }
        }

        public bool GetFold()
        {
            return Fold;
        }

        private void typeNameTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (InitTextFocus) { typeNameTextBox.Focus(); }
            typeNameTextBox.CaretIndex = typeNameTextBox.Text.Length;
        }

        private void deleteButton_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
