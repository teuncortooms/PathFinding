using System.Collections.Generic;

namespace PathFindingDotnetCore.Algorithms.Dijkstra
{
    public class DijkstraReport
    {
        public List<int> VisitedInOrder { get; }
        public List<int> ShortestPathToDest { get; }

        public DijkstraReport()
        {
            VisitedInOrder = new List<int>();
            ShortestPathToDest = new List<int>();
        }
        
        public DijkstraReport(List<int> visitedInOrder, List<int> shortestPathToDest)
        {
            VisitedInOrder = visitedInOrder;
            ShortestPathToDest = shortestPathToDest;
        }
    }
}