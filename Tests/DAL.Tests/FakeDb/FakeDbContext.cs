using DAL;
using Microsoft.EntityFrameworkCore;
using System;

namespace Tests.DAL.Tests.FakeDb
{
    public class FakeDbContext
    {
        public static ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().EnableSensitiveDataLogging()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var _context = new ApplicationDbContext(options);
            _context.Database.EnsureDeletedAsync();
            TestData.Initialize(_context);
            return _context;
        }


    }
}
