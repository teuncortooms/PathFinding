using PathFindingDotnetCore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PathFindingDotnetCore.Algorithms.Dijkstra
{
    public class DijkstraAnalysis_v1
    {
        public DijkstraReport_v1 Report { get; }

        public DijkstraAnalysis_v1()
        {
            Report = new DijkstraReport_v1();
        }

        public Graph_v1 AnalyseAndUpdate(Graph_v1 graph)
        {   
            for (int num = 0; num < graph.Nodes.Count - 1; num++)
            {
                Node_v1 currentNode = GetClosest(graph); // equals src in first iteration. 
                if (currentNode.DistanceToSrc == int.MaxValue) break; // no more paths from src
                currentNode.VisitedSerialNo = num;
                Report.VisitedInOrder.Add(currentNode.Id);
                if (currentNode.IsFinish) break; // finished
                UpdateNeighbours(graph, currentNode);
            }
            GetShortestPathToDest(graph);

            return graph;
        }

        private Node_v1 GetClosest(Graph_v1 graph)
        {
            int minDistance = int.MaxValue;
            Node_v1 closestNode = null;

            foreach (var node in graph.Nodes)
            {
                if (IsNotVisited(node) && node.DistanceToSrc <= minDistance)
                {
                    minDistance = node.DistanceToSrc;
                    closestNode = node;
                }
            }
            return closestNode;
        }
        
        private void UpdateNeighbours(Graph_v1 graph, Node_v1 currentNode)
        {
            foreach (var newNode in graph.Nodes)
            {
                if (IsNotVisited(newNode))
                {
                    // TODO: refactor because increases complexity from O(V^2) to O(V * V * V^2)
                    // a) searches ALL (upto 3080) edges. In the original 2d-array I could just use indexes 
                    //    (I wanted to use ids instead of indexes for readability)
                    // b) does this for ALL (upto 800) non-visited nodes
                    // c) for ALL visited nodes
                    Edge_v1 connection = graph.Edges.Find(edge => edge.FromId == currentNode.Id && edge.ToId == newNode.Id);
                    if (connection != null && IsShorterPath(currentNode, connection, newNode))
                    {
                        newNode.DistanceToSrc = currentNode.DistanceToSrc + connection.Weight;
                        newNode.ParentId = currentNode.Id;
                    }
                }
            }
        }

        private bool IsNotVisited(Node_v1 node)
        {
            return node.VisitedSerialNo < 0;
        }

        private bool IsShorterPath(Node_v1 currentNode, Edge_v1 connection, Node_v1 newNode)
        {
            return currentNode.DistanceToSrc + connection.Weight < newNode.DistanceToSrc;
        }

        private void GetShortestPathToDest(Graph_v1 graph)
        {
            var nodes = graph.Nodes;
            Node_v1 currentNode = nodes.Find(node => node.IsFinish);

            while (currentNode != null) // NB: another O(V^2) in worst case, makes O(2V^2). Still equals O(V^2) in asymptotic analysis
            {
                Report.ShortestPathToDest.Insert(0, currentNode.Id);
                currentNode = nodes.Find(node => node.Id == currentNode.ParentId); // hidden iteration
            }
        }
    }
}
