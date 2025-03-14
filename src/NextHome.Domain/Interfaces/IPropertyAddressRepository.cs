using NextHome.Domain.Entities;

namespace NextHome.Domain.Interfaces
{
    public interface IPropertyAddressRepository
    {
        Task<IEnumerable<PropertyAddress>> GetAllAsync();
        Task<PropertyAddress> GetByIdAsync(int id);
        Task<int> AddAsync(PropertyAddress address);
        Task<bool> UpdateAsync(PropertyAddress address);
        Task<bool> DeleteAsync(int id);
    }
}
