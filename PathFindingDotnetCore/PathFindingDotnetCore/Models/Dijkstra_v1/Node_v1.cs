using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models
{
    public class Node_v1
    {
        private bool _isStart;
        private int _distanceToSrc;

        public int Id { get; private set; }
        public bool IsWall { get; set; }
        public bool IsFinish { get; set; }
        public bool IsStart { get { return _isStart; } set { DistanceToSrc = 0; _isStart = value; } }

        // dijkstra props
        public int VisitedSerialNo { get; set; }
        public int ParentId { get; set; }
        public int DistanceToSrc { get { return _distanceToSrc; } set { if(!IsStart) _distanceToSrc = value; } }

        public Node_v1(int id = -1, bool isStart = false, bool isFinish = false, bool isWall = false)
        {
            Id = id == -1 ? new Random().Next() : id;
            IsStart = isStart;
            IsFinish = isFinish;
            IsWall = isWall;
            VisitedSerialNo = -1;
            ParentId = -1;
            DistanceToSrc = int.MaxValue;
        }
    }
}
