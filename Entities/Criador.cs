using System.Collections.Generic;

// Verifique se este namespace está correto
namespace PimVIII.Api.Entities
{
    public class Criador
    {
        public int ID { get; set; }
        public string Nome { get; set; } = string.Empty;

        public virtual ICollection<Conteudo> Conteudos { get; set; } = new List<Conteudo>();
    }
}