using LR5.Core.Interfaces;
using LR5.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LR5.Pages
{
    public class AddModel : PageModel
    {
        private readonly ICatalogService _catalog;
        private readonly IDataService _data;

        [BindProperty]
        public string Name { get; set; } = string.Empty;

        [BindProperty]
        public decimal? Price { get; set; }

        [BindProperty]
        public bool IsLeaf { get; set; }

        [BindProperty]
        public int? ParentId { get; set; }

        public CatalogItem? ParentItem { get; set; }
        public List<SelectListItem> GroupOptions { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public AddModel(ICatalogService catalog, IDataService data)
        {
            _catalog = catalog;
            _data = data;
        }

        public async Task OnGetAsync(int? parentId)
        {
            ParentId = parentId;
            await LoadOptionsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                ErrorMessage = "Назва обов'язкова";
                await LoadOptionsAsync();
                return Page();
            }

            if (IsLeaf && (Price == null || Price < 0))
            {
                ErrorMessage = "Вкажіть коректну ціну для товару";
                await LoadOptionsAsync();
                return Page();
            }

            await _catalog.AddItem(Name.Trim(), IsLeaf ? Price : null, IsLeaf, ParentId);
            return RedirectToPage("/Index");
        }

        private async Task LoadOptionsAsync()
        {
            var all = await _data.GetAll();
            GroupOptions = all
                .Where(i => !i.IsLeaf)
                .OrderBy(i => i.AbsoluteCode)
                .Select(i => new SelectListItem(
                    $"{i.AbsoluteCode} — {i.Name}",
                    i.Id.ToString()))
                .ToList();

            if (ParentId.HasValue)
                ParentItem = all.FirstOrDefault(i => i.Id == ParentId.Value);
        }
    }
}
