namespace LI2.Pages.ModelsDTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int TypeOfProductId { get; set; }
        public int OrderToProductId { get; set; }
        public int CategoryOfProductId { get; set; }
    }
}
