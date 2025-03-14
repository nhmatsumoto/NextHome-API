using NextHome.Domain.Entities;

namespace NextHome.Domain.Interfaces;

public interface IInquiryRepository
{
    Task<IEnumerable<Inquiry>> GetAllAsync();
    Task<Inquiry> GetByIdAsync(int id);
    Task<int> AddAsync(Inquiry inquiry);
    Task<bool> UpdateAsync(Inquiry inquiry);
    Task<bool> DeleteAsync(int id);
}
