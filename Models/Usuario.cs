namespace PrestacaoServico.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string SenhaHash { get; set; } = null!;
        public string Tipo { get; set; } = null!;
    }
}
