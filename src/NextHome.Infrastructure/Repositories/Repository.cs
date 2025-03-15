using Dapper;
using NextHome.Domain.Interfaces.Repositories;
using System.Data;

namespace NextHome.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDbConnection _dbConnection;

        public Repository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        private IDbConnection GetOpenConnection()
        {
            if (_dbConnection.State != ConnectionState.Open)
                _dbConnection.Open();
            return _dbConnection;
        }

        private static string GetTableName() => $"[{typeof(T).Name}]";

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            string tabelName = GetTableName();
            string sql = $"SELECT * FROM {tabelName}";

            return await GetOpenConnection().QueryAsync<T>(
                new CommandDefinition(sql, cancellationToken: cancellationToken));
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            string tabelName = GetTableName();
            string sql = $"SELECT * FROM {tabelName} WHERE Id = @Id";

            return await GetOpenConnection().QueryFirstOrDefaultAsync<T>(
                new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));
        }

        public async Task<int> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            var properties = typeof(T).GetProperties().Where(p => p.Name != "Id").ToList();
            var columnNames = string.Join(", ", properties.Select(p => p.Name));
            var paramNames = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            string tabelName = GetTableName();
            string sql = $"INSERT INTO {tabelName} ({columnNames}) OUTPUT INSERTED.Id VALUES ({paramNames})";

            return await GetOpenConnection().ExecuteScalarAsync<int>(
                new CommandDefinition(sql, entity, cancellationToken: cancellationToken));
        }

        public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            var properties = typeof(T).GetProperties().Where(p => p.Name != "Id").ToList();
            var setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));

            string tabelName = GetTableName();
            string sql = $"UPDATE {tabelName} SET {setClause} WHERE Id = @Id";

            return await GetOpenConnection().ExecuteAsync(
                new CommandDefinition(sql, entity, cancellationToken: cancellationToken)) > 0;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            string tabelName = GetTableName();
            string sql = $"DELETE FROM {tabelName} WHERE Id = @Id";

            return await GetOpenConnection().ExecuteAsync(
                new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken)) > 0;
        }
    }
}
