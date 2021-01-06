using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models
{
    public class Graph_v1
    {
        public List<Node_v1> Nodes { get; private set; }
        public List<Edge_v1> Edges { get; private set; }

        public Graph_v1(Grid_v1 grid)
        {
            Nodes = new List<Node_v1>();
            Edges = new List<Edge_v1>(); 
            PopulateNodes(grid);
            PopulateEdges(grid);
        }

        private void PopulateNodes(Grid_v1 grid)
        {
            Node_v1[,] nodes2d = grid.Nodes2D;

            for (int iRow = 0; iRow < nodes2d.GetLength(0); iRow++)
            {
                for (int iCol = 0; iCol < nodes2d.GetLength(1); iCol++)
                {
                    Nodes.Add(nodes2d[iRow, iCol]);
                }
            }
        }

        private void PopulateEdges(Grid_v1 grid)
        {
            Node_v1[,] nodes2d = grid.Nodes2D;
            int nRows = nodes2d.GetLength(0);
            int nCols = nodes2d.GetLength(1);

            for (int iRow = 0; iRow < nRows; iRow++)
            {
                for (int iCol = 0; iCol < nCols; iCol++)
                {
                    Node_v1 currentNode = nodes2d[iRow, iCol];
                    if (!currentNode.IsWall)
                        GetEdgesOfNode(iRow, iCol, grid);
                }
            }
        }

        private void GetEdgesOfNode(int gridRow, int gridCol, Grid_v1 grid)
        {
            Node_v1[,] nodes2d = grid.Nodes2D;
            Node_v1 currentNode = nodes2d[gridRow, gridCol];

            // if has right non-wall neighbour
            if (gridCol < nodes2d.GetLength(1) - 1 && !nodes2d[gridRow, gridCol + 1].IsWall)
            {
                Node_v1 rightNeighbour = nodes2d[gridRow, gridCol + 1];
                AddEdgeWithWeight1(currentNode, rightNeighbour);
            }
            // if has below non-wall neighbour
            if (gridRow < nodes2d.GetLength(0) - 1 && !nodes2d[gridRow + 1, gridCol].IsWall)
            {
                Node_v1 belowNeighbour = nodes2d[gridRow + 1, gridCol];
                AddEdgeWithWeight1(currentNode, belowNeighbour);
            }
            // bidirectional edges, so above and left aren't needed
        }

        private void AddEdgeWithWeight1(Node_v1 currentNode, Node_v1 neighbour)
        {
            Edges.Add(new Edge_v1(currentNode.Id, neighbour.Id, 1));
            Edges.Add(new Edge_v1(neighbour.Id, currentNode.Id, 1)); // bidirectional edge        
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
