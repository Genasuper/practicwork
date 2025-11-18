using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ModelsDb
{
    [Table("equipment_images")]
    public class EquipmentImageDb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int EquipmentId { get; set; }

        [Required]
        [MaxLength(255)]
        public string ImagePath { get; set; }

        // Навигационные свойства
        [ForeignKey("EquipmentId")]
        public virtual EquipmentDb Equipment { get; set; }
    }
}