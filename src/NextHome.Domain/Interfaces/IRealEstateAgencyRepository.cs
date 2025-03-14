using NextHome.Domain.Entities;

namespace NextHome.Domain.Interfaces;

public interface IRealEstateAgencyRepository
{
    Task<IEnumerable<RealEstateAgency>> GetAllAsync();
    Task<RealEstateAgency> GetByIdAsync(int id);
    Task<int> AddAsync(RealEstateAgency agency);
    Task<bool> UpdateAsync(RealEstateAgency agency);
    Task<bool> DeleteAsync(int id);
}
