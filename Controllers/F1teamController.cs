using F1Project.DTO;
using F1Project.Interface;
using F1Project.Models;
using F1Project.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class F1teamController : ControllerBase
    {
        private readonly F1teamService _service;


        public F1teamController(F1teamService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<List<F1team>>> GetF1teams()
        {
            return await _service.GetF1teamsAsync();
        }

        [HttpGet("Search_by_TeamId/{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<F1team>> GetF1team(int id)
        {
            var team = await _service.GetF1teamByIdAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return team;
        }

        [HttpGet("Search_by_TeamName/{name}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<F1team>> GetF1teamByName(string name)
        {
            var team = await _service.GetF1teamByNameAsync(name);
            if (team == null)
            {
                return NotFound();
            }
            return team;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<F1team>> PostF1team(F1teamDTO f1teamDto)
        {
            if (f1teamDto == null)
            {
                return BadRequest("F1 team data is null.");
            }

            // Map DTO to Entity
            var f1team = new F1team
            {
                TeamId = f1teamDto.TeamId,
                TeamName = f1teamDto.TeamName,
                BaseCountry = f1teamDto.BaseCountry,
                TeamPrincipal = f1teamDto.TeamPrincipal,
                FoundedYear = f1teamDto.FoundedYear
            };

            var createdTeam = await _service.AddF1teamAsync(f1team);

            return CreatedAtAction(nameof(GetF1team), new { id = createdTeam.TeamId }, createdTeam);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutF1team(int id, F1teamDTO f1teamDto)
        {
            if (f1teamDto == null || id != f1teamDto.TeamId)
            {
                return BadRequest("Team ID mismatch or invalid data.");
            }

            // Map DTO to Entity
            var f1team = new F1team
            {
                TeamId = f1teamDto.TeamId,
                TeamName = f1teamDto.TeamName,
                BaseCountry = f1teamDto.BaseCountry,
                TeamPrincipal = f1teamDto.TeamPrincipal,
                FoundedYear = f1teamDto.FoundedYear
            };

            var updatedTeam = await _service.UpdateF1teamAsync(f1team);

            if (updatedTeam == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteF1team(int id)
        {
            var deleted = await _service.DeleteF1teamAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("CountOfTeams")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<int>> GetCountOfTeams()
        {
            var count = await _service.GetTeamCountAsync();
            return Ok(count);
        }

        [HttpGet("TeamsFoundedBefore/{year}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<List<F1team>>> GetTeamsFoundedAfter(int year)
        {
            var teams = await _service.GetF1teamsByFoundedYearAsync(year);
            return Ok(teams);
        }
    }
}
