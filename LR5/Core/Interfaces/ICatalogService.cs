using LR5.Core.Model;

namespace LR5.Core.Interfaces
{
    public interface ICatalogService
    {
        Task<CatalogGroup> LoadTree();
        Task AddItem(string name, decimal? price, bool isLeaf, int? parentId);
        Task DeleteItem(int id);
        Task<IEnumerable<CatalogItem>> SearchByName(string name);
        Task<CatalogItem?> SearchByCode(string code);
    }
}
