namespace ECommerceManagement.API.Models
{
    public class OrderProduct
    {
        public Guid Id { get; set; }
        
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Amount { get; set; }

        public float Price { get; set; }

        public virtual Order Order { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;
    }
}
