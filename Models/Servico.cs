namespace PrestacaoServico.Models
{
    public class Servico
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int PrestadorId { get; set; }
        public List<OrdemServico> OrdemServicos { get; set; }
    }
}
