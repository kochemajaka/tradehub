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
    public class DDSController : ControllerBase
    {
        private readonly DDSRepository _dDSRepository;
        private readonly CarRepository _CarRepository;
        private readonly Context _context;

        public DDSController(Context context)
        {
            _context = context;
            _dDSRepository = new DDSRepository(_context);
            _CarRepository = new CarRepository(_context);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DDS>>> GetDDSItems()
        {
            var dDSItems = await _dDSRepository.GetAllAsync();
            return dDSItems;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DDS>> GetDDSItem(Guid id)
        {
            var dDSItem = await _dDSRepository.GetByIdAsync(id);
            if (dDSItem == null)
            {
                return NotFound();
            }
            return dDSItem;
        }

        [HttpGet("car/{carId}")]
        public async Task<ActionResult<IEnumerable<DDS>>> GetbyCarId(Guid carId)
        {
            var dDSItems = await _dDSRepository.GetbyCarIdAsync(carId);
            if (dDSItems == null)
            {
                return NotFound();
            }
            return dDSItems;
        }


        [HttpPost]
        public async Task<ActionResult<DDS>> PostDDSItem(DDS dDSItem)
        {
            foreach (var car in dDSItem.Cars.ToList())
            {
                if (car.Id != Guid.Empty)
                {
                    var existingCar = await _CarRepository.GetByIdAsync(car.Id);
                    if (existingCar != null)
                    {
                        dDSItem.Cars = new List<Car> { existingCar };

                    }
                }
            }

            await _dDSRepository.AddAsync(dDSItem);
            return CreatedAtAction("GetDDSItem", new { id = dDSItem.Id }, dDSItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDDSItem(Guid id, DDS dDSItem)
        {
            if (id != dDSItem.Id)
            {
                return BadRequest();
            }

            await _dDSRepository.UpdateAsync(dDSItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDDSItem(Guid id)
        {
            var dDSItem = await _dDSRepository.GetByIdAsync(id);
            if (dDSItem == null)
            {
                return NotFound();
            }

            await _dDSRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}