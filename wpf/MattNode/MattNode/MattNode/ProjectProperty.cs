using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MattNode
{
    public struct NodeType
    {
        public string Name;
        public SolidColorBrush Color;
        public bool IsScript;
    }
    public partial class ProjectProperty
    {
        static List<NodeType> NodeTypes = new List<NodeType>();
    }
}
