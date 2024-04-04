using EmpFullStack.Data;
using EmpFullStack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpFullStack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly FullstackDbContext _fullstackDbContext;
        public EmployeeController(FullstackDbContext fullstackDbContext)
        {
            this._fullstackDbContext=fullstackDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _fullstackDbContext.Employees.ToListAsync();

            return Ok(employees);
        }
        [HttpPost]
       

public async Task<IActionResult> AddEmployee([FromBody] Emp empRequest )
        {empRequest.Id = Guid.NewGuid();
            await _fullstackDbContext.Employees.AddAsync(empRequest);
            await _fullstackDbContext.SaveChangesAsync();
            return Ok(empRequest);
        }
    }
}
