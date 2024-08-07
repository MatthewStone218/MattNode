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
using System.Windows.Shapes;

namespace MattNode
{
    /// <summary>
    /// ProjectSaveLoading.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ProjectSaveLoading : UserControl
    {
        public int NodeCountNow = 0;
        public int NodeCountAll;
        public ProjectSaveLoading(int nodeCount)
        {
            NodeCountAll = nodeCount;
            InitializeComponent();
        }

        public void SetNodeCount(int nodeCount)
        {
            NodeCountNow = nodeCount;
            loadingLabel.Content = $"Saving Project... ({NodeCountNow}/{NodeCountAll})";
        }

        public void Dispose()
        {
            ((Grid)Parent).Children.Remove(this);
        }
    }
}
