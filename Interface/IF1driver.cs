using F1Project.Models;

namespace F1Project.Interface
{
    public interface IF1driver
    {
        public Task<List<F1driver>> GetF1driversAsync();
        public Task<F1driver?> GetF1driverByIdAsync(int id);
        public Task<F1driver?> GetF1driverByNameAsync(string name);
        public Task<F1driver?> AddF1driverAsync(F1driver f1driver);
        public Task<F1driver?> UpdateF1driverAsync(F1driver f1driver);
        public Task<bool> DeleteF1driverAsync(int id);
    }
}
