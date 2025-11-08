using Microsoft.AspNetCore.Mvc;
using PimVIII.Api.Entities;
using PimVIII.Api.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PimVIII.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConteudosController : ControllerBase
    {
        private readonly IConteudoRepository _conteudoRepository;

        public ConteudosController(IConteudoRepository conteudoRepository)
        {
            _conteudoRepository = conteudoRepository;
        }

        // GET: /api/Conteudos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conteudo>>> GetConteudos()
        {
            return Ok(await _conteudoRepository.GetAllConteudos());
        }

        // GET: /api/Conteudos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Conteudo>> GetConteudo(int id)
        {
            var conteudo = await _conteudoRepository.GetConteudoById(id);
            if (conteudo == null)
            {
                return NotFound();
            }
            return Ok(conteudo);
        }

        // POST: /api/Conteudos
        [HttpPost]
        public async Task<ActionResult<Conteudo>> PostConteudo(Conteudo conteudo)
        {
            var novoConteudo = await _conteudoRepository.AddConteudo(conteudo);
            return CreatedAtAction(nameof(GetConteudo), new { id = novoConteudo.ID }, novoConteudo);
        }

        // PUT: /api/Conteudos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConteudo(int id, Conteudo conteudo)
        {
            if (id != conteudo.ID)
            {
                return BadRequest();
            }

            var result = await _conteudoRepository.UpdateConteudo(conteudo);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: /api/Conteudos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConteudo(int id)
        {
            var result = await _conteudoRepository.DeleteConteudo(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}