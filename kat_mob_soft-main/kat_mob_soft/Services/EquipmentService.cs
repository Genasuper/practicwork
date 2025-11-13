using Microsoft.EntityFrameworkCore;
using PhotoRental.Models;
using PraticProect.Data;
using PraticProect.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PraticProect.Services
{
    public class EquipmentService
    {
        private readonly ApplicationDbContext _context;

        public EquipmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Equipment>> GetAllEquipmentAsync()
        {
            return await _context.Equipments
                .Where(e => e.IsAvailable)
                .OrderBy(e => e.Name)
                .ToListAsync();
        }

        public async Task<Equipment> GetEquipmentByIdAsync(int id)
        {
            return await _context.Equipments
                .FirstOrDefaultAsync(e => e.Id == id && e.IsAvailable);
        }
    }
}