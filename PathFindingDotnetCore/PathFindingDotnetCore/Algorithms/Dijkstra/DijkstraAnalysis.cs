using PathFindingDotnetCore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PathFindingDotnetCore.Algorithms.Dijkstra
{
    public class DijkstraAnalysis
    {
        private readonly int[,] graph;
        private readonly int nVertices;
        private int[] distances; // distances[i] will hold the shortest distance from src to i 
        private bool[] isVisited; // isVisited[i] will be true if distances[i] is set
        public int[] VisitedInOrder { get; private set; } // needed for frontend to visualise analysis
        public int[] Parents { get; private set; } // parents[i] will hold closest vertex between src and i
        public int[] ShortestPathToDest { get; private set; }

        public DijkstraAnalysis(int[,] graph, int src, int dest)
        {
            this.graph = graph;
            this.nVertices = graph.GetLength(0);
            InitMemberArrays();
            this.distances[src] = 0;
            LogDistances();
            FindShortestPathForAllVertices();
            this.ShortestPathToDest = GetSpeciFicPathFromParents(dest);
        }

        private void InitMemberArrays()
        {
            distances = new int[nVertices];
            isVisited = new bool[nVertices];
            VisitedInOrder = new int[nVertices];
            Parents = new int[nVertices];

            for (int i = 0; i < nVertices; i++)
            {
                distances[i] = int.MaxValue;
                isVisited[i] = false;
                Parents[i] = -1;
            }
        }

        private void FindShortestPathForAllVertices()
        {
            for (int count = 0; count < nVertices - 1; count++)
            {
                int current = GetClosest(); // equals src in first iteration. 
                isVisited[current] = true;
                VisitedInOrder[count] = current;

                if (distances[current] == int.MaxValue) return; // no more paths from src

                for (int v = 0; v < nVertices; v++)
                    if (IsConnectedAndNotVisited(v, current) && IsShorterPath(v, current))
                    {
                        distances[v] = distances[current] + graph[current, v];
                        Parents[v] = current;
                    }
            }
        }

        private int GetClosest()
        {
            int minDistance = int.MaxValue;
            int closestVertex = -1;

            for (int v = 0; v < nVertices; v++)
                if (isVisited[v] == false && distances[v] <= minDistance)
                {
                    minDistance = distances[v];
                    closestVertex = v;
                }

            return closestVertex;
        }

        private bool IsShorterPath(int v, int step)
        {
            return distances[step] + graph[step, v] < distances[v];
        }

        private bool IsConnectedAndNotVisited(int v, int step)
        {
            return !isVisited[v] && graph[step, v] != 0;
        }

        private void LogDistances()
        {
            Console.Write("Vertex \t\t Distance from Source\n");
            for (int i = 0; i < nVertices; i++)
                Console.Write(i + " \t\t " + distances[i] + "\n");
        }

        private int[] GetSpeciFicPathFromParents(int dest)
        {
            Stack<int> path = new Stack<int>();
            
            int vertex = dest;
            while (vertex != -1)
            {
                path.Push(vertex);
                vertex = Parents[vertex];
            }

            return path.ToArray();
        }
    }
}
