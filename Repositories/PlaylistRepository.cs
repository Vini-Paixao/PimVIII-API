
using Microsoft.EntityFrameworkCore;
using PimVIII.Api.Data;
using PimVIII.Api.Entities;
using PimVIII.Api.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PimVIII.Api.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly AppDbContext _context;

        public PlaylistRepository(AppDbContext context)
        {
            _context = context;
        }

        // Implementação dos métodos exigidos pelo PIM
        public async Task<List<Playlist>> GetAllPlaylists()
        {
            return await _context.Playlists.ToListAsync();
        }

        // CORREÇÃO AQUI (adicionado o '?')
        public async Task<Playlist?> GetPlaylistById(int id)
        {
            return await _context.Playlists
                .Include(p => p.Conteudos) 
                .FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task AddPlaylist(Playlist playlist)
        {
            _context.Playlists.Add(playlist);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePlaylist(Playlist playlist)
        {
            _context.Entry(playlist).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePlaylist(int id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist != null)
            {
                _context.Playlists.Remove(playlist);
                await _context.SaveChangesAsync();
            }
        }
    }
}