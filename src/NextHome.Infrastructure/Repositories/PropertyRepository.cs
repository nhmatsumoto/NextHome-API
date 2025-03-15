using Dapper;
using NextHome.Domain.Entities;
using NextHome.Domain.Interfaces.Repositories;
using System.Data;

namespace NextHome.Infrastructure.Repositories;

public class PropertyRepository : Repository<Property>, IPropertyRepository
{
    private readonly IDbConnection _dbConnection;

    public PropertyRepository(IDbConnection dbConnection) : base(dbConnection)
    {
        _dbConnection = dbConnection;
    }

    private IDbConnection GetOpenConnection()
    {
        if (_dbConnection.State != ConnectionState.Open)
            _dbConnection.Open();
        return _dbConnection;
    }

    public async Task<IEnumerable<Property>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        const string sql = @"
        SELECT 
            p.Id, p.Title, p.Description, p.Price, p.Bedrooms, p.Bathrooms, p.ParkingSpaces, 
            p.FloorArea, p.YearBuilt, p.IsAvailable, p.Type, p.Category, p.AddressId, p.ManagementFee,
            a.Street, a.City,
            ph.Url AS PhotoUrl
        FROM Property p
        LEFT JOIN PropertyAddress a ON p.AddressId = a.Id
        LEFT JOIN PropertyPhoto ph ON p.Id = ph.PropertyId";

        var propertyDictionary = new Dictionary<int, Property>();

        var properties = await GetOpenConnection().QueryAsync<Property, string, Property>(
            sql,
            (property, photoUrl) =>
            {
                if (!propertyDictionary.TryGetValue(property.Id, out var existingProperty))
                {
                    existingProperty = property;
                    existingProperty.Photos = new List<PropertyPhoto>();
                    propertyDictionary.Add(existingProperty.Id, existingProperty);
                }

                if (!string.IsNullOrEmpty(photoUrl))
                {
                    existingProperty.Photos.Add(new PropertyPhoto { Url = photoUrl });
                }

                return existingProperty;
            },
            splitOn: "PhotoUrl",
            commandTimeout: null,
            commandType: CommandType.Text
        );

        return properties.Distinct().ToList();
    }

    public async Task<Property?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        const string sql = "SELECT * FROM Property WHERE Id = @Id";

        return await GetOpenConnection().QueryFirstOrDefaultAsync<Property>(
            new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));
    }

    public async Task<int> AddAsync(Property property, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            INSERT INTO Property (Title, Description, Price, Bedrooms, AddressId) 
            OUTPUT INSERTED.Id 
            VALUES (@Title, @Description, @Price, @Bedrooms, @AddressId)";

        return await GetOpenConnection().ExecuteScalarAsync<int>(
            new CommandDefinition(sql, property, cancellationToken: cancellationToken));
    }

    public async Task<bool> UpdateAsync(Property property, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            UPDATE Property 
            SET Title = @Title, 
                Description = @Description, 
                Price = @Price, 
                Bedrooms = @Bedrooms 
            WHERE Id = @Id";

        return await GetOpenConnection().ExecuteAsync(
            new CommandDefinition(sql, property, cancellationToken: cancellationToken)) > 0;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        const string sql = "DELETE FROM Property WHERE Id = @Id";

        return await GetOpenConnection().ExecuteAsync(
            new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken)) > 0;
    }
}
