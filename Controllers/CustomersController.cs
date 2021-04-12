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
  public class CustomersController : ControllerBase
  {
    private readonly RailsApp_developmentContext _context;

    public CustomersController(RailsApp_developmentContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customers>>> GetCustomers()
    {
      return await _context.Customers.ToListAsync();
    }

    // [HttpGet("{id}")]
    // public async Task<ActionResult<Customers>> GetCustomerById(long id)
    // {
    //   var customer = await _context.Customers.FindAsync(id);

    //   if (customer == null)
    //   {
    //     return NotFound();
    //   }

    //   return customer;
    // }

    [HttpGet("{email}")]
    public async Task<ActionResult<IEnumerable<Customers>>> GetCustomerByEmail(string email)
    {
      var customer = await _context.Customers.Where(customer => customer.EmailOfCompanyContact == email).ToListAsync();

      if (customer == null)
      {
        return NotFound();
      }

      return customer;
    }

    private bool CustomerExists(long id)
    {
      return _context.Customers.Any(e => e.Id == id);
    }
  }
}