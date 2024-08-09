using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Inspector.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Inspector : UserControl
    {
        private bool SettingNodeTypeItems = false;
        private static Inspector MainInspector;
        public Inspector()
        {
            MainInspector = this;
            InitializeComponent();
            RegisterRenderEvent();
        }

        public void Dispose()
        {
            CompositionTarget.Rendering -= SetTextWithFocus;
            resizeThumb.DragDelta -= resizeThumb_DragDelta;
            contentTextBox.TextChanged -= contentTextBox_TextChanged;

            typeComboBox.SelectionChanged -= typeComboBox_SelectionChanged;

            ((Grid)Parent).Children.Remove(this);
        }
        private void RegisterRenderEvent()
        {
            CompositionTarget.Rendering += SetTextWithFocus;
        }
        private void SetTextWithFocus(object sender, EventArgs e)
        {
            if(Node.FocusedNode == null)
            {
                SetType(-1);
                SetContent("");
            }
        }
        private void RepositionElements()
        {
            typeComboBox.Width = Width - 88;
            contentTextBox.Width = Width - 20;
            Canvas.SetLeft(resizeThumb, Width - 10);
        }
        
        private void resizeThumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double newWidth = Width + e.HorizontalChange;

            if (newWidth > 200 && newWidth < MainWindow.GetWindowWidth()) { Width = newWidth; RepositionElements(); }
        }

        public static void SetType(object selectedValue)
        {
            MainInspector.SetTypeItems();
            MainInspector.typeComboBox.SelectedValue = selectedValue;
        }
        public static void SetType()
        {
            MainInspector.typeComboBox.SelectedIndex = -1;
        }

        public static void SetContent(string text)
        {
            MainInspector.contentTextBox.Text = text;
        }

        private void contentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (contentTextBox.IsFocused)
            {
                if (Node.FocusedNode != null)
                {
                    Node.FocusedNode.SetContent(contentTextBox.Text);
                }
                else
                {
                    contentTextBox.Text = "";
                }
            }
        }
        public void SetTypeItems()
        {
            SettingNodeTypeItems = true;

            object obj = null;
            if (typeComboBox.SelectedIndex >= 0)
            {
                obj = typeComboBox.Items[typeComboBox.SelectedIndex];
            }
            string name = null;

            if (obj != null)
            {
                name = obj.ToString();
            }

            for (int i = 0; i < typeComboBox.Items.Count; i++)
            {
                typeComboBox.Items.RemoveAt(i);
                i--;
            }

            for (int i = 0; i < ProjectProperty.NodeTypes.Count; i++)
            {
                typeComboBox.Items.Add(ProjectProperty.NodeTypes[i].Name);
            }

            if (name != null)
            {
                typeComboBox.SelectedValue = name;
            }

            SettingNodeTypeItems = false;
        }
        private void typeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!SettingNodeTypeItems && typeComboBox.SelectedIndex != -1 && typeComboBox.IsMouseOver)
            {
                if (Node.FocusedNode != null)
                {
                    Node.FocusedNode.SetType(typeComboBox.SelectedValue);
                }
                else
                {
                    typeComboBox.Text = "";
                }
            }
        }

        private void contentTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                TextBox textBox = sender as TextBox;
                if (textBox != null)
                {
                    int caretIndex = textBox.CaretIndex;
                    textBox.Text = textBox.Text.Insert(caretIndex, "\t");
                    textBox.CaretIndex = caretIndex + 1;

                    // 기본 Tab 동작을 막기 위해 처리 완료로 표시
                    e.Handled = true;
                }
            }
        }
    }
}
