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
    public class NewDijkstraController : Controller
    {
        [HttpGet("example")]
        public NewDijkstraReport Example()
        {
            BetterGrid grid = new BetterGrid(5, 5);
            grid.SetStart(2, 1);
            grid.SetFinish(3, 4);

            BetterGraph graph = new BetterGraph(grid);

            NewDijkstraAnalysis newDijkstra = new NewDijkstraAnalysis(graph);
            return newDijkstra.Report;
        }

        [HttpPost("analyse")]
        public NewDijkstraReport Analyse([FromBody] GridVM input)
        {
            BetterGrid grid = input.ConvertToBetterGrid();
            BetterGraph graph = new BetterGraph(grid);

            NewDijkstraAnalysis newDijkstra = new NewDijkstraAnalysis(graph);
            return newDijkstra.Report;
        }
    }
}
