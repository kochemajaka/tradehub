using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tradehub.Domain.Model;
using tradehub.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Numerics;

namespace TestProject1
{
    public class TestHelper
    {
        private readonly Context _context;

        public TestHelper()
        {
            var builder = new DbContextOptionsBuilder<Context>();
            builder.UseInMemoryDatabase(databaseName: "YourTestDB"); // Замените "YourTestDB" на подходящее имя

            var dbContextOptions = builder.Options;
            _context = new Context(dbContextOptions);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        public CarRepository CarRepository
        {
            get
            {
                return new CarRepository(_context);
            }
        }
    }

}

