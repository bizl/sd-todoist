using Dapper;
using Microsoft.Data.SqlClient;
using SeamlessDigital.Todo.Data.Interfaces;
using SeamlessDigital.Todo.Domain;
using System.Data;

namespace SeamlessDigital.Todo.Data
{
    public class TodoDbContext : IRepository<TodoItem>
    {
        string _connectionString;

        public TodoDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<TodoItem> Get(TodoItem todoItem)
        {
            IEnumerable<TodoItem> data;
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                data = conn.Query(
                          @"SELECT t.[Id]
                                  ,t.[UserId]
                                  ,t.[Todo]
                                  ,t.[Completed]
                                  ,t.[Priority]
                                  ,t.[Latitude]
                                  ,t.[Longitude]
                                  ,t.[DueDate]
                                  ,c.[Name] as Category 
                                  ,t.[CreateDate]
                                  ,t.[LastUpdateDate]
                                  ,t.[CreatedBy]
                                  ,t.[LastUpdatedBy]
                                  ,t.[Deleted]
                              FROM [dbo].[Todos] t 
	                    INNER JOIN [dbo].[Categories] c on t.Category = c.CategoryKey 
                             WHERE ISNULL(Deleted,0)=0 
                              AND ( 
		                            (ISNULL([Priority],3) = @Priority AND @Priority is not NULL) 
		                            OR 
		                            (YEAR(DueDate) =  YEAR(@DueDate) AND MONTH(DueDate) =  MONTH(@DueDate) AND DAY(DueDate) =  DAY(@DueDate) AND @DueDate is not NULL) 
		                            )",
                           todoItem
                          )
                    .Select(row => new TodoItem
                    {
                        Id = row.Id,
                        UserId = row.UserId,
                        Completed = row.Completed,
                        Priority = row.Priority,
                        Location = new Location { Latitude = row.Latitude, Longitude = row.Longitude },
                        DueDate = row.DueDate,
                        Category = row.Category  
                    }
                      ); 
            }
            return data.ToList();
        } 

        int IRepository<TodoItem>.Insert(TodoItem t, int createUser)
        {
            int affectedRows = 0;
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                //TODO: Move into stored proc to validate Category via Category keys 
                affectedRows = conn.Execute(
       @"INSERT INTO [dbo].[Todos] ( [UserId] ,[Todo] ,[Completed],
                                 [Priority] ,[Latitude],[Longitude]
                                ,[DueDate],[Category],[CreatedBy]) 
                        VALUES ( @UserId, @Todo, @Completed, @Priority,@Latitude,@Longitude,@DueDate,@Category,@CreatedBy  )",
                      new {  UserId=t.UserId,Todo= t.Todo , Completed=t.Completed,
                             Priority=t.Priority,Latitude=t.Location?.Latitude, Longitude=t.Location?.Longitude,
                             DueDate=t.DueDate,Category=t.Category, CreatedBy= createUser}
                      );
            }
            return affectedRows;
        }

        int IRepository<TodoItem>.Update(TodoItem t, int updateUser)
        {     //TODO - delete flag to be set
            int affectedRows = 0;
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                affectedRows = conn.Execute(
                       "update [dbo].[Todos]  set Priority=@Priority, DueDate=@DueDate WHERE Id = @Id",
                      new { Priority=t.Priority, DueDate=t.DueDate, LastUpdateDate=DateTime.Now, LastUpdatedBy=updateUser, Id= t.Id }
                      );
            }
            return affectedRows;
        }
    }
}
