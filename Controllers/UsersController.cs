using F1Project.Models;
using F1Project.Service;
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
    public class UsersController : ControllerBase
    {
        private readonly F1projectContext _context;
        private readonly UserService _userService;

        public UsersController(F1projectContext context,UserService service)
        {
            _context = context;
            _userService = service;
}
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (user == null)
            {
                return BadRequest("User data is null.");
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsers), new { id = user.UserId }, user);
        }

        [HttpGet("Filter_by_Role/{role}")]
        public async Task<ActionResult<List<User>>> GetUsersByRole(string role)
        {
            var users = await _userService.GetUsersByRoleAsync(role);
            if (users == null || !users.Any())
            {
                return NotFound($"No users found with role {role}");
            }
            return Ok(users);
        }

    }
}
