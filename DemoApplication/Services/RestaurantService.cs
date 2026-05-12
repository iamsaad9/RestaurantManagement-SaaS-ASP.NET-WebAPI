
using Microsoft.EntityFrameworkCore;

public class RestaurantService : IRestaurantService
{
    private readonly AppDbContext _context;

    public RestaurantService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Restaurant> CreateRestaurant(
        int userId,
        CreateRestaurantDto dto)
    {
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

        return restaurant;
    }

    public async Task<List<Restaurant>> GetRestaurant(int userId)
    {
        var restaurants = await _context.Memberships
            .Where(m => m.UserId == userId)
            .Select(m => m.Restaurant)
            .ToListAsync();

        return restaurants;
    }
}