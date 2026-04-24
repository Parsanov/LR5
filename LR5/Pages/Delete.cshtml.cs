using LR5.Core.Interfaces;
using LR5.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LR5.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ICatalogService _catalog;
        private readonly IDataService _data;

        public CatalogItem? Item { get; set; }
        public int ChildrenCount { get; set; }

        public DeleteModel(ICatalogService catalog, IDataService data)
        {
            _catalog = catalog;
            _data = data;
        }

        public async Task OnGetAsync(int id)
        {
            var all = (await _data.GetAll()).ToList();
            Item = all.FirstOrDefault(i => i.Id == id);
            if (Item != null)
                ChildrenCount = CountChildren(all, id);
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _catalog.DeleteItem(id);
            return RedirectToPage("/Index");
        }

        private int CountChildren(List<CatalogItem> all, int parentId)
        {
            var children = all.Where(i => i.ParentId == parentId).ToList();
            return children.Count + children.Sum(c => CountChildren(all, c.Id));
        }
    }
}
