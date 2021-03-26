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
    public async Task<ActionResult<IEnumerable<Interventions>>> GetBatteries()
    {
      return await _context.Interventions.ToListAsync();
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


    [HttpPut("{id}/pending")]
    public async Task<IActionResult> ChangeInterventionStatus(long id, [FromBody] Interventions intervention)
    {
      var findIntervention = await _context.Interventions.FindAsync(id);

      if (intervention == null)
      {
        return BadRequest();
      }

      if (findIntervention == null)
      {
        return NotFound();
      }

      if (intervention.Status == findIntervention.Status)
      {
        ModelState.AddModelError("Status", "Looks like you didn't change the status.");
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      findIntervention.Status = intervention.Status;
      findIntervention.StartDate = DateTime.Today;

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

    [HttpPut("{id}/completed")]
    public async Task<IActionResult> CompleteIntervention(long id, [FromBody] Interventions intervention)
    {
      var findIntervention = await _context.Interventions.FindAsync(id);

      if (intervention == null)
      {
        return BadRequest();
      }

      if (findIntervention == null)
      {
        return NotFound();
      }

      if (intervention.Status == findIntervention.Status)
      {
        ModelState.AddModelError("Status", "Looks like you didn't change the status.");
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      findIntervention.Status = intervention.Status;
      findIntervention.EndDate = DateTime.Today;

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