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

    public async Task<Table> UpdateTable(
        int restaurantId,
        int tableId,
        UpdateTableDto dto
    )
    {
        var table = await _context.Tables
            .FirstOrDefaultAsync(t =>
                t.Id == tableId &&
                t.RestaurantId == restaurantId);

        if (table == null)
            throw new Exception("Table not found");

        table.TableNumber = dto.TableNumber;
        table.Capacity = dto.Capacity;

        await _context.SaveChangesAsync();

        return table;
    }

    public async Task DeleteTable(
        int restaurantId,
        int tableId
    )
    {
        var table = await _context.Tables
            .FirstOrDefaultAsync(t =>
                t.Id == tableId &&
                t.RestaurantId == restaurantId);

        if (table == null)
            throw new Exception("Table not found");

        _context.Tables.Remove(table);

        await _context.SaveChangesAsync();
    }
}