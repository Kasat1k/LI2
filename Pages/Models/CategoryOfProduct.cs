namespace LI2.Pages.Models
{
    public class CategoryOfProduct

    {
        public CategoryOfProduct()
        {
            Products = new List<Product>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
