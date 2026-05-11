public class Reservation
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public int TableId { get; set; }

    public Table Table { get; set; } = null!;

public string CustomerName { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

}