using Microsoft.AspNetCore.Mvc;
using PathFindingDotnetCore.Algorithms.Dijkstra;
using PathFindingDotnetCore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DijkstraController : Controller
    {
        [HttpGet("example")]
        public Report Example()
        {
            Grid grid = new Grid(5, 5);
            grid.SetStart(2, 1);
            grid.SetDestination(3, 4);

            Graph graph = new Graph(grid);

            DijkstraGraphAnalysis newDijkstra = new DijkstraGraphAnalysis(graph);
            return newDijkstra.Report;
        }

        [HttpPost("analyse")]
        public Report Analyse([FromBody] GridVM input)
        {
            Grid grid = input.ConvertToGrid();
            Graph graph = new Graph(grid);

            DijkstraGraphAnalysis newDijkstra = new DijkstraGraphAnalysis(graph);
            return newDijkstra.Report;
        }
    }
}
