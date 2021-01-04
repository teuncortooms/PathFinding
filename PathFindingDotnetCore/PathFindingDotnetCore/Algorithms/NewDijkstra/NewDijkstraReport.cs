using System.Collections.Generic;

namespace PathFindingDotnetCore.Algorithms.Dijkstra
{
    public class NewDijkstraReport
    {
        public List<int> VisitedInOrder { get; }
        public List<int> ShortestPathToDest { get; }

        public NewDijkstraReport()
        {
            VisitedInOrder = new List<int>();
            ShortestPathToDest = new List<int>();
        }
        
        public NewDijkstraReport(List<int> visitedInOrder, List<int> shortestPathToDest)
        {
            VisitedInOrder = visitedInOrder;
            ShortestPathToDest = shortestPathToDest;
        }
    }
}