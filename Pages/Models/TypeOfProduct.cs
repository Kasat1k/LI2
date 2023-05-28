namespace LI2.Pages.Models
{
    public class TypeOfProduct
    {
        public TypeOfProduct()
        {
            Products = new List<Product>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
