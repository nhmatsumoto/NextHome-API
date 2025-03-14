using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NextHome.Domain.Entities;
using NextHome.Domain.Interfaces;

namespace NextHome.Infrastructure.Repositories;

/// <summary>
/// Repositório específico para a entidade Property.
/// Implementa operações CRUD utilizando Dapper.
/// </summary>
public class PropertyRepository : Repository<Property>, IPropertyRepository
{
    private readonly string _connectionString;

    public PropertyRepository(IConfiguration configuration) : base(configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    /// <summary>
    /// Obtém todos os imóveis cadastrados.
    /// </summary>
    /// <returns>Lista de imóveis.</returns>
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


    /// <summary>
    /// Obtém um imóvel pelo ID.
    /// </summary>
    /// <param name="id">ID do imóvel.</param>
    /// <returns>O imóvel correspondente ou null se não encontrado.</returns>
    public async Task<Property> GetByIdAsync(int id)
    {
        const string sql = "SELECT * FROM Property WHERE Id = @Id";
        using (var connection = new SqlConnection(_connectionString))
        {
            return await connection.QueryFirstOrDefaultAsync<Property>(sql, new { Id = id });
        }
    }

    /// <summary>
    /// Adiciona um novo imóvel ao banco de dados.
    /// </summary>
    /// <param name="property">Dados do imóvel.</param>
    /// <returns>ID do imóvel inserido.</returns>
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

    /// <summary>
    /// Atualiza um imóvel existente no banco de dados.
    /// </summary>
    /// <param name="property">Dados do imóvel com as alterações.</param>
    /// <returns>True se a atualização foi bem-sucedida, False caso contrário.</returns>
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

    /// <summary>
    /// Exclui um imóvel pelo ID.
    /// </summary>
    /// <param name="id">ID do imóvel a ser excluído.</param>
    /// <returns>True se a exclusão foi bem-sucedida, False caso contrário.</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        const string sql = "DELETE FROM Property WHERE Id = @Id";
        using (var connection = new SqlConnection(_connectionString))
        {
            return await connection.ExecuteAsync(sql, new { Id = id }) > 0;
        }
    }
}
