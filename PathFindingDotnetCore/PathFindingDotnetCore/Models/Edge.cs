using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models
{
    public class Edge
    {
        public Node From { get; }
        public Node To { get; }
        public int Weight { get; }

        Edge(Node from, Node to, int weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }
    }
}
