public interface IReservationService
{
    Task<Reservation> CreateReservation(
        int restaurantId,
        CreateReservationDto dto
    );

    Task<List<Reservation>> GetReservation(
        int restaurantId
    );
}