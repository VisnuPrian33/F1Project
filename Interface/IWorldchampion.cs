using F1Project.Models;

namespace F1Project.Interface
{
    public interface IWorldchampion
    {
        public Task<List<WorldChampion>> GetWorldChampionsAsync();
        public Task<WorldChampion?> GetWorldChampionByIdAsync(int id);
        public Task<WorldChampion?> GetWorldChampionByNameAsync(string name);
        public Task<WorldChampion?> AddWorldChampionAsync(WorldChampion worldChampion);
        public Task<WorldChampion?> UpdateWorldChampionAsync(WorldChampion worldChampion);
        public Task<bool> DeleteWorldChampionAsync(int id);
        public Task<int> GetChampionCountAsync();
        public Task<List<WorldChampion>> GetWorldChampionsByPointsRangeAsync(int minPoints, int maxPoints);
    }
}
