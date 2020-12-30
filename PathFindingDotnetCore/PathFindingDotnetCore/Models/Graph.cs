using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models
{
    public class Graph
    {
        public List<Node> Nodes { get; private set; }
        public List<Edge> Edges { get; private set; }

        public Graph(Grid grid)
        {
            Nodes = new List<Node>();
            Edges = new List<Edge>(); 
            PopulateNodes(grid);
            PopulateEdges(grid);
        }

        private void PopulateNodes(Grid grid)
        {
            Node[,] nodes2d = grid.Nodes2D;

            for (int iRow = 0; iRow < nodes2d.GetLength(0); iRow++)
            {
                for (int iCol = 0; iCol < nodes2d.GetLength(1); iCol++)
                {
                    Nodes.Add(nodes2d[iRow, iCol]);
                }
            }
        }

        private void PopulateEdges(Grid grid)
        {
            Node[,] nodes2d = grid.Nodes2D;
            int nRows = nodes2d.GetLength(0);
            int nCols = nodes2d.GetLength(1);

            for (int iRow = 0; iRow < nRows; iRow++)
            {
                for (int iCol = 0; iCol < nCols; iCol++)
                {
                    Node currentNode = nodes2d[iRow, iCol];
                    if (!currentNode.IsWall)
                        GetEdgesOfNode(iRow, iCol, grid);
                }
            }
        }

        private void GetEdgesOfNode(int gridRow, int gridCol, Grid grid)
        {
            Node[,] nodes2d = grid.Nodes2D;
            Node currentNode = nodes2d[gridRow, gridCol];

            // if has right non-wall neighbour
            if (gridCol < nodes2d.GetLength(1) - 1 && !nodes2d[gridRow, gridCol + 1].IsWall)
            {
                Node rightNeighbour = nodes2d[gridRow, gridCol + 1];
                AddEdgeWithWeight1(currentNode, rightNeighbour);
            }
            // if has below non-wall neighbour
            if (gridRow < nodes2d.GetLength(0) - 1 && !nodes2d[gridRow + 1, gridCol].IsWall)
            {
                Node belowNeighbour = nodes2d[gridRow + 1, gridCol];
                AddEdgeWithWeight1(currentNode, belowNeighbour);
            }
            // bidirectional edges, so above and left aren't needed
        }

        private void AddEdgeWithWeight1(Node currentNode, Node neighbour)
        {
            Edges.Add(new Edge(currentNode.Id, neighbour.Id, 1));
            Edges.Add(new Edge(neighbour.Id, currentNode.Id, 1)); // bidirectional edge        
        }

        public int GetSourceId()
        {
            foreach (var node in Nodes)
            {
                if (node.IsStart) return node.Id;
            }
            return -1;
        }

        public int GetDestinationId()
        {
            foreach (var node in Nodes)
            {
                if (node.IsFinish) return node.Id;
            }
            return -1;
        }
    }
}
