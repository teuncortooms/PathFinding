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

        public int[,] GetGraph()
        {
            int n = Nodes.Length;
            int[,] graph = new int[n, n];

            int currentVertex = 0;
            for (int gridRow = 0; gridRow < Nodes.GetLength(0); gridRow++)
            {
                for (int gridCol = 0; gridCol < Nodes.GetLength(1); gridCol++)
                {
                    if (Nodes[gridRow, gridCol].IsWall) continue;
                    AddEdgesOfCurrentVertexToGraph(graph, currentVertex, gridRow, gridCol);
                    if (Nodes[gridRow, gridCol].IsStart) src = currentVertex;
                    if (Nodes[gridRow, gridCol].IsFinish) dest = currentVertex;

                    currentVertex++;
                }
            }

            return graph;
        }

        private void AddEdgesOfCurrentVertexToGraph(int[,] graph, int currentVertex, int gridRow, int gridCol)
        {
            if (gridCol < Nodes.GetLength(1) - 1) // if has right neighbour
            {
                if (!Nodes[gridRow, gridCol + 1].IsWall)
                {
                    int rightNeighbour = currentVertex + 1;
                    graph[rightNeighbour, currentVertex] = 1; // add edge with weight 1
                    graph[currentVertex, rightNeighbour] = 1; // bidirectional edge
                }
            }

            if (gridRow < Nodes.GetLength(0) - 1) // if has below neighbour
            {
                if (!Nodes[gridRow + 1, gridCol].IsWall)
                {
                    int belowNeighbour = currentVertex + Nodes.GetLength(1);
                    AddEdgeWithWeight1(graph, currentVertex, belowNeighbour);
                }
            }

            //if (col > 0) // if has left neighbour
            //{
            //    int leftNeighbour = currentVertex - 1;
            //    graph[leftNeighbour, currentVertex] = 1; // add edge with weight 1
            //    graph[currentVertex, leftNeighbour] = 1; // bidirectional edge
            //}
            //if (row > 0) // if has above neighbour
            //{
            //    int aboveNeighbour = currentVertex - Nodes.GetLength(1);
            //    graph[aboveNeighbour, currentVertex] = 1; // add edge with weight 1
            //    graph[currentVertex, aboveNeighbour] = 1; // bidirectional edge
            //}
        }

        private void AddEdgeWithWeight1(int[,] graph, int currentVertex, int neighbour)
        {
            graph[neighbour, currentVertex] = 1;
            graph[currentVertex, neighbour] = 1; // bidirectional edge
        }

        public int GetSource()
        {
            return GetVertex((row, col) => Nodes[row, col].IsStart);
        }

        public int GetDestination()
        {
            return GetVertex((row, col) => Nodes[row, col].IsFinish);
        }

        private delegate bool IsRequestedVertex(int gridRow, int gridCol);
        private int GetVertex(IsRequestedVertex isRequest)
        {
            int currentVertex = 0;
            for (int gridRow = 0; gridRow < Nodes.GetLength(0); gridRow++)
            {
                for (int gridCol = 0; gridCol < Nodes.GetLength(0); gridCol++)
                {
                    if (isRequest(gridRow, gridCol)) return currentVertex;
                    currentVertex++;
                }
            }
            return -1;
        }
    }
}
