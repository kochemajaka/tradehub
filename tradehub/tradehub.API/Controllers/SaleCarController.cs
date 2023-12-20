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
    public class SaleCarController : ControllerBase
    {
        private readonly Context _context;
        private readonly SaleCarRepository _saleCarRepository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly DDSRepository _dDSRepository;
        private readonly CarRepository _CarRepository;

        public SaleCarController(Context context)
        {
            _context = context;
            _saleCarRepository = new SaleCarRepository(_context);
            _employeeRepository = new EmployeeRepository(_context);
            _dDSRepository = new DDSRepository(_context);
            _CarRepository = new CarRepository(_context);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleCar>>> GetSaleCars()
        {
            var saleCars = await _saleCarRepository.GetAllAsync();
            return saleCars;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SaleCar>> GetSaleCar(Guid id)
        {
            var saleCar = await _saleCarRepository.GetByIdAsync(id);
            if (saleCar == null)
            {
                return NotFound();
            }
            return saleCar;
        }

        [HttpPost]
        public async Task<ActionResult<SaleCar>> PostSaleCar(SaleCar saleCar)
        {
            foreach (var employee in saleCar.Employees.ToList())
            {
                if (employee.Id != Guid.Empty)
                {
                    var existingEmployee = await _employeeRepository.GetByIdAsync(employee.Id);
                    if (existingEmployee != null)
                    {
                        saleCar.Employees = new List<Employee> { existingEmployee };

                    }
                }
            }

            foreach (var dDS in saleCar.BuyRow.ToList())
            {
                if (dDS.Id != Guid.Empty)
                {
                    var existingDDS = await _dDSRepository.GetByIdAsync(dDS.Id);
                    if (existingDDS != null)
                    {
                        saleCar.BuyRow = new List<DDS> { existingDDS };

                    }
                }
            }

            foreach (var car in saleCar.Cars.ToList())
            {
                if (car.Id != Guid.Empty)
                {
                    var existingCar = await _CarRepository.GetByIdAsync(car.Id);
                    if (existingCar != null)
                    {
                        saleCar.Cars = new List<Car> { existingCar };

                    }
                }
            }

            await _saleCarRepository.AddAsync(saleCar);
            return CreatedAtAction("GetSaleCar", new { id = saleCar.Id }, saleCar);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSaleCar(Guid id, SaleCar saleCar)
        {
            if (id != saleCar.Id)
            {
                return BadRequest();
            }

            await _saleCarRepository.UpdateAsync(saleCar);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaleCar(Guid id)
        {
            var saleCar = await _saleCarRepository.GetByIdAsync(id);
            if (saleCar == null)
            {
                return NotFound();
            }

            await _saleCarRepository.DeleteAsync(id);

            return NoContent();
        }
    }

}