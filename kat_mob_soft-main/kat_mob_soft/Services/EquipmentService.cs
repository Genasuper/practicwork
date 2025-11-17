using PraticProect.Models;

namespace PraticProect.Services
{
    public class EquipmentService
    {
        private readonly List<Equipment> _equipment = new();

        public List<Equipment> GetAllEquipment() => _equipment;
        public void AddEquipment(Equipment equipment) => _equipment.Add(equipment);
    }
}