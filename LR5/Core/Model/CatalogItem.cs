namespace LR5.Core.Model
{
    public class CatalogItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AbsoluteCode { get; set; }
        public string RelativeCode { get; set; }
        public decimal? Price { get; set; }
        public bool IsLeaf { get; set; }
        public int? ParentId { get; set; }

        public CatalogItem? Parent { get; set; }
        public List<CatalogItem> Children { get; set; } = new();
    }
}
