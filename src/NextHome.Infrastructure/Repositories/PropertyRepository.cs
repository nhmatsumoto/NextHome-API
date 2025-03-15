using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NextHome.Domain.Entities;
using NextHome.Domain.Interfaces;
using System.Data;

namespace NextHome.Infrastructure.Repositories;

public class PropertyRepository : Repository<Property>, IPropertyRepository
{
    private readonly string _connectionString;

    public PropertyRepository(IConfiguration configuration) : base(configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private async Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken)
    {
        var connection = new SqlConnection(this._connectionString);
        await connection.OpenAsync(cancellationToken);
        return connection;
    }

    public async Task<IEnumerable<Property>> GetAllAsync()
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

        using (var connection = new SqlConnection(_connectionString))
        {
            var propertyDictionary = new Dictionary<int, Property>();

            var properties = await connection.QueryAsync<Property, string, Property>(
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
                splitOn: "PhotoUrl"
            );

            return properties.Distinct().ToList();
        }
    }

    public async Task<Property> GetByIdAsync(int id)
    {
        const string sql = "SELECT * FROM Property WHERE Id = @Id";
        using (var connection = new SqlConnection(_connectionString))
        {
            return await connection.QueryFirstOrDefaultAsync<Property>(sql, new { Id = id });
        }
    }

    public async Task<int> AddAsync(Property property)
    {
        const string sql = @"
            INSERT INTO Property (Title, Description, Price, Bedrooms, AddressId) 
            OUTPUT INSERTED.Id 
            VALUES (@Title, @Description, @Price, @Bedrooms, @AddressId)";

        using (var connection = new SqlConnection(_connectionString))
        {
            return await connection.ExecuteScalarAsync<int>(sql, property);
        }
    }

    public async Task<bool> UpdateAsync(Property property)
    {
        const string sql = @"
            UPDATE Property 
            SET Title = @Title, 
                Description = @Description, 
                Price = @Price, 
                Bedrooms = @Bedrooms 
            WHERE Id = @Id";

        using (var connection = new SqlConnection(_connectionString))
        {
            return await connection.ExecuteAsync(sql, property) > 0;
        }
    }


    public async Task<bool> DeleteAsync(int id)
    {
        const string sql = "DELETE FROM Property WHERE Id = @Id";
        using (var connection = new SqlConnection(_connectionString))
        {
            return await connection.ExecuteAsync(sql, new { Id = id }) > 0;
        }
    }
}
