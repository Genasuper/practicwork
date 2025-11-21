using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using kat_mob_soft.DAL.Interfaces;
using kat_mob_soft.Domain.ModelsDb;

namespace kat_mob_soft.DAL.Storages
{
    public class UserProfileStorage : IBaseStorage<UserProfileDb>
    {
        private readonly ApplicationDbContext _db;

        public UserProfileStorage(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<UserProfileDb> GetByIdAsync(int id)
        {
            return await _db.UserProfiles.FindAsync(id);
        }

        public async Task<IEnumerable<UserProfileDb>> GetAllAsync()
        {
            return await _db.UserProfiles.ToListAsync();
        }

        public async Task<UserProfileDb> CreateAsync(UserProfileDb entity)
        {
            await _db.UserProfiles.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task<UserProfileDb> UpdateAsync(UserProfileDb entity)
        {
            _db.UserProfiles.Update(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var profile = await GetByIdAsync(id);
            if (profile != null)
            {
                _db.UserProfiles.Remove(profile);
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