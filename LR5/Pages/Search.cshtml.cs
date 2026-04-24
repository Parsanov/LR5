using LR5.Core.Interfaces;
using LR5.Core.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LR5.Pages
{
    public class SearchModel : PageModel
    {
        private readonly ICatalogService _catalog;

        public string? Query { get; set; }
        public string? Mode { get; set; }
        public List<CatalogItem> Results { get; set; } = new();

        public SearchModel(ICatalogService catalog)
        {
            _catalog = catalog;
        }

        public async Task OnGetAsync(string? q, string? mode)
        {
            Query = q;
            Mode = mode ?? "name";

            if (!string.IsNullOrWhiteSpace(q))
            {
                if (Mode == "code")
                {
                    var item = await _catalog.SearchByCode(q.Trim());
                    if (item != null) Results.Add(item);
                }
                else
                {
                    Results = (await _catalog.SearchByName(q.Trim())).ToList();
                }
            }
        }
    }
}
