using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace WebEngineering_2.Controllers;

[ApiController]
[Route("/api/v3/reservations/status")]
public class StatusController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public StatusController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            authors = new[] { "Abdallah Akour, Marc Matthäus" },
            api_version = "3.0.0"
        });
    }
    
    [HttpGet("health")]
    public async Task<IActionResult> Health()
    {
        bool dbConnected = await _context.Database.CanConnectAsync();

        if (!dbConnected)
        {
            var errorResponse = new
            {
                errors = new[]
                {
                    new
                    {
                        code = "service_unavailable",
                        message = "Database connection failed",
                        more_info = "The service cannot reach the database"
                    }
                },
                trace = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return StatusCode(StatusCodes.Status503ServiceUnavailable, errorResponse);
        }

        var okResponse = new
        {
            live = true,
            ready = true,
            databases = new
            {
                assets = new
                {
                    connected = true
                }
            }
        };

        return Ok(okResponse);
    }
    
    [HttpGet("health/live")]
    public IActionResult Live()
    {
        bool isLive = true;

        if (!isLive)
        {
            var errorResponse = new
            {
                errors = new[]
                {
                    new
                    {
                        code = "service_unavailable",
                        message = "Service is not live",
                        more_info = "The application process is not responding correctly"
                    }
                },
                trace = HttpContext.TraceIdentifier
            };

            return StatusCode(StatusCodes.Status503ServiceUnavailable, errorResponse);
        }

        return Ok(new { live = true });
    }
    
    [HttpGet("health/ready")]
    public async Task<IActionResult> Ready()
    {
        bool dbConnected = await _context.Database.CanConnectAsync();

        if (!dbConnected)
        {
            var errorResponse = new
            {
                errors = new[]
                {
                    new
                    {
                        code = "service_unavailable",
                        message = "Service is not ready to accept requests",
                        more_info = "Database connection is unavailable"
                    }
                },
                trace = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return StatusCode(StatusCodes.Status503ServiceUnavailable, errorResponse);
        }

        return Ok(new { ready = true });
    }
}