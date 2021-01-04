using PathFindingDotnetCore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PathFindingDotnetCore.Algorithms.Dijkstra
{
    public class Dijkstra
    {
        public DijkstraReport Report { get; }

        public Dijkstra()
        {
            Report = new DijkstraReport();
        }

        public Graph AnalyseAndUpdate(Graph graph)
        {   
            for (int num = 0; num < graph.Nodes.Count - 1; num++)
            {
                Node currentNode = GetClosest(graph); // equals src in first iteration. 
                if (currentNode.DistanceToSrc == int.MaxValue) break; // no more paths from src
                currentNode.VisitedSerialNo = num;
                Report.VisitedInOrder.Add(currentNode.Id);
                if (currentNode.IsFinish) break; // finished
                UpdateNeighbours(graph, currentNode);
            }
            GetShortestPathToDest(graph);

            return graph;
        }

        private Node GetClosest(Graph graph)
        {
            int minDistance = int.MaxValue;
            Node closestNode = null;

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
        
        private void UpdateNeighbours(Graph graph, Node currentNode)
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
                    Edge connection = graph.Edges.Find(edge => edge.FromId == currentNode.Id && edge.ToId == newNode.Id);
                    if (connection != null && IsShorterPath(currentNode, connection, newNode))
                    {
                        newNode.DistanceToSrc = currentNode.DistanceToSrc + connection.Weight;
                        newNode.ParentId = currentNode.Id;
                    }
                }
            }
        }

        private bool IsNotVisited(Node node)
        {
            return node.VisitedSerialNo < 0;
        }

        private bool IsShorterPath(Node currentNode, Edge connection, Node newNode)
        {
            return currentNode.DistanceToSrc + connection.Weight < newNode.DistanceToSrc;
        }

        private void GetShortestPathToDest(Graph graph)
        {
            var nodes = graph.Nodes;
            Node currentNode = nodes.Find(node => node.IsFinish);

            while (currentNode != null) // NB: another O(V^2) in worst case, makes O(2V^2). Still equals O(V^2) in asymptotic analysis
            {
                Report.ShortestPathToDest.Insert(0, currentNode.Id);
                currentNode = nodes.Find(node => node.Id == currentNode.ParentId); // hidden iteration
            }
        }
    }
}
