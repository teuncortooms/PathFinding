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
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    Nodes[row, col] = new Node();
                }
            }
        }

        public Grid(Node[,] nodes)
        {
            Id = Guid.NewGuid();
            Nodes = nodes;
        }

        public int[,] GetGraph()
        {
            int n = Nodes.Length;
            int[,] graph = new int[n, n];

            int currentVertex = 0;
            for (int gridRow = 0; gridRow < Nodes.GetLength(0); gridRow++)
            {
                for (int gridCol = 0; gridCol < Nodes.GetLength(1); gridCol++)
                {
                    if (!Nodes[gridRow, gridCol].IsWall)
                        AddEdgesOfCurrentVertexToGraph(graph, currentVertex, gridRow, gridCol);
                    currentVertex++;
                }
            }

            return graph;
        }

        private void AddEdgesOfCurrentVertexToGraph(int[,] graph, int currentVertex, int gridRow, int gridCol)
        {
            // if has right non-wall neighbour
            if (gridCol < Nodes.GetLength(1) - 1 && !Nodes[gridRow, gridCol + 1].IsWall)
            {
                int rightNeighbour = currentVertex + 1;
                AddEdgeWithWeight1(graph, currentVertex, rightNeighbour);
            }
            // if has below non-wall neighbour
            if (gridRow < Nodes.GetLength(0) - 1 && !Nodes[gridRow + 1, gridCol].IsWall)
            {
                int belowNeighbour = currentVertex + Nodes.GetLength(1);
                AddEdgeWithWeight1(graph, currentVertex, belowNeighbour);
            }
            // bidirectional edges, so above and left aren't needed
        }

        private void AddEdgeWithWeight1(int[,] graph, int currentVertex, int neighbour)
        {
            graph[neighbour, currentVertex] = 1;
            graph[currentVertex, neighbour] = 1; // bidirectional edge
        }

        public int GetSourceSerial()
        {
            return GetVertex((row, col) => Nodes[row, col].IsStart);
        }

        public int GetDestinationSerial()
        {
            return GetVertex((row, col) => Nodes[row, col].IsFinish);
        }

        private delegate bool IsRequestedVertex(int gridRow, int gridCol);
        private int GetVertex(IsRequestedVertex isRequest)
        {
            int currentVertex = 0;
            for (int gridRow = 0; gridRow < Nodes.GetLength(0); gridRow++)
            {
                for (int gridCol = 0; gridCol < Nodes.GetLength(1); gridCol++)
                {
                    if (isRequest(gridRow, gridCol)) return currentVertex;
                    currentVertex++;
                }
            }
            return -1;
        }
    }
}
