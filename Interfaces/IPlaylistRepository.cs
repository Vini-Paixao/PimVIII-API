using PimVIII.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PimVIII.Api.Interfaces
{
    public interface IPlaylistRepository
    {
        // Métodos exigidos pelo PIM
        Task<List<Playlist>> GetAllPlaylists();
        Task<Playlist?> GetPlaylistById(int id);
        Task AddPlaylist(Playlist playlist);
        Task UpdatePlaylist(Playlist playlist);
        Task DeletePlaylist(int id);
    }
}