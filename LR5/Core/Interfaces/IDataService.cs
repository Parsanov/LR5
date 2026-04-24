using LR5.Core.Model;

namespace LR5.Core.Interfaces
{
    public interface IDataService
    {
        Task Add(CatalogItem item);
        Task<IEnumerable<CatalogItem>> GetAll();
        Task<CatalogItem> GetById(int id);
        Task Delete(CatalogItem item);
        Task Update(CatalogItem item);
    }
}
