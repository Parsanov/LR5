using LR5.Persistence.Data;

namespace LR5.Core.Model
{
    public class CatalogGroup : ICatalogComponent
    {
        public string Name { get; set; }
        public string AbsoluteCode { get; set; }
        public string RelativeCode { get; set; }
        public List<ICatalogComponent> Children { get; } = new();

        public CatalogGroup(string name)
        {
            Name = name;
        }


        public void Add(ICatalogComponent item)
        {
            Children.Add(item);
            item.RelativeCode = Children.Count.ToString("D2");
            item.AbsoluteCode = AbsoluteCode + item.RelativeCode;
        }


        public void Remove(ICatalogComponent item)
        {
            Children.Remove(item);
            RecalculateCodes();
        }

        private void RecalculateCodes()
        {
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].RelativeCode = (i + 1).ToString("D2");
                Children[i].AbsoluteCode = AbsoluteCode + Children[i].RelativeCode;
            }
        }

        public void Display(int depth)
        {
            Console.WriteLine($"{new string(' ', depth * 2)}{AbsoluteCode} {Name}");
            foreach (var child in Children)
                child.Display(depth + 1);
        }
 
       
    }
}
