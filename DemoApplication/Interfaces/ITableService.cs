public interface ITableService
{
    Task<Table> CreateTable(
     int restaurantId,
     CreateTableDto dto
    );

    Task<Table> UpdateTable(
   int restaurantId,
   int tableId,
   UpdateTableDto dto
  );
    Task DeleteTable(
     int restaurantId,
     int tableId
    );

    Task<List<Table>> GetAllTables(
     int restaurantId
    );


}