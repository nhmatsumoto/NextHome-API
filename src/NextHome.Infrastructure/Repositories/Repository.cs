using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NextHome.Domain.Interfaces;
using System.Data;

namespace NextHome.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly string _connectionString;

        public Repository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
               ?? throw new ArgumentNullException(nameof(configuration), "Connection string not found.");
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            string sql = $"SELECT * FROM {typeof(T).Name}";
            using var connection = CreateConnection();
            return await connection.QueryAsync<T>(sql);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            string sql = $"SELECT * FROM {typeof(T).Name} WHERE Id = @Id";
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
        }

        public async Task<int> AddAsync(T entity)
        {
            var properties = typeof(T).GetProperties().Where(p => p.Name != "Id").ToList();
            var columnNames = string.Join(", ", properties.Select(p => p.Name));
            var paramNames = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            string sql = $"INSERT INTO {typeof(T).Name} ({columnNames}) OUTPUT INSERTED.Id VALUES ({paramNames})";

            using var connection = CreateConnection();
            return await connection.ExecuteScalarAsync<int>(sql, entity);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var properties = typeof(T).GetProperties().Where(p => p.Name != "Id").ToList();
            var setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));

            string sql = $"UPDATE {typeof(T).Name} SET {setClause} WHERE Id = @Id";

            using var connection = CreateConnection();
            return await connection.ExecuteAsync(sql, entity) > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            string sql = $"DELETE FROM {typeof(T).Name} WHERE Id = @Id";
            using var connection = CreateConnection();
            return await connection.ExecuteAsync(sql, new { Id = id }) > 0;
        }
    }
}
