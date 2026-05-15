using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/table")]
[Authorize]
public class TablesController : ControllerBase
{
    private readonly ITableService _service;
    public TablesController(ITableService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CreateTableDto dto)
    {
        var restaurantId = int.Parse(User.FindFirst("restaurantId")!.Value);
        Console.WriteLine("Restaurant ID: ", restaurantId);
        var table = await _service
        .CreateTable(restaurantId, dto);

        return Ok(table);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTables()
    {
        var restaurantId = int.Parse(User.FindFirst("restuarantId")!.Value);

        var tables = await _service.GetAllTables(restaurantId);

        return Ok(tables);
    }
}
