using Microsoft.EntityFrameworkCore;
using PimVIII.Api.Data;
using PimVIII.Api.Entities;
using PimVIII.Api.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PimVIII.Api.Repositories
{
    public class ConteudoRepository : IConteudoRepository
    {
        private readonly AppDbContext _context;

        public ConteudoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Conteudo>> GetAllConteudos()
        {
            // Inclui o Criador de cada conteúdo
            return await _context.Conteudos.Include(c => c.Criador).ToListAsync();
        }

        public async Task<Conteudo?> GetConteudoById(int id)
        {
            // Inclui o Criador e as Playlists onde este conteúdo aparece
            return await _context.Conteudos
                .Include(c => c.Criador)
                .Include(c => c.Playlists)
                .FirstOrDefaultAsync(c => c.ID == id);
        }

        public async Task<Conteudo> AddConteudo(Conteudo conteudo)
        {
            _context.Conteudos.Add(conteudo);
            await _context.SaveChangesAsync();
            return conteudo;
        }

        public async Task<bool> UpdateConteudo(Conteudo conteudo)
        {
            _context.Entry(conteudo).State = EntityState.Modified;
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

        public async Task<bool> DeleteConteudo(int id)
        {
            var conteudo = await _context.Conteudos.FindAsync(id);
            if (conteudo == null)
            {
                return false;
            }

            _context.Conteudos.Remove(conteudo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}