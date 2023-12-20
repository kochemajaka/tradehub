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
    public class PrepareCarRepository
    {
        private readonly Context _context;

        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public PrepareCarRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<PrepareCar>> GetAllAsync()
        {
            return await _context.PrepareCars
                .Include(prepareCar => prepareCar.Cars)
                .Include(prepareCar => prepareCar.Employees)
                .Include(prepareCar => prepareCar.PrepareRow)
                .ToListAsync();
        }

        public async Task<PrepareCar> GetByIdAsync(Guid id)
        {
            return await _context.PrepareCars
                .Include(prepareCar => prepareCar.Cars)
                .Include(prepareCar => prepareCar.Employees)
                .Include(prepareCar => prepareCar.PrepareRow)
                .FirstOrDefaultAsync(prepareCar => prepareCar.Id == id);
        }

        public async Task<PrepareCar> AddAsync(PrepareCar prepareCar)
        {
            _context.PrepareCars.Add(prepareCar);
            await _context.SaveChangesAsync();
            return prepareCar;
        }

        public async Task UpdateAsync(PrepareCar prepareCar)
        {
            var existingPrepareCar = await GetByIdAsync(prepareCar.Id);

            if (existingPrepareCar != null)
            {
                _context.Entry(existingPrepareCar).CurrentValues.SetValues(prepareCar);

                // Update related Cars
                foreach (var car in prepareCar.Cars)
                {
                    var existingCar = existingPrepareCar.Cars.FirstOrDefault(c => c.Id == car.Id);
                    if (existingCar == null)
                    {
                        existingPrepareCar.Cars.Add(car);
                    }
                    else
                    {
                        _context.Entry(existingCar).CurrentValues.SetValues(car);
                    }
                }

                // Update related Employees
                foreach (var employee in prepareCar.Employees)
                {
                    var existingEmployee = existingPrepareCar.Employees.FirstOrDefault(e => e.Id == employee.Id);
                    if (existingEmployee == null)
                    {
                        existingPrepareCar.Employees.Add(employee);
                    }
                    else
                    {
                        _context.Entry(existingEmployee).CurrentValues.SetValues(employee);
                    }
                }

                // Update related PrepareRow
                foreach (var prepareRow in prepareCar.PrepareRow)
                {
                    var existingPrepareRow = existingPrepareCar.PrepareRow.FirstOrDefault(p => p.Id == prepareRow.Id);
                    if (existingPrepareRow == null)
                    {
                        existingPrepareCar.PrepareRow.Add(prepareRow);
                    }
                    else
                    {
                        _context.Entry(existingPrepareRow).CurrentValues.SetValues(prepareRow);
                    }
                }

                // Handle deleted related Cars
                foreach (var existingCar in existingPrepareCar.Cars.ToList())
                {
                    if (!prepareCar.Cars.Any(c => c.Id == existingCar.Id))
                    {
                        existingPrepareCar.Cars.Remove(existingCar);
                    }
                }

                // Handle deleted related Employees
                foreach (var existingEmployee in existingPrepareCar.Employees.ToList())
                {
                    if (!prepareCar.Employees.Any(e => e.Id == existingEmployee.Id))
                    {
                        existingPrepareCar.Employees.Remove(existingEmployee);
                    }
                }

                // Handle deleted related PrepareRow
                foreach (var existingPrepareRow in existingPrepareCar.PrepareRow.ToList())
                {
                    if (!prepareCar.PrepareRow.Any(p => p.Id == existingPrepareRow.Id))
                    {
                        existingPrepareCar.PrepareRow.Remove(existingPrepareRow);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var prepareCar = await _context.PrepareCars.FindAsync(id);
            if (prepareCar != null)
            {
                _context.PrepareCars.Remove(prepareCar);
                await _context.SaveChangesAsync();
            }
        }
        public void ChangeTrackerClear()
        {
            _context.ChangeTracker.Clear();
        }
    }
}