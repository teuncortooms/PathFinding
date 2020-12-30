using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models
{
    public class Grid
    {
        public Guid Id { get; }
        public Node[,] Nodes2D { get; set; }

        public Grid(int rows, int cols)
        {
            Id = Guid.NewGuid();
            Nodes2D = new Node[rows, cols];
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Nodes2D[row, col] = new Node();
                }
            }
        }

        public Grid(Node[,] nodes)
        {
            Id = Guid.NewGuid();
            Nodes2D = nodes;
        }
    }
}
