using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models
{
    public class Grid
    {
        public Guid Id { get; }
        public Node[,] Nodes { get; set; }

        public Grid(int width, int height)
        {
            Id = Guid.NewGuid();
            Nodes = new Node[height, width];
        }

        public Grid(Node[,] nodes)
        {
            Id = Guid.NewGuid();
            Nodes = nodes;
        }
    }
}
