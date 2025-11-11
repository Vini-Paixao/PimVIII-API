
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

        public async Task UpdatePlaylist(Playlist playlistEnviadaPeloApp)
        {
            // 1. Carregue a playlist *real* do banco de dados, 
            //    incluindo a lista de conteúdos que ela tem ATUALMENTE.
            var playlistNoBanco = await _context.Playlists
                                        .Include(p => p.Conteudos) // ESSENCIAL!
                                        .FirstOrDefaultAsync(p => p.ID == playlistEnviadaPeloApp.ID);

            if (playlistNoBanco == null)
            {
                // Lança uma exceção que o 'catch' do seu controller (DbUpdateConcurrencyException)
                // pode interpretar como "não encontrado".
                throw new DbUpdateConcurrencyException("Playlist não encontrada para atualização.");
            }

            // 2. Atualize os dados simples (propriedades escalares)
            playlistNoBanco.Nome = playlistEnviadaPeloApp.Nome;
            // (Não mexa no UsuarioID, a menos que seja a regra de negócio)

            // 3. Gerenciamento da Relação N-N
            //    Limpe a lista de conteúdos *atualmente* associados no banco.
            playlistNoBanco.Conteudos.Clear();

            // 4. Verifique se o app (MAUI) enviou novos conteúdos
            if (playlistEnviadaPeloApp.Conteudos != null && playlistEnviadaPeloApp.Conteudos.Any())
            {
                // 5. Pegue apenas os IDs da lista que o app enviou
                var idsDosConteudosDoApp = playlistEnviadaPeloApp.Conteudos.Select(c => c.ID).ToList();

                // 6. Busque os *verdadeiros* objetos 'Conteudo' do banco
                var conteudosParaAdicionar = await _context.Conteudos
                                                         .Where(c => idsDosConteudosDoApp.Contains(c.ID))
                                                         .ToListAsync();

                // 7. Adicione os objetos (trackeados pelo EF Core) à playlist do banco
                foreach (var c in conteudosParaAdicionar)
                {
                    playlistNoBanco.Conteudos.Add(c);
                }
            }

            // 8. Salve as alterações
            //    O seu 'await _context.SaveChangesAsync();' original
            //    agora salvará as mudanças escalares E as mudanças na relação.
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