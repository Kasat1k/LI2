namespace LI2.Pages.Models
{
    public class StatusOfOrder
    {
        public StatusOfOrder()
        {
            Orders = new List<Order>();
        }
        public int Id { get; set; }
        public string Status { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
