using F1Project.DTO;
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
    public class F1driverController : ControllerBase
    {
        private readonly F1driverService _service;

        public F1driverController(F1driverService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<List<F1driver>>> GetF1drivers()
        {
            return await _service.GetF1driversAsync();
        }

        [HttpGet("Search_by_DriverId/{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<F1driver>> GetF1driver(int id)
        {
            var driver = await _service.GetF1driverByIdAsync(id);
            if (driver == null)
            {
                return NotFound();
            }
            return driver;
        }

        [HttpGet("Search_by_DriverName/{name}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<F1driver>> GetF1driverByName(string name)
        {
            var driver = await _service.GetF1driverByNameAsync(name);
            if (driver == null)
            {
                return NotFound();
            }
            return driver;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<F1driver>> PostF1driver(F1driverDTO f1driverDto)
        {
            if (f1driverDto == null)
            {
                return BadRequest("F1 driver data is null.");
            }

            // Map DTO → Entity
            var f1driver = new F1driver
            {
                DriverNumber = f1driverDto.DriverNumber,
                DriverName = f1driverDto.DriverName,
                Nationality = f1driverDto.Nationality,
                TeamId = f1driverDto.TeamId,
                TeamName = f1driverDto.TeamName
            };

            var createdDriver = await _service.AddF1driverAsync(f1driver);

            return CreatedAtAction(nameof(GetF1driver), new { id = createdDriver.DriverNumber }, createdDriver);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<F1driver>> PutF1driver(int id, F1driverDTO f1driverDto)
        {
            if (f1driverDto == null || id != f1driverDto.DriverNumber)
            {
                return BadRequest("Driver number mismatch or invalid data.");
            }

            // Map DTO → Entity
            var f1driver = new F1driver
            {
                DriverNumber = f1driverDto.DriverNumber,
                DriverName = f1driverDto.DriverName,
                Nationality = f1driverDto.Nationality,
                TeamId = f1driverDto.TeamId,
                TeamName = f1driverDto.TeamName
            };

            var updatedDriver = await _service.UpdateF1driverAsync(f1driver);

            if (updatedDriver == null)
            {
                return NotFound();
            }

            return updatedDriver;
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteF1driver(int id)
        {
            var deleted = await _service.DeleteF1driverAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("CountOfDrivers")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<int>> GetDriverCount()
        {
            var count = await _service.GetDriverCountAsync();
            return Ok(count);
        }

        [HttpGet("Filter_by_Nationality/{nationality}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<List<F1driver>>> GetDriversByNationality(string nationality)
        {
            var drivers = await _service.GetDriversByNationalityAsync(nationality);
            if (drivers == null || drivers.Count == 0)
            {
                return NotFound($"No drivers found with nationality: {nationality}");
            }
            return Ok(drivers);
        }
    }
}
