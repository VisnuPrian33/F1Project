using F1Project.Interface;
using F1Project.Models;

namespace F1Project.Service
{
    public class WorldchampionService
    {
        private readonly IWorldchampion _worldchampionRepository;
        public WorldchampionService(IWorldchampion worldchampionRepository)
        {
            _worldchampionRepository = worldchampionRepository;
        }
        public Task<List<WorldChampion>> GetWorldChampionsAsync() => _worldchampionRepository.GetWorldChampionsAsync();
        public Task<WorldChampion?> GetWorldChampionByIdAsync(int id) => _worldchampionRepository.GetWorldChampionByIdAsync(id);
        public Task<WorldChampion?> GetWorldChampionByNameAsync(string name) => _worldchampionRepository.GetWorldChampionByNameAsync(name);
        public Task<WorldChampion?> AddWorldChampionAsync(WorldChampion worldChampion) => _worldchampionRepository.AddWorldChampionAsync(worldChampion);
        public Task<WorldChampion?> UpdateWorldChampionAsync(WorldChampion worldChampion) => _worldchampionRepository.UpdateWorldChampionAsync(worldChampion);
        public Task<bool> DeleteWorldChampionAsync(int id) => _worldchampionRepository.DeleteWorldChampionAsync(id);
        public Task<int> GetChampionCountAsync() => _worldchampionRepository.GetChampionCountAsync();
        public Task<List<WorldChampion>> GetWorldChampionsByPointsRangeAsync(int minPoints, int maxPoints) => _worldchampionRepository.GetWorldChampionsByPointsRangeAsync(minPoints, maxPoints);
    }
}
