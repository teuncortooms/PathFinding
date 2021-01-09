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
        public IEnumerable<Grid> Get()
        {
            return gridService.GetAll();
        }

        // GET grids/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(gridService.GetById(id));
        }

        // POST grids
        [HttpPost]
        public IActionResult Post([FromBody] Grid grid)
        {
            return CreatedAtAction("Get", new { id = grid.Id }, gridService.Create(grid));
        }

        // PUT grids/5
        [HttpPut("{id}")]
#pragma warning disable IDE0060 // Remove unused parameter
        public IActionResult Put(Guid id, [FromBody] Grid grid)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            throw new NotImplementedException();
            //gridService.Update(id, grid);
            //return NoContent();
        }

        // DELETE grids/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            gridService.Delete(id);
            return NoContent();
        }

        public override NoContentResult NoContent()
        {
            return base.NoContent();
        }
    }
}
