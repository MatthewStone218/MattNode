using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace MattNode
{
    public partial class CollisionNode
    {
        private static int Capacity = 4;
        public Instance Boundary;
        public CollisionNode? Parent;
        public List<Instance> Instances = new List<Instance>();
        public CollisionNode[]? Children;
        public bool Divided = false;
        public CollisionNode(CollisionNode parent, double x,double y,double width,double height)
        {
            this.Parent = parent;
            Boundary = new Instance(x,y,width,height);
        }

        public bool Insert(Instance instance)
        {
            if (!Boundary.Intersects(instance)) { return false; }

            if(Divided)
            {
                bool _result = false;//무조건 true인게 정상이긴 한데 일단 코드는 넣음.
                for(int i = 0; i < 4; i++)
                {
                    _result = Children[i].Insert(instance) || _result;
                }

                return _result;
            }
            else if(Capacity > Instances.Count || Boundary.Width <= 16)
            {
                AddInstance(instance);

                return true;
            }
            else
            {
                Subdivide();

                bool _result = false;
                for (int i = 0; i < 4; i++)
                {
                    _result = Children[i].Insert(instance) || _result;
                }

                return _result;
            }
        }

        private void AddInstance(Instance instance)
        {
            instance.Nodes.Add(this);
            Instances.Add(instance);
        }

        private void Subdivide()
        {
            Children = new CollisionNode[4];
            Children[0] = new CollisionNode(this, Boundary.Margin.Left, Boundary.Margin.Top, Boundary.Width / 2, Boundary.Height / 2);
            Children[1] = new CollisionNode(this, Boundary.Margin.Left + Boundary.Width / 2, Boundary.Margin.Top, Boundary.Width / 2, Boundary.Height / 2);
            Children[2] = new CollisionNode(this, Boundary.Margin.Left, Boundary.Margin.Top + Boundary.Height / 2, Boundary.Width / 2, Boundary.Height / 2);
            Children[3] = new CollisionNode(this, Boundary.Margin.Left + Boundary.Width / 2, Boundary.Margin.Top + Boundary.Height / 2, Boundary.Width / 2, Boundary.Height / 2);
        }

        public void RemoveInstance(Instance instance)
        {
            instance.Nodes.Remove(this);
            Instances.Remove(instance);
            if(Parent == null) { return; }

            Parent.TryMarge();
        }

        private void TryMarge()
        {
            if(GetInstanceSum(Capacity+1) <= Capacity)
            {
                Marge();
            }
        }

        private int GetInstanceSum(int max = -1) //내 아래로 총 몇개의 값이 들어가있는지 확인하는 함수.
        {
            if (Divided)
            {
                int Sum = 0;
                for (int i = 0; i < 4; i++)
                {
                    Sum += Children[i].GetInstanceSum(max);
                    if (max != -1 || Sum >= max) { return max; }
                }
                return Sum;
            }
            else
            {
                return Instances.Count;
            }
        }

        private void Marge()
        {
            Divided = false;
            for(int i = 0;i < 4;i++) 
            {
                for(int ii = 0; ii < Children[i].Instances.Count; i++)
                {
                    Children[i].Instances[ii].Nodes.Remove(Children[i]);
                    Children[i].Instances[ii].Nodes.Add(this);
                    Instances.Add(Children[i].Instances[ii]);
                }

                Children = null;
            }
        }
    }
}
