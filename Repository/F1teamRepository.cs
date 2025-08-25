using F1Project.Interface;
using F1Project.Models;
using Microsoft.EntityFrameworkCore;

namespace F1Project.Repository
{
    public class F1teamRepository : IF1team
    {
        private readonly F1projectContext _context;
        public F1teamRepository(F1projectContext context)
        {
            _context = context;
        }

        public async Task<List<F1team>> GetF1teamsAsync()
        {
            return await _context.F1teams.ToListAsync();
        }

        public async Task<F1team?> GetF1teamByIdAsync(int id)
        {
            return await _context.F1teams.FindAsync(id);
        }

        public async Task<F1team?> GetF1teamByNameAsync(string name)
        {
            return await _context.F1teams.FindAsync(name);
        }

        public async Task<F1team?> AddF1teamAsync(F1team f1team)
        {
            _context.F1teams.Add(f1team);
            await _context.SaveChangesAsync();
            return f1team;

        }

        public async Task<F1team?> UpdateF1teamAsync(F1team f1team)
        {
            var existingTeam = await _context.F1teams.FindAsync(f1team.TeamId);
            if (existingTeam == null)
            {
                return null;
            }
            existingTeam.TeamName = f1team.TeamName;
            existingTeam.TeamPrincipal = f1team.TeamPrincipal;
            existingTeam.BaseCountry = f1team.BaseCountry;
            existingTeam.FoundedYear = f1team.FoundedYear;
            _context.F1teams.Update(existingTeam);
            await _context.SaveChangesAsync();
            return existingTeam;
        }

        public async Task<bool> DeleteF1teamAsync(int id)
        {
            var team = await _context.F1teams.FindAsync(id);
            if (team == null)
            {
                return false;
            }
            _context.F1teams.Remove(team);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<int> GetTeamCountAsync()
        {
            return await _context.F1teams.CountAsync();
        }
        public async Task<List<F1team>> GetF1teamsByFoundedYearAsync(int year) =>
            await _context.F1teams
                .Where(t => t.FoundedYear <= year)
                .ToListAsync();
    }
}
