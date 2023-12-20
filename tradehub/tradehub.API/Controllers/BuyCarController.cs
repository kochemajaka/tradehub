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
    [Route("api/[controller]")]
    [ApiController]
    public class BuyCarController : ControllerBase
    {
        private readonly Context _context;
        private readonly BuyCarRepository _buyCarRepository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly DDSRepository _dDSRepository;
        private readonly CarRepository _CarRepository;

        public BuyCarController(Context context)
        {
            _context = context;
            _buyCarRepository = new BuyCarRepository(_context);
            _employeeRepository = new EmployeeRepository(_context);
            _dDSRepository = new DDSRepository(_context);
            _CarRepository = new CarRepository(_context);
        }

        // GET: api/BuyCar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BuyCar>>> GetBuyCars()
        {
            // Возвращаем все записи о покупке автомобилей из базы данных
            return await _buyCarRepository.GetAllAsync();
        }

        // GET: api/BuyCar/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BuyCar>> GetBuyCar(Guid id)
        {
            // Получаем запись о покупке автомобиля по идентификатору
            var buyCar = await _buyCarRepository.GetByIdAsync(id);
            if (buyCar == null)
            {
                return NotFound();
            }
            return buyCar;
        }

        // PUT: api/BuyCar/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuyCar(Guid id, BuyCar buyCar)
        {
            if (id != buyCar.Id)
            {
                return BadRequest();
            }

            // Обновляем запись о покупке автомобиля в базе данных
            await _buyCarRepository.UpdateAsync(buyCar);

            return NoContent();
        }

        // POST: api/BuyCar
        [HttpPost]
        //public async Task<ActionResult<BuyCar>> PostBuyCar(BuyCar buyCar)
        //{
        //    // Создаем новую запись о покупке автомобиля в базе данных
        //    await _buyCarRepository.AddAsync(buyCar);
        //    return CreatedAtAction("GetBuyCar", new { id = buyCar.Id }, buyCar);
        //}
        [HttpPost]
        public async Task<ActionResult<BuyCar>> PostBuyCar(BuyCar buyCar)
        {
            foreach (var employee in buyCar.Employees.ToList())
            {
                if (employee.Id != Guid.Empty)
                {
                    var existingEmployee = await _employeeRepository.GetByIdAsync(employee.Id);
                    if (existingEmployee != null)
                    {
                        buyCar.Employees = new List<Employee> { existingEmployee };

                    }
                }
            }

            foreach (var dDS in buyCar.BuyRow.ToList())
            {
                if (dDS.Id != Guid.Empty)
                {
                    var existingDDS = await _dDSRepository.GetByIdAsync(dDS.Id);
                    if (existingDDS != null)
                    {
                        buyCar.BuyRow = new List<DDS> { existingDDS };

                    }
                }
            }

            foreach (var car in buyCar.Cars.ToList())
            {
                if (car.Id != Guid.Empty)
                {
                    var existingCar = await _CarRepository.GetByIdAsync(car.Id);
                    if (existingCar != null)
                    {
                        buyCar.Cars = new List<Car> { existingCar };

                    }
                }
            }

            await _buyCarRepository.AddAsync(buyCar);

            return CreatedAtAction("GetBuyCar", new { id = buyCar.Id }, buyCar);
        }

        // DELETE: api/BuyCar/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuyCar(Guid id)
        {
            // Удаляем запись о покупке автомобиля из базы данных по идентификатору
            var buyCar = await _buyCarRepository.GetByIdAsync(id);
            if (buyCar == null)
            {
                return NotFound();
            }

            await _buyCarRepository.DeleteAsync(id);

            return NoContent();
        }

        private bool BuyCarExists(Guid id)
        {
            // Проверяем наличие записи о покупке автомобиля в базе данных по идентификатору
            return _context.BuyCars.Any(e => e.Id == id);
        }
    }
}
