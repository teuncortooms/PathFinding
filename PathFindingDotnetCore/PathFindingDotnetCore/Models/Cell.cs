using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models
{
    public class Cell : Node
    {
        public int Row { get; }
        public int Col { get; }

        public Cell(int row, int col, int id = -1, bool isStart = false, bool isDestination = false, bool isWall = false)
            : base(id, isStart, isDestination, isWall)
        {
            Col = col;
            Row = row;
        }

        public Node ConvertToNode()
        {
            return new Node(Id, IsStart, IsDestination, IsWall);
        }
    }
}
