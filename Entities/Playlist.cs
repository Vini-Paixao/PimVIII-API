using System.Collections.Generic;

// Verifique se este namespace está correto
namespace PimVIII.Api.Entities
{
    public class Playlist
    {
        public int ID { get; set; }
        public string Nome { get; set; } = string.Empty;

        public int UsuarioID { get; set; }
        public virtual Usuario? Usuario { get; set; }

        public virtual ICollection<Conteudo> Conteudos { get; set; } = new List<Conteudo>();
    }
}