using PimVIII.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PimVIII.Api.Interfaces
{
    public interface IConteudoRepository
    {
        Task<List<Conteudo>> GetAllConteudos();
        Task<Conteudo?> GetConteudoById(int id);
        Task<Conteudo> AddConteudo(Conteudo conteudo);
        Task<bool> UpdateConteudo(Conteudo conteudo);
        Task<bool> DeleteConteudo(int id);
    }
}