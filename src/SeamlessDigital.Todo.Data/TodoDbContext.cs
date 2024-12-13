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
                data = conn.Query<TodoItem>(
                           @"select ID, 
                            ISNULL(JSON_VALUE(c.Text, '$.FirstName'),'') as FirstName,
                            ISNULL(JSON_VALUE(c.Text, '$.LastName'),'') as LastName,
                            JSON_VALUE(c.Text, '$.Age')  as Age,
                            ISNULL(JSON_VALUE(c.Text, '$.Address'),'') as Address,
                            ISNULL(JSON_VALUE(c.Text, '$.Reference'),'') as Reference,
                            ISNULL(JSON_VALUE(c.Text, '$.Telephone'),'') as Telephone
                            from [dbo].[TodoItems] c WHERE (Id= @Id or @Id is null)",
                           todoItem
                          );
            }
            return data.ToList();
        } 

        int IRepository<TodoItem>.Insert(TodoItem t, Guid createUser)
        {
            throw new NotImplementedException();
        }

        int IRepository<TodoItem>.Update(TodoItem t, Guid updateUser)
        {
            throw new NotImplementedException();
        }
    }
}
