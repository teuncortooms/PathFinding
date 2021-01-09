using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models.v3
{
    public class AStarCell : Cell
    {
        public double F { get; set; }
        public double G { get; set; }
        public double H { get; set; }
        public bool IsOpen { get; set; }
        public bool IsClosed { get; set; }
        public AStarCell Parent { get; set; }

        public AStarCell(Cell cell)
            : base(cell.Row, cell.Col, cell.Id, cell.IsStart, cell.IsFinish, cell.IsWall)
        {
            F = double.MaxValue;
            G = double.MaxValue;
            H = double.MaxValue;
            IsOpen = false;
            IsClosed = false;
            Parent = null;
        }
    }
}
