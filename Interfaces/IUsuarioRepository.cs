using PimVIII.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PimVIII.Api.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAllUsuarios();
        Task<Usuario?> GetUsuarioById(int id); // '?' permite retorno nulo
        Task<Usuario> AddUsuario(Usuario usuario);
        Task<bool> UpdateUsuario(Usuario usuario);
        Task<bool> DeleteUsuario(int id);
    }
}