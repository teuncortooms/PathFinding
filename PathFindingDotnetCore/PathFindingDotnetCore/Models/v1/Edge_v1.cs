using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models
{
    public class Edge_v1
    {
        public int FromId { get; }
        public int ToId { get; }
        public int Weight { get; }

        public Edge_v1(int fromId, int toId, int weight)
        {
            FromId = fromId;
            ToId = toId;
            Weight = weight;
        }
    }
}
