using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly JwtService _jwt;

    public AuthController(AppDbContext context, JwtService jwt)
    {
        _context = context;
        _jwt = jwt;
    }



    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var exist = await _context.Users.AnyAsync(u => u.Email == dto.Email);
        if (exist) return BadRequest("User already exists");

        var user = new User
        {
            Email = dto.Email,
            Name = dto.Name,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(
    dto.Password
)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("Registered");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);


        if (user == null || !BCrypt.Net.BCrypt.Verify(
    dto.Password,
    user.PasswordHash
))
            return Unauthorized("Invalid credentials");

        var token = _jwt.GenerateToken(user);

        return Ok(new { token });
    }

    [Authorize]
    [HttpPost("switch-restaurant")]
    public async Task<IActionResult> SwitchRestaurant(SwitchRestaurantDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var membership = await _context.Memberships
        .FirstOrDefaultAsync(m => m.UserId == userId && m.RestaurantId == dto.RestaurantId);

        if (membership == null)
        {
            return Forbid("You are not a member of this restaurant");
        }

        var user = await _context.Users.FindAsync(userId);

        var token = _jwt.GenerateTenantToken(user!, membership.RestaurantId, membership.Role);

        return Ok(new { token });
    }

}