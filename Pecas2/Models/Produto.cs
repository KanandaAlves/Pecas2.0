using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pecas2.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A marca é obrigatória.")]
        [StringLength(100, ErrorMessage = "A marca deve ter no máximo 100 caracteres.")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.01, 1000000.00, ErrorMessage = "O preço deve estar entre 0,01 e 1.000.000,00.")]
        [DisplayName("Preço")]
        public decimal Preco { get; set; }

        public ICollection<ItemPedido> ItemPedidos { get; set; } = new List<ItemPedido>();
    }
}
