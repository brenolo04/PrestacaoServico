using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrestacaoServico.Data;
using PrestacaoServico.Models;
using PrestacaoServico.Views;
using System.Security.Claims;

namespace PrestacaoServico.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdemServicoController : ControllerBase
    {
        public PrestacaoServicoContext _db { get; set; }
        public OrdemServicoController(PrestacaoServicoContext db) 
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetOrdemServicos()
        {
            var ordensServico = _db.OrdemServicos.AsNoTracking().ToList();
            return Ok(ordensServico);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOrdemServico
        (int id)
        {
            var ordemServico = _db.OrdemServicos.AsNoTracking().FirstOrDefault(os => os.Id == id);
        
            if (ordemServico == null)
            {
                return NotFound();
            }
            return Ok(ordemServico);
        }

        [HttpPost("cadastrar")]
        public IActionResult PostOrdemServico
        ([FromBody] OrdemServicoView model)
        {
            if (model.ServicoId == null) return BadRequest();

            var servico = _db.Servicos.AsNoTracking().FirstOrDefault(x => x.Id == model.ServicoId);
            var email = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

            var solicitante = _db.Solicitantes
                .Include(p => p.Usuario)
                .FirstOrDefault(x => x.Usuario.Email == email.Value);

            var ordemServico = new OrdemServico
            {
                ServicoId = (int)model.ServicoId!,
                SolicitanteId = solicitante!.Id,
                PrestadorId = servico.PrestadorId,
                DataSolicitacao = DateTime.UtcNow,
                Status = model.Status
            };

            _db.OrdemServicos.Add(ordemServico);
            _db.SaveChanges();
            return CreatedAtAction($"api/ordemservico/{ordemServico.Id}", ordemServico);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOrdemServico
        (int id) 
        {
            var ordemServico = _db.OrdemServicos.FirstOrDefault(os => os.Id == id);
            if (ordemServico == null)
            {
                return NotFound();
            }
            _db.OrdemServicos.Remove(ordemServico);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [Authorize(Roles = "prestador")]
        public IActionResult PutOrdemServico
        (int id, [FromBody] OrdemServicoView model)
        { 
            var ordemServico = _db.OrdemServicos.FirstOrDefault(os => os.Id == id);

            if (ordemServico == null) return NotFound();

            ordemServico.Status = model.Status;

            _db.OrdemServicos.Update(ordemServico);
            _db.SaveChanges();

            return Ok(); 
        }
        
    }
}
