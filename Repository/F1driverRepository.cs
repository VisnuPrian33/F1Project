using F1Project.Interface;
using F1Project.Models;
using Microsoft.EntityFrameworkCore;

namespace F1Project.Repository
{
    public class F1driverRepository : IF1driver
    {
        private readonly F1projectContext _context;
        public F1driverRepository(F1projectContext context)
        {
            _context = context;
        }

        public async Task<F1driver?> AddF1driverAsync(F1driver f1driver)
        {
            _context.F1drivers.Add(f1driver);
            await _context.SaveChangesAsync();
            return f1driver;
        }

        public async Task<bool> DeleteF1driverAsync(int id)
        {
            var driver = await _context.F1drivers.FindAsync(id);
            if (driver == null)
            {
                return false;
            }
            _context.F1drivers.Remove(driver);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<F1driver?> GetF1driverByIdAsync(int id)
        {
            return await _context.F1drivers.FindAsync(id);
        }

        public async Task<F1driver?> GetF1driverByNameAsync(string name)
        {
            return await _context.F1drivers.FindAsync(name);
        }

        public async Task<List<F1driver>> GetF1driversAsync()
        {
            return await _context.F1drivers.ToListAsync();
        }

        public async Task<F1driver?> UpdateF1driverAsync(F1driver f1driver)
        {
            var existingDriver = await _context.F1drivers.FindAsync(f1driver.DriverNumber);
            if (existingDriver == null)
            {
                return null;
            }
            existingDriver.DriverName = f1driver.DriverName;
            existingDriver.TeamId = f1driver.TeamId;
            existingDriver.Nationality = f1driver.Nationality;
            existingDriver.TeamName = f1driver.TeamName;
            _context.F1drivers.Update(existingDriver);
            await _context.SaveChangesAsync();
            return existingDriver;
        }
        public async Task<int> GetDriverCountAsync()
        {
            return await _context.F1drivers.CountAsync();
        }
        public async Task<List<F1driver>> GetDriversByNationalityAsync(string nationality)
        {
            return await _context.F1drivers
                .Where(d => d.Nationality == nationality)
                .ToListAsync();
        }
    }
}
