using System.ComponentModel.DataAnnotations;

namespace Pecas2.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"\d{11}", ErrorMessage = "CPF deve conter 11 dígitos.")]
        public string? CPF { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public long Telefone { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string? Email { get; set; }

        public ICollection<Pedido>? Pedidos { get; set; }

    }
}
