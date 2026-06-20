using Microsoft.EntityFrameworkCore;
using TaskApi.Data;
using TaskApi.Models;

namespace TaskApi.Services;

public class ShipmentService
{
    private readonly AppDbContext _db;
    private readonly ILogger<ShipmentService> _logger;

    public ShipmentService(AppDbContext db, ILogger<ShipmentService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<List<ShipmentDto>> GetDelayedShipmentsAsync(int minDays)
    {
        _logger.LogInformation("Fetching delayed shipments with minDays {MinDays}", minDays);

        var cutoffDate = DateTime.Now.AddDays(-minDays);

        return await _db.Shipments
            .AsNoTracking()
            .Where(s => s.Status != "Delivered" && s.EstimatedDeliveryDate <= cutoffDate)
            .OrderBy(s => s.EstimatedDeliveryDate)
            .Select(s => new ShipmentDto
            {
                Id = s.Id,
                TrackingCode = s.TrackingCode,
                Destination = s.Destination,
                Status = s.Status,
                EstimatedDeliveryDate = s.EstimatedDeliveryDate,
                CarrierName = s.Carrier != null ? s.Carrier.Name : null
            })
            .ToListAsync();
    }
}
