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

        private static Grid GetRandomGrid(int width, int height)
        {
            Random rnd = new Random();
            Grid grid = new Grid(height, width);

            // set walls
            int nWalls = width * height / 4;
            for (int j = 0; j < nWalls; j++)
            {
                grid.Nodes[rnd.Next(height), rnd.Next(width)].IsWall = true;
            }

            // set start
            Node startNode = grid.Nodes[rnd.Next(height), rnd.Next(width)];
            startNode.IsStart = true;
            startNode.IsWall = false;

            // set finish
            Node finishNode;
            do
            {
                finishNode = grid.Nodes[rnd.Next(height), rnd.Next(width)];
            } while (finishNode.IsStart);
            finishNode.IsFinish = true;
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
            Grid newGrid = new Grid(input.Nodes);
            grids.Add(newGrid);
            return newGrid;
        }

        public void Update(Guid id, Grid grid)
        {
            Grid found = grids.Where(n => n.Id == id).FirstOrDefault();
            found.Nodes = grid.Nodes;
        }

        public void Delete(Guid id)
        {
            grids.RemoveAll(n => n.Id == id);
        }
    }
}
