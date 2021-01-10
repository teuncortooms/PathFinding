using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models
{
    public class Graph
    {
        public Node[] NodeDetails { get; private set; }
        public int[,] EdgeMatrix { get; private set; }

        public Graph(Grid grid)
        {
            int n = grid.Cells.Length;
            NodeDetails = new Node[n];
            EdgeMatrix = new int[n, n];
            for (int matrixRow = 0; matrixRow < n; matrixRow++)
                for (int matrixCol = 0; matrixCol < n; matrixCol++)
                    EdgeMatrix[matrixRow, matrixCol] = 0;

            PopulateNodes(grid);
            PopulateEdges(grid);
        }

        private void PopulateNodes(Grid grid)
        {
            Node[,] nodes2d = grid.Cells;

            int iNode = 0;
            for (int iRow = 0; iRow < nodes2d.GetLength(0); iRow++)
            {
                for (int iCol = 0; iCol < nodes2d.GetLength(1); iCol++)
                {
                    NodeDetails[iNode] = nodes2d[iRow, iCol];
                    iNode++;
                }
            }
        }

        private void PopulateEdges(Grid grid)
        {
            Node[,] nodes2d = grid.Cells;
            int nRows = nodes2d.GetLength(0);
            int nCols = nodes2d.GetLength(1);
            
            int iNode = 0;
            for (int iRow = 0; iRow < nRows; iRow++)
            {
                for (int iCol = 0; iCol < nCols; iCol++)
                {
                    Node currentNode = nodes2d[iRow, iCol];
                    if (!currentNode.IsWall)
                        AddEdgesOfNode(iNode, iRow, iCol, grid);
                    iNode++;
                }
            }
        }

        private void AddEdgesOfNode(int iNode, int iRow, int iCol, Grid grid)
        {
            if (this.NodeDetails[iNode] != grid.Cells[iRow, iCol]) throw new Exception("Node index error!");

            Node[,] nodes2d = grid.Cells;
            int nRow = nodes2d.GetLength(0); 
            int nCol = nodes2d.GetLength(1);

            // if has right non-wall neighbour
            if (iCol < nCol - 1 && !nodes2d[iRow, iCol + 1].IsWall)
            {
                EdgeMatrix[iNode, iNode + 1] = 1;
                EdgeMatrix[iNode + 1, iNode] = 1;
            }
            // if has below non-wall neighbour
            if (iRow < nRow - 1 && !nodes2d[iRow + 1, iCol].IsWall)
            {
                EdgeMatrix[iNode, iNode + nCol] = 1;
                EdgeMatrix[iNode + nCol, iNode] = 1;
            }
            // bidirectional edges, so above and left aren't needed
        }

        public int GetStartId()
        {
            foreach (var node in NodeDetails)
            {
                if (node.IsStart) return node.Id;
            }
            return -1;
        }

        public int GetDestinationId()
        {
            foreach (var node in NodeDetails)
            {
                if (node.IsDestination) return node.Id;
            }
            return -1;
        }

        public int GetStartIdx()
        {
            for (int i = 0; i < NodeDetails.Length; i++)
            {
                if (NodeDetails[i].IsStart) return i;
            }
            return -1;
        }

        public int GetDestinationIdx()
        {
            for (int i = 0; i < NodeDetails.Length; i++)
            {
                if (NodeDetails[i].IsDestination) return i;
            }
            return -1;
        }
    }
}
