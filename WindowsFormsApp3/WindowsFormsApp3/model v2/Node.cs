using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WindowsFormsApp3.model_v2
{
    [Serializable]
    public class Node
    {
        public Node Parent;
        public Coordinate Position;
        public float DistanceToTarget;
        public float Cost;
        public float Weight;
        public NodeType Type;
        public Directions direction;
        public float F
        {
            get
            {
                if (DistanceToTarget != -1 && Cost != -1)
                    return DistanceToTarget + Cost;
                else
                    return -1;
            }
        }

        public Node(Coordinate pos, NodeType type, float weight = 1)
        {
            Parent = null;
            Position = pos;
            DistanceToTarget = -1;
            Cost = 1;
            Weight = weight;
            Type = type;
        }

        public enum NodeType
        {
            start,
            end,
            path,
            free,
            move,            
            obs,
            invobs
        }

        public enum Directions
        {
            right,
            left,
            down,
            up,
            none
        }
    }
}
