using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models.v3
{
    public class CellWithAStarDetails : Cell
    {
        private double _distanceToSrc;
        private double _estimateToDest;

        public CellWithAStarDetails Parent { get; set; }
        public double DistanceToSrc { get { return _distanceToSrc; } set { _distanceToSrc = value; UpdateSum(); } }
        public double EstimateToDest { get { return _estimateToDest; } set { _estimateToDest = value; UpdateSum(); } }
        public double Sum { get; private set; }
        public bool IsClosed { get; set; }

        public CellWithAStarDetails(Cell cell)
            : base(cell.Row, cell.Col, cell.Id, cell.IsStart, cell.IsFinish, cell.IsWall)
        {
            DistanceToSrc = double.MaxValue;
            EstimateToDest = double.MaxValue;
            IsClosed = false;
            Parent = null;
        }

        private void UpdateSum()
        {
            Sum = DistanceToSrc + EstimateToDest;
        }
    }
}
