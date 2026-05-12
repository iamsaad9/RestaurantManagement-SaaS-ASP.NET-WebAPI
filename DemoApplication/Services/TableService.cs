using Microsoft.EntityFrameworkCore;

public class TableService : ITableService
{
    private readonly AppDbContext _context;

    public TableService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Table> CreateTable(
        int restaurantId,
        CreateTableDto dto
    )
    {
        var table = new Table
        {
            RestaurantId = restaurantId,
            TableNumber = dto.TableNumber,
            Capacity = dto.Capacity
        };

        _context.Tables.Add(table);
        await _context.SaveChangesAsync();

        return table;
    }

    public async Task<List<Table>> GetAllTables(
        int restaurantId
    )
    {
        return await _context.Tables
        .Where(t => t.RestaurantId == restaurantId)
        .ToListAsync();

    }
}