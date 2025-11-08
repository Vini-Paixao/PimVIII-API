using Microsoft.EntityFrameworkCore;
using PimVIII.Api.Data;
using PimVIII.Api.Entities;
using PimVIII.Api.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PimVIII.Api.Repositories
{
    public class CriadorRepository : ICriadorRepository
    {
        private readonly AppDbContext _context;

        public CriadorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Criador>> GetAllCriadores()
        {
            return await _context.Criadores.ToListAsync();
        }

        public async Task<Criador?> GetCriadorById(int id)
        {
            // Inclui os conteúdos associados a este criador
            return await _context.Criadores
                .Include(c => c.Conteudos)
                .FirstOrDefaultAsync(c => c.ID == id);
        }

        public async Task<Criador> AddCriador(Criador criador)
        {
            _context.Criadores.Add(criador);
            await _context.SaveChangesAsync();
            return criador;
        }

        public async Task<bool> UpdateCriador(Criador criador)
        {
            _context.Entry(criador).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteCriador(int id)
        {
            var criador = await _context.Criadores.FindAsync(id);
            if (criador == null)
            {
                return false;
            }

            _context.Criadores.Remove(criador);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}