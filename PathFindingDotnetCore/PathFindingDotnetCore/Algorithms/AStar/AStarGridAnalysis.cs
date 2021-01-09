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
        private readonly AStarCell[,] cells;

        public AStarReport Report { get; }

        public AStarGridAnalysis(Grid grid)
        {
            nRows = grid.Cells.GetLength(0);
            nCols = grid.Cells.GetLength(1);
            cells = new AStarCell[nRows, nCols];
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
                    this.cells[iRow, iCol] = new AStarCell(cells[iRow, iCol]);
                }
            }
        }

        private void StartAnalysis()
        {
            var src = GetSource();
            var dest = GetDestination();
            src.G = 0;
            src.H = 0;
            src.F = 0;

            int nCells = cells.Length;
            for (int i = 0; i < nCells - 1; i++)
            {
                var cell = GetNextBest(); // equals src in first iteration. 
                CloseCell(cell);
                if (cell.G == int.MaxValue) break; // no more paths from src
                if (cell.IsFinish) break; // finished
                UpdateNeighbours(cell, dest);
            }

            GetShortestPathToDest(dest);
        }

        private AStarCell GetSource()
        {
            for (int row = 0; row < nRows; row++)
            {
                for (int col = 0; col < nCols; col++)
                {
                    AStarCell cell = cells[row, col];
                    if (cell.IsStart) return cell;
                }
            }
            return null;
        }

        private AStarCell GetDestination()
        {
            for (int row = 0; row < nRows; row++)
            {
                for (int col = 0; col < nCols; col++)
                {
                    AStarCell cell = cells[row, col];
                    if (cell.IsFinish) return cell;
                }
            }
            return null;
        }

        private AStarCell GetNextBest()
        {
            AStarCell winner = cells[0, 0];

            for (int row = 0; row < nRows; row++)
            {
                for (int col = 0; col < nCols; col++)
                {
                    var cell = cells[row, col];
                    if (!cell.IsClosed && cell.F < winner.F)
                    {
                        winner = cell;
                    }
                    if (!cell.IsClosed && cell.F == winner.F)
                    {
                        //Prefer to explore options with longer known paths (closer to goal)
                        if (cell.G > winner.G)
                        {
                            winner = cell;
                        }
                    }
                }
            }
            return winner;
        }


        private void CloseCell(AStarCell cell)
        {
            cell.IsClosed = true;
            Report.VisitedInOrder.Add(cell.Id);
        }

        private void UpdateNeighbours(AStarCell current, AStarCell dest)
        {
            var neighbours = GetNeighbours(current);

            foreach (var neighbour in neighbours)
            {
                double tempG = current.G + 1;
                if (!neighbour.IsClosed && tempG < neighbour.G)
                {
                    neighbour.G = tempG;
                    neighbour.H = GetManhattan(neighbour, dest);
                    neighbour.F = neighbour.G + neighbour.H;
                    neighbour.Parent = current;
                }
            }
        }

        private List<AStarCell> GetNeighbours(AStarCell cell)
        {
            var neighbours = new List<AStarCell>();

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

        private void AddNonWallNeighbour(List<AStarCell> neighbours, AStarCell neighbour)
        {
            if (!neighbour.IsWall) neighbours.Add(neighbour);
        }

        private int GetManhattan(AStarCell cell, AStarCell dest)
        {
            
            return Math.Abs(dest.Col - cell.Col) + Math.Abs(dest.Row - cell.Row);
        }

        private void GetShortestPathToDest(AStarCell dest)
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
