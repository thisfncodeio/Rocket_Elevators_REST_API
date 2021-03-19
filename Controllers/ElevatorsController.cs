using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rocket_Elevators_REST_API.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using System.Net.Http;


namespace Rocket_Elevators_REST_API.Controllers
{   [ApiController]
    [Route("[controller]")]
    public class ElevatorsController : ControllerBase
    {
        private readonly RailsApp_developmentContext _context;

        public ElevatorsController(RailsApp_developmentContext context)
        {
            _context = context;
        }

        // GET: api/Elevators   ==> retrieves all Elevators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Elevators>>> GetElevators()
        {
            return await _context.Elevators.ToListAsync();
        }

       // GET: api/Elevators/{id}      ==> retrieve a specific elevator
      [HttpGet("{id}")]
        public async Task<ActionResult<Elevators>> Getelevator(long id)
        {
            var elevator = await _context.Elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }

            return elevator;
        }

        
     [HttpPut("{id}")]
        public async Task<IActionResult> PutmodifyBatterisStatus(long id, string Status)
        {
            if (Status == null)
            {
                return BadRequest();
            } 
            var elevator = await _context.Elevators.FindAsync(id);
            elevator.Status = Status;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElevatorsExists(id))
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


        // GET: api/Elevators/{id}/status
        [HttpGet("{id}/Status")]
        public async Task<ActionResult<string>> GetelevatorStatus( long id)
        {
            var elevator = await _context.Elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }

            return elevator.Status;
        }

        private bool ElevatorsExists(long id)
        {
            return _context.Elevators.Any(e => e.Id == id);
        }
          [HttpGet("inactive")]
            public async Task<ActionResult<List<Elevators>>> InactiveElevators()
            {
            var elevators = await _context.Elevators
                .Where(elevators => elevators.Status != "active")
                .ToListAsync();

            

            return elevators;
            }
   
        

    }
    
    
}