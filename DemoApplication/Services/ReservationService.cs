using Microsoft.EntityFrameworkCore;

public class ReservationService : IReservationService
{
    private readonly AppDbContext _context;

    public ReservationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Reservation> CreateReservation(
        int restaurantId,
        CreateReservationDto dto)
    {
        var table = await _context.Tables
            .FirstOrDefaultAsync(t =>
                t.Id == dto.TableId &&
                t.RestaurantId == restaurantId);

        if (table == null)
            throw new Exception("Invalid table");

        var conflict = await _context.Reservations
            .AnyAsync(r =>
                r.TableId == dto.TableId &&
                r.StartTime < dto.EndTime &&
                r.EndTime > dto.StartTime
            );

        if (conflict)
            throw new Exception(
                "Table already reserved during this time"
            );

        var reservation = new Reservation
        {
            RestaurantId = restaurantId,
            TableId = dto.TableId,
            CustomerName = dto.CustomerName,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime
        };

        _context.Reservations.Add(reservation);

        await _context.SaveChangesAsync();

        return reservation;
    }

    public async Task<List<Reservation>> GetReservation(
        int restaurantId)
    {
        return await _context.Reservations
            .Include(r => r.Table)
            .Where(r => r.RestaurantId == restaurantId)
            .ToListAsync();
    }
}