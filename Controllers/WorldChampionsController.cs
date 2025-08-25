using F1Project.DTO;
using F1Project.Models;
using F1Project.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F1Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorldchampionController : ControllerBase
    {
        private readonly WorldchampionService _worldchampionService;

        public WorldchampionController(WorldchampionService worldchampionService)
        {
            _worldchampionService = worldchampionService;
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<List<WorldChampion>>> GetWorldChampions()
        {
            var champions = await _worldchampionService.GetWorldChampionsAsync();
            return Ok(champions);
        }

        [HttpGet("Search_by_ChampionId/{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<WorldChampion>> GetWorldChampionById(int id)
        {
            var champion = await _worldchampionService.GetWorldChampionByIdAsync(id);
            if (champion == null)
                return NotFound($"WorldChampion with Id = {id} not found.");
            return Ok(champion);
        }

        [HttpGet("Search_by_ChampionName/{name}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<WorldChampion>> GetWorldChampionByName(string name)
        {
            var champion = await _worldchampionService.GetWorldChampionByNameAsync(name);
            if (champion == null)
                return NotFound($"WorldChampion with Name = {name} not found.");
            return Ok(champion);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<WorldChampion>> AddWorldChampion([FromBody] WorldchampionDTO worldChampionDto)
        {
            if (worldChampionDto == null)
                return BadRequest("Invalid data.");

            // Map DTO → Entity
            var worldChampion = new WorldChampion
            {
                DriverNumber = worldChampionDto.DriverNumber ?? 0,
                WinningYear = worldChampionDto.WinningYear ?? 0,
                DriverName = worldChampionDto.DriverName,
                TeamName = worldChampionDto.TeamName,
                Points = worldChampionDto.Points ?? 0
            };

            var createdChampion = await _worldchampionService.AddWorldChampionAsync(worldChampion);

            return CreatedAtAction(nameof(GetWorldChampionById), new { id = createdChampion.DriverNumber }, createdChampion);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<WorldChampion>> UpdateWorldChampion(int id, [FromBody] WorldchampionDTO worldChampionDto)
        {
            if (worldChampionDto == null || id != (worldChampionDto.DriverNumber ?? 0))
                return BadRequest("Id mismatch or invalid data.");

            // Map DTO → Entity
            var worldChampion = new WorldChampion
            {
                DriverNumber = worldChampionDto.DriverNumber ?? 0,
                WinningYear = worldChampionDto.WinningYear ?? 0,
                DriverName = worldChampionDto.DriverName,
                TeamName = worldChampionDto.TeamName,
                Points = worldChampionDto.Points ?? 0
            };

            var updatedChampion = await _worldchampionService.UpdateWorldChampionAsync(worldChampion);
            if (updatedChampion == null)
                return NotFound($"WorldChampion with Id = {id} not found.");

            return Ok(updatedChampion);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteWorldChampion(int id)
        {
            var deleted = await _worldchampionService.DeleteWorldChampionAsync(id);
            if (!deleted)
                return NotFound($"WorldChampion with Id = {id} not found.");
            return NoContent();
        }

        [HttpGet("CountOfChampions")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<int>> GetCountOfChampions()
        {
            var count = await _worldchampionService.GetChampionCountAsync();
            return Ok(count);
        }

        [HttpGet("Filter_by_Points/{min}/{max}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<List<WorldChampion>>> GetByPointsRange(int min, int max)
        {
            var champions = await _worldchampionService.GetWorldChampionsByPointsRangeAsync(min, max);
            return Ok(champions);
        }
    }
}
