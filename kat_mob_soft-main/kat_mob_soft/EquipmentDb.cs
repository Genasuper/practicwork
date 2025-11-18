using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ModelsDb
{
    [Table("equipment")]
    public class EquipmentDb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(100)]
        public string Model { get; set; }

        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal PricePerDay { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Available";

        public string? Specifications { get; set; }

        [MaxLength(255)]
        public string? MainImage { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        // Навигационные свойства
        [ForeignKey("CategoryId")]
        public virtual CategoryDb Category { get; set; }

        public virtual ICollection<EquipmentImageDb> Images { get; set; } = new List<EquipmentImageDb>();
        public virtual ICollection<RentalOrderDb> Orders { get; set; } = new List<RentalOrderDb>();
    }
}