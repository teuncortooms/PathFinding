using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models
{
    public class NodeVM
    {
        public int Id{ get; set; }
        public bool IsStart { get; set; }
        public bool IsDestination { get; set; } 
        public bool IsWall { get; set; }

        public NodeVM()
        {
            IsStart = false;
            IsDestination = false;
            IsWall = false;
        }

        public NodeVM(Node node)
        {
            Id = node.Id;
            IsStart = node.IsStart;
            IsDestination = node.IsDestination;
            IsWall = node.IsWall;
        }
    }
}
