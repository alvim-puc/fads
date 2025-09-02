using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
        
        [Required]
        [StringLength(50)]
        public string? Code { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }
        
        [Range(0, 5)]
        public decimal Rating { get; set; }
        
        [Required]
        [StringLength(50)]
        public string? Category { get; set; }
    }
}