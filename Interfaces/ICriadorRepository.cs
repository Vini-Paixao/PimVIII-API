using PimVIII.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PimVIII.Api.Interfaces
{
    public interface ICriadorRepository
    {
        Task<List<Criador>> GetAllCriadores();
        Task<Criador?> GetCriadorById(int id);
        Task<Criador> AddCriador(Criador criador);
        Task<bool> UpdateCriador(Criador criador);
        Task<bool> DeleteCriador(int id);
    }
}