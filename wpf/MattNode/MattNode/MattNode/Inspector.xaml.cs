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

            var textBox = typeComboBox.Template.FindName("PART_EditableTextBox", typeComboBox) as TextBox;
            if (textBox != null)
            {
                textBox.TextChanged -= typeComboBox_TextChanged;
            }

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
                SetType("");
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

        public static void SetType(string text)
        {
            MainInspector.typeComboBox.Text = text;
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

        private void typeComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)typeComboBox.Template.FindName("PART_EditableTextBox", typeComboBox);
            if (textBox.IsFocused)
            {
                if (Node.FocusedNode != null)
                {
                    Node.FocusedNode.SetType(typeComboBox.Text);
                }
                else
                {
                    typeComboBox.Text = "";
                }
            }
        }
    }
}
