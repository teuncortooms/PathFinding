﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models
{
    public class GridVM
    {
        public List<List<NodeVM>> Nodes { get; set; } // List for Swagger

        public Grid_v1 ConvertToGrid_v1()
        {
            int nRows = Nodes.Count();
            int nCols = Nodes[0].Count();
            Node_v1[,] nodes = new Node_v1[nRows, nCols];
            for (int iRow = 0; iRow < nRows; iRow++)
            {
                for (int iCol = 0; iCol < nCols; iCol++)
                {
                    int id = Nodes[iRow][iCol].Id;
                    bool isStart = Nodes[iRow][iCol].IsStart;
                    bool isFinish = Nodes[iRow][iCol].IsFinish;
                    bool isWall = Nodes[iRow][iCol].IsWall;
                    nodes[iRow, iCol] = new Node_v1(id, isStart, isFinish, isWall);
                }
            }
            return new Grid_v1(nodes);
        }

        public Grid ConvertToBetterGrid() // not using template method to avoid factories for now
        {
            int nRows = Nodes.Count();
            int nCols = Nodes[0].Count();
            Node[,] nodes = new Node[nRows, nCols];
            for (int iRow = 0; iRow < nRows; iRow++)
            {
                for (int iCol = 0; iCol < nCols; iCol++)
                {
                    int id = Nodes[iRow][iCol].Id;
                    bool isStart = Nodes[iRow][iCol].IsStart;
                    bool isFinish = Nodes[iRow][iCol].IsFinish;
                    bool isWall = Nodes[iRow][iCol].IsWall;
                    nodes[iRow, iCol] = new Node(id, isStart, isFinish, isWall);
                }
            }
            return new Grid(nodes);
        }
    }
}
