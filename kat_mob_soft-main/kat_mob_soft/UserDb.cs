using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ModelsDb
{
    [Table("users")]
    public class UserDb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Login { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = "User";

        [MaxLength(255)]
        public string? Avatar { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        // Навигационные свойства
        public virtual ICollection<RentalOrderDb> Orders { get; set; } = new List<RentalOrderDb>();
        public virtual ICollection<RentalRequestDb> Requests { get; set; } = new List<RentalRequestDb>();
    }
}