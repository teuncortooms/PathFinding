using System.Collections.Generic;

namespace PathFindingDotnetCore.Algorithms.Dijkstra
{
    public class AStarReport
    {
        public List<int> VisitedInOrder { get; }
        public List<int> ShortestPathToDest { get; }

        public AStarReport()
        {
            VisitedInOrder = new List<int>();
            ShortestPathToDest = new List<int>();
        }
        
        public AStarReport(List<int> visitedInOrder, List<int> shortestPathToDest)
        {
            VisitedInOrder = visitedInOrder;
            ShortestPathToDest = shortestPathToDest;
        }
    }
}