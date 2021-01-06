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
    public class AStarController : Controller
    {
        [HttpGet("example")]
        public AStarReport Example()
        {
            Grid grid = new Grid(5, 5);
            grid.SetStart(2, 1);
            grid.SetFinish(3, 4);

            AStarGridAnalysis aStar = new AStarGridAnalysis(grid);
            return aStar.Report;
        }

        [HttpPost("analyse")]
        public AStarReport Analyse([FromBody] GridVM input)
        {
            Grid grid = input.ConvertToBetterGrid();

            AStarGridAnalysis aStar = new AStarGridAnalysis(grid);
            return aStar.Report;
        }
    }
}
