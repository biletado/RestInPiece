using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebEngineering_2.Classes;
namespace WebEngineering_2.Controllers;

[ApiController]
[Route("/api/v3/reservations/reservations")]
public class ReservationsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public ReservationsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var reservations = await _context.Reservations.ToListAsync();
        
        return Ok(reservations);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var reservation = await _context.Reservations
            .FirstOrDefaultAsync(r => r.id == id);

        if (reservation == null)
            return NotFound();
        
        return Ok(reservation);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Reservation reservation)
    {
        reservation.id = Guid.Empty;

        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(
            nameof(GetById),
            new { id = reservation.id },
            reservation);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReservationDto dto)
    {
        if (dto.from >= dto.to)
            return BadRequest("From must be before To");

        var reservation = await _context.Reservations.FindAsync(id);

        if (reservation == null)
            return NotFound();

        reservation.from = dto.from;
        reservation.to = dto.to;

        await _context.SaveChangesAsync();

        return Ok(reservation);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var reservation = await _context.Reservations.FindAsync(id);

        if (reservation == null)
            return NotFound($"Reservation {id} not found");

        reservation.deleted_at = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}