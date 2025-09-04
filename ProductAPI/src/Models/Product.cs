using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public required string Codigo { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Preco { get; set; }

        [StringLength(500)]
        public required string Descricao { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int QuantidadeEmEstoque { get; set; }

        [Range(1, 5)]
        public float Avaliacao { get; set; }

        [StringLength(50)]
        public required string Categoria { get; set; }
    }
}