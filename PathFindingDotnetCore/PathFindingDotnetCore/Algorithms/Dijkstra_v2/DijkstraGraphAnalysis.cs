using PathFindingDotnetCore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PathFindingDotnetCore.Algorithms.Dijkstra
{
    public class DijkstraGraphAnalysis
    {
        private readonly Graph graph;
        private readonly int[] distanceToSrc;
        private readonly bool[] isVisited;
        private readonly int[] parents;

        public DijkstraReport Report { get; }

        public DijkstraGraphAnalysis(Graph graph)
        {
            this.graph = graph;
            int n = graph.NodeDetails.Length;
            distanceToSrc = new int[n];
            isVisited = new bool[n];
            parents = new int[n];
            for (int i = 0; i < n; i++)
            {
                distanceToSrc[i] = int.MaxValue;
                isVisited[i] = false;
                parents[i] = -1;
            }

            Report = new DijkstraReport();

            int iSrc = graph.GetSourceIdx();
            int iDest = graph.GetDestinationIdx();
            StartAnalysis(iSrc, iDest);
        }

        private void StartAnalysis(int iSrc, int iDest)
        {
            distanceToSrc[iSrc] = 0;

            int nNodes = graph.NodeDetails.Length;
            for (int i = 0; i < nNodes - 1; i++)
            {
                int Idx = GetClosestIdx(); // equals src in first iteration. 
                if (distanceToSrc[Idx] == int.MaxValue) break; // no more paths from src
                isVisited[Idx] = true;
                Report.VisitedInOrder.Add(graph.NodeDetails[Idx].Id);
                if (graph.NodeDetails[Idx].IsFinish) break; // finished
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
                    minDistance = distanceToSrc[iNode];
                    closestNodeIdx = iNode;
                }
            }
            return closestNodeIdx;
        }

        private bool IsShorterDistance(int iNode, int minDistance)
        {
            return distanceToSrc[iNode] <= minDistance;
        }

        private void UpdateNeighbours(int currentNodeIdx)
        {
            int nNodes = graph.NodeDetails.Length;

            for (int newNodeIdx = 0; newNodeIdx < nNodes; newNodeIdx++)
            {
                if (!isVisited[newNodeIdx] && IsConnected(currentNodeIdx,newNodeIdx) && IsShorterPath(currentNodeIdx, newNodeIdx))
                {
                    int connectionWeight = graph.EdgeMatrix[currentNodeIdx, newNodeIdx];
                    distanceToSrc[newNodeIdx] = distanceToSrc[currentNodeIdx] + connectionWeight;
                    parents[newNodeIdx] = currentNodeIdx;
                }
            }
        }

        private bool IsShorterPath(int currentNodeIdx, int newNodeIdx)
        {
            int connectionWeight = graph.EdgeMatrix[currentNodeIdx, newNodeIdx];
            return distanceToSrc[currentNodeIdx] + connectionWeight < distanceToSrc[newNodeIdx];
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
