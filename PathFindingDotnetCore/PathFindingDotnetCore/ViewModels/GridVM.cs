using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models
{
    public class GridVM
    {
        public Guid Id { get; set; }
        public List<List<CellVM>> Cells { get; set; } // List for Swagger

        public GridVM() { }

        public GridVM(Grid grid)
        {
            Id = grid.Id;
            Cells = new List<List<CellVM>>();
            int nRows = grid.Cells.GetLength(0);
            int nCols = grid.Cells.GetLength(1);

            for (int row = 0; row < nRows; row++)
            {
                Cells.Add(new List<CellVM>());
                for (int col = 0; col < nCols; col++)
                {
                    Cell gridCell = grid.Cells[row, col];
                    Cells[row].Add(new CellVM(gridCell));
                }
            }
        }

        public Grid ConvertToGrid() // not using template method to avoid factories for now
        {
            int nRows = Cells.Count();
            int nCols = Cells[0].Count();
            var cells = new Cell[nRows, nCols];
            for (int row = 0; row < nRows; row++)
            {
                for (int col = 0; col < nCols; col++)
                {
                    int id = Cells[row][col].Id;
                    bool isStart = Cells[row][col].IsStart;
                    bool isDestination = Cells[row][col].IsDestination;
                    bool isWall = Cells[row][col].IsWall;
                    cells[row, col] = new Cell(row, col, id, isStart, isDestination, isWall);
                }
            }
            return new Grid(cells);
        }
    }
}
