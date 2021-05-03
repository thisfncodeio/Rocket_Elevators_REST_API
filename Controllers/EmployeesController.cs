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
  public class EmployeesController : ControllerBase
  {
    private readonly RailsApp_developmentContext _context;

    public EmployeesController(RailsApp_developmentContext context)
    {
      _context = context;
    }

    [HttpGet("{email}")]
    public async Task<ActionResult<List<Employees>>> VerifyEmployee(string email)
    {
      // var battery = await _context.Batteries.FindAsync(id);
      var employee = await _context.Employees.Where(employee => employee.Email == email).ToListAsync();

      if (employee.Count <= 0)
      {
        return NotFound();
      }

      return employee;
    }

    private bool EmployeeExists(string email)
    {
      return _context.Employees.Any(employee => employee.Email == email);
    }
  }
}