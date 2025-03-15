using Dapper;
using NextHome.Domain.Entities;
using NextHome.Domain.Interfaces;
using System.Data;

namespace NextHome.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly IDbConnection _dbConnection;

    public UserRepository(IDbConnection dbConnection) : base(dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<int> AddAsync(User user, CancellationToken cancellationToken = default)
    {
        var sql = "INSERT INTO [User] (Id, Name, Email, CreatedAt) VALUES (@Id, @Name, @Email, @CreatedAt);" +
                  "SELECT CAST(SCOPE_IDENTITY() as int)";

        var parameters = new { user.Id, user.Username, user.Email, user.CreatedAt };

        return await _dbConnection.ExecuteScalarAsync<int>(new CommandDefinition(sql, parameters, cancellationToken: cancellationToken));
    }

    public async Task<User?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var sql = "SELECT * FROM [User] WHERE Id = @Id";

        return await _dbConnection.QueryFirstOrDefaultAsync<User>(
            new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var sql = "SELECT * FROM [User]";

        return await _dbConnection.QueryAsync<User>(
            new CommandDefinition(sql, cancellationToken: cancellationToken));
    }

    public async Task<bool> UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        var sql = "UPDATE [User] SET Name = @Name, Email = @Email WHERE Id = @Id";

        var affectedRows = await _dbConnection.ExecuteAsync(
            new CommandDefinition(sql, user, cancellationToken: cancellationToken));

        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var sql = "DELETE FROM [User] WHERE Id = @Id";

        var affectedRows = await _dbConnection.ExecuteAsync(
            new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));

        return affectedRows > 0;
    }

    public async Task<bool> ExistsByEmail(string email, CancellationToken cancellationToken = default)
    {
        var sql = "SELECT COUNT(1) FROM [User] WHERE Email = @Email";

        return await _dbConnection.ExecuteScalarAsync<bool>(
            new CommandDefinition(sql, new { Email = email }, cancellationToken: cancellationToken));
    }
}
