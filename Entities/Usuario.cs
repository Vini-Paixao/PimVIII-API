using System.Collections.Generic;

// Verifique se este namespace está correto
namespace PimVIII.Api.Entities
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}