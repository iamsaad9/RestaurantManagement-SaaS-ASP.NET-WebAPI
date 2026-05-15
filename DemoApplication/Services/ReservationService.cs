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
        Console.WriteLine("CREATE RESERVATION DATA: ", restaurantId, dto);
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
        int restaurantId, ReservationQueryDto query)
    {
        IQueryable<Reservation> reservationQuery = _context.Reservations
        .Include(r => r.Table)
        .Where(r => r.RestaurantId == restaurantId);

        //Query Filteration
        if (!string.IsNullOrWhiteSpace(query.CustomerName))
        {
            reservationQuery = reservationQuery.Where(r =>
            r.CustomerName.ToLower()
            .Contains(query.CustomerName.ToLower()));
        }

        //Table Filter
        if (query.TableId.HasValue)
        {
            reservationQuery = reservationQuery.Where(r =>
            r.TableId == query.TableId.Value);
        }


        return await reservationQuery
            .ToListAsync();
    }
}