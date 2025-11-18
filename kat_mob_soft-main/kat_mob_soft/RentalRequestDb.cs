using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ModelsDb
{
    [Table("rental_requests")]
    public class RentalRequestDb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Budget { get; set; }

        [MaxLength(100)]
        public string? DesiredDates { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "New";

        public DateTime Created { get; set; } = DateTime.UtcNow;

        // Навигационные свойства
        [ForeignKey("UserId")]
        public virtual UserDb User { get; set; }
    }
}