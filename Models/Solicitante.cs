namespace PrestacaoServico.Models
{
    public class Solicitante
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public List<OrdemServico> OrdemServicos { get; set; } = new();
    }
}