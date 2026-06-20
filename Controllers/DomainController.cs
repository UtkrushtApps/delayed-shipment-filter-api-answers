using Microsoft.AspNetCore.Mvc;
using TaskApi.Services;

namespace TaskApi.Controllers;

[ApiController]
[Route("api/shipments")]
public class ShipmentsController : ControllerBase
{
    private readonly ShipmentService _shipmentService;
    private readonly ILogger<ShipmentsController> _logger;

    public ShipmentsController(ShipmentService shipmentService, ILogger<ShipmentsController> logger)
    {
        _shipmentService = shipmentService;
        _logger = logger;
    }

    [HttpGet("delayed")]
    public async Task<IActionResult> GetDelayed([FromQuery] string? minDays = null)
    {
        var minDaysValue = Request.Query.ContainsKey("minDays") ? minDays : "1";

        if (!int.TryParse(minDaysValue, out var parsedMinDays) || parsedMinDays < 1)
        {
            _logger.LogWarning("Invalid minDays query value: {MinDays}", minDaysValue);
            return BadRequest(new { message = "minDays must be a positive integer." });
        }

        var result = await _shipmentService.GetDelayedShipmentsAsync(parsedMinDays);
        return Ok(result);
    }
}
