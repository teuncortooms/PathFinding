﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models
{
    public class BetterNode : INode
    {
        public int Id { get; private set; }
        public bool IsWall { get; set; }
        public bool IsFinish { get; set; }
        public bool IsStart { get; set; }

        public BetterNode(int id = -1, bool isStart = false, bool isFinish = false, bool isWall = false)
        {
            Id = id == -1 ? new Random().Next() : id;
            IsStart = isStart;
            IsFinish = isFinish;
            IsWall = isWall;
        }
    }
}
