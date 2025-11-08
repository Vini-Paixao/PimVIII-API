using System.Collections.Generic;

namespace PimVIII.Api.Entities
{
    public class Conteudo
    {
        public int ID { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;

        public int CriadorID { get; set; }
        public virtual Criador? Criador { get; set; }

        public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}