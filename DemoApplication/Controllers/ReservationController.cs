using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/{restaurantId}/reservations")]
[Authorize]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _service;

    public ReservationsController(IReservationService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        int restaurantId,
        CreateReservationDto dto
    )
    {
        // var restaurantId = int.Parse(User.FindFirst("restaurantId")!.Value);

        var reservation = await _service
        .CreateReservation(restaurantId, dto);

        return Ok(reservation);
    }


    [HttpGet]
    public async Task<IActionResult> GetAllReservations(
        [FromQuery] ReservationQueryDto query
    )
    {
        var restaurantId = int.Parse(User.FindFirst("restaurantId")!.Value);

        var reservations = await _service.GetReservation(restaurantId, query);

        return Ok(reservations);
    }

}