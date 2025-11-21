using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using kat_mob_soft.DAL.Interfaces;
using kat_mob_soft.Domain.ModelsDb;

namespace kat_mob_soft.DAL.Storages
{
    public class OrderStorage : IBaseStorage<OrderDb>
    {
        private readonly ApplicationDbContext _db;

        public OrderStorage(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<OrderDb> GetByIdAsync(int id)
        {
            return await _db.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<OrderDb>> GetAllAsync()
        {
            return await _db.Orders.ToListAsync();
        }

        public async Task<OrderDb> CreateAsync(OrderDb entity)
        {
            await _db.Orders.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task<OrderDb> UpdateAsync(OrderDb entity)
        {
            _db.Orders.Update(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await GetByIdAsync(id);
            if (order != null)
            {
                _db.Orders.Remove(order);
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