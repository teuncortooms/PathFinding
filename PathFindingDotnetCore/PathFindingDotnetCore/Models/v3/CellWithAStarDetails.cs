using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models.v3
{
    public class CellWithAStarDetails : Cell
    {
        public CellWithAStarDetails Parent { get; set; }
        public double DistanceToSrc { get; set; }
        public double EstimateToDest { get; set; }
        public double Sum { get { return DistanceToSrc + EstimateToDest; } }
        public bool IsClosed { get; set; }

        public CellWithAStarDetails(Cell cell)
            : base(cell.Row, cell.Col, cell.Id, cell.IsStart, cell.IsFinish, cell.IsWall)
        {
            DistanceToSrc = double.MaxValue;
            EstimateToDest = double.MaxValue;
            IsClosed = false;
            Parent = null;
        }
    }
}
