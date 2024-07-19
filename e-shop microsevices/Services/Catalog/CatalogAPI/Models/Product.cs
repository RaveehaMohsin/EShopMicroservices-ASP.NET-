namespace CatalogAPI.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<string> Category { get; set; } = new List<string>();
        public string Description { get; set; } = default!;
        public string Imagefile { get; set; } = default!;

        public decimal Price { get; set; }


    }
}
