using Microsoft.AspNetCore.Mvc;
using PathFindingDotnetCore.Algorithms.Dijkstra;
using PathFindingDotnetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DijkstraController : Controller
    {
        // GET dijkstra/example
        [HttpGet("example")]
        public DijkstraAnalysis Example()
        {
            int[,] graph = new int[,] { { 0,  4,  0,  0,  0,  0,  0,  8,  0 },
                                        { 4,  0,  8,  0,  0,  0,  0,  11, 0 },
                                        { 0,  8,  0,  7,  0,  4,  0,  0,  2 },
                                        { 0,  0,  7,  0,  9,  14, 0,  0,  0 },
                                        { 0,  0,  0,  9,  0,  10, 0,  0,  0 },
                                        { 0,  0,  4,  14, 10, 0,  2,  0,  0 },
                                        { 0,  0,  0,  0,  0,  2,  0,  1,  6 },
                                        { 8,  11, 0,  0,  0,  0,  1,  0,  7 },
                                        { 0,  0,  2,  0,  0,  0,  6,  7,  0 } };
            return new DijkstraAnalysis(graph, 0, 7);
        }

        // POST dijkstra
        [HttpPost("analyse")]
        public DijkstraAnalysis Analyse([FromBody] GridVM input)
        {
            Node[,] nodes = ConvertInputToNodesArray(input);
            Grid grid = new Grid(nodes);

            int[,] graph = grid.GetGraph();
            int src = grid.GetSourceSerial();
            int dest = grid.GetDestinationSerial();

            return new DijkstraAnalysis(graph, src, dest);
        }

        private Node[,] ConvertInputToNodesArray(GridVM input)
        {
            int nRows = input.Nodes.Count();
            int nCols = input.Nodes[0].Count();
            Node[,] nodes = new Node[nRows, nCols];
            for (int iRow = 0; iRow < nRows; iRow++)
            {
                for (int iCol = 0; iCol < nCols; iCol++)
                {
                    nodes[iRow, iCol] = input.Nodes[iRow][iCol];
                }
            }

            return nodes;
        }
    }
}
