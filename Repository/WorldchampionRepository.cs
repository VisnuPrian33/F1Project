using F1Project.Interface;
using F1Project.Models;
using Microsoft.EntityFrameworkCore;

namespace F1Project.Repository
{
    public class WorldchampionRepository : IWorldchampion
    {
        private readonly F1projectContext _context;
        public WorldchampionRepository(F1projectContext context)
        {
            _context = context;
        }

        public async Task<WorldChampion?> AddWorldChampionAsync(WorldChampion worldChampion)
        {
            _context.Add(worldChampion);
            await _context.SaveChangesAsync();
            return worldChampion;
        }

        public async Task<bool> DeleteWorldChampionAsync(int id)
        {
            var champion = await _context.WorldChampions.FindAsync(id);
            if (champion == null)
            {
                return false;
            }
            _context.WorldChampions.Remove(champion);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<WorldChampion?> GetWorldChampionByIdAsync(int id)
        {
            return await _context.WorldChampions.FindAsync(id);
        }

        public async Task<WorldChampion?> GetWorldChampionByNameAsync(string name)
        {
            return await _context.WorldChampions.FindAsync(name);
        }

        public async Task<List<WorldChampion>> GetWorldChampionsAsync()
        {
            return await _context.WorldChampions.ToListAsync();
        }

        public async Task<WorldChampion?> UpdateWorldChampionAsync(WorldChampion worldChampion)
        {
            var existingChampion = await _context.WorldChampions.FindAsync(worldChampion.DriverNumber);
            if (existingChampion == null)
            {
                return null;
            }
            existingChampion.WinningYear = worldChampion.WinningYear;
            existingChampion.DriverName = worldChampion.DriverName;
            existingChampion.TeamName = worldChampion.TeamName;
            existingChampion.Points = worldChampion.Points;
            _context.WorldChampions.Update(existingChampion);
            await _context.SaveChangesAsync();
            return existingChampion;
        }

        public async Task<int> GetChampionCountAsync()
        {
            return await _context.WorldChampions.CountAsync();
        }

        public async Task<List<WorldChampion>> GetWorldChampionsByPointsRangeAsync(int minPoints, int maxPoints)
        {
            return await _context.WorldChampions
                .Where(c => c.Points >= minPoints && c.Points <= maxPoints)
                .ToListAsync();
        }
    }
}
