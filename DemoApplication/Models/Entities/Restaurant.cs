public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int OwnerId { get; set; } 

    public User Owner { get; set; }= null!;

    public List<Table> Tables { get; set; } = new();   
}