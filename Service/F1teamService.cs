using F1Project.Models;
using F1Project.Interface;

namespace F1Project.Service
{
    public class F1teamService
    {
        private readonly IF1team _f1teamRepository;

        public F1teamService(IF1team f1teamRepository)
        {
            _f1teamRepository = f1teamRepository;
        }

        public Task<List<F1team>> GetF1teamsAsync() => _f1teamRepository.GetF1teamsAsync();
        public Task<F1team?> GetF1teamByIdAsync(int id) => _f1teamRepository.GetF1teamByIdAsync(id);
        public Task<F1team?> GetF1teamByNameAsync(string name) => _f1teamRepository.GetF1teamByNameAsync(name);
        public Task<F1team?> AddF1teamAsync(F1team f1team) => _f1teamRepository.AddF1teamAsync(f1team);
        public Task<F1team?> UpdateF1teamAsync(F1team f1team) => _f1teamRepository.UpdateF1teamAsync(f1team);
        public Task<bool> DeleteF1teamAsync(int id) => _f1teamRepository.DeleteF1teamAsync(id);
    }
}
