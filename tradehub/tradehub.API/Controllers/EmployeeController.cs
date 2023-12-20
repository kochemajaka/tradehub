using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tradehub.Domain;
using tradehub.Infrastructure;
using tradehub.Domain.Model;

namespace tradehub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly Context _context;

        public EmployeeController(Context context)
        {
            _context = context;
            _employeeRepository = new EmployeeRepository(_context);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return employees;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            await _employeeRepository.AddAsync(employee);
            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            await _employeeRepository.UpdateAsync(employee);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            await _employeeRepository.DeleteAsync(id);

            return NoContent();
        }
    }

}