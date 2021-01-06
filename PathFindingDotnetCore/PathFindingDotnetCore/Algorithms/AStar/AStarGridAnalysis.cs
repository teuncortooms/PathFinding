using PathFindingDotnetCore.Models;
using PathFindingDotnetCore.Models.v3;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PathFindingDotnetCore.Algorithms.Dijkstra
{
    public class AStarGridAnalysis
    {
        private readonly int nRows;
        private readonly int nCols;
        private readonly CellWithAStarDetails[,] cells;

        public AStarReport Report { get; }

        public AStarGridAnalysis(Grid grid)
        {
            nRows = grid.Cells.GetLength(0);
            nCols = grid.Cells.GetLength(1);
            cells = new CellWithAStarDetails[nRows, nCols];
            Report = new AStarReport();

            InitCells(grid.Cells);
            StartAnalysis();
        }

        private void InitCells(Cell[,] cells)
        {
            for (int iRow = 0; iRow < nRows; iRow++)
            {
                for (int iCol = 0; iCol < nCols; iCol++)
                {
                    this.cells[iRow, iCol] = new CellWithAStarDetails(cells[iRow, iCol]);
                }
            }
        }

        private void StartAnalysis()
        {
            var src = GetSource();
            var dest = GetDestination();
            src.DistanceToSrc = 0;
            src.EstimateToDest = 0;

            int nCells = cells.Length;
            for (int i = 0; i < nCells - 1; i++)
            {
                var cell = GetNext(); // equals src in first iteration. 
                if (cell.Sum == int.MaxValue) break; // no more paths from src
                cell.IsClosed = true;
                Report.VisitedInOrder.Add(cell.Id);
                if (cell.IsFinish) break; // finished
                UpdateNeighbours(cell, dest);
            }

            GetShortestPathToDest(dest);
        }

        private CellWithAStarDetails GetSource()
        {
            for (int row = 0; row < nRows; row++)
            {
                for (int col = 0; col < nCols; col++)
                {
                    CellWithAStarDetails cell = cells[row, col];
                    if (cell.IsStart) return cell;
                }
            }
            return null;
        }

        private CellWithAStarDetails GetDestination()
        {
            for (int row = 0; row < nRows; row++)
            {
                for (int col = 0; col < nCols; col++)
                {
                    CellWithAStarDetails cell = cells[row, col];
                    if (cell.IsFinish) return cell;
                }
            }
            return null;
        }

        private CellWithAStarDetails GetNext()
        {
            double lowestSum = double.MaxValue;
            CellWithAStarDetails potentialNext = null;

            for (int row = 0; row < nRows; row++)
            {
                for (int col = 0; col < nCols; col++)
                {
                    var cell = cells[row, col];
                    if (!cell.IsClosed && cell.Sum <= lowestSum)
                    {
                        lowestSum = cell.Sum;
                        potentialNext = cell;
                    }
                }
            }
            return potentialNext;
        }

        private void UpdateNeighbours(CellWithAStarDetails currentCell, CellWithAStarDetails dest)
        {
            var neighbours = GetNeighbours(currentCell);

            foreach (var neighbour in neighbours)
            {
                if (!neighbour.IsClosed && IsLowerSum(currentCell, neighbour))
                {
                    neighbour.DistanceToSrc = currentCell.DistanceToSrc + 1;
                    neighbour.EstimateToDest = GetManhattan(neighbour, dest);
                    neighbour.Parent = currentCell;
                }
                if (neighbour.IsClosed && IsLowerSum(currentCell, neighbour))
                {
                    neighbour.IsClosed = false;
                }
            }
        }

        private List<CellWithAStarDetails> GetNeighbours(CellWithAStarDetails cell)
        {
            var neighbours = new List<CellWithAStarDetails>();

            int row = cell.Row;
            int col = cell.Col;

            // add if right non-wall neighbour
            if (col < nCols - 1) AddNonWallNeighbour(neighbours, cells[row, col + 1]);

            // add if left non-wall neighbour
            if (col > 0) AddNonWallNeighbour(neighbours, cells[row, col - 1]);
            
            // add if below non-wall neighbour
            if (row < nRows - 1) AddNonWallNeighbour(neighbours, cells[row + 1, col]);

            // add if above non-wall neighbour
            if (row > 0) AddNonWallNeighbour(neighbours, cells[row - 1, col]);

            return neighbours;
        }

        private void AddNonWallNeighbour(List<CellWithAStarDetails> neighbours, CellWithAStarDetails neighbour)
        {
            if (!neighbour.IsWall) neighbours.Add(neighbour);
        }

        private bool IsLowerSum(CellWithAStarDetails currentCell, CellWithAStarDetails newCell)
        {
            return currentCell.Sum + 1 < newCell.Sum;
        }

        private int GetManhattan(CellWithAStarDetails cell, CellWithAStarDetails dest)
        {
            
            return Math.Abs(cell.Col - dest.Col) + Math.Abs(cell.Row - dest.Row);
        }

        private void GetShortestPathToDest(CellWithAStarDetails dest)
        {
            var currentCell = dest;

            while (currentCell != null)
            {
                Report.ShortestPathToDest.Insert(0, currentCell.Id);
                currentCell = currentCell.Parent;
            }
        }
    }
}
