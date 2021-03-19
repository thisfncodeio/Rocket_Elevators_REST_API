using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rocket_Elevators_REST_API.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

namespace Rocket_Elevators_REST_API.Models.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColumnsController : ControllerBase
    {
        private readonly RailsApp_developmentContext _context;
        public ColumnsController(RailsApp_developmentContext context)
        {
            _context = context;
        }
        // GET: api/columns    ==> retrieves all columns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Columns>>> Getcolumns()
        {
            return await _context.Columns.ToListAsync();
        }

         
        // GET: api/columns/id   ==> retreives a specific column
        [HttpGet("{id}")]
        public async Task<ActionResult<Columns>> Getcolumns(long id)
        {
            var column = await _context.Columns.FindAsync(id);

            if (column == null)
            {
                return NotFound();
            }

            return column;
        }
    

        
        // PUT: api/columns/id/updatestatus        
     [HttpPut("{id}")]
        public async Task<IActionResult> PutmodifyColumnsStatus(long id, [FromBody] Columns body)
        {



            if (body.Status == null)
                return BadRequest();

            var column = await _context.Columns.FindAsync(id);
            column.Status = body.Status;          
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!columnExists(id))
                    return NotFound();
                else
                    throw;
            }

            return new OkObjectResult("success");
        }
        
        [HttpGet("{id}/status")]
        public async Task<ActionResult<string>> GetcolumnStatus(long id)
        {
            var columns = await _context.Columns.FindAsync(id);
            if (columns == null)
            {
                return NotFound();
            }
            return columns.Status; 
        }
        private bool columnExists(long id)
        {
            return _context.Columns.Any(e => e.Id == id);
        }
    }
}