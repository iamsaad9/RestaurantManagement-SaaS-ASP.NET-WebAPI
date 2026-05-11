using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


[ApiController]
[Route("api/restaurants")]
[Authorize]
public class RestaurantsController : ControllerBase
{
    private readonly AppDbContext _context;
    public RestaurantsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRestaurantDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var restaurant = new Restaurant
        {
            Name = dto.Name,
            OwnerId = userId
        };

        _context.Restaurants.Add(restaurant);
        await _context.SaveChangesAsync();

        //Create Membership
        var membership = new Membership
        {
            UserId = userId,
            RestaurantId = restaurant.Id,
            Role = "Admin"
        };

        _context.Memberships.Add(membership);
        await _context.SaveChangesAsync();

        return Ok(restaurant);
    }

    [HttpGet]
    public async Task<IActionResult> GetMyRestaurants()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var restaurants = _context.Memberships
            .Where(m => m.UserId == userId)
            .Select(m => m.Restaurant)
            .ToList();

        return Ok(restaurants);
    }
}