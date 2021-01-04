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
    public class Dijkstra_v2Controller : Controller
    {
        [HttpGet("example")]
        public DijkstraReport Example()
        {
            Grid grid = new Grid(5, 5);
            grid.SetStart(2, 1);
            grid.SetFinish(3, 4);

            Graph graph = new Graph(grid);

            DijkstraAnalysis newDijkstra = new DijkstraAnalysis(graph);
            return newDijkstra.Report;
        }

        [HttpPost("analyse")]
        public DijkstraReport Analyse([FromBody] GridVM input)
        {
            Grid grid = input.ConvertToBetterGrid();
            Graph graph = new Graph(grid);

            DijkstraAnalysis newDijkstra = new DijkstraAnalysis(graph);
            return newDijkstra.Report;
        }
    }
}
