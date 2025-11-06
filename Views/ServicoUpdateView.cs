using System.ComponentModel.DataAnnotations;

namespace PrestacaoServico.Views
{
    public class ServicoUpdateView
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal? Valor { get; set; }
    }
}