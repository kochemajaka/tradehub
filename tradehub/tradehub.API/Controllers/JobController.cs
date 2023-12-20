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
    public class JobController : ControllerBase
    {
        private readonly JobRepository _jobRepository;

        public JobController(JobRepository jobRepository)
        {
            _jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
            var jobs = await _jobRepository.GetAllAsync();
            return jobs;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(Guid id)
        {
            var job = await _jobRepository.GetByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return job;
        }

        [HttpPost]
        public async Task<ActionResult<Job>> PostJob(Job job)
        {
            await _jobRepository.AddAsync(job);
            return CreatedAtAction("GetJob", new { id = job.Id }, job);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(Guid id, Job job)
        {
            if (id != job.Id)
            {
                return BadRequest();
            }

            await _jobRepository.UpdateAsync(job);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(Guid id)
        {
            var job = await _jobRepository.GetByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            await _jobRepository.DeleteAsync(id);

            return NoContent();
        }
    }

}