﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Models
{
    public class Grid
    {
        public Guid Id { get; }
        public Cell[,] Cells { get; set; }

        public Grid(Cell[,] cells)
        {
            Id = Guid.NewGuid();
            Cells = cells;
        }

        public Grid(int rows, int cols)
        {
            Id = Guid.NewGuid();
            Cells = new Cell[rows, cols];
            BuildCells(rows, cols);
        }

        private void BuildCells(int rows, int cols)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Cells[row, col] = new Cell(row, col);
                }
            }
        }

        public void SetStart(int row, int col)
        {
            Cell oldStart = GetCell(cell => cell.IsStart);
            if (oldStart != null) oldStart.IsStart = false;
            Cells[row, col].IsStart = true;
        }

        public void SetDestination(int row, int col)
        {
            Cell oldDest = GetCell(cell => cell.IsDestination);
            if (oldDest != null) oldDest.IsDestination = false;
            Cells[row, col].IsDestination = true;
        }

        private delegate bool IsRequestedCell(Cell cell);
        private Cell GetCell(IsRequestedCell isRequest)
        {
            for (int iRow = 0; iRow < Cells.GetLength(0); iRow++)
            {
                for (int iCol = 0; iCol < Cells.GetLength(1); iCol++)
                {
                    if (isRequest(Cells[iRow, iCol])) return Cells[iRow, iCol];
                }
            }
            return null;
        }

        public bool ToggleWall(int row, int col)
        {
            Cell cell = Cells[row, col];
            cell.IsWall = !cell.IsWall;
            return cell.IsWall;
        }
    }
}
