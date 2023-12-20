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
    public class DDSRepository
    {
        private readonly Context _context;

        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public DDSRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<DDS>> GetAllAsync()
        {
            return await _context.DDSs.ToListAsync();
        }
        public async Task<List<DDS>> GetbyCarIdAsync(Guid carId)
        {
            return await _context.DDSs
                .Where(dds => dds.Cars.Any(car => car.Id == carId))
                .Include(dds => dds.Cars)  
                .ToListAsync();
        }

        public async Task<DDS> GetByIdAsync(Guid id)
        {
            return await _context.DDSs.FindAsync(id);
        }

        public async Task<DDS> AddAsync(DDS dds)
        {
            _context.DDSs.Add(dds);
            await _context.SaveChangesAsync();
            return dds;
        }

        public async Task UpdateAsync(DDS dds)
        {
            var existingDDS = await GetByIdAsync(dds.Id);

            if (existingDDS != null)
            {
                _context.Entry(existingDDS).CurrentValues.SetValues(dds);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var dds = await _context.DDSs.FindAsync(id);
            if (dds != null)
            {
                _context.DDSs.Remove(dds);
                await _context.SaveChangesAsync();
            }
        }
        public void ChangeTrackerClear()
        {
            _context.ChangeTracker.Clear();
        }
    }
}