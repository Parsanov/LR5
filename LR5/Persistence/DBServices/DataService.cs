using LR5.Core.Interfaces;
using LR5.Core.Model;
using LR5.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace LR5.Persistence.DBServices
{
    public class DataService : IDataService
    {
        private readonly AppDbContext _context;

        public DataService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(CatalogItem item)
        {
            await _context.catalogItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(CatalogItem item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CatalogItem>> GetAll()
        {
            return await _context.catalogItems.ToListAsync();
        }

        public async Task<CatalogItem> GetById(int id)
        {
            return await _context.catalogItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(CatalogItem item)
        {
            _context.catalogItems.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
