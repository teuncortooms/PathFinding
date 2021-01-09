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
                Grid grid = GetRandomGrid(20, 40);
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
            Cell start = grid.Cells[rnd.Next(rows), rnd.Next(cols)];
            start.IsStart = true;
            start.IsWall = false;

            // set finish
            Cell finish;
            do { finish = grid.Cells[rnd.Next(rows), rnd.Next(cols)]; }
            while (finish.IsStart);
            finish.IsDestination = true;
            finish.IsWall = false;

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

        public bool Add(Grid newGrid)
        {
            grids.Add(newGrid);
            return true;
        }

        public bool Update(Guid id, Grid update)
        {
            Grid found = grids.Where(n => n.Id == id).FirstOrDefault();
            found.Cells = update.Cells;
            return true;
        }

        public void Delete(Guid id)
        {
            grids.RemoveAll(n => n.Id == id);
        }
    }
}
