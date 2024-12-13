using Dapper;
using SeamlessDigital.Todo.Data.Interfaces;
using SeamlessDigital.Todo.Domain;
using System.Data;
using System.Data.SqlClient;

namespace SeamlessDigital.Todo.Data
{
    public class CategoryRepository : IRepository<Category>
    {
        string _connectionString;
        public CategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task<int> Update(Category t, int updateUser)
        {
            throw new NotImplementedException();
        }

        public List<Category> Get(Category category)
        {
            IEnumerable<Category> data;
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                data = conn.Query(
                          $@"SELECT CategoryKey , Name
                              FROM [dbo].[Categories] 
                            WHERE Name LIKE '%{category?.Title}%'",
                           category
                          )
                    .Select(row => new Category
                            { Title = row.Name, Key = row.CategoryKey }
                      ).AsQueryable();
            }
            return data.ToList();
        }

        public Task<int> Insert(Category t, int createUser)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int id, int updateUser)
        {
            throw new NotImplementedException();
        }
    }
}
