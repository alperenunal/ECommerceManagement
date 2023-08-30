using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.DTOs
{
    public class StockInfoObject
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int? StoreId { get; set; }
        [Required]
        public string StoreName { get; set; } = null!;
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public string ProductName { get; set; } = null!;
        [Required]
        public int Amount { get; set; }
    }
}
