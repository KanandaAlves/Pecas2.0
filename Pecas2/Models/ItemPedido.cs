using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pecas2.Models
{
    public class ItemPedido
    {
        [DisplayName("Pedido")]
        public int PedidoId { get; set; }
        public Pedido? Pedido { get; set; }

        [DisplayName("Produto")]
        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }

     

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(1, 1000, ErrorMessage = "A quantidade deve estar entre 1 e 1000.")]
        public int Quantidade { get; set; }

        public decimal Total { get; set; }
   
    }
}
