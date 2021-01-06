using System.Collections.Generic;

namespace PathFindingDotnetCore.Algorithms.Dijkstra
{
    public class DijkstraReport_v1
    {
        public List<int> VisitedInOrder { get; }
        public List<int> ShortestPathToDest { get; }

        public DijkstraReport_v1()
        {
            VisitedInOrder = new List<int>();
            ShortestPathToDest = new List<int>();
        }
        
        public DijkstraReport_v1(List<int> visitedInOrder, List<int> shortestPathToDest)
        {
            VisitedInOrder = visitedInOrder;
            ShortestPathToDest = shortestPathToDest;
        }
    }
}