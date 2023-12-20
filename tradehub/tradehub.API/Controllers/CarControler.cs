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
    public class CarController : ControllerBase
    {
        private readonly Context _context;
        private readonly CarRepository _CarRepository;

        public CarController(Context context)
        {
            _context = context;
            _CarRepository = new CarRepository(_context);
        }

        // GET: api/Car
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetBuyCars()
        {
            // Возвращаем все записи о покупке автомобилей из базы данных
            return await _CarRepository.GetAllAsync();
        }

        // GET: api/Car/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(Guid id)
        {
            // Получаем запись о покупке автомобиля по идентификатору
            var Car = await _CarRepository.GetByIdAsync(id);
            if (Car == null)
            {
                return NotFound();
            }
            return Car;
        }

        // PUT: api/Car/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(Guid id, Car Car)
        {
            if (id != Car.Id)
            {
                return BadRequest();
            }

            // Обновляем запись о покупке автомобиля в базе данных
            await _CarRepository.UpdateAsync(Car);

            return NoContent();
        }

        // POST: api/Car
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car Car)
        {
            // Создаем новую запись о покупке автомобиля в базе данных
            await _CarRepository.AddAsync(Car);
            return CreatedAtAction("GetCar", new { id = Car.Id }, Car);
        }

        // DELETE: api/Car/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(Guid id)
        {
            // Удаляем запись о покупке автомобиля из базы данных по идентификатору
            var Car = await _CarRepository.GetByIdAsync(id);
            if (Car == null)
            {
                return NotFound();
            }

            await _CarRepository.DeleteAsync(id);

            return NoContent();
        }

        private bool CarExists(Guid id)
        {
            // Проверяем наличие записи о покупке автомобиля в базе данных по идентификатору
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
