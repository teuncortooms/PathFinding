using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models
{
    public class Node
    {
        public Guid Id { get; }
        public bool IsStart { get; set; }
        public bool IsFinish { get; set; } 
        public bool IsWall { get; set; }
        public bool IsVisited { get; set; }
        public int Distance { get; set; }
        public Node PreviousNode { get; set; }

        public Node()
        {
            Id = Guid.NewGuid();
            IsStart = false;
            IsFinish = false;
            IsWall = false;
            IsVisited = false; 
            Distance = Convert.ToInt32(double.PositiveInfinity);
        }
    }
}
