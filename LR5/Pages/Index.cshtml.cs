using LR5.Core.Interfaces;
using LR5.Core.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LR5.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICatalogService _catalog;
        private readonly IDataService _data;

        public CatalogGroup? Root { get; set; }
        public List<CatalogItem> AllItems { get; set; } = new();
        public int TotalGroups { get; set; }
        public int TotalProducts { get; set; }
        public int MaxDepth { get; set; }

        public IndexModel(ICatalogService catalog, IDataService data)
        {
            _catalog = catalog;
            _data = data;
        }

        public async Task OnGetAsync()
        {
            Root = await _catalog.LoadTree();
            AllItems = (await _data.GetAll()).ToList();

            TotalGroups = AllItems.Count(i => !i.IsLeaf);
            TotalProducts = AllItems.Count(i => i.IsLeaf);
            MaxDepth = AllItems.Any()
                ? AllItems.Max(i => i.AbsoluteCode.Length / 2)
                : 0;
        }
    }
}
