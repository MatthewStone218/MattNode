using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MattNode
{
    public partial class CollisionNode
    {
        public double X,Y,Width,Height;
        public CollisionNode[] Nodes = [];
        public CollisionNode[] Nodes = [];
        public CollisionNode(double _X,double _Y,double _Width,double _Height)
        {
            X = _X;
            Y = _Y;
            Width = _Width;
            Height = _Height;
        }

    }
}
