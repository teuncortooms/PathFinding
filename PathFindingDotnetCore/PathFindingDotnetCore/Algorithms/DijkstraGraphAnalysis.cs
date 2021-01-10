using PathFindingDotnetCore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PathFindingDotnetCore.Algorithms.Dijkstra
{
    public class DijkstraGraphAnalysis
    {
        private readonly Graph graph;
        private readonly int[] distanceToStart;
        private readonly bool[] isVisited;
        private readonly int[] parents;

        public Report Report { get; }

        public DijkstraGraphAnalysis(Graph graph)
        {
            this.graph = graph;
            int n = graph.NodeDetails.Length;
            distanceToStart = new int[n];
            isVisited = new bool[n];
            parents = new int[n];
            for (int i = 0; i < n; i++)
            {
                distanceToStart[i] = int.MaxValue;
                isVisited[i] = false;
                parents[i] = -1;
            }

            Report = new Report();

            int iStart = graph.GetStartIdx();
            int iDest = graph.GetDestinationIdx();
            StartAnalysis(iStart, iDest);
        }

        private void StartAnalysis(int iStart, int iDest)
        {
            distanceToStart[iStart] = 0;

            int nNodes = graph.NodeDetails.Length;
            for (int i = 0; i < nNodes - 1; i++)
            {
                int Idx = GetClosestIdx(); // equals start in first iteration. 
                if (distanceToStart[Idx] == int.MaxValue) break; // no more paths from start
                isVisited[Idx] = true;
                Report.VisitedInOrder.Add(graph.NodeDetails[Idx].Id);
                if (graph.NodeDetails[Idx].IsDestination) break; // finished
                UpdateNeighbours(Idx);
            }

            GetShortestPathToDest(iDest);
        }

        private int GetClosestIdx()
        {
            int minDistance = int.MaxValue;
            int closestNodeIdx = -1;

            int nNodes = graph.NodeDetails.Length;
            for (int iNode = 0; iNode < nNodes; iNode++)
            {
                if (!isVisited[iNode] && IsShorterDistance(iNode, minDistance))
                {
                    minDistance = distanceToStart[iNode];
                    closestNodeIdx = iNode;
                }
            }
            return closestNodeIdx;
        }

        private bool IsShorterDistance(int iNode, int minDistance)
        {
            return distanceToStart[iNode] <= minDistance;
        }

        private void UpdateNeighbours(int currentNodeIdx)
        {
            int nNodes = graph.NodeDetails.Length;

            for (int newNodeIdx = 0; newNodeIdx < nNodes; newNodeIdx++)
            {
                if (!isVisited[newNodeIdx] && IsConnected(currentNodeIdx,newNodeIdx) && IsShorterPath(currentNodeIdx, newNodeIdx))
                {
                    int connectionWeight = graph.EdgeMatrix[currentNodeIdx, newNodeIdx];
                    distanceToStart[newNodeIdx] = distanceToStart[currentNodeIdx] + connectionWeight;
                    parents[newNodeIdx] = currentNodeIdx;
                }
            }
        }

        private bool IsShorterPath(int currentNodeIdx, int newNodeIdx)
        {
            int connectionWeight = graph.EdgeMatrix[currentNodeIdx, newNodeIdx];
            return distanceToStart[currentNodeIdx] + connectionWeight < distanceToStart[newNodeIdx];
        }

        private bool IsConnected(int currentNodeIdx, int newNodeIdx)
        {
            int connectionWeight = graph.EdgeMatrix[currentNodeIdx, newNodeIdx]; 
            return connectionWeight != 0;
        }

        private void GetShortestPathToDest(int iDest)
        {
            int iCurrentNode = iDest;

            while (iCurrentNode != -1)
            {
                Report.ShortestPathToDest.Insert(0, graph.NodeDetails[iCurrentNode].Id);
                iCurrentNode = parents[iCurrentNode];
            }
        }
    }
}
