using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models
{
    public class Node
    {
        public int Id { get; private set; }
        public bool IsStart { get; set; }
        public bool IsFinish { get; set; } 
        public bool IsWall { get; set; }

        public Node(int id = 0, bool isStart = false, bool isFinish = false, bool isWall = false)
        {
            Id = id == 0 ? new Random().Next() : id;
            IsStart = isStart;
            IsFinish = isFinish;
            IsWall = isWall;
        }
    }
}
