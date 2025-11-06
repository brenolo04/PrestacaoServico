namespace PrestacaoServico.Models
{
    public class OrdemServico
    {
        public int Id { get; set; }
        public int ServicoId { get; set; }
        public Servico Servico { get; set; }
        public int SolicitanteId { get; set; }
        public Solicitante Solicitante { get; set; }
        public int PrestadorId { get; set; }
        public Prestador Prestador { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public string Status { get; set; } = null!;
    }
}
