namespace PrestacaoServico.Models
{
    public class Prestador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public string Profissao { get; set; }   
        public List<OrdemServico> OrdemServicos { get; set; } = new();
        public List<Servico> Servicos { get; set; } = new();
    }
}