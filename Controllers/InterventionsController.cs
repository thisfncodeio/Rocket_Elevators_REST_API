using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocket_Elevators_REST_API.Models;

namespace Rocket_Elevators_REST_API.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class InterventionsController : ControllerBase
  {
    private readonly RailsApp_developmentContext _context;

    public InterventionsController(RailsApp_developmentContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Interventions>>> GetInterventions()
    {
      return await _context.Interventions.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Interventions>> GetIntervention(long id)
    {
      var intervention = await _context.Interventions.FindAsync(id);

      if (intervention == null)
      {
        return NotFound();
      }

      return intervention;
    }

    [HttpGet("pending")]
    public async Task<ActionResult<List<Interventions>>> Pending()
    {
      var interventions = await _context.Interventions
        .Where(intervention => intervention.StartDate == null && intervention.Status == "Pending")
        .ToListAsync();

      if (interventions == null)
      {
        return NotFound();
      }

      return interventions;
    }


    [HttpPut("start/{id}")]
    public async Task<IActionResult> ChangeInterventionStatusToInProgress(long id)
    {
      var findIntervention = await _context.Interventions.FindAsync(id);

      // if (intervention == null)
      // {
      //   return BadRequest();
      // }

      if (findIntervention == null)
      {
        return NotFound();
      }

      if (findIntervention.Status == "InProgress")
      {
        ModelState.AddModelError("Status", "Looks like this intervention has already been started.");
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      findIntervention.Status = "InProgress";
      // string date = DateTime.Today.ToString("yyyy-MM-dd H:mm:ss");
      findIntervention.StartDate = DateTimeOffset.Now;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!InterventionExists(id))
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

    [HttpPut("complete/{id}")]
    public async Task<IActionResult> ChangeInterventionStatusToCompleted(long id)
    {
      var findIntervention = await _context.Interventions.FindAsync(id);

      // if (intervention == null)
      // {
      //   return BadRequest();
      // }

      if (findIntervention == null)
      {
        return NotFound();
      }

      if (findIntervention.Status == "Completed")
      {
        ModelState.AddModelError("Status", "Looks like this intervention has already been completed.");
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      findIntervention.Status = "Completed";
      findIntervention.EndDate = DateTimeOffset.Now;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!InterventionExists(id))
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


    private bool InterventionExists(long id)
    {
      return _context.Interventions.Any(e => e.Id == id);
    }
  }
}