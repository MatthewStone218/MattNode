using MattNode.Property;
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
using System.Xml.Linq;

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
        private bool IsInitializingColor = false;
        private string OldName;
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
            OldName = ProjectProperty.NodeTypes[Num].Name;
            IsInitializingColor = true;
            colorPicker.SelectedColor = ProjectProperty.NodeTypes[Num].Color.Color;
            colorPicker.Background = ProjectProperty.NodeTypes[Num].Color;
            IsInitializingColor = false;

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

            deleteButton.MouseDown -= deleteButton_MouseDown;
            upButton.Click -= upButton_Click;
            downButton.Click -= downButton_Click;
            colorPicker.SelectedColorChanged -= ColorPicker_SelectedColorChanged;
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


        public bool GetFold()
        {
            return Fold;
        }
        public void SetFold(bool fold)
        {
            Fold = fold;
        }

        private void typeNameTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (InitTextFocus) { typeNameTextBox.Focus(); }
            typeNameTextBox.CaretIndex = typeNameTextBox.Text.Length;
        }

        private void deleteButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NodeTypeDeletionAsk askWindow = new NodeTypeDeletionAsk(Num, this);
            askWindow.HorizontalAlignment = HorizontalAlignment.Left;
            askWindow.VerticalAlignment = VerticalAlignment.Top;
            Canvas.SetTop(askWindow, 0);
            Canvas.SetBottom(askWindow, 0);
            Grid.SetZIndex(askWindow, 2000);
            MainWindow._MainWindow.mainGrid.Children.Add(askWindow);
        }

        private void upButton_Click(object sender, RoutedEventArgs e)
        {
            if(Num > 0)
            {
                ProjectProperty.SwapNodeType(Num,Num-1);
                PropertyMenu.mainProperty.SwapNodeTypeFold(Num, Num - 1);
                PropertyMenu.mainProperty.SetPropertyTypeNodes();
            }
        }

        private void downButton_Click(object sender, RoutedEventArgs e)
        {
            if (Num < ProjectProperty.NodeTypes.Count-1)
            {
                ProjectProperty.SwapNodeType(Num, Num + 1);
                PropertyMenu.mainProperty.SwapNodeTypeFold(Num, Num + 1);
                PropertyMenu.mainProperty.SetPropertyTypeNodes();
            }
        }
        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            if (!IsInitializingColor)
            {
                if (e.NewValue.HasValue)
                {
                    SolidColorBrush newBrush = new SolidColorBrush(e.NewValue.Value);
                    colorPicker.Background = newBrush;
                    ProjectProperty.NodeTypes[Num] = new NodeType(ProjectProperty.NodeTypes[Num].Name,newBrush, ProjectProperty.NodeTypes[Num].ExportOption);

                    for (int i = 0; i < Node.NodeList.Count; i++)
                    {
                        if (Node.NodeList[i].typeComboBox.SelectedValue == ProjectProperty.NodeTypes[Num].Name)
                        {
                            Node.NodeList[i].SetColor();
                        }
                    }
                }
            }
        }

        public void typeNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PropertyMenu.ChangingNodeType = false;
            if (OldName != typeNameTextBox.Text)
            {
                NodeTypeMigrateAsk migrateWindow = new NodeTypeMigrateAsk(OldName, typeNameTextBox.Text, this);
                migrateWindow.HorizontalAlignment = HorizontalAlignment.Left;
                migrateWindow.VerticalAlignment = VerticalAlignment.Top;
                Canvas.SetTop(migrateWindow, 0);
                Canvas.SetBottom(migrateWindow, 0);
                Grid.SetZIndex(migrateWindow, 4000);
                MainWindow._MainWindow.mainGrid.Children.Add(migrateWindow);

                e.Handled = true;
            }
        }

        private void typeNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PropertyMenu.ChangingNodeType = true;
        }

        public void ApplyName()
        {
            ProjectProperty.ModifyNodeType(Num, typeNameTextBox.Text, ProjectProperty.NodeTypes[Num].Color, ProjectProperty.NodeTypes[Num].ExportOption);
        }

        private void control_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (PropertyMenu.ChangingNodeType)
            {
                e.Handled = true;
                MainWindow._MainWindow.focusCatcher.Focus();
            }
        }
    }
}
