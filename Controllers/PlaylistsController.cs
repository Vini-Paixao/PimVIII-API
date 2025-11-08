using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PimVIII.Api.Entities;
using PimVIII.Api.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PimVIII.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Define a rota base como /api/Playlists
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylistRepository _playlistRepository;

        // O repositório é injetado no construtor
        public PlaylistsController(IPlaylistRepository playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        // GET: /api/Playlists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Playlist>>> GetPlaylists()
        {
            var playlists = await _playlistRepository.GetAllPlaylists();
            return Ok(playlists);
        }

        // GET: /api/Playlists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Playlist>> GetPlaylist(int id)
        {
            var playlist = await _playlistRepository.GetPlaylistById(id);

            if (playlist == null)
            {
                return NotFound(); // Retorna 404 se não encontrar
            }

            return Ok(playlist);
        }

        // POST: /api/Playlists
        [HttpPost]
        public async Task<ActionResult<Playlist>> PostPlaylist(Playlist playlist)
        {
            // Validação simples (pode ser melhorada com DTOs)
            if (playlist == null)
            {
                return BadRequest();
            }

            await _playlistRepository.AddPlaylist(playlist);

            // Retorna 201 Created com a nova playlist e um link para ela
            return CreatedAtAction(nameof(GetPlaylist), new { id = playlist.ID }, playlist);
        }

        // PUT: /api/Playlists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaylist(int id, Playlist playlist)
        {
            if (id != playlist.ID)
            {
                return BadRequest("O ID da URL não corresponde ao ID do corpo da requisição.");
            }

            try
            {
                await _playlistRepository.UpdatePlaylist(playlist);
            }
            catch (DbUpdateConcurrencyException)
            {
                // Adicionar verificação se a playlist realmente existe
                return NotFound();
            }

            return NoContent(); // Retorna 204 No Content (sucesso)
        }

        // DELETE: /api/Playlists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            await _playlistRepository.DeletePlaylist(id);
            return NoContent(); // Retorna 204 No Content (sucesso)
        }
    }
}