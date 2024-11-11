using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Pecas2.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A data do pedido é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [DisplayName("Descrição do Pedido")]
        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres.")]
        public string? Descricao { get; set; }
        

        [DisplayName("Cliente")]
        public int ClienteId { get; set; }

        public Cliente? Cliente { get; set; }

        public ICollection<ItemPedido>? ItemPedidos { get; set; } = new List<ItemPedido>();
    }
}
