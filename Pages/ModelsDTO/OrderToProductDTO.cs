using LI2.Pages.Models;

namespace LI2.Pages.ModelsDTO
{
    public class OrderToProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
    }
}
