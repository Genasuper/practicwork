using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using kat_mob_soft.DAL.Interfaces;
using kat_mob_soft.Domain.ModelsDb;

namespace kat_mob_soft.DAL.Storages
{
    public class UserStorage : IBaseStorage<UserDb>
    {
        private readonly ApplicationDbContext _db;

        public UserStorage(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<UserDb> GetByIdAsync(int id)
        {
            return await _db.Users.FindAsync(id);
        }

        public async Task<IEnumerable<UserDb>> GetAllAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<UserDb> CreateAsync(UserDb entity)
        {
            await _db.Users.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task<UserDb> UpdateAsync(UserDb entity)
        {
            _db.Users.Update(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                _db.Users.Remove(user);
                await SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}