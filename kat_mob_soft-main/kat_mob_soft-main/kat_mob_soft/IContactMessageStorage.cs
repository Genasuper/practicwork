using System.Collections.Generic;
using System.Threading.Tasks;
using kat_mob_soft.Domain.ModelsDb; // ИЗМЕНИТЬ using

namespace kat_mob_soft.DAL.Interfaces
{
    public interface IContactMessageStorage
    {
        Task<ContactMessageDb> GetByIdAsync(int id);
        Task<List<ContactMessageDb>> GetAllAsync();
        Task<List<ContactMessageDb>> GetByUserIdAsync(int userId);
        Task<int> CreateAsync(ContactMessageDb message);
        Task<bool> UpdateAsync(ContactMessageDb message);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateStatusAsync(int id, string status, string adminNotes = null, int? adminId = null);
    }
}