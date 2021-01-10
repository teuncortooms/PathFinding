using System.Collections.Generic;

namespace PathFindingDotnetCore.Algorithms.Dijkstra
{
    public class Report
    {
        public List<int> VisitedInOrder { get; }
        public List<int> ShortestPathToDest { get; }

        public Report()
        {
            VisitedInOrder = new List<int>();
            ShortestPathToDest = new List<int>();
        }
        
        public Report(List<int> visitedInOrder, List<int> shortestPathToDest)
        {
            VisitedInOrder = visitedInOrder;
            ShortestPathToDest = shortestPathToDest;
        }
    }
}