using Microsoft.AspNetCore.Mvc;
using PathFindingDotnetCore.Models;
using PathFindingDotnetCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFindingDotnetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GridsController : Controller
    {
        private readonly GridRepository gridService;

        public GridsController(GridRepository gridService)
        {
            this.gridService = gridService;
        }

        // GET grids
        [HttpGet]
        public IEnumerable<GridVM> Get()
        {
            List<Grid> grids = gridService.GetAll();
            List<GridVM> response = new List<GridVM>();
            foreach (var grid in grids)
            {
                response.Add(new GridVM(grid));
            }

            return response;
        }

        // GET grids/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(gridService.GetById(id));
        }

        // POST grids
        [HttpPost]
        public IActionResult Post([FromBody] GridVM input)
        {
            Grid grid = input.ConvertToGrid();
            return CreatedAtAction("GET", gridService.Add(grid));
        }

        // PUT grids/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] GridVM input)
        {
            Grid newGrid = input.ConvertToGrid();
            gridService.Update(id, newGrid);
            return NoContent();
        }

        // DELETE grids/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            gridService.Delete(id);
            return NoContent();
        }

    }
}
