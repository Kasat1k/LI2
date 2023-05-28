namespace LI2.Pages.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int OrderId { get; set; }
        public int TypeOfProductId { get; set; }
        public int CategoryOfProductId { get; set; }

        public int OrderToProductId { get; set; }
        public virtual TypeOfProduct TypeOfProduct { get; set; } 

        public virtual CategoryOfProduct CategoryOfProduct { get; set; }
        public virtual OrderToProduct OrderToProduct { get; set; } = null!;
    }

}
