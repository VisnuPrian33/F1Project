using F1Project.Interface;
using F1Project.Models;

namespace F1Project.Service
{
    public class F1driverService
    {
        private readonly IF1driver _f1driverrepository;
        public F1driverService(IF1driver f1driverrepository)
        {
            _f1driverrepository = f1driverrepository;
        }
        
        public Task<List<F1driver>> GetF1driversAsync() => _f1driverrepository.GetF1driversAsync();
        public Task<F1driver?> GetF1driverByIdAsync(int id) => _f1driverrepository.GetF1driverByIdAsync(id);
        public Task<F1driver?> GetF1driverByNameAsync(string name) => _f1driverrepository.GetF1driverByNameAsync(name);
        public Task<F1driver?> AddF1driverAsync(F1driver f1driver) => _f1driverrepository.AddF1driverAsync(f1driver);
        public Task<F1driver?> UpdateF1driverAsync(F1driver f1driver) => _f1driverrepository.UpdateF1driverAsync(f1driver);
        public Task<bool> DeleteF1driverAsync(int id) => _f1driverrepository.DeleteF1driverAsync(id);
    }
}
