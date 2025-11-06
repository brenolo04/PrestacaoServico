using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrestacaoServico.Data;
using PrestacaoServico.Views;
using System.Security.Claims;

namespace PrestacaoServico.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ServicoController : ControllerBase
    {
        public PrestacaoServicoContext _db { get; set; }

        public ServicoController(PrestacaoServicoContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetServicos()
        {
            return Ok(await _db.Servicos.AsNoTracking().ToArrayAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetServico
        (int id)
        {
            var servico = await _db.Servicos.FirstOrDefaultAsync(s => s.Id == id);

            if (servico == null)
                return NotFound();

            return Ok(new { servico });
        }

        [HttpPost("cadastrar")]
        [Authorize(Roles = "prestador")]
        public async Task<IActionResult> PostServico
        ([FromBody] ServicoView model)
        {
            var email = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

            var prestador = await _db.Prestadores
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(x => x.Usuario.Email == email!.Value);

            var servico = new Models.Servico
            {
                Nome = model.Nome,
                Descricao = model.Descricao,
                Valor = model.Valor,
                PrestadorId = prestador!.Id
            };

            _db.Servicos.Add(servico);
            await _db.SaveChangesAsync();

            return Created($"api/servico/{servico.Id}", servico);

        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "prestador")]
        public async Task<IActionResult> DeleteServico
        (int id)
        {
            var servico = await _db.Servicos.FirstOrDefaultAsync(s => s.Id == id);

            if (servico == null)
                return NotFound();

            _db.Servicos.Remove(servico);
            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "prestador")]
        public async Task<IActionResult> PutServico
        (int id, [FromBody] ServicoUpdateView model)
        {
            var servico = await _db.Servicos.FirstOrDefaultAsync(s => s.Id == id);

            if (servico == null)
                return NotFound();

            if (model.Nome is not null)
            {
                if (!string.IsNullOrWhiteSpace(model.Nome))
                    servico.Nome = model.Nome;
            }

            if (model.Descricao is not null)
            {
                if (!string.IsNullOrWhiteSpace(model.Descricao))
                    servico.Descricao = model.Descricao;
            }

            if (model.Valor.HasValue)
            {
                if (model.Valor.Value >= 0)
                    servico.Valor = model.Valor.Value;
            }

            _db.Servicos.Update(servico);
            await _db.SaveChangesAsync();
            return Ok(servico);
        }

    }
}
