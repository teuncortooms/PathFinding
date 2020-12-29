﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models
{
    public class Node
    {
        public bool Id { get; private set; }
        public bool IsStart { get; private set; }
        public bool IsFinish { get; private set; } 
        public bool IsWall { get; private set; }

        public Node()
        {
            Id = 
            IsStart = false;
            IsFinish = false;
            IsWall = false;
        }
    }
}
