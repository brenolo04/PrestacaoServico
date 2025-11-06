using PrestacaoServico.Models;
using System.ComponentModel.DataAnnotations;

namespace PrestacaoServico.Views
{
    public class ServicoView
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public decimal Valor { get; set; }
    }
}
