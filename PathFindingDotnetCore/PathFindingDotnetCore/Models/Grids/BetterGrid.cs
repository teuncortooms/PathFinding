using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models
{
    public class BetterGrid : IGrid
    {
        public Guid Id { get; }
        public BetterNode[,] Nodes2D { get; }

        public BetterGrid(BetterNode[,] nodes)
        {
            Id = Guid.NewGuid();
            Nodes2D = nodes;
        }
        
        public BetterGrid(int rows, int cols)
        {
            Id = Guid.NewGuid();
            Nodes2D = new BetterNode[rows, cols];
            BuildNodes(rows, cols);
        }

        private void BuildNodes(int rows, int cols)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Nodes2D[row, col] = new BetterNode();
                }
            }
        }

        public void SetStart(int row, int col)
        {
            BetterNode oldStart = GetNode(node => node.IsStart);
            if (oldStart != null) oldStart.IsStart = false;
            Nodes2D[row, col].IsStart = true;
        }

        public void SetFinish(int row, int col)
        {
            BetterNode oldFinish = GetNode(node => node.IsFinish);
            if (oldFinish != null) oldFinish.IsFinish = false;
            Nodes2D[row, col].IsFinish = true;
        }

        private delegate bool IsRequestedNode(BetterNode node);
        private BetterNode GetNode(IsRequestedNode isRequest)
        {
            for (int iRow = 0; iRow < Nodes2D.GetLength(0); iRow++)
            {
                for (int iCol = 0; iCol < Nodes2D.GetLength(1); iCol++)
                {
                    if (isRequest(Nodes2D[iRow, iCol])) return Nodes2D[iRow, iCol];
                }
            }
            return null;
        }

        public bool ToggleWall(int row, int col)
        {
            BetterNode node = Nodes2D[row, col];
            node.IsWall = !node.IsWall;
            return node.IsWall;
        }
    }
}
