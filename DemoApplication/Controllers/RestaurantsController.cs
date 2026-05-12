using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


[ApiController]
[Route("api/restaurants")]
[Authorize]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantService _service;
    public RestaurantsController(IRestaurantService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestauarant(CreateRestaurantDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var restaurant = await _service
        .CreateRestaurant(userId, dto);

        return Ok(restaurant);
    }

    [HttpGet]
    public async Task<IActionResult> GetMyRestaurants()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var restaurants = await _service.GetRestaurant(userId);

        return Ok(restaurants);
    }
}