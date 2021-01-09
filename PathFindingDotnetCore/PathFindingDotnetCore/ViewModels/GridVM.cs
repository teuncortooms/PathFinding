using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models
{
    public class GridVM
    {
        public List<List<NodeVM>> Nodes { get; set; } // List for Swagger

        public Grid ConvertToGrid() // not using template method to avoid factories for now
        {
            int nRows = Nodes.Count();
            int nCols = Nodes[0].Count();
            var cells = new Cell[nRows, nCols];
            for (int row = 0; row < nRows; row++)
            {
                for (int col = 0; col < nCols; col++)
                {
                    int id = Nodes[row][col].Id;
                    bool isStart = Nodes[row][col].IsStart;
                    bool isFinish = Nodes[row][col].IsFinish;
                    bool isWall = Nodes[row][col].IsWall;
                    cells[row, col] = new Cell(row, col, id, isStart, isFinish, isWall);
                }
            }
            return new Grid(cells);
        }
    }
}
