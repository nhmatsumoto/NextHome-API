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

        private async Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken)
        {
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken); 
            return connection;
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            string sql = $"SELECT * FROM {typeof(T).Name}";

            using var connection = await CreateConnectionAsync(cancellationToken);
            return await connection.QueryAsync<T>(sql, cancellationToken);
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            string sql = $"SELECT * FROM {typeof(T).Name} WHERE Id = @Id";

            using var connection = await CreateConnectionAsync(cancellationToken);
            return await connection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
        }

        public async Task<int> AddAsync(T entity, CancellationToken cancellationToken)
        {
            var properties = typeof(T).GetProperties().Where(p => p.Name != "Id").ToList();
            var columnNames = string.Join(", ", properties.Select(p => p.Name));
            var paramNames = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            string sql = $"INSERT INTO {typeof(T).Name} ({columnNames}) OUTPUT INSERTED.Id VALUES ({paramNames})";

            using var connection = await CreateConnectionAsync(cancellationToken);
            return await connection.ExecuteScalarAsync<int>(sql, entity);
        }

        public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            var properties = typeof(T).GetProperties().Where(p => p.Name != "Id").ToList();
            var setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));

            string sql = $"UPDATE {typeof(T).Name} SET {setClause} WHERE Id = @Id";

            using var connection = await CreateConnectionAsync(cancellationToken);
            return await connection.ExecuteAsync(sql, entity) > 0;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            string sql = $"DELETE FROM {typeof(T).Name} WHERE Id = @Id";

            using var connection = await CreateConnectionAsync(cancellationToken);
            return await connection.ExecuteAsync(sql, new { Id = id }) > 0;
        }
    }
}
