using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using kat_mob_soft.DAL.Interfaces;
using kat_mob_soft.Domain.ModelsDb; // ИЗМЕНИТЬ using

namespace kat_mob_soft.DAL.Storages
{
    public class ContactMessageStorage : IContactMessageStorage
    {
        private readonly ApplicationDbContext _context;

        public ContactMessageStorage(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ContactMessageDb> GetByIdAsync(int id)
        {
            return await _context.ContactMessages
                .Include(cm => cm.User)
                .FirstOrDefaultAsync(cm => cm.Id == id);
        }

        public async Task<List<ContactMessageDb>> GetAllAsync()
        {
            return await _context.ContactMessages
                .Include(cm => cm.User)
                .OrderByDescending(cm => cm.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<ContactMessageDb>> GetByUserIdAsync(int userId)
        {
            return await _context.ContactMessages
                .Where(cm => cm.UserId == userId)
                .OrderByDescending(cm => cm.CreatedAt)
                .ToListAsync();
        }

        public async Task<int> CreateAsync(ContactMessageDb message)
        {
            _context.ContactMessages.Add(message);
            await _context.SaveChangesAsync();
            return message.Id;
        }

        public async Task<bool> UpdateAsync(ContactMessageDb message)
        {
            message.UpdatedAt = DateTime.UtcNow;
            _context.ContactMessages.Update(message);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);
            if (message != null)
            {
                _context.ContactMessages.Remove(message);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> UpdateStatusAsync(int id, string status, string adminNotes = null, int? adminId = null)
        {
            var message = await _context.ContactMessages.FindAsync(id);
            if (message != null)
            {
                message.Status = status;
                message.AdminNotes = adminNotes;
                message.AdminId = adminId;
                message.UpdatedAt = DateTime.UtcNow;

                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}