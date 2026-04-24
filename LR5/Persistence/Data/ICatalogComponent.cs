namespace LR5.Persistence.Data
{
    public interface ICatalogComponent
    {
        string Name { get; set; }
        string AbsoluteCode { get; set; }
        string RelativeCode { get; set; }

        void Display(int depth);
    }
}
