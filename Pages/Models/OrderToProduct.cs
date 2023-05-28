namespace LI2.Pages.Models
{
    public class OrderToProduct
    {
        public OrderToProduct()
        {

            Products = new List<Product>();

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;


    }
}
