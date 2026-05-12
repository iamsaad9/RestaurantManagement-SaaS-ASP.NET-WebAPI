using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/table")]
[Authorize]
public class TablesController : ControllerBase
{
    private readonly AppDbContext _context;
    public TablesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateTableDto dto)
    {
        var restaurantId = int.Parse(User.FindFirst("restuarantId")!.Value);

        var table = new Table
        {
            RestaurantId = restaurantId,
            TableNumber = dto.TableNumber,
            Capacity = dto.Capacity
        };

        _context.Tables.Add(table);
        await _context.SaveChangesAsync();

        return Ok(table);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTables()
    {
        var restaurantId = int.Parse(User.FindFirst("restuarantId")!.Value);

        var tables = await _context.Tables
        .Where(t => t.RestaurantId == restaurantId)
        .ToListAsync();

        return Ok(tables);
    }
}
