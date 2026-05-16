using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
public class TablesController : ControllerBase
{
    private readonly ITableService _service;
    public TablesController(ITableService service)
    {
        _service = service;
    }

    [HttpPost("api/tables")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateTable(CreateTableDto dto)
    {
        // Extracted securely from the JWT token
        var restaurantId = int.Parse(User.FindFirst("restaurantId")!.Value);
        var table = await _service.CreateTable(restaurantId, dto);

        return Ok(table);
    }

    [HttpPut("api/tables/{tableId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateTable(UpdateTableDto dto, int tableId)
    {
        var restaurantId = int.Parse(User.FindFirst("restaurantId")!.Value);
        var table = await _service.UpdateTable(restaurantId, tableId, dto);

        return Ok(table);
    }

    [HttpDelete("api/tables/{tableId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteTable(int tableId)
    {
        var restaurantId = int.Parse(User.FindFirst("restaurantId")!.Value);
        await _service.DeleteTable(restaurantId, tableId);

        return Ok("Table deleted successfully!");
    }

    // This route explicitly binds the restaurantId from the URL path
    [HttpGet("api/{restaurantId}/tables")]
    public async Task<IActionResult> GetAllTables([FromRoute] int restaurantId)
    {
        var tables = await _service.GetAllTables(restaurantId);

        return Ok(tables);
    }
}