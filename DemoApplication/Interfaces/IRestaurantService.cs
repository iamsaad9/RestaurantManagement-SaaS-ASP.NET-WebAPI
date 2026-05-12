public interface IRestaurantService
{
    Task<Restaurant> CreateRestaurant(
        int userId,
        CreateRestaurantDto dto
    );

    Task<List<Restaurant>> GetRestaurant(
        int userId
    );
}