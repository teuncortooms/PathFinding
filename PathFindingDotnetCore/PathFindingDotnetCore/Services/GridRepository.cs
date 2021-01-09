using PathFindingDotnetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Services
{
    public class GridRepository
    {
        private static readonly List<Grid> grids = new List<Grid>();

        static GridRepository()
        {
            for (int i = 0; i < 5; i++)
            {
                Grid grid = GetRandomGrid(40, 40);
                grids.Add(grid);
            }
        }

        private static Grid GetRandomGrid(int rows, int cols)
        {
            Random rnd = new Random();
            Grid grid = new Grid(rows, cols);

            // set walls
            int nWalls = cols * rows / 4;
            for (int j = 0; j < nWalls; j++)
            {
                grid.ToggleWall(rnd.Next(rows), rnd.Next(cols));
            }

            // set start
            Node startNode = grid.Cells[rnd.Next(rows), rnd.Next(cols)];
            startNode.IsStart = true;
            startNode.IsWall = false;

            // set finish
            Node finishNode;
            do
            {
                finishNode = grid.Cells[rnd.Next(rows), rnd.Next(cols)];
            } while (finishNode.IsStart);
            finishNode.IsDestination = true;
            finishNode.IsWall = false;

            return grid;
        }

        public List<Grid> GetAll()
        {
            return grids;
        }

        public Grid GetById(Guid id)
        {
            return grids.Where(grid => grid.Id == id).FirstOrDefault();
        }

        public Grid Create(Grid input)
        {
            Grid newGrid = new Grid(input.Cells);
            grids.Add(newGrid);
            return newGrid;
        }

        //public void Update(Guid id, Grid grid)
        //{
        //    Grid found = grids.Where(n => n.Id == id).FirstOrDefault();
        //    found.Nodes2D = grid.Nodes2D;
        //}

        public void Delete(Guid id)
        {
            grids.RemoveAll(n => n.Id == id);
        }
    }
}
