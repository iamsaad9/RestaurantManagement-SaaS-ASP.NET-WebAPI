using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<Membership> Memberships => Set<Membership>();
    public DbSet<Table> Tables => Set<Table>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Composite key for Membership
        modelBuilder.Entity<Membership>()
            .HasKey(m => new { m.UserId, m.RestaurantId });

        //Relationships
        modelBuilder.Entity<Membership>()
        .HasOne(m => m.User)                    //Many-to-many relationship between User and Restaurant through Membership
        .WithMany(u => u.Memberships)           //A user can have many memberships
        .HasForeignKey(m => m.UserId);          //Foreign key in Membership pointing to User

        modelBuilder.Entity<Membership>()
        .HasOne(m => m.Restaurant)              //Many-to-many relationship between User and Restaurant through Membership
        .WithMany()                             //A restaurant can have many memberships
        .HasForeignKey(m => m.RestaurantId);    //Foreign key in Membership pointing to Restaurant

        base.OnModelCreating(modelBuilder);
    }

}