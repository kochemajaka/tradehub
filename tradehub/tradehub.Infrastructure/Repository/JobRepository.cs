using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using tradehub.Infrastructure;
using tradehub.Domain;
using tradehub.Domain.Model;

namespace tradehub.Infrastructure
{
    public class JobRepository
    {
        private readonly Context _context;

        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public JobRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Job>> GetAllAsync()
        {
            return await _context.Jobs
                .Include(job => job.Cars)
                .Include(job => job.Employees)
                .OrderBy(job => job.Date)
                .ToListAsync();
        }

        public async Task<Job> GetByIdAsync(Guid id)
        {
            return await _context.Jobs
                .Include(job => job.Cars)
                .Include(job => job.Employees)
                .FirstOrDefaultAsync(job => job.Id == id);
        }

        public async Task<Job> AddAsync(Job job)
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
            return job;
        }

        public async Task UpdateAsync(Job job)
        {
            var existingJob = await GetByIdAsync(job.Id);

            if (existingJob != null)
            {
                _context.Entry(existingJob).CurrentValues.SetValues(job);

                // Update related Cars
                foreach (var car in job.Cars)
                {
                    var existingCar = existingJob.Cars.FirstOrDefault(c => c.Id == car.Id);
                    if (existingCar == null)
                    {
                        existingJob.Cars.Add(car);
                    }
                    else
                    {
                        _context.Entry(existingCar).CurrentValues.SetValues(car);
                    }
                }

                // Update related Employees
                foreach (var employee in job.Employees)
                {
                    var existingEmployee = existingJob.Employees.FirstOrDefault(e => e.Id == employee.Id);
                    if (existingEmployee == null)
                    {
                        existingJob.Employees.Add(employee);
                    }
                    else
                    {
                        _context.Entry(existingEmployee).CurrentValues.SetValues(employee);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job != null)
            {
                _context.Jobs.Remove(job);
                await _context.SaveChangesAsync();
            }
        }
        public void ChangeTrackerClear()
        {
            _context.ChangeTracker.Clear();
        }
    }
}