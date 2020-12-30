using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models
{
    public class Edge
    {
        public int FromId { get; }
        public int ToId { get; }
        public int Weight { get; }

        public Edge(int fromId, int toId, int weight)
        {
            FromId = fromId;
            ToId = toId;
            Weight = weight;
        }
    }
}
