using F1Project.DTO;
using F1Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace F1Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly F1projectContext _context;
        private readonly IConfiguration _config;

        public TokenController(F1projectContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Post(LoginDTO loginData)
        {
            if (loginData != null && !string.IsNullOrEmpty(loginData.UserName) &&
                !string.IsNullOrEmpty(loginData.Password))
            {
                var user = await GetUser(loginData.UserName, loginData.Password);
                if (user != null)
                {
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"]));
                    var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                        new Claim(ClaimTypes.Role, user.Role!)
                    };

                    var tokenDescription = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.Now.AddDays(2),
                        SigningCredentials = cred,
                        Issuer = _config["Jwt:Issuer"],
                        Audience = _config["Jwt:Audience"]
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var myToken = tokenHandler.CreateToken(tokenDescription);
                    var token = tokenHandler.WriteToken(myToken);

                    return Ok(new { token });
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest("Invalid request data");
            }
        }

        private async Task<User?> GetUser(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username &&
                                                             u.Password == password);
        }
    }
}
