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
    public class PrepareCarController : ControllerBase
    {
        private readonly PrepareCarRepository _prepareCarRepository;

        public PrepareCarController(PrepareCarRepository prepareCarRepository)
        {
            _prepareCarRepository = prepareCarRepository ?? throw new ArgumentNullException(nameof(prepareCarRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrepareCar>>> GetPrepareCars()
        {
            var prepareCars = await _prepareCarRepository.GetAllAsync();
            return prepareCars;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PrepareCar>> GetPrepareCar(Guid id)
        {
            var prepareCar = await _prepareCarRepository.GetByIdAsync(id);
            if (prepareCar == null)
            {
                return NotFound();
            }
            return prepareCar;
        }

        [HttpPost]
        public async Task<ActionResult<PrepareCar>> PostPrepareCar(PrepareCar prepareCar)
        {
            await _prepareCarRepository.AddAsync(prepareCar);
            return CreatedAtAction("GetPrepareCar", new { id = prepareCar.Id }, prepareCar);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrepareCar(Guid id, PrepareCar prepareCar)
        {
            if (id != prepareCar.Id)
            {
                return BadRequest();
            }

            await _prepareCarRepository.UpdateAsync(prepareCar);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrepareCar(Guid id)
        {
            var prepareCar = await _prepareCarRepository.GetByIdAsync(id);
            if (prepareCar == null)
            {
                return NotFound();
            }

            await _prepareCarRepository.DeleteAsync(id);

            return NoContent();
        }
    }

}