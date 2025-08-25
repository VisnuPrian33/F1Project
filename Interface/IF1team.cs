using F1Project.Models;

namespace F1Project.Interface
{
    public interface IF1team
    {
        Task<List<F1team>> GetF1teamsAsync();
        Task<F1team?> GetF1teamByIdAsync(int id);
        Task<F1team?> GetF1teamByNameAsync(string name);
        Task<F1team?> AddF1teamAsync(F1team f1team);
        Task<F1team?> UpdateF1teamAsync(F1team f1team);
        Task<bool> DeleteF1teamAsync(int id);
        Task<int> GetTeamCountAsync();
        Task<List<F1team>> GetF1teamsByFoundedYearAsync(int year);

    }
}
