using PathFindingDotnetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Services
{
    public class GridRepository
    {
        private static readonly List<Grid_v1> grids = new List<Grid_v1>();

        static GridRepository()
        {
            for (int i = 0; i < 5; i++)
            {
                Grid_v1 grid = GetRandomGrid(40, 40);
                grids.Add(grid);
            }
        }

        private static Grid_v1 GetRandomGrid(int rows, int cols)
        {
            Random rnd = new Random();
            Grid_v1 grid = new Grid_v1(rows, cols);

            // set walls
            int nWalls = cols * rows / 4;
            for (int j = 0; j < nWalls; j++)
            {
                grid.ToggleWall(rnd.Next(rows), rnd.Next(cols));
            }

            // set start
            Node_v1 startNode = grid.Nodes2D[rnd.Next(rows), rnd.Next(cols)];
            startNode.IsStart = true;
            startNode.IsWall = false;

            // set finish
            Node_v1 finishNode;
            do
            {
                finishNode = grid.Nodes2D[rnd.Next(rows), rnd.Next(cols)];
            } while (finishNode.IsStart);
            finishNode.IsFinish = true;
            finishNode.IsWall = false;

            return grid;
        }

        public List<Grid_v1> GetAll()
        {
            return grids;
        }

        public Grid_v1 GetById(Guid id)
        {
            return grids.Where(grid => grid.Id == id).FirstOrDefault();
        }

        public Grid_v1 Create(Grid_v1 input)
        {
            Grid_v1 newGrid = new Grid_v1(input.Nodes2D);
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
