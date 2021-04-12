using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rocket_Elevators_REST_API.Models;
using Microsoft.EntityFrameworkCore;


namespace Rocket_Elevators_REST_API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class BuildingsController : ControllerBase
  {
    private readonly RailsApp_developmentContext _context;

    public BuildingsController(RailsApp_developmentContext context)
    {
      _context = context;
    }

    // GET: /buildings
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Buildings>>> GetBuilding()
    {

      var queryBuildings = from build in _context.Buildings
                           from bat in build.Batteries
                           from col in bat.Columns
                           from elv in col.Elevators
                           where bat.Status.Equals("Intervention") || col.Status.Equals("Intervention") || elv.Status.Equals("Intervention")
                           select build;

      var distinctBuildings = (from build in queryBuildings
                               select build).Distinct();


      return await distinctBuildings.ToListAsync();
    }

    [HttpGet("from-customer/{customer_id}")]
    public async Task<ActionResult<List<Buildings>>> GetbuildingByCustomerId(long customer_id)
    {
      var buildings = await _context.Buildings.Where(building => building.CustomerId == customer_id).ToListAsync();

      if (buildings == null)
      {
        return NotFound();
      }

      return buildings;
    }

    private bool BuildingExists(int id)
    {
      return _context.Buildings.Any(e => e.Id == id);
    }
  }
}
