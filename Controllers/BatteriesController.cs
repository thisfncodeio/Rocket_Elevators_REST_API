using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rocket_Elevators_REST_API.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;


namespace Rocket_Elevators_REST_API.Controllers
{   [ApiController]
    [Route("[controller]")]
    public class BatteriesController : ControllerBase
    {
        private readonly RailsApp_developmentContext _context;

        public BatteriesController(RailsApp_developmentContext context)
        {
            _context = context;
        }

        // GET: api/Batteries   ==> retrieves all batteries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Batteries>>> GetBatteries()
        {
            return await _context.Batteries.ToListAsync();
        }

        // GET: api/Batteries/{id}      ==> retrieve a specific battery
      [HttpGet("{id}")]
        public async Task<ActionResult<Batteries>> GetBattery(long id)
        {
            var battery = await _context.Batteries.FindAsync(id);

            if (battery == null)
            {
                return NotFound();
            }

            return battery;
        }

        
     [HttpPut("{id}")]
        public async Task<IActionResult> PutmodifyBatterisStatus(long id, string Status)
        {
            if (Status == null)
            {
                return BadRequest();
            } 
            var battery = await _context.Batteries.FindAsync(id);
            battery.Status = Status;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatteriesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // GET: api/Batteries/{id}/status
        [HttpGet("{id}/Status")]
        public async Task<ActionResult<string>> GetBatteryStatus( long id)
        {
            var battery = await _context.Batteries.FindAsync(id);

            if (battery == null)
            {
                return NotFound();
            }

            return battery.Status;
        }

        private bool BatteriesExists(long id)
        {
            return _context.Batteries.Any(e => e.Id == id);
        }

    }
    
}