namespace PraticProect.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string ImageUrl { get; set; } = string.Empty;
    }
}