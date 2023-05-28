namespace LI2.Pages.Models
{
    public class Order
    {
        public Order()
        {
            OrderToProducts = new List<OrderToProduct>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        // public decimal Price { get; set; }
        public int StatusOfOrderId { get; set; }
        public virtual StatusOfOrder StatusOfOrder { get; set; } = null!;
        public virtual ICollection<OrderToProduct> OrderToProducts { get; set; } = null!;
    }
}
