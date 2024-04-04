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
            this._fullstackDbContext = fullstackDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _fullstackDbContext.Employees.ToListAsync();

            return Ok(employees);
        }
        [HttpGet ("{id:guid}") ]
        public async Task<ActionResult<Emp>> GetAllEmployees(Guid id)
        {
            //ArgumentNullException.ThrowIfNull(id);
            var emp = await _fullstackDbContext.Employees
                .FirstOrDefaultAsync(e => e.Id.ToString() == id.ToString());
            //emp.SetException(new NotImplementedException());
            return emp;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Emp empRequest)
        {
            empRequest.Id = Guid.NewGuid();
            await _fullstackDbContext.Employees.AddAsync(empRequest);
            await _fullstackDbContext.SaveChangesAsync();
            return Ok(empRequest);
        }

        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<Emp>> GetEmployee(int id)
        //{
          
        //        var result = await _fullstackDbContext.GetEmployee(id);

        //        if (result == null) return NotFound();

        //        return result;
        //    }

                [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutEmployee(Guid id,[FromBody] Emp employee)
        {
            //Guid gid = Guid.NewGuid();
            //const string sid = "3f72497b-188f-4d3a-92a1-c7432cfae62a";
            //Guid guidId = new Guid(id);
            var emp = await _fullstackDbContext.Employees
               .FirstOrDefaultAsync(e => e.Id.ToString() == id.ToString());

            if (emp == null)
            {
                return BadRequest();
            }
            else
            {
                //_fullstackDbContext.Entry(employee).State = EntityState.Modified;
                emp.Name = employee.Name;
                emp.Email = employee.Email;
                await _fullstackDbContext.SaveChangesAsync();
            }

            return NoContent();

            //try
            //{
            //    if (guidId != employee.Id)
            //        return BadRequest("Employee ID mismatch");

            //    var employeeToUpdate = await _fullstackDbContext.GetAllEmployees(id);

            //    if (employeeToUpdate == null)
            //        return NotFound($"Employee with Id = {id} not found");

            //    return await _fullstackDbContext.PutEmployee(employee);
            //}
            //catch (Exception)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError,
            //        "Error updating data");
            //}
        }

    }
}
