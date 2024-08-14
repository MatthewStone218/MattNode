using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

//.......................................................................................................................................................
//using System.Windows.Shapes;
//using System.Windows.Media;

namespace MattNode
{
    public partial class CollisionNode
    {
        //.......................................................................................................................................................
        //public Rectangle GraphicRectangle { get; private set; }


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

            //.......................................................................................................................................................
            //CreateGraphicRectangle();
        }


        //.......................................................................................................................................................
        /*
        private void CreateGraphicRectangle()
        {
            Random rand = new Random();
            byte r = (byte)rand.Next(256);
            byte g = (byte)rand.Next(256);
            byte b = (byte)rand.Next(256);

            // 그래픽 사각형 초기화
            GraphicRectangle = new Rectangle
            {
                Stroke = System.Windows.Media.Brushes.Black,
                StrokeThickness = 10,
                Width = Boundary.Width,
                Height = Boundary.Height,
                Fill = new SolidColorBrush(Color.FromRgb(r, g, b))
        };
            // 위치 설정 (Canvas.SetLeft, Canvas.SetTop 사용 가능)
            Canvas.SetLeft(GraphicRectangle, Canvas.GetLeft(Boundary));
            Canvas.SetTop(GraphicRectangle, Canvas.GetTop(Boundary));
            

            MainCanvas.Canvas.mainCanvas.Children.Add(GraphicRectangle);
        }
        */

        public bool Insert(Instance instance)
        {
            if (!Boundary.Intersects(instance)) { return false; }

            if (Divided)
            {
                //MessageBox.Show("내 자식에게 넣음");
                bool _result = false;//무조건 true인게 정상이긴 한데 일단 코드는 넣음.
                for(int i = 0; i < 4; i++)
                {
                    _result = Children[i].Insert(instance) || _result;
                }

                return _result;
            }
            else if(Capacity > Instances.Count || Boundary.Width <= 256)
            {
                //MessageBox.Show("나에게 넣음");
                AddInstance(instance);

                return true;
            }
            else
            {
                //MessageBox.Show("나를 분할하고 내 자식들에게 넣음. 기존에 나에게 있던 값들도 자식들에게 넣음.");
                Subdivide();

                bool _result = false;
                for (int i = 0; i < 4; i++)
                {
                    _result = Children[i].Insert(instance) || _result;
                }

                for (int i = 0; i < 4; i++)
                {
                    for (int ii = 0; ii < Instances.Count; ii++)
                    {
                        Children[i].Insert(Instances[ii]);
                    }
                }

                Instances = new List<Instance>();

                return _result;
            }
        }

        public void PushInstancesInBoundary(List<Instance> list, Instance boundary)
        {
            if(Boundary.Intersects(boundary))
            {
                if(Divided)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Children[i].PushInstancesInBoundary(list, boundary);
                    }
                }
                else
                {
                    for(int i = 0; i < Instances.Count; i++)
                    {
                        if (Instances[i].Intersects(boundary))
                        {
                            list.Add(Instances[i]);
                        }
                    }
                    return;
                }
            }
            else
            {
                return;
            }
        }

        private void AddInstance(Instance instance)
        {
            instance.Nodes.Add(this);
            Instances.Add(instance);
        }

        private void Subdivide()
        {
            Divided = true;
            Children = new CollisionNode[4];
            Children[0] = new CollisionNode(this, Canvas.GetLeft(Boundary), Canvas.GetTop(Boundary), Boundary.Width / 2, Boundary.Height / 2);
            Children[1] = new CollisionNode(this, Canvas.GetLeft(Boundary) + Boundary.Width / 2, Canvas.GetTop(Boundary), Boundary.Width / 2, Boundary.Height / 2);
            Children[2] = new CollisionNode(this, Canvas.GetLeft(Boundary), Canvas.GetTop(Boundary) + Boundary.Height / 2, Boundary.Width / 2, Boundary.Height / 2);
            Children[3] = new CollisionNode(this, Canvas.GetLeft(Boundary) + Boundary.Width / 2, Canvas.GetTop(Boundary) + Boundary.Height / 2, Boundary.Width / 2, Boundary.Height / 2);

            for (int i = 0; i < Instances.Count; i++)
            {
                Instances[i].Nodes.Remove(this);
            }
        }

        public void RemoveInstance(Instance instance)
        {
            instance.Nodes.Remove(this);
            Instances.Remove(instance);
            if(Parent == null) { return; }

            Parent.TryMerge();
        }

        private void TryMerge()
        {
            if(GetInstanceSum(Capacity+1) <= Capacity)
            {
                Merge();
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
                    if (max != -1 && Sum >= max) { return max; }
                }
                return Sum;
            }
            else
            {
                return Instances.Count;
            }
        }

        private void Merge()
        {
            if (Divided)
            {
                Divided = false;
                for (int i = 0; i < 4; i++)
                {
                    for (int ii = 0; ii < Children[i].Instances.Count; ii++)
                    {
                        Children[i].Instances[ii].Nodes.Remove(Children[i]);
                        Children[i].Instances[ii].Nodes.Add(this);
                        Instances.Add(Children[i].Instances[ii]);
                    }
                }
                Children = null;

                if (Parent != null) { Parent.TryMerge(); }
            }
        }

        public void Extend()
        {
            Boundary = new Instance(Canvas.GetLeft(Boundary) * 2, Canvas.GetTop(Boundary) * 2, Boundary.Width * 2, Boundary.Height * 2);
            //...............................................................................................................................
            //CreateGraphicRectangle();
            if (Divided)
            {
                CollisionNode[] newChildren = new CollisionNode[4];
                newChildren[0] = new CollisionNode(this, Canvas.GetLeft(Boundary), Canvas.GetTop(Boundary), Boundary.Width / 2, Boundary.Height / 2);
                newChildren[1] = new CollisionNode(this, Canvas.GetLeft(Boundary) + Boundary.Width / 2, Canvas.GetTop(Boundary), Boundary.Width / 2, Boundary.Height / 2);
                newChildren[2] = new CollisionNode(this, Canvas.GetLeft(Boundary), Canvas.GetTop(Boundary) + Boundary.Height / 2, Boundary.Width / 2, Boundary.Height / 2);
                newChildren[3] = new CollisionNode(this, Canvas.GetLeft(Boundary) + Boundary.Width / 2, Canvas.GetTop(Boundary) + Boundary.Height / 2, Boundary.Width / 2, Boundary.Height / 2);

                newChildren[0].Subdivide();
                newChildren[0].Children[3] = Children[0];
                Children[0].Parent = newChildren[0];

                newChildren[1].Subdivide();
                newChildren[1].Children[2] = Children[1];
                Children[1].Parent = newChildren[1];

                newChildren[2].Subdivide();
                newChildren[2].Children[1] = Children[2];
                Children[2].Parent = newChildren[2];

                newChildren[3].Subdivide();
                newChildren[3].Children[0] = Children[3];
                Children[3].Parent = newChildren[3];

                for (int i = 0; i < 4; i++)
                {
                    Children[i] = newChildren[i];
                }

                for (int i = 0; i < 4; i++)
                {
                    Children[i].TryMerge();
                }
            }
        }
    }
}
