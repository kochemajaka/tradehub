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
    public class BuyoutCarController : ControllerBase
    {
        private readonly BuyoutCarRepository _buyoutCarRepository;

        public BuyoutCarController(BuyoutCarRepository buyoutCarRepository)
        {
            _buyoutCarRepository = buyoutCarRepository ?? throw new ArgumentNullException(nameof(buyoutCarRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BuyoutCar>>> GetBuyoutCars()
        {
            var buyoutCars = await _buyoutCarRepository.GetAllAsync();
            return buyoutCars;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BuyoutCar>> GetBuyoutCar(Guid id)
        {
            var buyoutCar = await _buyoutCarRepository.GetByIdAsync(id);
            if (buyoutCar == null)
            {
                return NotFound();
            }
            return buyoutCar;
        }

        [HttpPost]
        public async Task<ActionResult<BuyoutCar>> PostBuyoutCar(BuyoutCar buyoutCar)
        {
            await _buyoutCarRepository.AddAsync(buyoutCar);
            return CreatedAtAction("GetBuyoutCar", new { id = buyoutCar.Id }, buyoutCar);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuyoutCar(Guid id, BuyoutCar buyoutCar)
        {
            if (id != buyoutCar.Id)
            {
                return BadRequest();
            }

            await _buyoutCarRepository.UpdateAsync(buyoutCar);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuyoutCar(Guid id)
        {
            var buyoutCar = await _buyoutCarRepository.GetByIdAsync(id);
            if (buyoutCar == null)
            {
                return NotFound();
            }

            await _buyoutCarRepository.DeleteAsync(id);

            return NoContent();
        }
    }

}