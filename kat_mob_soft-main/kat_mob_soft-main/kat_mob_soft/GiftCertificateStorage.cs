using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using kat_mob_soft.DAL.Interfaces;
using kat_mob_soft.Domain.ModelsDb;

namespace kat_mob_soft.DAL.Storages
{
    public class GiftCertificateStorage : IBaseStorage<GiftCertificateDb>
    {
        private readonly ApplicationDbContext _db;

        public GiftCertificateStorage(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<GiftCertificateDb> GetByIdAsync(int id)
        {
            return await _db.GiftCertificates.FindAsync(id);
        }

        public async Task<IEnumerable<GiftCertificateDb>> GetAllAsync()
        {
            return await _db.GiftCertificates.ToListAsync();
        }

        public async Task<GiftCertificateDb> CreateAsync(GiftCertificateDb entity)
        {
            await _db.GiftCertificates.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task<GiftCertificateDb> UpdateAsync(GiftCertificateDb entity)
        {
            _db.GiftCertificates.Update(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var certificate = await GetByIdAsync(id);
            if (certificate != null)
            {
                _db.GiftCertificates.Remove(certificate);
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