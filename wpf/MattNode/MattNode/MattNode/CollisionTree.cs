using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MattNode
{
    public partial class CollisionTree
    {
        public static CollisionNode RootNode = new CollisionNode(null,-64*10,-64*10,128*10,128*10);

        public static void Instert(Instance instance)
        {//divied을 한 뒤 extend하면 오류.
            while (true)
            {
                if (RootNode.Insert(instance)) { break; }
                else
                {
                    RootNode.Extend();
                }
            }
        }

        public static List<Instance> GetInstancesInBoundaryList(Instance boundary)
        {
            List<Instance> instances = new List<Instance>();
            RootNode.PushInstancesInBoundary(instances, boundary);
            return instances;
        }
    }
}
