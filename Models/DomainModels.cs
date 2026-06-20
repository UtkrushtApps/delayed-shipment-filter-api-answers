namespace TaskApi.Models;

public class Carrier
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class Shipment
{
    public int Id { get; set; }
    public string TrackingCode { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime EstimatedDeliveryDate { get; set; }
    public int? CarrierId { get; set; }
    public Carrier? Carrier { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ShipmentDto
{
    public int Id { get; set; }
    public string TrackingCode { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime EstimatedDeliveryDate { get; set; }
    public string? CarrierName { get; set; }
}
