using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models
{
    public class NodeVM
    {
        public bool Id { get; set; }
        public bool IsStart { get; set; }
        public bool IsFinish { get; set; } 
        public bool IsWall { get; set; }

        public NodeVM()
        {
            IsStart = false;
            IsFinish = false;
            IsWall = false;
        }
    }
}
