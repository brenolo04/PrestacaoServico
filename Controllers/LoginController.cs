using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrestacaoServico.Data;
using PrestacaoServico.Models;
using PrestacaoServico.Services;
using PrestacaoServico.Views;
using SecureIdentity.Password;

namespace PrestacaoServico.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        public PrestacaoServicoContext _db { get; set; }
        public TokenService _tokenService { get; set; }
        public LoginController(PrestacaoServicoContext db, TokenService tk)
        {
            _db = db;
            _tokenService = tk;
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync
        ([FromBody] LoginView modelo)
        {
            var usuario = await _db.Usuarios
                .FirstOrDefaultAsync(u => u.Email == modelo.Email);

            if (usuario == null) return BadRequest(new { menssagem = "Usuário não encontrado." });

            if (!PasswordHasher.Verify(usuario.SenhaHash, modelo.Senha))
                return BadRequest(new { menssagem = "Senha ou Email inválidos" });
            
            var token = _tokenService.GerarToken(usuario);

            return Ok(new { token });
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarAsync
        ([FromBody] CadastrarView modelo)
        {
            var usuarioExistente = _db.Usuarios
                .FirstOrDefault(u => u.Email == modelo.Email);
            if (usuarioExistente != null) return BadRequest(new { menssagem = "Usuário já cadastrado." });

            var usuario = new Usuario
            {
                Email = modelo.Email,
                SenhaHash = PasswordHasher.Hash(modelo.Senha),
                Tipo = modelo.Tipo!.ToLower(),
            };

            _db.Usuarios.Add(usuario);
            
            if (modelo.Tipo.ToLower() == "prestador")
            {
                var prestador = new Prestador
                {
                    Nome = modelo.Nome,
                    Profissao = modelo.Profissao!,
                    Usuario = usuario,
                };
                _db.Prestadores.Add(prestador);
            }
            else
            {
                var cliente = new Solicitante
                {
                    Nome = modelo.Nome,
                    Usuario = usuario,
                };
                _db.Solicitantes.Add(cliente);
            }

            await _db.SaveChangesAsync();

            return Ok(new { usuario });
        }
    }
}