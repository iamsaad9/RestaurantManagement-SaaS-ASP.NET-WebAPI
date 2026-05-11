public class Membership
{
    public int UserId{get; set;}
    public User User { get; set; } = null!;

    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = null!;

    public string Role { get; set; } = "Staff"; //Admin, Manager, Staff
}