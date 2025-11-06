using System.ComponentModel.DataAnnotations;

namespace PrestacaoServico.Views
{
    public class CadastrarView
    {
        public string Nome { get; set; }
        public string Profissao { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Tipo { get; set; }
    }
}
