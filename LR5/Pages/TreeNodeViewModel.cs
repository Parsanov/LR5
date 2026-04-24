using LR5.Core.Model;
using LR5.Persistence.Data;

namespace LR5.Pages
{
    public class TreeNodeViewModel
    {
        public ICatalogComponent Component { get; set; } = null!;
        public int Depth { get; set; }
        public List<CatalogItem> AllItems { get; set; } = new();
    }
}
