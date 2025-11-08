using Microsoft.AspNetCore.Mvc;
using PimVIII.Api.Entities;
using PimVIII.Api.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PimVIII.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CriadoresController : ControllerBase
    {
        private readonly ICriadorRepository _criadorRepository;

        public CriadoresController(ICriadorRepository criadorRepository)
        {
            _criadorRepository = criadorRepository;
        }

        // GET: /api/Criadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Criador>>> GetCriadores()
        {
            return Ok(await _criadorRepository.GetAllCriadores());
        }

        // GET: /api/Criadores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Criador>> GetCriador(int id)
        {
            var criador = await _criadorRepository.GetCriadorById(id);
            if (criador == null)
            {
                return NotFound();
            }
            return Ok(criador);
        }

        // POST: /api/Criadores
        [HttpPost]
        public async Task<ActionResult<Criador>> PostCriador(Criador criador)
        {
            var novoCriador = await _criadorRepository.AddCriador(criador);
            return CreatedAtAction(nameof(GetCriador), new { id = novoCriador.ID }, novoCriador);
        }

        // PUT: /api/Criadores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCriador(int id, Criador criador)
        {
            if (id != criador.ID)
            {
                return BadRequest();
            }

            var result = await _criadorRepository.UpdateCriador(criador);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: /api/Criadores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCriador(int id)
        {
            var result = await _criadorRepository.DeleteCriador(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}