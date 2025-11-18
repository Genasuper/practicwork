using PraticProect.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ModelsDb
{
    [Table("categories")]
    public class CategoryDb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string? Image { get; set; }

        public int EquipmentCount { get; set; } = 0;

        public DateTime Created { get; set; } = DateTime.UtcNow;

        // Навигационное свойство
        public virtual ICollection<EquipmentDb> Equipment { get; set; } = new List<EquipmentDb>();
    }
}