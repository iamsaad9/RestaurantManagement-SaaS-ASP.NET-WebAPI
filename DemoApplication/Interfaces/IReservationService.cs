public interface IReservationService
{
    Task<Reservation> CreateReservation(
        int restaurantId,
        CreateReservationDto dto
    );

    Task<Reservation> UpdateReservation(
       int restaurantId,
       int reservationId,
       UpdateReservationDto dto
   );

    Task<List<Reservation>> GetReservation(
        int restaurantId,
        ReservationQueryDto query
    );

    Task DeleteReservation(
        int restaurantId,
        int reservationId
    );
}