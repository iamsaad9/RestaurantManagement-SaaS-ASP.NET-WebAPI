using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Authorize]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _service;

    public ReservationsController(IReservationService service)
    {
        _service = service;
    }

    [HttpPost("api/{restaurantId}/reservations")]
    public async Task<IActionResult> CreateReservation(
        int restaurantId,
        CreateReservationDto dto
    )
    {
        var tenantRestaurantClaim =
    User.FindFirst("restaurantId");

        if (tenantRestaurantClaim != null)
        {
            var tenantRestaurantId =
                int.Parse(tenantRestaurantClaim.Value);

            if (tenantRestaurantId != restaurantId)
                return Forbid();
        }

        var reservation = await _service
        .CreateReservation(restaurantId, dto);

        return Ok(reservation);
    }

    [HttpPut("api/{restaurantId}/reservations/{reservationId}")]
    public async Task<IActionResult> UpdateReservation(
       int restaurantId,
       int reservationId,
       UpdateReservationDto dto
   )
    {
        var tenantRestaurantClaim =
    User.FindFirst("restaurantId");

        if (tenantRestaurantClaim != null)
        {
            var tenantRestaurantId =
                int.Parse(tenantRestaurantClaim.Value);

            if (tenantRestaurantId != restaurantId)
                return Forbid();
        }

        var reservation = await _service
        .UpdateReservation(restaurantId, reservationId, dto);

        return Ok(reservation);
    }

    [HttpDelete("api/{restaurantId}/reservations/{reservationId}")]
    public async Task<IActionResult> DeleteReservation(int restaurantId, int reservationId)
    {
        var tenantRestaurantClaim =
    User.FindFirst("restaurantId");

        if (tenantRestaurantClaim != null)
        {
            var tenantRestaurantId =
                int.Parse(tenantRestaurantClaim.Value);

            if (tenantRestaurantId != restaurantId)
                return Forbid();
        }

        await _service.DeleteReservation(restaurantId, reservationId);

        return Ok("Reservation deleted successfully");
    }


    [HttpGet("api/{restaurantId}/reservations")]
    public async Task<IActionResult> GetAllReservations(
        [FromQuery] ReservationQueryDto query,
        int restaurantId
    )
    {

        var tenantRestaurantClaim =
    User.FindFirst("restaurantId");

        if (tenantRestaurantClaim != null)
        {
            var tenantRestaurantId =
                int.Parse(tenantRestaurantClaim.Value);

            if (tenantRestaurantId != restaurantId)
                return Forbid();
        }

        var reservations = await _service.GetReservation(restaurantId, query);

        return Ok(reservations);
    }



}