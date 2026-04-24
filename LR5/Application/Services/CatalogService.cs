using LR5.Core.Interfaces;
using LR5.Core.Model;

public class CatalogService : ICatalogService
{
    private readonly IDataService _data;

    public CatalogService(IDataService data)
    {
        _data = data;
    }
    public async Task<CatalogGroup> LoadTree()
    {
        var all = (await _data.GetAll()).ToList();
        var root = new CatalogGroup("Прайс") { AbsoluteCode = "" };
        await BuildChildren(root, all, null);
        return root;
    }

    private async Task BuildChildren(CatalogGroup parent, List<CatalogItem> all, int? parentId)
    {
        var children = all.Where(i => i.ParentId == parentId).ToList();

        foreach (var child in children)
        {
            if (child.IsLeaf)
            {
                parent.Children.Add(new Product(child.Name, child.Price!.Value)
                {
                    AbsoluteCode = child.AbsoluteCode,
                    RelativeCode = child.RelativeCode
                });
            }
            else
            {
                var group = new CatalogGroup(child.Name)
                {
                    AbsoluteCode = child.AbsoluteCode,
                    RelativeCode = child.RelativeCode
                };
                await BuildChildren(group, all, child.Id);
                parent.Children.Add(group);
            }
        }
    }

    public async Task AddItem(string name, decimal? price, bool isLeaf, int? parentId)
    {
        var all = await _data.GetAll();
        var siblings = all.Where(i => i.ParentId == parentId).ToList();
        var relativeCode = (siblings.Count + 1).ToString("D2");

        string parentAbsoluteCode = "";
        if (parentId.HasValue)
        {
            var parent = await _data.GetById(parentId.Value);
            parentAbsoluteCode = parent.AbsoluteCode;
        }

        await _data.Add(new CatalogItem
        {
            Name = name,
            Price = price,
            IsLeaf = isLeaf,
            ParentId = parentId,
            RelativeCode = relativeCode,
            AbsoluteCode = parentAbsoluteCode + relativeCode
        });
    }

    public async Task DeleteItem(int id)
    {
        var all = (await _data.GetAll()).ToList();
        var item = await _data.GetById(id);

        await DeleteRecursive(all, id);

        await RecalculateSiblings(item.ParentId);
    }

    private async Task DeleteRecursive(List<CatalogItem> all, int id)
    {
        var children = all.Where(i => i.ParentId == id).ToList();

        foreach (var child in children)
            await DeleteRecursive(all, child.Id);

        var item = await _data.GetById(id);
        await _data.Delete(item);
    }

    private async Task RecalculateSiblings(int? parentId)
    {
        var all = await _data.GetAll();
        var siblings = all
            .Where(i => i.ParentId == parentId)
            .OrderBy(i => i.RelativeCode)
            .ToList();

        string parentAbsoluteCode = "";
        if (parentId.HasValue)
        {
            var parent = await _data.GetById(parentId.Value);
            parentAbsoluteCode = parent.AbsoluteCode;
        }

        for (int i = 0; i < siblings.Count; i++)
        {
            siblings[i].RelativeCode = (i + 1).ToString("D2");
            siblings[i].AbsoluteCode = parentAbsoluteCode + siblings[i].RelativeCode;
            await _data.Update(siblings[i]);
        }
    }

    public async Task<IEnumerable<CatalogItem>> SearchByName(string name)
    {
        var all = await _data.GetAll();
        return all.Where(i => i.Name.ToLower().Contains(name.ToLower()));
    }
    public async Task<CatalogItem?> SearchByCode(string code)
    {
        var all = await _data.GetAll();
        return all.FirstOrDefault(i => i.AbsoluteCode == code);
    }
}