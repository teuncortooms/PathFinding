using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models
{
    public class CellVM
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int Id{ get; set; }
        public bool IsStart { get; set; }
        public bool IsDestination { get; set; } 
        public bool IsWall { get; set; }

        public CellVM()
        {
            IsStart = false;
            IsDestination = false;
            IsWall = false;
        }

        public CellVM(Cell cell)
        {
            Row = cell.Row;
            Col = cell.Col;
            Id = cell.Id;
            IsStart = cell.IsStart;
            IsDestination = cell.IsDestination;
            IsWall = cell.IsWall;
        }
    }
}
