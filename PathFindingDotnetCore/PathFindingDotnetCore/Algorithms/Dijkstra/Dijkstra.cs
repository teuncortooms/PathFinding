using PathFindingDotnetCore.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace PathFindingDotnetCore.Algorithms.Dijkstra
{
    public class Dijkstra
    {
        
        
        public List<Node> Go(Node[][] grid, Node startNode, Node finishNode)
        {
            if (startNode == null || finishNode == null|| startNode.Equals(finishNode)) return null;

            List<Node> visitedNodesInOrder = new List<Node>();
            //startNode.Distance = 0;
            //List<Node> unvisitedNodes = GetAllNodes(grid);
            //bool foundHim = false;
            //while (unvisitedNodes.Count != 0 && !foundHim)
            //{
            //    SortNodesByDistance(unvisitedNodes); // can't sort queues
            //    Node closestNode = unvisitedNodes[0];
            //    unvisitedNodes.RemoveAt(0);
            //    if (closestNode.IsWall) continue;
            //    closestNode.IsVisited = true;
            //    visitedNodesInOrder.Add(closestNode);
            //    if (closestNode.Equals(finishNode)) foundHim = true;
            //    else UpdateUnvisitedNeighbours(closestNode, grid);
            //}
            return visitedNodesInOrder;
        }

        //private List<Node> GetAllNodes(Node[][] grid)
        //{
        //    List<Node> nodes = new List<Node>();
        //    foreach (Node[] row in grid) {
        //        foreach (Node node in row) {
        //            nodes.Add(node);
        //        }
        //    }
        //    return nodes;
        //}

        //private void SortNodesByDistance(List<Node> unvisitedNodes)
        //{
        //    unvisitedNodes.Sort((nodeA, nodeB) => nodeA.Distance - nodeB.Distance);
        //}

        //private void UpdateUnvisitedNeighbours(Node node, Node[][] grid)
        //{
        //    List<Node> unvisitedNeighbours = GetUnvisitedNeighbours(node, grid);
        //    foreach (Node neighbour in unvisitedNeighbours) {
        //        neighbour.Distance = node.Distance + 1;
        //        neighbour.PreviousNode = node;
        //    }
        //}

        //List<Node> GetUnvisitedNeighbours(Node node, Node[][] grid)
        //{
        //    List<Node> neighbours = new List<Node>();
        //    int row = node.Row;
        //    int col = node.Col;
        //    if (row > 0) neighbours.Add(grid[row - 1][col]); // add above
        //    if (row < grid.Length - 1) neighbours.Add(grid[row + 1][col]); // add below
        //    if (col > 0) neighbours.Add(grid[row][col - 1]); // add left
        //    if (col < grid[0].Length - 1) neighbours.Add(grid[row][col + 1]); // add right
        //    return neighbours.FindAll(neighbour => !neighbour.IsVisited);
        //}

        //public List<Node> GetPath(List<Node> visitedNodesInOrder)
        //{
        //    List<Node> path = new List<Node>();
        //    int last = visitedNodesInOrder.Count - 1;
        //    Node node = visitedNodesInOrder[last];
        //    if (!node.IsFinish) return null;

        //    while (!node.IsStart)
        //    {
        //        path.Add(node);
        //        node = node.PreviousNode;
        //    }
        //    path.Add(node);
        //    return path;
        //}
    }
}
