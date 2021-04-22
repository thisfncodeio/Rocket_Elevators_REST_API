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
  public class SummaryController : ControllerBase
  {
    private readonly RailsApp_developmentContext _context;

    public SummaryController(RailsApp_developmentContext context)
    {
      _context = context;
    }

    [HttpGet]
    // public async Task<ActionResult<IEnumerable<Batteries>>> GetSummary()
    public async Task<ActionResult<string>> GetSummary()
    {
      var elevators = await _context.Elevators.ToListAsync();
      var customers = await _context.Customers.ToListAsync();
      var batteries = await _context.Batteries.ToListAsync();
      var quotes = await _context.Quotes.ToListAsync();
      var leads = await _context.Leads.ToListAsync();

      var elevatorsInactive = await _context.Elevators
          .Where(elevator => elevator.Status != "Active")
          .ToListAsync();

      var buildings = await _context.Buildings.ToListAsync();

      var addressCities = await _context.Addresses
          .Select(address => address.City).Distinct().ToListAsync();
      

      return $"Greetings. There are currently {elevators.Count} elevators deployed in the {buildings.Count} buildings of your {customers.Count} customers. Currently, {elevatorsInactive.Count} elevators are not in Running Status and are being serviced. {batteries.Count} batteries are deployed across {addressCities.Count} cities. On another note, you currently have {quotes.Count} quotes awaiting processing. You also have {leads.Count} leads in your contact requests.";
    }
  }
}