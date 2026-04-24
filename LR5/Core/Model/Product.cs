using LR5.Persistence.Data;

namespace LR5.Core.Model
{
    public class Product : ICatalogComponent
    {
        public string Name { get; set; }
        public string AbsoluteCode { get; set; }
        public string RelativeCode { get; set; }
        public decimal? Price { get; set; }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public void Display(int depth) =>
           Console.WriteLine($"{new string(' ', depth * 2)}{AbsoluteCode} {Name} — {Price}грн");
     
    }
}
