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
    public class Dijkstra_v1Controller : Controller
    {
        // GET dijkstra/example
        [HttpGet("example")]
        public DijkstraReport_v1 Example()
        {
            Grid_v1 grid = new Grid_v1(5, 5);
            grid.SetStart(2, 1);
            grid.SetFinish(3, 4);

            Graph_v1 graph = new Graph_v1(grid);

            DijkstraAnalysis_v1 dijkstra = new DijkstraAnalysis_v1();
            dijkstra.AnalyseAndUpdate(graph);

            return dijkstra.Report;
        }

        // POST dijkstra
        [HttpPost("analyse")]
        public DijkstraReport_v1 Analyse([FromBody] GridVM input)
        {
            Grid_v1 grid = input.ConvertToGrid_v1();
            Graph_v1 graph = new Graph_v1(grid);

            DijkstraAnalysis_v1 dijkstra = new DijkstraAnalysis_v1();
            dijkstra.AnalyseAndUpdate(graph);

            return dijkstra.Report;
        }
    }
}
