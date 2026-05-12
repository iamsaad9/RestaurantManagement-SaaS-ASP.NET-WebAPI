public interface ITableService
{
    Task<Table> CreateTable(
     int restaurantId,
     CreateTableDto dto
    );

    Task<List<Table>> GetAllTables(
     int restaurantId
    );

}